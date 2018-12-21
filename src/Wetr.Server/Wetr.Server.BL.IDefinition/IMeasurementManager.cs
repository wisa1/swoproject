using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.Server.Common;

namespace Wetr.Server.BL.IDefinition
{
    public interface IMeasurementManager
    {
        Task<IEnumerable<GroupedResultRecord>> PerformQueryAsync(MeasurementFilter filter);
    }
}
