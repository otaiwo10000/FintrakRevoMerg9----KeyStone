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
using Fintrak.Presentation.WebClient.Models;
using Fintrak.Shared.Common.Extensions;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/scorecardmapping")]
    [UsesDisposableService]
    public class ScoreCardMappingApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ScoreCardMappingApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);
        }


        [HttpPost]
        [Route("updatescorecardmapping")]
        public HttpResponseMessage UpdateMapping(HttpRequestMessage request, [FromBody]ScoreCardMapping scmModel)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var scm = _MPRCoreService.UpdateScoreCardMapping(scmModel);

                //var response =  request.CreateResponse<ScoreCardMetrics>(HttpStatusCode.OK, scm);
                 response = request.CreateResponse<ScoreCardMapping>(HttpStatusCode.OK, scm);

                return response;
            });
        }


        [HttpPost]
        [Route("deletescorecardmapping")]
        public HttpResponseMessage DeleteMapping(HttpRequestMessage request, [FromBody]int MappingId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                ScoreCardMapping scm = _MPRCoreService.GetScoreCardMapping(MappingId);

                if (scm != null)
                {
                    _MPRCoreService.DeleteScoreCardMapping(MappingId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Mapping found under the ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getscorecardmapping/{MappingId}")]
        public HttpResponseMessage GetMetric(HttpRequestMessage request, int MappingId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ScoreCardMapping scm = _MPRCoreService.GetScoreCardMapping(MappingId);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<ScoreCardMapping>(HttpStatusCode.OK, scm);

                return response;
            });
        }


        [HttpGet]
        [Route("getallscorecardmapping")]
        public HttpResponseMessage GetAllmapping(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ScoreCardMapping[] scm = _MPRCoreService.GetAllScoreCardMapping();

                return request.CreateResponse<ScoreCardMapping[]>(HttpStatusCode.OK, scm);
            });
        }

        [HttpGet]
        [Route("getscorecardmappingusingsearch/{searchvalue}")]
        public HttpResponseMessage GetMappingUsingSearch(HttpRequestMessage request, string searchvalue)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ScoreCardMappingData[] scm = _MPRCoreService.GetScoreCardMappingUsingSearchValue(searchvalue);

                // notice no need to create a seperate model object since TeamDefinition entity will do just fine
               response =  request.CreateResponse<ScoreCardMappingData[]>(HttpStatusCode.OK, scm);

                return response;
            });
        }

        [HttpGet]
        [Route("getscorecardmappingusingsetup")]
        public HttpResponseMessage GetMetricsUsingSetup(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ScoreCardMappingData[] scm = _MPRCoreService.GetScoreCardMappingUsingSetUp();
                                                         
                // notice no need to create a seperate model object since TeamDefinition entity will do just fine
                response = request.CreateResponse<ScoreCardMappingData[]>(HttpStatusCode.OK, scm);

                return response;
            });
        }

    }
}
