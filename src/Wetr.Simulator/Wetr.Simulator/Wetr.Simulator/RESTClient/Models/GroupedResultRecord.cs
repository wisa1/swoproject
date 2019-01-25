﻿// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Wetr.Simulator.REST.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;

    public partial class GroupedResultRecord
    {
        /// <summary>
        /// Initializes a new instance of the GroupedResultRecord class.
        /// </summary>
        public GroupedResultRecord() { }

        /// <summary>
        /// Initializes a new instance of the GroupedResultRecord class.
        /// </summary>
        public GroupedResultRecord(DateTime? dateTimeStart = default(DateTime?), int? periodType = default(int?), int? measurementTypeID = default(int?), int? deviceID = default(int?), int? unitOfMeasureID = default(int?), double? value = default(double?))
        {
            DateTimeStart = dateTimeStart;
            PeriodType = periodType;
            MeasurementTypeID = measurementTypeID;
            DeviceID = deviceID;
            UnitOfMeasureID = unitOfMeasureID;
            Value = value;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "DateTimeStart")]
        public DateTime? DateTimeStart { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "PeriodType")]
        public int? PeriodType { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "MeasurementTypeID")]
        public int? MeasurementTypeID { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "DeviceID")]
        public int? DeviceID { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "UnitOfMeasureID")]
        public int? UnitOfMeasureID { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Value")]
        public double? Value { get; set; }

    }
}