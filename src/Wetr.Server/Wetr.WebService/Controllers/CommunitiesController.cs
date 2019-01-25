using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Wetr.Server.BL.IDefinition;
using Wetr.Server.BL.Implementation;
using Wetr.Server.DAL.DTO;

namespace Wetr.WebService.Controllers
{
    public class CommunitiesController : ApiController
    {
        private IMasterdataManager masterDataManager = new MasterdataManager();

        // GET: api/Devices
        [HttpGet]
        [ResponseType(typeof(IEnumerable<Community>))]
        [Route("communities", Name = nameof(GetAllCommunities))]
        // GET: api/Communities
        public async Task<HttpResponseMessage> GetAllCommunities()
        {
            try
            {
                IEnumerable<Community> communities = await masterDataManager.FindAllCommunitiesAsync();
                return Request.CreateResponse(HttpStatusCode.OK, communities);
            }
            catch (Exception)
            {
                //in case an exception is thrown, notify the user that something went horribly wrong
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something went horribly wrong.");
            }
        }

        // GET: api/Communities/5
        [HttpGet]
        [ResponseType(typeof(Community))]
        [Route("communities/{id}", Name = nameof(GetCommunityById))]
        public async Task<HttpResponseMessage> GetCommunityById(int id)
        {
            try
            {
                Community community = await masterDataManager.FindCommunityByIdAsync(id);
                return Request.CreateResponse(HttpStatusCode.OK, community);
            }
            catch (Exception)
            {
                //in case an exception is thrown, notify the user that something went horribly wrong
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something went horribly wrong.");
            }
        }
    }
}
