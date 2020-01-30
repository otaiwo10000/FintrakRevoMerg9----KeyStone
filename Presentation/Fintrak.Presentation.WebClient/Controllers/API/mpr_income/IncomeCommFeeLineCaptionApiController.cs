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
    [RoutePrefix("api/incomecommfeelinecaption")]
    [UsesDisposableService]
    public class IncomeCommFeeLineCaptionApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomeCommFeeLineCaptionApiController(IMPRIncomeService mprincomeService)
        {
            _MPRIncomeService = mprincomeService;
        }

        IMPRIncomeService _MPRIncomeService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRIncomeService);
        }


        [HttpPost]
        [Route("updateincomecommfeelinecaption")]
        public HttpResponseMessage UpdateIncomeCommFeeLineCaption(HttpRequestMessage request, [FromBody]IncomeCommFeeLineCaption icflModel)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var icfl = _MPRIncomeService.UpdateIncomeCommFeeLineCaption(icflModel);

                //var response =  request.CreateResponse<ScoreCardMetrics>(HttpStatusCode.OK, scm);
                 response = request.CreateResponse<IncomeCommFeeLineCaption>(HttpStatusCode.OK, icfl);

                return response;
            });
        }


        [HttpPost]
        [Route("deleteincomecommfeelinecaption")]
        public HttpResponseMessage DeleteIncomeCommFeeLineCaption(HttpRequestMessage request, [FromBody]int ICFLcaptionId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IncomeCommFeeLineCaption icfl = _MPRIncomeService.GetIncomeCommFeeLineCaption(ICFLcaptionId);

                if (icfl != null)
                {
                    _MPRIncomeService.DeleteIncomeCommFeeLineCaption(ICFLcaptionId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No IncomeCommFeeLineCaption found under the ICFLcaptionId.");

                return response;
            });
        }


        [HttpGet]
        [Route("getincomecommfeelinecaption/{ICFLcaptionId}")]
        public HttpResponseMessage GetIncomeCommFeeLineCaption(HttpRequestMessage request, int ICFLcaptionId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IncomeCommFeeLineCaption icfl = _MPRIncomeService.GetIncomeCommFeeLineCaption(ICFLcaptionId);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<IncomeCommFeeLineCaption>(HttpStatusCode.OK, icfl);

                return response;
            });
        }


        [HttpGet]
        [Route("getallincomecommfeelinecaption")]
        public HttpResponseMessage GetAllIncomeCommFeeLineCaption(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeCommFeeLineCaption[] icfl = _MPRIncomeService.GetAllIncomeCommFeeLineCaption();

                return request.CreateResponse<IncomeCommFeeLineCaption[]>(HttpStatusCode.OK, icfl);
            });
        }

        [HttpGet]
        [Route("getincomecommfeelinecaptionusingsearch/{searchvalue}")]
        public HttpResponseMessage GetIncomeCommFeeLineCaptionUsingSearchValue(HttpRequestMessage request, string searchvalue)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                IncomeCommFeeLineCaption[] icfl = _MPRIncomeService.GetIncomeCommFeeLineCaptionUsingSearchValue(searchvalue);

                // notice no need to create a seperate model object since TeamDefinition entity will do just fine
                response = request.CreateResponse<IncomeCommFeeLineCaption[]>(HttpStatusCode.OK, icfl);

                return response;
            });
        }

    

    }
}
