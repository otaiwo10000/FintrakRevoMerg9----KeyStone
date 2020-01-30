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
    [RoutePrefix("api/teamsegment")]
    [UsesDisposableService]
    public class TeamSegmentApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public TeamSegmentApiController(IMPRBSService mprBSService)
        {
            _MPRBSService = mprBSService;
        }

        IMPRBSService _MPRBSService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRBSService);
        }

        [HttpPost]
        [Route("updateteamsegment")]
        public HttpResponseMessage UpdateTeamSegment(HttpRequestMessage request, [FromBody]TeamSegment teamsegmentModel)
        {
            return GetHttpResponse(request, () =>
            {
                var teamsegment = _MPRBSService.UpdateTeamSegment(teamsegmentModel);

                return request.CreateResponse<TeamSegment>(HttpStatusCode.OK, teamsegment);
            });
        }

        [HttpPost]
        [Route("deleteteamsegment")]
        public HttpResponseMessage DeleteTeamSegment(HttpRequestMessage request, [FromBody]int Mpr_Team_Segment_ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                TeamSegment TeamSegment = _MPRBSService.GetTeamSegment(Mpr_Team_Segment_ID);

                if (TeamSegment != null)
                {
                    _MPRBSService.DeleteTeamSegment(Mpr_Team_Segment_ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No team sector found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getteamsegment/{Mpr_Team_Segment_ID}")]
        public HttpResponseMessage GetTeamSegment(HttpRequestMessage request, int Mpr_Team_Segment_ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                TeamSegment TeamSegment = _MPRBSService.GetTeamSegment(Mpr_Team_Segment_ID);

                // notice no need to create a seperate model object since TeamSegment entity will do just fine
                response = request.CreateResponse<TeamSegment>(HttpStatusCode.OK, TeamSegment);

                return response;
            });
        }

        [HttpGet]
        [Route("availableteamsegment")]
        public HttpResponseMessage GetAvailableTeamSegment(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                TeamSegment[] teamsegment = _MPRBSService.GetAllTeamSegments();


                return request.CreateResponse<TeamSegment[]>(HttpStatusCode.OK, teamsegment);
            });
        }

        [HttpGet]
        [Route("getteamsegmentusingsearch/{search}")]
        public HttpResponseMessage TeamSegmentUsingsearch(HttpRequestMessage request, string search)
        {
            return GetHttpResponse(request, () =>
            {
                TeamSegment[] teamsegment = _MPRBSService.GetTeamSegmentUsingsearch(search);


                return request.CreateResponse<TeamSegment[]>(HttpStatusCode.OK, teamsegment);
            });
        }
    }
}
