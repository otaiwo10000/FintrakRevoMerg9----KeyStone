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
    [RoutePrefix("api/scorecardKPItypesKBL")]
    [UsesDisposableService]
    public class ScoreCardKPITypesKBLApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ScoreCardKPITypesKBLApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);
        }


        [HttpPost]
        [Route("updatescorecardKPItypesKBL")]
        public HttpResponseMessage UpdateScoreCardKPITypesKBL(HttpRequestMessage request, [FromBody]ScoreCardKPITypesKBL scmModel)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var scm = _MPRCoreService.UpdateScoreCardKPITypesKBL(scmModel);

                //var response =  request.CreateResponse<ProductTransferPrice>(HttpStatusCode.OK, scm);
                response = request.CreateResponse<ScoreCardKPITypesKBL>(HttpStatusCode.OK, scm);

                return response;
            });
        }


        [HttpPost]
        [Route("deletescorecardKPItypesKBL")]
        public HttpResponseMessage DeleteScoreCardKPITypesKBL(HttpRequestMessage request, [FromBody]int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                ScoreCardKPITypesKBL scm = _MPRCoreService.GetScoreCardKPITypesKBL(ID);

                if (scm != null)
                {
                    _MPRCoreService.DeleteScoreCardKPITypesKBL(ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No ScoreCard KPIType found under the ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getscorecardKPItypesKBL/{ID}")]
        public HttpResponseMessage GetMetric(HttpRequestMessage request, int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ScoreCardKPITypesKBL scm = _MPRCoreService.GetScoreCardKPITypesKBL(ID);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<ScoreCardKPITypesKBL>(HttpStatusCode.OK, scm);

                return response;
            });
        }


        [HttpGet]
        [Route("getallscorecardKPItypesKBL")]
        public HttpResponseMessage GetAllmetrics(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ScoreCardKPITypesKBL[] scm = _MPRCoreService.GetAllScoreCardKPITypesKBL();

                return request.CreateResponse<ScoreCardKPITypesKBL[]>(HttpStatusCode.OK, scm);
            });
        }

        [HttpGet]
        [Route("getscorecardKPItypesKBLusingsearch/{searchvalue}")]
        public HttpResponseMessage GetScoreCardKPITypesKBLUsingSearch(HttpRequestMessage request, string searchvalue)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ScoreCardKPITypesKBL[] scm = _MPRCoreService.GetScoreCardKPITypesKBLUsingSearchValue(searchvalue);

                // notice no need to create a seperate model object since TeamDefinition entity will do just fine
                response = request.CreateResponse<ScoreCardKPITypesKBL[]>(HttpStatusCode.OK, scm);

                return response;
            });
        }

        [HttpGet]
        [Route("getscorecardKPItypesKBLusingperiodyearsearchvalue/{period}/{year}/{searchvalue}")]
        public HttpResponseMessage GetScoreCardKPITypesKBLUsingSearch(HttpRequestMessage request, int period, int year, string searchvalue)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ScoreCardKPITypesKBL[] scm = _MPRCoreService.GetScoreCardKPITypesKBLByPeriodYearKPIType(period, year, searchvalue);

                // notice no need to create a seperate model object since TeamDefinition entity will do just fine
                response = request.CreateResponse<ScoreCardKPITypesKBL[]>(HttpStatusCode.OK, scm);

                return response;
            });
        }

    }
}
