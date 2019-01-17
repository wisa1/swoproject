using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.Server.Common;
using Wetr.Server.DAL.DTO;
using Wetr.Server.DAL.IDAO;
using static Wetr.Server.Common.ADOTemplate;

namespace Wetr.Server.DAL.DAO
{
    public class MeasurementDAO : IMeasurementDAO
    {
        private ADOTemplate template;
        private RowMapper<Measurement> measurementMapper = record =>
        {
            return new Measurement()
            {
                ID = (int)record["ID"],
                TypeID = (int)record["TypeID"],
                DeviceID = (int)record["DeviceID"],
                Timestamp = DateTime.Parse((record["Timestamp"]).ToString()),
                UnitOfMeasureID = (int)record["Unit of Measure ID"],
                Value = (double)record["Value"]
            };
        };
        public MeasurementDAO(IConnectionFactory connectionFactory)
        {
            this.template = new ADOTemplate(connectionFactory);
        }
        public async Task<IEnumerable<Measurement>> FindAllAsync()
         => await template.QueryAsync<Measurement>("SELECT * FROM [Measurement]", measurementMapper);
        public async Task<Measurement> FindByIDAsync(int id)
         => (await template.QueryAsync<Measurement>("SELECT * FROM [Measurement] WHERE ID = @ID",
                                 measurementMapper,
                                 new SqlParameter[] { new SqlParameter("@ID", id) }
                                )).SingleOrDefault();

        public async Task<IEnumerable<GroupedResultRecord>> GetAggregatedDataForDevice(MeasurementFilter filter)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            string sql = AssembleSqlStringFromFilter(filter, out parameters);

            var resultSet = await template.QueryAsync<GroupedResultRecord>(
                sql,
                GroupedResultRecord.groupResultMapper,
                parameters.ToArray()
            );

            foreach(GroupedResultRecord record in resultSet)
            {
                record.PeriodType = filter.PeriodType;
                //record.MeasurementType = filter.MeasurementType;
            }
            return resultSet;
        }

        private string AssembleSqlStringFromFilter(MeasurementFilter filter, out List<SqlParameter> parameters)
        {
            parameters = new List<SqlParameter>();
            string dateCalc = $"dateadd({filter.PeriodType.ToSqlDatepart()}, datediff({filter.PeriodType.ToSqlDatepart()}, 0, [Timestamp]), 0) ";

            string select;
            //Period and Aggregation type ARE BOTH NOT set
            select = "";
            if (filter.PeriodType == Constants.PeriodType.None && filter.AggregationType == Constants.AggregationType.None)
            {
                select = "SELECT * ";
            }
            //Period and Aggregation type ARE BOTH set
            else if (filter.PeriodType != Constants.PeriodType.None && filter.AggregationType != Constants.AggregationType.None)
            {
                select = $"SELECT { filter.AggregationType.ToSqlAggregate()} ([Value]) AS Value, " + dateCalc + "as DateTimeStart ";
            } 
            //Only Aggregation is Set, Period is not.
            else if (filter.AggregationType != Constants.AggregationType.None && filter.PeriodType == Constants.PeriodType.None)
            {
                select = $"SELECT {filter.AggregationType.ToSqlAggregate()}" + "([Value]) AS Value ";
            }

            string from = "FROM [Measurement] INNER JOIN [Measurement Device] ON ([Measurement].DeviceID = [Measurement Device].ID)";

            List<string> where = new List<string>();
            if (filter.MeasurementDevice != null)
            {
                /*
                where.Add("[DeviceID] = @DeviceID ");
                parameters.Add(new SqlParameter("@DeviceID", filter.MeasurementDevice.ID));
                */
                if (filter.RadiusKm != 0)
                {
                    where.Add($"([DeviceID] = @DeviceID  OR " +
                        $"(geography::Point({filter.MeasurementDevice.Latitude.ToString().Replace(',','.')}, {filter.MeasurementDevice.Longitude.ToString().Replace(',', '.')}, 4326).STDistance(geography::Point(ISNULL([Latitude],0),ISNULL([Longitude],0), 4326))) < {filter.RadiusKm * 1000}) ");
                    parameters.Add(new SqlParameter("@DeviceID", filter.MeasurementDevice.ID));
                } else
                {
                    where.Add("[DeviceID] = @DeviceID ");
                    parameters.Add(new SqlParameter("@DeviceID", filter.MeasurementDevice.ID));
                }
            }
            if (filter.MeasurementType != null)
            {
                where.Add("[TypeID] = @measurementTypeID ");
                parameters.Add(new SqlParameter("@measurementTypeID", filter.MeasurementType.ID));
            }
            if (filter.DateFrom != null && filter.DateTo != null)
            {
                where.Add("[Timestamp] BETWEEN @DateTimeStart AND @DateTimeEnd ");
                parameters.Add(new SqlParameter("@DateTimeStart", filter.DateFrom));
                parameters.Add(new SqlParameter("@DateTimeEnd", filter.DateTo));
            }
            else if (filter.DateFrom != null)
            {
                where.Add("[Timestamp] > @DateTimeStart ");
                parameters.Add(new SqlParameter("@DateTimeStart", filter.DateFrom));
            }
            else
            {
                where.Add("[Timestamp] < @DateTimeEnd ");
                parameters.Add(new SqlParameter("@DateTimeEnd", filter.DateTo));
            }

            string groupBy = "";
            if (filter.AggregationType != Constants.AggregationType.None && filter.PeriodType != Constants.PeriodType.None)
            {
                groupBy = $"GROUP BY " + dateCalc + ";";
            }

            StringBuilder sql = new StringBuilder();
            sql.Append(select);
            sql.Append(from);
            if(where.Count > 0)
            {
                sql.Append(" WHERE ");
                foreach (string clause in where)
                {
                    sql.Append(clause);
                    if (clause != where.Last<string>())
                    {
                        sql.Append("AND ");
                    }
                }
            }
            sql.Append(groupBy);
            string debug = sql.ToString();
            return sql.ToString();
        }

