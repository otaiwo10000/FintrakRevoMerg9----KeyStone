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
using Fintrak.Presentation.WebClient.Additionalmethods;
using Fintrak.Presentation.WebClient.Models;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/pls")]
    [UsesDisposableService]
    public class PLsApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public PLsApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }

        [HttpGet]
        [Route("getincomeincomenewdetails")]
        public HttpResponseMessage GetIncomeIncomeNeDetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeIncomeNewDetailsMtd obj = new IncomeIncomeNewDetailsMtd();
                List<IncomeIncomeNewDetailsModel> dm = obj.GetIncomeIncomeNeDetails().ToList();

                return request.CreateResponse(HttpStatusCode.OK, dm);

            });
        }

        [HttpGet]
        [Route("getincomeincomenewdetailsByParam/{search}/{period}")]
        public HttpResponseMessage GetIncomeIncomeNeDetailsByParam(HttpRequestMessage request, string search, int period)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeIncomeNewDetailsMtd obj = new IncomeIncomeNewDetailsMtd();
                List<IncomeIncomeNewDetailsModel> dm = obj.GetIncomeIncomeNewDetailsUsingParams(search, period).ToList();

                return request.CreateResponse(HttpStatusCode.OK, dm);

            });
        }

        [HttpGet]
        [Route("getincomeincomeotherbreakdown")]
        public HttpResponseMessage GetIncomeIncomeOtherBreakdown(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeIncomeOtherBreakdownMtd obj = new IncomeIncomeOtherBreakdownMtd();
                List<IncomeIncomeOtherBreakdownModel> dm = obj.GetIncomeIncomeOtherBreakdown().ToList();

                return request.CreateResponse(HttpStatusCode.OK, dm);

            });
        }

        [HttpGet]
        [Route("getincomeincomeotherbreakdownByParam/{search}/{period}")]
        public HttpResponseMessage GetIncomeIncomeOtherBreakdownByParam(HttpRequestMessage request, string search, int period)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeIncomeOtherBreakdownMtd obj = new IncomeIncomeOtherBreakdownMtd();
                List<IncomeIncomeOtherBreakdownModel> dm = obj.GetIncomeIncomeOtherBreakdownUsingParams(search, period).ToList();

                return request.CreateResponse(HttpStatusCode.OK, dm);

            });
        }


    }
}
