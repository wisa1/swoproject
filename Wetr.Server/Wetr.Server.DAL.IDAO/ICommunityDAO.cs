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
        IEnumerable<Community> FindAll();
        Community FindByID(int id);
        District GetDistrictForCommunity(Community community);
        Province GetProvinceForCommunity(Community community);

    }
}
