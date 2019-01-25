﻿// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Wetr.Simulator.REST
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Rest;
    using Models;

    /// <summary>
    /// Measurements operations.
    /// </summary>
    public partial interface IMeasurements
    {
        /// <param name='measurement'>
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse<object>> InsertMeasurementWithHttpMessagesAsync(Measurement measurement, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));
        /// <param name='measurements'>
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse<object>> InsertMultipleMeasurementsWithHttpMessagesAsync(IList<Measurement> measurements, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));
        /// <param name='aggregate'>
        /// </param>
        /// <param name='period'>
        /// </param>
        /// <param name='deviceId'>
        /// </param>
        /// <param name='latitude'>
        /// </param>
        /// <param name='longitude'>
        /// </param>
        /// <param name='measurementTypeId'>
        /// </param>
        /// <param name='dateFrom'>
        /// </param>
        /// <param name='dateTo'>
        /// </param>
        /// <param name='radiusKm'>
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse<IList<GroupedResultRecord>>> QueryMeasurementsWithHttpMessagesAsync(int aggregate, int period, int deviceId, double? latitude = default(double?), double? longitude = default(double?), int? measurementTypeId = default(int?), DateTime? dateFrom = default(DateTime?), DateTime? dateTo = default(DateTime?), int? radiusKm = default(int?), Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}
