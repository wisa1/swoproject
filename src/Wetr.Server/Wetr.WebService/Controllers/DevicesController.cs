using System;
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
using Wetr.Server.DAL.DTO;

namespace Wetr.WebService.App_Start
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class DevicesController : ApiController
    {
        private IMasterdataManager masterDataManager = new MasterdataManager();

        // GET: api/Devices
        [HttpGet]
        [ResponseType(typeof(IEnumerable<MeasurementDevice>))]
        [Route("devices", Name = nameof(GetAllDevices))]
        public async Task<HttpResponseMessage> GetAllDevices()
        {
            try
            {
                IEnumerable<MeasurementDevice> devices = await masterDataManager.FindAllMeasurementDevicesAsync();
                return Request.CreateResponse(HttpStatusCode.OK, devices);
            }
            catch (Exception)
            {
                //in case an exception is thrown, notify the user that something went horribly wrong
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something went horribly wrong.");
            }
        }

        // GET: api/Devices/5
        [HttpGet]
        [ResponseType(typeof(MeasurementDevice))]
        [Route("devices/{id}", Name = nameof(GetDeviceById))]
        public async Task<HttpResponseMessage> GetDeviceById(int id)
        {
            try
            {
                MeasurementDevice device = await masterDataManager.FindMeasurementDeviceByIdAsync(id);
                return Request.CreateResponse(HttpStatusCode.OK, device);
            }
            catch (Exception)
            {
                //in case an exception is thrown, notify the user that something went horribly wrong
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something went horribly wrong.");
            }
        }

        // POST: api/Devices
        [HttpPost]
        [Route("devices", Name = nameof(InsertDevice))]
        public async Task<HttpResponseMessage> InsertDevice([FromBody]MeasurementDevice value)
        {         
            try
            {
                MeasurementDevice device = await masterDataManager.InsertMeasurementDeviceAsync(value);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                //in case an exception is thrown, notify the user that something went horribly wrong
                return Request.CreateErrorResponse(HttpStatusCode.NotModified, "Something went horribly wrong.");
            }
            
        }


        // PUT: api/Devices/5
        [HttpPut]
        [Route("devices", Name = nameof(UpdateDevice))]
        public async Task<HttpResponseMessage> UpdateDevice([FromBody]MeasurementDevice value)
        {
            try
            {
                int updated = await masterDataManager.UpdateMeasurementDeviceAsync(value);
                if (updated != 1)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotModified, "Resource not found or not updateable");
                } else
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception)
            {
                //in case an exception is thrown, notify the user that something went horribly wrong
                return Request.CreateErrorResponse(HttpStatusCode.NotModified, "Something went horribly wrong.");
            }
        }

        // DELETE: api/Devices/5
        [HttpDelete]
        [Route("devices", Name = nameof(DeleteDevice))]
        public async Task<HttpResponseMessage> DeleteDevice([FromBody]MeasurementDevice value)
        {
            try
            {
                int deleted = await masterDataManager.DeleteMeasurementDeviceAsync(value);
                if (deleted != 1)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotModified, "Resource not found or not updateable");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception)
            {
                //in case an exception is thrown, notify the user that something went horribly wrong
                return Request.CreateErrorResponse(HttpStatusCode.NotModified, "Something went horribly wrong.");
            }
        }
    }
}
