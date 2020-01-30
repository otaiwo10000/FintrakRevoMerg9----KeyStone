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
    [RoutePrefix("api/scorecardweight")]
    [UsesDisposableService]
    public class ScoreCardWeightApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ScoreCardWeightApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);
        }


        [HttpPost]
        [Route("updatescorecardweight")]
        public HttpResponseMessage UpdateWeight(HttpRequestMessage request, [FromBody]ScoreCardWeight scwModel)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ScoreCardWeight scw = _MPRCoreService.UpdateScoreCardWeight(scwModel);

                //var response =  request.CreateResponse<ScoreCardMetrics>(HttpStatusCode.OK, scm);
                 response = request.CreateResponse<ScoreCardWeight>(HttpStatusCode.OK, scw);

                return response;
            });
        }


        [HttpPost]
        [Route("deletescorecardweight")]
        public HttpResponseMessage DeleteWeight(HttpRequestMessage request, [FromBody]int WeightId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                ScoreCardWeight scw = _MPRCoreService.GetScoreCardWeight(WeightId);

                if (scw != null)
                {
                    _MPRCoreService.DeleteScoreCardWeight(WeightId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Weight found under the ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getscorecardweight/{WeightId}")]
        public HttpResponseMessage GetMetric(HttpRequestMessage request, int WeightId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ScoreCardWeight scw = _MPRCoreService.GetScoreCardWeight(WeightId);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<ScoreCardWeight>(HttpStatusCode.OK, scw);

                return response;
            });
        }


        [HttpGet]
        [Route("getallscorecardweight")]
        public HttpResponseMessage GetAllWeight(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ScoreCardWeight[] scw = _MPRCoreService.GetAllScoreCardWeight();

                return request.CreateResponse<ScoreCardWeight[]>(HttpStatusCode.OK, scw);
            });
        }

        [HttpGet]
        [Route("getscorecardweightwithmetrics")]
        public HttpResponseMessage GetScoreCardWeightANDMetrics(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                //ScoreCardMetrics[] scm = _MPRCoreService.GetScoreCardMetricsUsingSetUp();
                ScoreCardWeightData[] scw = _MPRCoreService.GetScoreCardWeightWITHMetrics();

                // notice no need to create a seperate model object since TeamDefinition entity will do just fine
                response = request.CreateResponse<ScoreCardWeightData[]>(HttpStatusCode.OK, scw);

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
