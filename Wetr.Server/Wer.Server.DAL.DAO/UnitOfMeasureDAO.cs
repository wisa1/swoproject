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
    class UnitOfMeasureDAO
    {
        private ADOTemplate template;
        private RowMapper<UnitOfMeasure> unitOfMeasureMapper = record =>
        {
            return new UnitOfMeasure()
            {
                ID = (int)record["ID"],
                Code = (string)record["Code"]
            };
        };

        public UnitOfMeasureDAO(IConnectionFactory connectionFactory)
        {
            this.template = new ADOTemplate(connectionFactory);
        }
        public IEnumerable<UnitOfMeasure> findAll()
         => template.Query<UnitOfMeasure>("SELECT * FROM [Unit Of Measure]", unitOfMeasureMapper);

        public UnitOfMeasure findById(int id)
         => template.Query<UnitOfMeasure>("Select * FROM [Unit Of Measure] WHERE ID = @ID",
                                 unitOfMeasureMapper,
                                 new SqlParameter[] { new SqlParameter("ID", id) }
                                ).SingleOrDefault();
    }
}
