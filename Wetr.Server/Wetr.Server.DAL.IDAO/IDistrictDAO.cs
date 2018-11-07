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
        IEnumerable<District> FindAll();
        District FindByID(int id);
    }
}
