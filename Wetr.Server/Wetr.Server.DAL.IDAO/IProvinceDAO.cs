using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.Server.DAL.DTO;

namespace Wetr.Server.DAL.IDAO
{
    public interface IProvinceDAO
    {
        Task<IEnumerable<Province>> FindAllAsync();
        Task<Province> FindByIDAsync(int id);
    }
}
