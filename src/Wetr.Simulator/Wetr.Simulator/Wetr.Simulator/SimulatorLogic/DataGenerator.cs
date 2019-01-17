using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.Simulator.Helpers;
using Wetr.Simulator.ViewModels;


namespace Wetr.Simulator.SimulatorLogic
{
    public static class DataGenerator
    {
        public static Random random = new Random();
        public static MeasureModel CalculateNextValue(SimulatorConfiguration config,
            DateTime lastDate, double lastValue)
        {
            switch (config.DistributionStrategy)
            {
                case Server.Common.Constants.DistributionStrategy.Linear:
                    return CalcNextLinearValue(config, lastDate, lastValue);
                case Server.Common.Constants.DistributionStrategy.Random:
                    return CalcNextRandomValue(config, lastDate, lastValue);
                case Server.Common.Constants.DistributionStrategy.Sinus:
                    return CalcNextSinusValue(config, lastDate, lastValue);
            }
            return null;
        }

        private static MeasureModel CalcNextSinusValue(SimulatorConfiguration config, DateTime lastDate, double lastValue)
        {
            if (lastDate == default(DateTime))
            {
                lastDate = config.StartDateTime;
                lastValue = config.ValueRangeFrom;
            }

            int hour = lastDate.AddHours(config.HoursTimespan).Hour;
            double x = ((hour == 0) ? 0.01 : hour) / 24 * Math.PI;
            double offset, multiplacator;
            if (config.ValueRangeFrom < 0)
            {
                offset = config.ValueRangeFrom;
                multiplacator = config.ValueRangeTo + Math.Abs(config.ValueRangeFrom);
            } else
            {
                offset = config.ValueRangeFrom;
                multiplacator = config.ValueRangeTo - config.ValueRangeFrom;
            }
                
            return new MeasureModel()
            {
                DateTime = lastDate.AddHours(config.HoursTimespan),
                Value = offset + (Math.Abs(multiplacator)) * Math.Sin(x)
            };
        }

        private static MeasureModel CalcNextRandomValue(SimulatorConfiguration config, DateTime lastDate, double lastValue)
        {
            if (lastDate == default(DateTime))
            {
                lastDate = config.StartDateTime;
                lastValue = config.ValueRangeFrom;
            }
            return new MeasureModel()
            {
                DateTime = lastDate.AddHours(config.HoursTimespan),
                Value = random.NextDouble() * (config.ValueRangeTo - config.ValueRangeFrom) + config.ValueRangeFrom
            };
        }

        private static MeasureModel CalcNextLinearValue(SimulatorConfiguration config, DateTime lastDate, double lastValue)
        {
            if(lastDate == default(DateTime))
            {
                lastDate = config.StartDateTime;
                lastValue = config.ValueRangeFrom;
            }

            double totalHours = (config.EndDateTime - config.StartDateTime).TotalHours;
            double valueDiff = Math.Abs(config.ValueRangeFrom - config.ValueRangeTo);

            return new MeasureModel()
            {
                DateTime = lastDate.AddHours(config.HoursTimespan),
                Value = lastValue + (valueDiff / totalHours)
            };
        }
    }
}
