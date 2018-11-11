using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wetr.Server.DAL.DTO
{
    public class District
    {
        public int ID { get; set; }
        public int ProvinceID { get; set; }
        public string Name { get; set; }
    }
}
