using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.Server.DAL.DTO;
using static Wetr.Server.Common.ADOTemplate;
using static Wetr.Server.Common.Constants;

namespace Wetr.Server.Common
{
    public class GroupedResultRecord
    {
        public static RowMapper<GroupedResultRecord> groupResultMapper = record =>
        {
            return new GroupedResultRecord()
            {
                DateTimeStart = DateTime.Parse((record["DateTimeStart"]).ToString()),
                Value = (double)record["Value"]
            };
        };

        public DateTime DateTimeStart { set; get; }
        public PeriodType PeriodType { get; set; }
        public MeasurementType MeasurementType { get; set; }
        public double Value { get; set; }
    }
}
