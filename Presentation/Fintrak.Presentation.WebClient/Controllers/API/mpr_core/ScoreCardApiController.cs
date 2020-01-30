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
    [RoutePrefix("api/scorecard")]
    [UsesDisposableService]
    public class ScoreCardApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ScoreCardApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);
        }


        [HttpPost]
        [Route("updatescorecard")]
        public HttpResponseMessage UpdateSC(HttpRequestMessage request, [FromBody]ScoreCard scModel)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ScoreCard sc = _MPRCoreService.UpdateScoreCard(scModel);

                 response = request.CreateResponse<ScoreCard>(HttpStatusCode.OK, sc);

                return response;
            });
        }


        [HttpPost]
        [Route("deletescorecard")]
        public HttpResponseMessage DeleteSC(HttpRequestMessage request, [FromBody]int mpr_scorecard_stgId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                ScoreCard sc = _MPRCoreService.GetScoreCard(mpr_scorecard_stgId);

                if (sc != null)
                {
                    _MPRCoreService.DeleteScoreCard(mpr_scorecard_stgId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No score card found under the ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getscorecard/{mpr_scorecard_stgId}")]
        public HttpResponseMessage GetSC(HttpRequestMessage request, int mpr_scorecard_stgId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ScoreCard sc = _MPRCoreService.GetScoreCard(mpr_scorecard_stgId);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<ScoreCard>(HttpStatusCode.OK, sc);

                return response;
            });
        }


        [HttpGet]
        [Route("getallscorecard")]
        public HttpResponseMessage GetAllSC(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ScoreCard[] sc = _MPRCoreService.GetAllScoreCard();

                return request.CreateResponse<ScoreCard[]>(HttpStatusCode.OK, sc);
            });
        }

        [HttpGet]
        [Route("getscorecardcaptions")]
        public HttpResponseMessage GetSCCaptions(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ScoreCard[] scc = _MPRCoreService.ScoreCardCaptions();

                // notice no need to create a seperate model object since TeamDefinition entity will do just fine
                response = request.CreateResponse<ScoreCard[]>(HttpStatusCode.OK, scc);

                return response;
            });
        }

        //[HttpGet]
        //[Route("getscorecardmetricsusingsearch/{searchvalue}")]
        //public HttpResponseMessage GetMetricsUsingSearch(HttpRequestMessage request, string searchvalue)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;
        //        ScoreCardMetrics[] scm = _MPRCoreService.GetScoreCardMetricsUsingSearchValue(searchvalue);

        //        // notice no need to create a seperate model object since TeamDefinition entity will do just fine
        //       response =  request.CreateResponse<ScoreCardMetrics[]>(HttpStatusCode.OK, scm);

        //        return response;
        //    });
        //}

        //[HttpGet]
        //[Route("getscorecardmetricsusingsetup")]
        //public HttpResponseMessage GetMetricsUsingSetup(HttpRequestMessage request)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;

        //        ScoreCardMetrics[] scm = _MPRCoreService.GetScoreCardMetricsUsingSetUp();

        //        // notice no need to create a seperate model object since TeamDefinition entity will do just fine
        //        response = request.CreateResponse<ScoreCardMetrics[]>(HttpStatusCode.OK, scm);

        //        return response;
        //    });
        //}

    }
}
