using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wetr.Server.DAL.DTO
{
    public class Community
    {
        public int ID { get; set; }
        public int PostalCode { get; set; }
        public string Name { get; set; }
        public int DistrictID { get; set; }
    }
}
