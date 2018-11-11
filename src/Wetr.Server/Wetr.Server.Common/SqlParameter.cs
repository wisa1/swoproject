using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wetr.Server.Common
{
    public class SqlParameter
    {
        public SqlParameter(string name, object value)
        {
            this.Name = name;
            this.Value = value;
        }

        public string Name { get; }
        public object Value { get; }
    }
}
