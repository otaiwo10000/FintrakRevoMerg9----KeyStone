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
    [RoutePrefix("api/borrowingschedule")]
    [UsesDisposableService]
    public class BorrowingScheduleApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public BorrowingScheduleApiController(IIFRSDataViewService ifrsDataService)
        {
            _IFRSDataService = ifrsDataService;
        }

        IIFRSDataViewService _IFRSDataService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRSDataService);
        }



        [HttpGet]
        [Route("getborrowingschedule/{refno}")]
        public HttpResponseMessage GetBorrowingSchedule(HttpRequestMessage request, string refno)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                BorrowingSchedule[] borrowingschedule = _IFRSDataService.GetBorrowingSchedulebyRefNo(refno);

                // notice no need to create a seperate model object since BorrowingSchedule entity will do just fine
                response = request.CreateResponse<BorrowingSchedule[]>(HttpStatusCode.OK, borrowingschedule);

                return response;
            });
        }


        [HttpGet]
        [Route("getborrowingscheduledistinctrefno")]
        public HttpResponseMessage GetBorrowingScheduleResultDistinctRefNo(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                BorrowingSchedule[] borrowingschedule = _IFRSDataService.GetBorrowingScheduleDistinctRefNo();

                return request.CreateResponse<BorrowingSchedule[]>(HttpStatusCode.OK, borrowingschedule);
            });
        }

        [HttpGet]
        [Route("availableborrowingschedule")]
        public HttpResponseMessage GetAvailableBorrowingSchedules(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                BorrowingSchedule[] borrowingschedule = _IFRSDataService.GetAllBorrowingSchedules();

                return request.CreateResponse<BorrowingSchedule[]>(HttpStatusCode.OK, borrowingschedule);
            });
        }

        [HttpGet]
        [Route("getrefnos")]
        public HttpResponseMessage GetDistinctRefNos(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {   
                IEnumerable<string> refNo = _IFRSDataService.GetBorrowingPeriodicRefNo();

                return request.CreateResponse<IEnumerable<string>>(HttpStatusCode.OK, refNo);
               
            });
        }
    }
}
