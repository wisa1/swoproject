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
    public class CommunityDAO : ICommunityDAO
    {
        private readonly ADOTemplate template;
        private readonly IDistrictDAO districtDAO;
        private readonly IProvinceDAO provinceDAO;

        private RowMapper<Community> communityMapper = record =>
        {
            return new Community()
            {
                ID = (int)record["ID"],
                PostalCode = (int)record["Postal Code"],
                DistrictID = (int)record["DistrictID"],
                Name = (string)record["Name"]
            };
        };

        public CommunityDAO(IConnectionFactory connectionFactory)
        {
            this.template = new ADOTemplate(connectionFactory);
            this.districtDAO = new DistrictDAO(connectionFactory);
            this.provinceDAO = new ProvinceDAO(connectionFactory);

        }

        public async Task<IEnumerable<Community>> FindAllAsync()
            => await template.QueryAsync<Community>("SELECT * FROM [Community]", communityMapper);

        public async Task<Community> FindByIDAsync(int id)
            => (await template.QueryAsync<Community>("SELECT * FROM [Community] WHERE ID = @ID",
                                       communityMapper,
                                       new Wetr.Server.Common.SqlParameter[] { new Wetr.Server.Common.SqlParameter("@ID", id) }
                                      )).SingleOrDefault();

        public async Task<int> InsertAsync(Community community)
            => (await template.ExecuteAsync("INSERT INTO [Community] ([Postal Code], DistrictID, Name) VALUES(@PostalCode, @DistrictID, @Name)",
                                    new SqlParameter[] { new SqlParameter("@PostalCode", community.PostalCode),
                                                         new SqlParameter("@DistrictID", community.DistrictID),
                                                         new SqlParameter("@Name", community.Name}));

        public async Task<District> GetDistrictForCommunityAsync(Community community)
            => await this.districtDAO.FindByIDAsync(community.DistrictID);

        public async Task<Province> GetProvinceForCommunityAsync(Community community)
            => await this.provinceDAO.FindByIDAsync(
                    ((await this.GetDistrictForCommunityAsync(community)).ProvinceID));



    }
}
