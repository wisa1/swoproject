using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.Server.DTO;

namespace Wetr.Server.DAL.IDAO
{
    public interface IUserDAO
    {
        IEnumerable<User> FindAll();
        User FindById(int id);
    }
}
