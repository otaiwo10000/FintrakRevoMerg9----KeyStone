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
    [RoutePrefix("api/loanperiodicschedule")]
    [UsesDisposableService]
    public class LoanPeriodicScheduleApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public LoanPeriodicScheduleApiController(IIFRSDataViewService ifrsDataService)
        {
            _IFRSDataService = ifrsDataService;
        }

        IIFRSDataViewService _IFRSDataService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRSDataService);
        }



        [HttpGet]
        [Route("getloanperiodicschedule/{refno}")]
        public HttpResponseMessage GetLoanPeriodicSchedule(HttpRequestMessage request, string refno)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                LoanPeriodicSchedule[] loanperiodicschedule = _IFRSDataService.GetLoanPeriodicSchedulebyRefNo(refno);

                // notice no need to create a seperate model object since LoanPeriodicSchedule entity will do just fine
                response = request.CreateResponse<LoanPeriodicSchedule[]>(HttpStatusCode.OK, loanperiodicschedule);

                return response;
            });
        }


        [HttpGet]
        [Route("getloanperiodicscheduledistinctrefno")]
        public HttpResponseMessage GetLoanPeriodicScheduleResultDistinctRefNo(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                LoanPeriodicSchedule[] loanperiodicschedule = _IFRSDataService.GetLoanPeriodicScheduleDistinctRefNo();

                return request.CreateResponse<LoanPeriodicSchedule[]>(HttpStatusCode.OK, loanperiodicschedule);
            });
        }

        [HttpGet]
        [Route("availableloanperiodicschedule")]
        public HttpResponseMessage GetAvailableLoanPeriodicSchedules(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                LoanPeriodicSchedule[] loanperiodicschedule = _IFRSDataService.GetAllLoanPeriodicSchedules();

                return request.CreateResponse<LoanPeriodicSchedule[]>(HttpStatusCode.OK, loanperiodicschedule);
            });
        }


        //[HttpGet]
        //[Route("getrefnos")]
        //public HttpResponseMessage Getgetrefnos(HttpRequestMessage request)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        LoanPeriodicSchedule[] loanPeriodicSchedule = _IFRSDataService.GetRefNoLoanPeriodicSchedule();

        //        return request.CreateResponse<LoanPeriodicSchedule[]>(HttpStatusCode.OK, loanPeriodicSchedule);
        //    });
        //}
        [HttpPost]
        [Route("deleteloanperiodicschedule/{refno}")]
        public HttpResponseMessage DeleteLoanPeriodic(HttpRequestMessage request, string refNo)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;


                 _IFRSDataService.DeleteLoanPeriodicSchedulebyRefNo(refNo);

                 response = request.CreateResponse(HttpStatusCode.OK);

                 return response;
            });
        }
        [HttpGet]
        [Route("getrefnos")]
        public HttpResponseMessage GetDistinctRefNos(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string[] refNo = _IFRSDataService.GetLoanPeriodicRefNo();
                List<ReferenceNoModel> refNos = new List<ReferenceNoModel>();
                foreach (var c in refNo)
                    refNos.Add(new ReferenceNoModel()
                    {
                        RefNo = c

                    });

                return request.CreateResponse<ReferenceNoModel[]>(HttpStatusCode.OK, refNos.ToArray());

            });
        }

    }
}
