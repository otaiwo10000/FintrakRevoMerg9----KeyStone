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
using Fintrak.Presentation.WebClient.Additionalmethods;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/downloadbasefintrakfinalmanual")]
    [UsesDisposableService]
    public class DownloadBaseFintrakFinalManualApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public DownloadBaseFintrakFinalManualApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }

        [HttpPost]
        [Route("updateddbaseffm")]
        public HttpResponseMessage UpdateDDBFFM(HttpRequestMessage request, [FromBody]DownloadBaseFintrakFinalManual ddbffmModel)
        {
            return GetHttpResponse(request, () =>
            {
                var ddbffm = _MPRCoreService.UpdateDDBaseFFM(ddbffmModel);

                return request.CreateResponse<DownloadBaseFintrakFinalManual>(HttpStatusCode.OK, ddbffm);
            });
        }


        [HttpPost]
        [Route("deleteddbaseffm")]
        public HttpResponseMessage DeleteDDBFFM(HttpRequestMessage request, [FromBody]int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                DownloadBaseFintrakFinalManual ddbffm = _MPRCoreService.GetDDBaseFFM(ID);

                if (ddbffm != null)
                {
                    _MPRCoreService.DeleteDDBaseFFM(ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No download base found under that ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getddbaseffm/{ID}")]
        public HttpResponseMessage GetDDBFFM(HttpRequestMessage request, int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                DownloadBaseFintrakFinalManual ddbffm = _MPRCoreService.GetDDBaseFFM(ID);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<DownloadBaseFintrakFinalManual>(HttpStatusCode.OK, ddbffm);

                return response;
            });
        }


        [HttpGet]
        [Route("availableddbaseffm")]
        public HttpResponseMessage GetAvailableDDBFFM(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                DownloadBaseFintrakFinalManual[] ddbffm = _MPRCoreService.GetAllDDBaseFFM();

                return request.CreateResponse<DownloadBaseFintrakFinalManual[]>(HttpStatusCode.OK, ddbffm);
            });
        }

        [HttpGet]
        [Route("availabledownloadbasefintrakfinalmanualforlatestyearmonth")]
        public HttpResponseMessage GetAvailableDDBaseForLatestYearMonth(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                MPRDownloadBaseFintrakFinalManualMtd obj = new MPRDownloadBaseFintrakFinalManualMtd();
                List<MPRDownloadBaseFintrakFinalManualModel> objList = new List<MPRDownloadBaseFintrakFinalManualModel>();

                //IncomeOtherBreakdown[] iob = _MPRCoreService.GetAllIncomeOtherBreakdown();
                objList = obj.GetMPRDownloadBaseFintrakFinalManual().ToList();

                return request.CreateResponse(HttpStatusCode.OK, objList);
            });
        }

        [HttpGet]
        [Route("availabledownloadbasefintrakfinalmanualusingparams/{year}/{period}/{search}")]
        public HttpResponseMessage GetAvailableIncomeBreakDownUsingYearMonth(HttpRequestMessage request, int year, int period, string search)
        {
            return GetHttpResponse(request, () =>
            {
                MPRDownloadBaseFintrakFinalManualMtd obj = new MPRDownloadBaseFintrakFinalManualMtd();
                List<MPRDownloadBaseFintrakFinalManualModel> objList = new List<MPRDownloadBaseFintrakFinalManualModel>();

                //IncomeOtherBreakdown[] iob = _MPRCoreService.GetAllIncomeOtherBreakdown();
                objList = obj.GetDownloadBaseFintrakFinalManualUsingParams(year, period, search).ToList();

                return request.CreateResponse(HttpStatusCode.OK, objList);
            });
        }

    }
}
