using System;
using System.Collections.ObjectModel;
using Wetr.Simulator.ViewModels;
using static Wetr.Server.Common.Constants;

namespace Wetr.Simulator.Helpers

{
    public class SimulatorConfiguration
    {
        public DateTime StartDateTime { set; get; }
        public DateTime EndDateTime { set; get; }
        public DistributionStrategy DistributionStrategy { set; get; }
        public MeasurementTypeVM MeasurementType { set; get; }
        public float ValueRangeFrom { set; get; }
        public float ValueRangeTo { set; get; }
        public float SimulationSpeed { set; get; }
        public int HoursTimespan { set; get; }

        public SimulatorConfiguration(DateTime start, DateTime end, MeasurementTypeVM type, float rangeFrom, float rangeTo, float simulationSpeed, DistributionStrategy distributionStrategy, int hoursTimespan)
        {
            this.StartDateTime = start;
            this.EndDateTime = end;
            this.MeasurementType = type;
            this.ValueRangeFrom = rangeFrom;
            this.ValueRangeTo = rangeTo;
            this.SimulationSpeed = simulationSpeed;
            this.DistributionStrategy = distributionStrategy;
            this.HoursTimespan = hoursTimespan == 0 ? 1 : hoursTimespan;
        }
    }

    
}