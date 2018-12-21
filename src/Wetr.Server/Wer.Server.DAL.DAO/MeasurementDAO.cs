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
            string dateCalc = $"dateadd({filter.PeriodType.ToSqlDatepart()}, datediff({filter.PeriodType.ToSqlDatepart()}, 0, [Timestamp]), 0) ";
            string sql = $"SELECT {filter.AggregationType.ToSqlAggregate()}(Value) AS Value, " +
                dateCalc + "as DateTimeStart " +
                "FROM [Measurement] " +
                "WHERE [Unit of Measure ID] = @measurementTypeID " +
                "AND [Timestamp] BETWEEN @DateTimeStart AND @DateTimeEnd " +
                "AND [DeviceID] = @DeviceID "+
                $"GROUP BY " + dateCalc +";";

            var resultSet = await template.QueryAsync<GroupedResultRecord>(
                sql,
                GroupedResultRecord.groupResultMapper,
                new SqlParameter[]{
                    new SqlParameter("@measurementTypeID", filter.MeasurementType.ID),
                    new SqlParameter("@DeviceID", filter.MeasurementDevice.ID),
                    new SqlParameter("@DateTimeStart", filter.DateFrom),
                    new SqlParameter("@DateTimeEnd", filter.DateTo)
                }
            );

            foreach(GroupedResultRecord record in resultSet)
            {
                record.PeriodType = filter.PeriodType;
                record.MeasurementType = filter.MeasurementType;
            }
            return resultSet;
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
                record.MeasurementType = measurementType;
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
