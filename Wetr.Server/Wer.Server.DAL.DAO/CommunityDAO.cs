using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Wetr.Server.Common;
using static Wetr.Server.Common.ADOTemplate;
using Wetr.Server.DAL.DTO;
using Wetr.Server.DAL.IDAO;

namespace Wer.Server.DAL.DAO
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
            => await template.QueryAsync<Community>("Select * from Community", communityMapper);

        public async Task<Community> FindByIDAsync(int id)
            => (await template.QueryAsync<Community>("Select * from Community where ID = @ID",
                                       communityMapper,
                                       new Wetr.Server.Common.SqlParameter[] { new Wetr.Server.Common.SqlParameter("@ID", id) }
                                      )).SingleOrDefault();

        public async Task<District> GetDistrictForCommunityAsync(Community community)
            => await this.districtDAO.FindByIDAsync(community.DistrictID);

        public async Task<Province> GetProvinceForCommunityAsync(Community community)
            => await this.provinceDAO.FindByIDAsync(
                    ((await this.GetDistrictForCommunityAsync(community)).ProvinceID));

    }
}
