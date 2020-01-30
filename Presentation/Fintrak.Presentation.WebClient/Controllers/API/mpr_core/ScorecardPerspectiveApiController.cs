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
    [RoutePrefix("api/scorecardperspective")]
    [UsesDisposableService]
    public class ScoreCardPerspectiveApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ScoreCardPerspectiveApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);
        }


        [HttpPost]
        [Route("updatescorecardperspective")]
        public HttpResponseMessage UpdatePerspective(HttpRequestMessage request, [FromBody]ScoreCardPerspective scpModel)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var scp = _MPRCoreService.UpdateScorecardPerspective(scpModel);

                //var response =  request.CreateResponse<ScoreCardMetrics>(HttpStatusCode.OK, scm);
                 response = request.CreateResponse<ScoreCardPerspective>(HttpStatusCode.OK, scp);

                return response;
            });
        }


        [HttpPost]
        [Route("deletescorecardperspective")]
        public HttpResponseMessage DeletePerspective(HttpRequestMessage request, [FromBody]int PerspectiveId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                ScoreCardPerspective scp = _MPRCoreService.GetScorecardPerspective(PerspectiveId);

                if (scp != null)
                {
                    _MPRCoreService.DeleteScorecardPerspective(PerspectiveId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Perspective found under the ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getscorecardperspective/{PerspectiveId}")]
        public HttpResponseMessage GetMetric(HttpRequestMessage request, int PerspectiveId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ScoreCardPerspective scp = _MPRCoreService.GetScorecardPerspective(PerspectiveId);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<ScoreCardPerspective>(HttpStatusCode.OK, scp);

                return response;
            });
        }


        [HttpGet]
        [Route("getallscorecardperspective")]
        public HttpResponseMessage GetAllmetrics(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ScoreCardPerspective[] scp = _MPRCoreService.GetAllScorecardPerspective();

                return request.CreateResponse<ScoreCardPerspective[]>(HttpStatusCode.OK, scp);
            });
        }

       

    }
}
