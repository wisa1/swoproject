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
    public class ProvinceDAO : IProvinceDAO
    {
        private readonly ADOTemplate template;
        private RowMapper<Province> provinceMapper = record =>
        {
            return new Province()
            {
                ID = (int)record["ID"],
                Name = (string)record["Name"]
            };
        };

        public ProvinceDAO(IConnectionFactory connectionFactory)
        {
            this.template = new ADOTemplate(connectionFactory);
        }

        public async Task<IEnumerable<Province>> FindAllAsync()
          => await template.QueryAsync<Province>("SELECT * FROM [Province]", provinceMapper);

        public async Task<Province> FindByIDAsync(int id)
          => (await template.QueryAsync<Province>("SELECT * FROM [Province] WHERE ID = @ID",
                                       provinceMapper,
                                       new SqlParameter[] { new SqlParameter("@ID", id) }
                                      )).SingleOrDefault();

        public async Task<int> InsertAsync(Province province)
          => (await template.ExecuteAsync("INSERT INTO [Province] (Name) VALUES(@Name)",
                                            new SqlParameter[] { new SqlParameter("@Name", province.Name) }));
    }
}

