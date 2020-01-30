using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Presentation.WebClient.Models;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.MPR.Contracts;
using Fintrak.Client.MPR.Entities;


namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/incomeadjustmentcommfeesearchmanual")]
    [UsesDisposableService]
    public class IncomeAdjustmentCommFeeSearchManualApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomeAdjustmentCommFeeSearchManualApiController(IMPRPLService mprPLService)
        {
            _MPRPLService = mprPLService;
        }

        IMPRPLService _MPRPLService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRPLService);
        }

        [HttpPost]
        [Route("updatecommfeemanual")]
        public HttpResponseMessage UpdateCommFeeManual(HttpRequestMessage request, [FromBody]IncomeAdjustmentCommFeeSearchManual cfModel)
        {
            return GetHttpResponse(request, () =>
            {
                var cf = _MPRPLService.UpdateCommFeeManual(cfModel);

                return request.CreateResponse<IncomeAdjustmentCommFeeSearchManual>(HttpStatusCode.OK, cf);
            });
        }

        [HttpPost]
        [Route("deletecommfeemanual")]
        public HttpResponseMessage Deleterevenue(HttpRequestMessage request, [FromBody]int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IncomeAdjustmentCommFeeSearchManual cf = _MPRPLService.GetCommFeeManual(Id);

                if (cf != null)
                {
                    _MPRPLService.DeleteCommFeeManual(Id);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No commfee manual found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getcommfeemanual/{Id}")]
        public HttpResponseMessage Getrevenue(HttpRequestMessage request, int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IncomeAdjustmentCommFeeSearchManual cf = _MPRPLService.GetCommFeeManual(Id);

                // notice no need to create a seperate model object since Revenue entity will do just fine
                response = request.CreateResponse<IncomeAdjustmentCommFeeSearchManual>(HttpStatusCode.OK, cf);

                return response;
            });
        }


        [HttpGet]
        [Route("getcommfeemanual")]
        public HttpResponseMessage GetBalanceSheets(HttpRequestMessage request)
        {
            HttpResponseMessage response = null;

            IncomeAdjustmentCommFeeSearchManual[] cf = _MPRPLService.GetCommFeeManuals();

            // notice no need to create a seperate model object since Revenue entity will do just fine
            response = request.CreateResponse<IncomeAdjustmentCommFeeSearchManual[]>(HttpStatusCode.OK, cf);

            return response;
        }

        [HttpGet]
        [Route("availablecommfeemanual/{year}/{period}/{search}")]
        public HttpResponseMessage GetAvailablerevenue(HttpRequestMessage request, int year, int period, string search)
        {
            HttpResponseMessage response = null;

            IncomeAdjustmentCommFeeSearchManual[] cf = _MPRPLService.GetCommFeesByYearPeriod(year, period, search);

            // notice no need to create a seperate model object since Revenue entity will do just fine
            response = request.CreateResponse<IncomeAdjustmentCommFeeSearchManual[]>(HttpStatusCode.OK, cf);

            return response;
        }

    }
}
