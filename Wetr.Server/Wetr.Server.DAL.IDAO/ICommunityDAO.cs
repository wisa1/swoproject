using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.Server.DAL.DTO;

namespace Wetr.Server.DAL.IDAO
{
    public interface ICommunityDAO
    {
        Task<IEnumerable<Community>> FindAllAsync();
        Task<Community> FindByIDAsync(int id);
        Task<District> GetDistrictForCommunityAsync(Community community);
        Task<Province> GetProvinceForCommunityAsync(Community community);

    }
}
