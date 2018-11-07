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
    class ProvinceDAO : IProvinceDAO
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

        public IEnumerable<Province> FindAll()
          => template.Query<Province>("Select * from Community", provinceMapper);

        public Province FindByID(int id)
          => template.Query<Province>("Select * from Community where ID = @ID",
                                       provinceMapper,
                                       new Wetr.Server.Common.SqlParameter[] { new Wetr.Server.Common.SqlParameter("@ID", id) }
                                      ).SingleOrDefault();
    }
}
}
