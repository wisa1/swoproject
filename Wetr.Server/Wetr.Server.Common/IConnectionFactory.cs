using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wetr.Server.Common
{
    public interface IConnectionFactory
    {
        string ProviderName { get; }
        string ConnectionString { get; }
        DbConnection CreateConnection();
    }
}
