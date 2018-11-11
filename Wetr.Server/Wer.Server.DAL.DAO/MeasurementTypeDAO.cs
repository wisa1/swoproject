using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.Server.Common;
using Wetr.Server.DAL.DTO;
using Wetr.Server.DAL.IDAO;
using static Wetr.Server.Common.ADOTemplate;

namespace Wetr.Server.DAL.DAO
{
    class MeasurementTypeDAO : IMeasurementTypeDAO
    {
        private ADOTemplate template;
        private RowMapper<MeasurementType> measurementTypeMapper = record =>
        {
            return new MeasurementType()
            {
                ID = (int)record["ID"],
                Description = (string)record["Description"]

            };
        };

        public MeasurementTypeDAO(IConnectionFactory connectionFactory)
        {
            this.template = new ADOTemplate(connectionFactory);
        }
        public async Task<IEnumerable<MeasurementType>> FindAllAsync()
         => await template.QueryAsync<MeasurementType>("SELECT * FROM [Measurement Type]", measurementTypeMapper);

        public async Task<MeasurementType> FindByIDAsync(int id)
         => (await template.QueryAsync<MeasurementType>("Select * FROM [Measurement Type] WHERE ID = @ID",
                                 measurementTypeMapper,
                                 new SqlParameter[] { new SqlParameter("@ID", id) }
                                )).SingleOrDefault();

        public async Task<int> InsertAsync(MeasurementType measurementType)
          => (await template.ExecuteAsync("INSERT INTO [Measurement Type] (Description) VALUES(@Description)",
                                            new SqlParameter[] { new SqlParameter("@Description", measurementType.Description)}));
    }
}
