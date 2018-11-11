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
         => (await template.QueryAsync<Measurement>("Select * FROM [Measurement] WHERE ID = @ID",
                                 measurementMapper,
                                 new SqlParameter[] { new SqlParameter("@ID", id) }
                                )).SingleOrDefault();

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
