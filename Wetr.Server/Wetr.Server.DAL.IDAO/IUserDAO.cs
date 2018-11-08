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
        Task<IEnumerable<User>> FindAllAsync();
        Task<User> FindByIdAsync(int id);
    }
}
