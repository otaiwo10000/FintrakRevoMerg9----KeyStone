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
    [RoutePrefix("api/scorecardmetrics")]
    [UsesDisposableService]
    public class ScoreCardMetricsApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ScoreCardMetricsApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);
        }


        [HttpPost]
        [Route("updatescorecardmetric")]
        public HttpResponseMessage UpdateMetric(HttpRequestMessage request, [FromBody]ScoreCardMetrics scmModel)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var scm = _MPRCoreService.UpdateScoreCardMetric(scmModel);

                //var response =  request.CreateResponse<ScoreCardMetrics>(HttpStatusCode.OK, scm);
                 response = request.CreateResponse<ScoreCardMetrics>(HttpStatusCode.OK, scm);

                return response;
            });
        }


        [HttpPost]
        [Route("deletescorecardmetric")]
        public HttpResponseMessage DeleteMetric(HttpRequestMessage request, [FromBody]int MetricId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                ScoreCardMetrics scm = _MPRCoreService.GetScoreCardMetric(MetricId);

                if (scm != null)
                {
                    _MPRCoreService.DeleteScoreCardMetric(MetricId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Metric found under the ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getscorecardmetric/{MetricId}")]
        public HttpResponseMessage GetMetric(HttpRequestMessage request, int MetricId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ScoreCardMetrics scm = _MPRCoreService.GetScoreCardMetric(MetricId);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<ScoreCardMetrics>(HttpStatusCode.OK, scm);

                return response;
            });
        }


        [HttpGet]
        [Route("getallscorecardmetrics")]
        public HttpResponseMessage GetAllmetrics(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ScoreCardMetrics[] scm = _MPRCoreService.GetAllScoreCardMetrics();

                return request.CreateResponse<ScoreCardMetrics[]>(HttpStatusCode.OK, scm);
            });
        }

        [HttpGet]
        [Route("getscorecardmetricsusingsearch/{searchvalue}")]
        public HttpResponseMessage GetMetricsUsingSearch(HttpRequestMessage request, string searchvalue)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ScoreCardMetricsData[] scm = _MPRCoreService.GetScoreCardMetricsUsingSearchValue(searchvalue);

                // notice no need to create a seperate model object since TeamDefinition entity will do just fine
                response = request.CreateResponse<ScoreCardMetricsData[]>(HttpStatusCode.OK, scm);

                return response;
            });
        }

        [HttpGet]
        [Route("getscorecardmetricsusingsetup")]
        public HttpResponseMessage GetMetricsUsingSetup(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ScoreCardMetricsData[] scm = _MPRCoreService.GetScoreCardMetricsUsingSetUp();

                // notice no need to create a seperate model object since TeamDefinition entity will do just fine
                response = request.CreateResponse<ScoreCardMetricsData[]>(HttpStatusCode.OK, scm);

                return response;
            });
        }

    }
}
