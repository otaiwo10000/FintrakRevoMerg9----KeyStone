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
    [RoutePrefix("api/scorecardsetmetrictargetKBL")]
    [UsesDisposableService]
    public class ScoreCardSetMetricTargetKBLApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ScoreCardSetMetricTargetKBLApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);
        }


        [HttpPost]
        [Route("updatescorecardsetmetrictargetKBL")]
        public HttpResponseMessage UpdateScoreCardSetMetricTargetKBL(HttpRequestMessage request, [FromBody]ScoreCardSetMetricTargetKBL scmModel)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var scm = _MPRCoreService.UpdateScoreCardSetMetricTargetKBL(scmModel);

                //var response =  request.CreateResponse<ProductTransferPrice>(HttpStatusCode.OK, scm);
                response = request.CreateResponse<ScoreCardSetMetricTargetKBL>(HttpStatusCode.OK, scm);

                return response;
            });
        }


        [HttpPost]
        [Route("deletescorecardsetmetrictargetKBL")]
        public HttpResponseMessage DeleteScoreCardSetMetricTargetKBL(HttpRequestMessage request, [FromBody]int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                ScoreCardSetMetricTargetKBL scm = _MPRCoreService.GetScoreCardSetMetricTargetKBL(ID);

                if (scm != null)
                {
                    _MPRCoreService.DeleteScoreCardSetMetricTargetKBL(ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No ScoreCard SetMetricTarget found under the ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getscorecardsetmetrictargetKBL/{ID}")]
        public HttpResponseMessage GetScoreCardSetMetricTargetKBL(HttpRequestMessage request, int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ScoreCardSetMetricTargetKBL scm = _MPRCoreService.GetScoreCardSetMetricTargetKBL(ID);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<ScoreCardSetMetricTargetKBL>(HttpStatusCode.OK, scm);

                return response;
            });
        }


        [HttpGet]
        [Route("getallscorecardsetmetrictargetKBL")]
        public HttpResponseMessage GetAllScoreCardSetMetricTargetKBL(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ScoreCardSetMetricTargetKBL[] scm = _MPRCoreService.GetAllScoreCardSetMetricTargetKBL();

                return request.CreateResponse<ScoreCardSetMetricTargetKBL[]>(HttpStatusCode.OK, scm);
            });
        }

        [HttpGet]
        [Route("getscorecardsetmetrictargetKBLusingsearch/{searchvalue}")]
        public HttpResponseMessage GetScoreCardSetMetricTargetKBLUsingSearch(HttpRequestMessage request, string searchvalue)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ScoreCardSetMetricTargetKBL[] scm = _MPRCoreService.GetScoreCardSetMetricTargetKBLUsingSearchValue(searchvalue);

                // notice no need to create a seperate model object since TeamDefinition entity will do just fine
                response = request.CreateResponse<ScoreCardSetMetricTargetKBL[]>(HttpStatusCode.OK, scm);

                return response;
            });
        }

        [HttpGet]
        [Route("getscorecardsetmetrictargetKBLusingperiodANDyear/{period}/{year}")]
        public HttpResponseMessage GetScoreCardSetMetricTargetKBLUsingPeriodANDYear(HttpRequestMessage request, int period, int year)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ScoreCardSetMetricTargetKBL[] scm = _MPRCoreService.GetScoreCardSetMetricTargetKBLByPeriodANDYear(period, year);

                // notice no need to create a seperate model object since TeamDefinition entity will do just fine
                response = request.CreateResponse<ScoreCardSetMetricTargetKBL[]>(HttpStatusCode.OK, scm);

                return response;
            });
        }

    }
}
