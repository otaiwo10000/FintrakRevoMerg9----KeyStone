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
using Fintrak.Presentation.WebClient.Additionalmethods;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/scorecardmetricsKBL")]
    [UsesDisposableService]
    public class ScoreCardMetricsKBLApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ScoreCardMetricsKBLApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);
        }


        [HttpPost]
        [Route("updatescorecardmetricsKBL")]
        public HttpResponseMessage UpdateMetric(HttpRequestMessage request, [FromBody]ScoreCardMetricsKBL scmModel)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var scm = _MPRCoreService.UpdateScoreCardMetricsKBL(scmModel);

                //var response =  request.CreateResponse<ProductTransferPrice>(HttpStatusCode.OK, scm);
                response = request.CreateResponse<ScoreCardMetricsKBL>(HttpStatusCode.OK, scm);

                return response;
            });
        }


        [HttpPost]
        [Route("deleteupdatescorecardmetricsKBL")]
        public HttpResponseMessage DeleteScoreCardMetricsKBL(HttpRequestMessage request, [FromBody]int MetricID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                ScoreCardMetricsKBL scm = _MPRCoreService.GetScoreCardMetricsKBL(MetricID);

                if (scm != null)
                {
                    _MPRCoreService.DeleteScoreCardMetricsKBL(MetricID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No ScoreCard Metrics found under the ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getscorecardmetricsKBL/{MetricID}")]
        public HttpResponseMessage GetScoreCardMetricsKBL(HttpRequestMessage request, int MetricID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ScoreCardMetricsKBL scm = _MPRCoreService.GetScoreCardMetricsKBL(MetricID);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<ScoreCardMetricsKBL>(HttpStatusCode.OK, scm);

                return response;
            });
        }


        [HttpGet]
        [Route("getallscorecardmetricsKBL")]
        public HttpResponseMessage GetAllScoreCardMetricsKBL(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ScoreCardMetricsKBL[] scm = _MPRCoreService.GetAllScoreCardMetricsKBL();

                return request.CreateResponse<ScoreCardMetricsKBL[]>(HttpStatusCode.OK, scm);
            });
        }

        [HttpGet]
        [Route("getscorecardmetricsKBLusingsearchvalue/{searchvalue}")]
        public HttpResponseMessage GetScoreCardMetricsKBLUsingSearchValue(HttpRequestMessage request, string searchvalue)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ScoreCardMetricsKBL[] scm = _MPRCoreService.GetScoreCardMetricsKBLUsingSearchValue(searchvalue);

                // notice no need to create a seperate model object since TeamDefinition entity will do just fine
                response = request.CreateResponse<ScoreCardMetricsKBL[]>(HttpStatusCode.OK, scm);

                return response;
            });
        }

        [HttpGet]
        [Route("getscorecardmetricsKBLusingyear/{year}")]
        public HttpResponseMessage GetScoreCardMetricsKBLUsingYear(HttpRequestMessage request, int year)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ScoreCardMetricsKBL[] scm = _MPRCoreService.GetScoreCardMetricsKBLUsingYear(year);

                // notice no need to create a seperate model object since TeamDefinition entity will do just fine
                response = request.CreateResponse<ScoreCardMetricsKBL[]>(HttpStatusCode.OK, scm);

                return response;
            });
        }

        [HttpGet]
        [Route("maincaptionsfromotherinfo")]
        public HttpResponseMessage GetMainCAptionFromOtherInfoAndTotalLine(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                OtherInfo obj = new OtherInfo();
                List<string> scm = obj.GetMainCAptionFromOtherInfo();

                // notice no need to create a seperate model object since TeamDefinition entity will do just fine
                response = request.CreateResponse(HttpStatusCode.OK, scm);

                return response;
            });
        }
    }
}
