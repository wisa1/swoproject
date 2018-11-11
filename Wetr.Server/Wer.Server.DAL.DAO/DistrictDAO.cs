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
    class DistrictDAO : IDistrictDAO
    {
        private readonly ADOTemplate template;
        private RowMapper<District> districtMapper = record =>
        {
            return new District()
            {
                ID = (int)record["ID"],
                ProvinceID = (int)record["ProvinceID"],
                Name = (string)record["Name"]
            };
        };

        public DistrictDAO(IConnectionFactory connectionFactory)
        {
            this.template = new ADOTemplate(connectionFactory);
        }

        public async Task<IEnumerable<District>> FindAllAsync()
          => await template.QueryAsync<District>("SELECT * FROM [District]", districtMapper);

        public async Task<District> FindByIDAsync(int id)
          => (await template.QueryAsync<District>("SELECT * FROM [District] WHERE ID = @ID",
                                       districtMapper,
                                       new Wetr.Server.Common.SqlParameter[] { new Wetr.Server.Common.SqlParameter("@ID", id) }
                                      )).SingleOrDefault();

        public async Task<int> InsertAsync(District district)
          => (await template.ExecuteAsync("INSERT INTO [District] (ProvinceID, Name) VALUES(@ProvinceID, @Name)",
                                    new SqlParameter[] { new SqlParameter("@ProvinceID", district.ProvinceID),
                                                         new SqlParameter("@Name", district.Name) }));
    }
}
