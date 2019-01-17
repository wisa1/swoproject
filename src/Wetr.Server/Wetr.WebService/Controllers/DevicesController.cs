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

namespace Wetr.WebService.App_Start
{
    public class DevicesController : ApiController
    {
        private IMasterdataManager masterDataManager = new MasterdataManager();

        // GET: api/Devices
        [HttpGet]
        [Route("devices", Name = nameof(GetAllDevices))]
        public async Task<IEnumerable<MeasurementDevice>> GetAllDevices()
        {
            //return new string[] { "value1", "value2" };
            return await masterDataManager.FindAllMeasurementDevicesAsync();
        }

        // GET: api/Devices/5
        [HttpGet]
        [Route("devices/{id}", Name = nameof(GetDeviceById))]
        public async Task<MeasurementDevice> GetDeviceById(int id)
        {
            return await masterDataManager.FindMeasurementDeviceById(id);
        }

        // POST: api/Devices
        [HttpPost]
        [Route("devices", Name = nameof(InsertDevice))]
        public async Task InsertDevice([FromBody]MeasurementDevice value)
        {
            await masterDataManager.InsertMeasurementDeviceAsync(value);
        }


        // PUT: api/Devices/5
        [HttpPut]
        [Route("devices", Name = nameof(UpdateDevice))]
        public async Task UpdateDevice([FromBody]MeasurementDevice value)
        {
            await masterDataManager.InsertMeasurementDeviceAsync(value);
        }

        // DELETE: api/Devices/5
        [HttpDelete]
        [Route("devices", Name = nameof(DeleteDevice))]
        public async Task<HttpResponseMessage> DeleteDevice([FromBody]MeasurementDevice value)
        {
            int deleted = await masterDataManager.DeleteMeasurementDeviceAsync(value);
            if(deleted == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotModified, "Resource not found or undeletable");
                
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
