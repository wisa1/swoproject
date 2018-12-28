using LiveCharts.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wetr.Simulator.ViewModels
{
    public class MeasureModel
    {
        public DateTime DateTime { get; set; }
        public double Value { get; set; }

        public static CartesianMapper<MeasureModel> mapper = 
            Mappers.Xy<MeasureModel>()
                //.X(model => (double)model.DateTime.Ticks / TimeSpan.FromHours(1).Ticks)
                .X(model => model.DateTime.Ticks)
                .Y(model => model.Value);

        public static string Formatter(double arg)
        {
            return new DateTime((long)arg).ToString("yyyy-MM:dd HH:mm:ss");
        }
    }
}