        public async Task<IEnumerable<GroupedResultRecord>> GetCumulatedDataForDevice(MeasurementDevice measurementDevice, Constants.AggregationType aggregationType, MeasurementType measurementType, Constants.PeriodType periodType)
        {
            string dateCalc = $"dateadd({periodType.ToSqlDatepart()}, datediff({periodType.ToSqlDatepart()}, 0, [Timestamp]), 0) ";
            string sql = "SELECT DISTINCT " +
                        dateCalc + "AS StartDateTime " +
                        "SUM([Value]) over(partition by [DeviceID] " +
                        "ORDER BY " + dateCalc + " AS Value" +
                        "FROM [Measurement] " +
                        "WHERE [DeviceID] = @DeviceID AND " +
                        "[Unit of Measure ID] = @measurementTypeID";

            var resultSet = await template.QueryAsync<GroupedResultRecord>(
                sql,
                GroupedResultRecord.groupResultMapper,
                new SqlParameter[]
                {
                    new SqlParameter("@measurementTypeID", measurementType.ID),
                    new SqlParameter("@DeviceID", measurementDevice.ID)
                });

            foreach (GroupedResultRecord record in resultSet)
            {
                record.PeriodType = periodType;
                //record.MeasurementType = measurementType;
            }
            return resultSet;
        }
        public async Task<int> InsertAsync(Measurement measurement)
         => (await template.ExecuteAsync("INSERT INTO [Measurement] (TypeID, DeviceID, Value, [Unit of Measure ID], Timestamp) " +
                                "VALUES(@TypeID, @DeviceID, @Value, @UnitOfMeasureID, @Timestamp)",
                        new SqlParameter[] { new SqlParameter("@TypeID", measurement.TypeID),
                                                new SqlParameter("@DeviceID", measurement.DeviceID),
                                                new SqlParameter("@Value", measurement.Value),
                                                new SqlParameter("@UnitOfMeasureID", measurement.UnitOfMeasureID),
                                                new SqlParameter("@Timestamp", measurement.Timestamp)}));

        
    }
}
