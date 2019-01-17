using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Wetr.Server.BL.IDefinition;
using Wetr.Server.BL.Implementation;
using Wetr.Server.DAL.DTO;

namespace Wetr.WebService.Controllers
{
    public class MeasurementsController : ApiController
    {
        private IMeasurementManager measurementManager = new MeasurementManager();

        // POST: api/Measurement
        // Insert single
        [HttpPost]
        [Route("measurements", Name = nameof(InsertMeasurement))]
        public async Task InsertMeasurement([FromBody]Measurement measurement)
        {
            int result = await measurementManager.InsertMeasurementAsync(measurement);

        }

        // POST: api/Measurement
        // Insert multiple
        [HttpPost]
        [Route("measurements/multiple", Name = nameof(InsertMultipleMeasurements))]
        public async Task InsertMultipleMeasurements([FromBody] IEnumerable<Measurement> measurements)
        {
            foreach (var measurement in measurements)
            {
                await measurementManager.InsertMeasurementAsync(measurement);
            }
        }
    }
}
