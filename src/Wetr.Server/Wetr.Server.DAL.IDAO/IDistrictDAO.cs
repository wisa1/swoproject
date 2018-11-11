using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.Server.DAL.DTO;

namespace Wetr.Server.DAL.IDAO
{
    public interface IDistrictDAO
    {
        Task<IEnumerable<District>> FindAllAsync();
        Task<District> FindByIDAsync(int id);
        Task<int> InsertAsync(District district);
    }
}
