using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wetr.Server.DAL.DTO
{
    public class Measurement
    {
        public int ID { get; set; }
        public int TypeID { get; set; }
        public int DeviceID { get; set; }
        public double Value { get; set; }
        public int UnitOfMeasureID { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
