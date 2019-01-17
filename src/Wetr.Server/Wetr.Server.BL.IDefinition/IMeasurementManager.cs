using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.Server.Common;
using Wetr.Server.DAL.DTO;

namespace Wetr.Server.BL.IDefinition
{
    public interface IMeasurementManager
    {
        Task<IEnumerable<GroupedResultRecord>> PerformQueryAsync(MeasurementFilter filter);
        Task<int> InsertMeasurementAsync(Measurement measurement);
    }
}
