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
    [RoutePrefix("api/incomeotherbreakdown")]
    [UsesDisposableService]
    public class IncomeOtherBreakdownApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomeOtherBreakdownApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }

        [HttpPost]
        [Route("updateincomeotherbreakdown")]
        public HttpResponseMessage UpdateIncomeOtherBreakdown(HttpRequestMessage request, [FromBody]IncomeOtherBreakdown iobModel)
        {
            return GetHttpResponse(request, () =>
            {
                var iob = _MPRCoreService.UpdateIncomeOtherBreakdown(iobModel);

                return request.CreateResponse<IncomeOtherBreakdown>(HttpStatusCode.OK, iob);
            });
        }


        [HttpPost]
        [Route("deleteincomeotherbreakdown")]
        public HttpResponseMessage DeleteAbcRatio(HttpRequestMessage request, [FromBody]int iobId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IncomeOtherBreakdown iob = _MPRCoreService.GetIncomeOtherBreakdown(iobId);

                if (iob != null)
                {
                    _MPRCoreService.DeleteIncomeOtherBreakdown(iobId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No income otherbreakdown found under that ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getincomeotherbreakdown/{iobID}")]
        public HttpResponseMessage GetAbcRatio(HttpRequestMessage request, int iobID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IncomeOtherBreakdown iob = _MPRCoreService.GetIncomeOtherBreakdown(iobID);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<IncomeOtherBreakdown>(HttpStatusCode.OK, iob);

                return response;
            });
        }


        [HttpGet]
        [Route("availableincomeotherbreakdown")]
        public HttpResponseMessage GetAvailableAbcRatio(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeOtherBreakdown[] iob = _MPRCoreService.GetAllIncomeOtherBreakdown();

                return request.CreateResponse<IncomeOtherBreakdown[]>(HttpStatusCode.OK, iob);
            });
        }


        [HttpGet]
        [Route("availableincomeotherbreakdownforlatestyearmonth")]
        public HttpResponseMessage GetAvailableIncomeBreakDownForLatestYearMonth(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeOtherBreakdownMtd obj = new IncomeOtherBreakdownMtd();
                List<IncomeOtherBreakdownModel> objList = new List<IncomeOtherBreakdownModel>();

                //IncomeOtherBreakdown[] iob = _MPRCoreService.GetAllIncomeOtherBreakdown();
                objList = obj.GetIncomeOtherBreakdown().ToList();

                return request.CreateResponse(HttpStatusCode.OK, objList);
            });
        }

        [HttpGet]
        [Route("availableincomeotherbreakdownusingyearmonth/{year}/{period}/{search}")]
        public HttpResponseMessage GetAvailableIncomeBreakDownUsingYearMonth(HttpRequestMessage request, int year, int period, string search)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeOtherBreakdownMtd obj = new IncomeOtherBreakdownMtd();
                List<IncomeOtherBreakdownModel> objList = new List<IncomeOtherBreakdownModel>();

                //IncomeOtherBreakdown[] iob = _MPRCoreService.GetAllIncomeOtherBreakdown();
                objList = obj.GetIncomeOtherBreakdownUsingYearPeriod(year, period, search).ToList();

                return request.CreateResponse(HttpStatusCode.OK, objList);
            });
        }

    }
}
