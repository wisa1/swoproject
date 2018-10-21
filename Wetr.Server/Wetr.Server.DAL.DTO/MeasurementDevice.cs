using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wetr.Server.DAL.DTO
{
    public class MeasurementDevice
    {
        public int ID { get; set; }
        public int CommunityID { get; set; }
        public string DeviceName { get; set; }
        public string Address { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
    }
}
