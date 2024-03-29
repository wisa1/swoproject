﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using Wetr.Server.BL.IDefinition;
using Wetr.Server.BL.Implementation;
using Wetr.Server.Common;
using Wetr.Server.DAL.DTO;
using static Wetr.Server.Common.Constants;

namespace Wetr.WebService.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class MeasurementsController : ApiController
    {
        private IMeasurementManager measurementManager = new MeasurementManager();

        // POST: api/Measurement
        // Insert single
        [HttpPost]
        [Route("measurements", Name = nameof(InsertMeasurement))]
        public async Task<HttpResponseMessage> InsertMeasurement([FromBody]Measurement measurement)
        {
            int result = await measurementManager.InsertMeasurementAsync(measurement);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // POST: api/Measurement
        // Insert multiple
        [HttpPost]
        [Route("measurements/multiple", Name = nameof(InsertMultipleMeasurements))]
        public async Task<HttpResponseMessage> InsertMultipleMeasurements([FromBody] IEnumerable<Measurement> measurements)
        {
            try
            {
                foreach (var measurement in measurements)
                {
                    await measurementManager.InsertMeasurementAsync(measurement);
                }
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                //in case an exception is thrown, notify the user that something went horribly wrong
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something went horribly wrong.");
            }
        }

        [HttpGet]
        [ResponseType(typeof(IEnumerable<GroupedResultRecord>))]
        [Route("measurements/query", Name = nameof(QueryMeasurements))]
        public async Task<HttpResponseMessage> QueryMeasurements(AggregationType aggregate, PeriodType period, 
                                                                 int deviceId, double? latitude = null, 
                                                                 double? longitude = null, int? measurementTypeId = null, 
                                                                 DateTime? dateFrom = null, DateTime? dateTo = null, int? radiusKm = 0)
        {
            try
            {
                MeasurementDevice device = new MeasurementDevice();
                device.ID = deviceId;
                device.Latitude = latitude ?? 0;
                device.Longitude = longitude ?? 0;

                MeasurementType type = null;
                if (measurementTypeId != null)
                {
                    type = new MeasurementType();
                    type.ID = measurementTypeId ?? 0;
                }
               

                MeasurementFilter filter = new MeasurementFilter()
                {
                    AggregationType = aggregate,
                    PeriodType = period,
                    MeasurementDevice = device,
                    DateFrom = dateFrom ?? DateTime.Now.AddDays(-3),
                    DateTo = dateTo ?? DateTime.Now,
                    RadiusKm = radiusKm ?? 0,
                    MeasurementType = type
                };

                IEnumerable<GroupedResultRecord> results = await measurementManager.PerformQueryAsync(filter);
                return Request.CreateResponse(HttpStatusCode.OK, results);
            }
            catch (Exception)
            {
                //in case an exception is thrown, notify the user that something went horribly wrong
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something went horribly wrong.");
            }
        }
    }
}
