using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Presentation.WebClient.Models;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.IFRS.Contracts;
using Fintrak.Client.IFRS.Entities;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/loanschedule")]
    [UsesDisposableService]
    public class LoanScheduleApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public LoanScheduleApiController(IIFRSDataViewService ifrsDataService)
        {
            _IFRSDataService = ifrsDataService;
        }

        IIFRSDataViewService _IFRSDataService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRSDataService);
        }



        [HttpGet]
        [Route("getloanschedule/{refno}")]
        public HttpResponseMessage GetLoanSchedule(HttpRequestMessage request, string refno)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                LoanSchedule[] loanschedule = _IFRSDataService.GetLoanSchedulebyRefNo(refno);

                // notice no need to create a seperate model object since LoanSchedule entity will do just fine
                response = request.CreateResponse<LoanSchedule[]>(HttpStatusCode.OK, loanschedule);

                return response;
            });
        }


        [HttpGet]
        [Route("getloanscheduledistinctrefno")]
        public HttpResponseMessage GetLoanScheduleResultDistinctRefNo(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                LoanSchedule[] loanschedule = _IFRSDataService.GetLoanScheduleDistinctRefNo();

                return request.CreateResponse<LoanSchedule[]>(HttpStatusCode.OK, loanschedule);
            });
        }

        [HttpGet]
        [Route("availableloanschedule")]
        public HttpResponseMessage GetAvailableLoanSchedules(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                LoanSchedule[] loanschedule = _IFRSDataService.GetAllLoanSchedules();

                return request.CreateResponse<LoanSchedule[]>(HttpStatusCode.OK, loanschedule);
            });
        }

        [HttpGet]
        [Route("getrefnos")]
        public HttpResponseMessage GetDistinctRefNos(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {   
                IEnumerable<string> refNo = _IFRSDataService.GetLoanPeriodicRefNo();

                return request.CreateResponse<IEnumerable<string>>(HttpStatusCode.OK, refNo);
               
            });
        }
    }
}
