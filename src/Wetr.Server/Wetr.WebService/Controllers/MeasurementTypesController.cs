using System;
using System.Collections;
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
    public class MeasurementTypesController : ApiController
    {
        IMasterdataManager masterDataManager = new MasterdataManager();
        
        // GET: api/MeasurementTypes
        [HttpGet]
        [Route("MeasurementTypes", Name = nameof(GetAllTypes))]
        public async Task<IEnumerable<MeasurementType>> GetAllTypes()
        {
            return await masterDataManager.FindAllMeasurmentTypesAsync();
        }
    }
}
