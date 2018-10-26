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
        }

        public IEnumerable<Community> FindAll()
          => template.Query<Community>("Select * from Community", communityMapper);

        public Community FindByID(int id)
          => template.Query<Community>("Select * from Community where ID = @ID", 
                                       communityMapper,  
                                       new Wetr.Server.Common.SqlParameter[] { new Wetr.Server.Common.SqlParameter("@ID", id) }
                                      ).SingleOrDefault(); 
    }
}
