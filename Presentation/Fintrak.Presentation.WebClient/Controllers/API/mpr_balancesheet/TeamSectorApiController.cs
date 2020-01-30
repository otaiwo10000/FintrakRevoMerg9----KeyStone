using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.MPR.Contracts;
using Fintrak.Client.MPR.Entities;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/teamsector")]
    [UsesDisposableService]
    public class TeamSectorApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public TeamSectorApiController(IMPRBSService mprBSService)
        {
            _MPRBSService = mprBSService;
        }

        IMPRBSService _MPRBSService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRBSService);
        }

        [HttpPost]
        [Route("updateteamsector")]
        public HttpResponseMessage UpdateTeamSector(HttpRequestMessage request, [FromBody]TeamSector teamsectorModel)
        {
            return GetHttpResponse(request, () =>
            {
                var teamsector = _MPRBSService.UpdateTeamSector(teamsectorModel);

                return request.CreateResponse<TeamSector>(HttpStatusCode.OK, teamsector);
            });
        }

        [HttpPost]
        [Route("deleteteamsector")]
        public HttpResponseMessage DeleteTeamSector(HttpRequestMessage request, [FromBody]int Mpr_Team_Sector_ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                TeamSector teamsector = _MPRBSService.GetTeamSector(Mpr_Team_Sector_ID);

                if (teamsector != null)
                {
                    _MPRBSService.DeleteTeamSector(Mpr_Team_Sector_ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No team sector found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getteamsector/{Mpr_Team_Sector_ID}")]
        public HttpResponseMessage GetTeamSector(HttpRequestMessage request, int Mpr_Team_Sector_ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                TeamSector teamsector = _MPRBSService.GetTeamSector(Mpr_Team_Sector_ID);

                // notice no need to create a seperate model object since TeamSector entity will do just fine
                response = request.CreateResponse<TeamSector>(HttpStatusCode.OK, teamsector);

                return response;
            });
        }

        [HttpGet]
        [Route("availableteamsector")]
        public HttpResponseMessage GetAvailableTeamSector(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                TeamSector[] teamsector = _MPRBSService.GetAllTeamSectors();


                return request.CreateResponse<TeamSector[]>(HttpStatusCode.OK, teamsector);
            });
        }

        [HttpGet]
        [Route("getteamsectorusingsearch/{search}")]
        public HttpResponseMessage TeamSectorUsingsearch(HttpRequestMessage request, string search)
        {
            return GetHttpResponse(request, () =>
            {
                TeamSector[] teamsector = _MPRBSService.GetTeamSectorUsingsearch(search);


                return request.CreateResponse<TeamSector[]>(HttpStatusCode.OK, teamsector);
            });
        }
    }
}
