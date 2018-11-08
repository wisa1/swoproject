using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.Server.DAL.DTO;

namespace Wetr.Server.DAL.IDAO
{
    public interface IMeasurementTypeDAO
    {
        Task<IEnumerable<MeasurementType>> FindAllAsync();
        Task<MeasurementType> FindByIDAsync(int id);
    }
}
