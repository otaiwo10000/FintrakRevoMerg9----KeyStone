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
    [RoutePrefix("api/borrowingperiodicschedule")]
    [UsesDisposableService]
    public class BorrowingPeriodicScheduleApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public BorrowingPeriodicScheduleApiController(IIFRSDataViewService ifrsDataService)
        {
            _IFRSDataService = ifrsDataService;
        }

        IIFRSDataViewService _IFRSDataService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRSDataService);
        }



        [HttpGet]
        [Route("getborrowingperiodicschedule/{refno}")]
        public HttpResponseMessage GetBorrowingPeriodicSchedule(HttpRequestMessage request, string refno)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                BorrowingPeriodicSchedule[] borrowingperiodicschedule = _IFRSDataService.GetBorrowingPeriodicSchedulebyRefNo(refno);

                // notice no need to create a seperate model object since BorrowingPeriodicSchedule entity will do just fine
                response = request.CreateResponse<BorrowingPeriodicSchedule[]>(HttpStatusCode.OK, borrowingperiodicschedule);

                return response;
            });
        }


        [HttpGet]
        [Route("getborrowingperiodicscheduledistinctrefno")]
        public HttpResponseMessage GetBorrowingPeriodicScheduleResultDistinctRefNo(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                BorrowingPeriodicSchedule[] borrowingperiodicschedule = _IFRSDataService.GetBorrowingPeriodicScheduleDistinctRefNo();

                return request.CreateResponse<BorrowingPeriodicSchedule[]>(HttpStatusCode.OK, borrowingperiodicschedule);
            });
        }

        [HttpGet]
        [Route("availableborrowingperiodicschedule")]
        public HttpResponseMessage GetAvailableBorrowingPeriodicSchedules(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                BorrowingPeriodicSchedule[] borrowingperiodicschedule = _IFRSDataService.GetAllBorrowingPeriodicSchedules();

                return request.CreateResponse<BorrowingPeriodicSchedule[]>(HttpStatusCode.OK, borrowingperiodicschedule);
            });
        }

        [HttpPost]
        [Route("deleteborrowingperiodicschedule/{refno}")]
        public HttpResponseMessage DeleteBorrowingPeriodic(HttpRequestMessage request, string refNo)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;


                 _IFRSDataService.DeleteBorrowingPeriodicSchedulebyRefNo(refNo);

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
                string[] refNo = _IFRSDataService.GetBorrowingPeriodicRefNo();
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
