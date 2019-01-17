using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.Server.DAL.DTO;
using static Wetr.Server.Common.Constants;

namespace Wetr.Server.Common
{
    public class MeasurementFilter
    {
        public AggregationType AggregationType { get; set; }
        public PeriodType PeriodType { get; set; }
        public MeasurementDevice MeasurementDevice { get; set; }
        public MeasurementType MeasurementType { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public double RadiusKm { get; set; }
    }
}
