using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.Server.Common;
using Wetr.Server.DAL.DTO;
using static Wetr.Server.Common.ADOTemplate;

namespace Wer.Server.DAL.DAO
{
    public class MeasurementDAO
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
        public IEnumerable<Measurement> FindAll()
         => template.Query<Measurement>("SELECT * FROM User", measurementMapper);

        public Measurement FindById(int id)
         => template.Query<Measurement>("Select * FROM User WHERE ID = @ID",
                                 measurementMapper,
                                 new SqlParameter[] { new SqlParameter("ID", id) }
                                ).SingleOrDefault();
    }
}
