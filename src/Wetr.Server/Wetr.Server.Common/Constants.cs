using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wetr.Server.Common
{
    public static class Constants
    {
        public enum PeriodType { None, /*Hour,*/ Day, Week, Month, Year }
        public enum AggregationType { None, Sum, Max, Min, Avg }
        public enum DistributionStrategy { Linear, Random }
    }

    public static class PeriodTypeExtension
    {
        public static string ToSqlDatepart(this Constants.PeriodType me)
        {
            switch (me)
            {
                case Constants.PeriodType.None:  return "";
                /*case Constants.PeriodType.Hour:  return "hh";*/
                case Constants.PeriodType.Day :  return "d";
                case Constants.PeriodType.Month: return "m";
                case Constants.PeriodType.Week:  return "wk";
                case Constants.PeriodType.Year:  return "yy";
                default: return "";
            }
        }
    }

    public static class AggregateTypeExtension
    {
        public static string ToSqlAggregate(this Constants.AggregationType me)
        {
            switch (me)
            {
                case Constants.AggregationType.None:return "";
                case Constants.AggregationType.Avg: return "AVG";
                case Constants.AggregationType.Max: return "MAX";
                case Constants.AggregationType.Min: return "MIN";
                case Constants.AggregationType.Sum: return "SUM";
                default: return "";
            }
        }
    }
}

