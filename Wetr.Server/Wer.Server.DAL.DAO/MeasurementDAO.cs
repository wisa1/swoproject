using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.Server.Common;
using Wetr.Server.DAL.DTO;
using Wetr.Server.DAL.IDAO;
using static Wetr.Server.Common.ADOTemplate;

namespace Wer.Server.DAL.DAO
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
                Timestamp = (int)record["Timestamp"],
                UnitOfMeasureID = (int)record["Unit of Measure ID"],
                Value = (float)record["Value"]
            };
        };

        public MeasurementDAO(IConnectionFactory connectionFactory)
        {
            this.template = new ADOTemplate(connectionFactory);
        }
        public async Task<IEnumerable<Measurement>> FindAllAsync()
         => await template.QueryAsync<Measurement>("SELECT * FROM User", measurementMapper);

        public async Task<Measurement> FindByIDAsync(int id)
         => (await template.QueryAsync<Measurement>("Select * FROM User WHERE ID = @ID",
                                 measurementMapper,
                                 new SqlParameter[] { new SqlParameter("ID", id) }
                                )).SingleOrDefault();
    }
}
