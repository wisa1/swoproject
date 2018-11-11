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
    public class UnitOfMeasureDAO : IUnitOfMeasureDAO
    {
        private ADOTemplate template;
        private RowMapper<UnitOfMeasure> unitOfMeasureMapper = record =>
        {
            return new UnitOfMeasure()
            {
                ID = (int)record["ID"],
                Abbreviation = (string)record["Abbreviation"],
                Description = (string)record["Description"]
            };
        };

        public UnitOfMeasureDAO(IConnectionFactory connectionFactory)
        {
            this.template = new ADOTemplate(connectionFactory);
        }
        public async Task<IEnumerable<UnitOfMeasure>> FindAllAsync()
         => await template.QueryAsync<UnitOfMeasure>("SELECT * FROM [Unit Of Measure]", unitOfMeasureMapper);

        public async Task<UnitOfMeasure> FindByIDAsync(int id)
          =>  (await template.QueryAsync<UnitOfMeasure>("SELECT * FROM [Unit Of Measure] WHERE ID = @ID",
                                                    unitOfMeasureMapper,
                                                    new SqlParameter[] { new SqlParameter("ID", id) }
                                                    )).SingleOrDefault();

        public async Task<int> InsertAsync(UnitOfMeasure unitOfMeasure)
             => (await template.ExecuteAsync("INSERT INTO [Unit of Measure] (Abbreviation, Description) VALUES(@Abbreviation, @Description)",
                                                new SqlParameter[] {new SqlParameter("@Abbreviation", unitOfMeasure.Abbreviation),
                                                                    new SqlParameter("@Description", unitOfMeasure.Description)}));
    }
}
