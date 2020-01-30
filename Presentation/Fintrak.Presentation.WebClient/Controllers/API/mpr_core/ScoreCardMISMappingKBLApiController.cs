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
    [RoutePrefix("api/scorecardMISmappingKBL")]
    [UsesDisposableService]
    public class ScoreCardMISMappingKBLApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ScoreCardMISMappingKBLApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);
        }


        [HttpPost]
        [Route("updatescorecardMISmappingKBL")]
        public HttpResponseMessage UpdateScoreCardMISMappingKBL(HttpRequestMessage request, [FromBody]ScoreCardMISMappingKBL scmModel)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var scm = _MPRCoreService.UpdateScoreCardMISMappingKBL(scmModel);

                 response = request.CreateResponse<ScoreCardMISMappingKBL>(HttpStatusCode.OK, scm);

                return response;
            });
        }


        [HttpPost]
        [Route("deletescorecardMISmappingKBL")]
        public HttpResponseMessage DeleteScoreCardMISMappingKBL(HttpRequestMessage request, [FromBody]int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                ScoreCardMISMappingKBL scm = _MPRCoreService.GetScoreCardMISMappingKBL(ID);

                if (scm != null)
                {
                    _MPRCoreService.DeleteScoreCardMISMappingKBL(ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No ScoreCard MIS Mapping  found under the ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getscorecardMISmappingKBL/{ID}")]
        public HttpResponseMessage GetScoreCardMISMappingKBL(HttpRequestMessage request, int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ScoreCardMISMappingKBL scm = _MPRCoreService.GetScoreCardMISMappingKBL(ID);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<ScoreCardMISMappingKBL>(HttpStatusCode.OK, scm);

                return response;
            });
        }


        [HttpGet]
        [Route("getallscorecardMISmappingKBL")]
        public HttpResponseMessage GetallScoreCardMISMappingKBL(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ScoreCardMISMappingKBL[] scm = _MPRCoreService.GetAllScoreCardMISMappingKBL();

                return request.CreateResponse<ScoreCardMISMappingKBL[]>(HttpStatusCode.OK, scm);
            });
        }

       
        [HttpGet] 
        [Route("getscorecardMISmappingKBLusingsearchvalue/{searchvalue}")]
        public HttpResponseMessage GetScoreCardMISMappingKBLUsingSearchValue(HttpRequestMessage request, string searchvalue)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ScoreCardMISMappingKBL[] scm = _MPRCoreService.GetScoreCardMISMappingKBLUsingSearchValue(searchvalue);

                // notice no need to create a seperate model object since TeamDefinition entity will do just fine
                response = request.CreateResponse<ScoreCardMISMappingKBL[]>(HttpStatusCode.OK, scm);

                return response;
            });
        }
    }
}
