using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.IFRS.Contracts;
using Fintrak.Client.IFRS.Entities;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/bondperiodicschedule")]
    [UsesDisposableService]
    public class BondPeriodicScheduleApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public BondPeriodicScheduleApiController(IIFRSDataViewService ifrsDataService)
        {
            _IFRSDataService = ifrsDataService;
        }

        IIFRSDataViewService _IFRSDataService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRSDataService);
        }



        [HttpGet]
        [Route("getbondperiodicschedule/{refno}")]
        public HttpResponseMessage GetBondPeriodicSchedule(HttpRequestMessage request, string refno)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                BondPeriodicSchedule[] bondperiodicschedule = _IFRSDataService.GetBondPeriodicSchedulebyRefNo(refno);

                // notice no need to create a seperate model object since BondPeriodicSchedule entity will do just fine
                response = request.CreateResponse<BondPeriodicSchedule[]>(HttpStatusCode.OK, bondperiodicschedule);

                return response;
            });
        }


        [HttpGet]
        [Route("getbondperiodicscheduledistinctrefno")]
        public HttpResponseMessage GetBondPeriodicScheduleResultDistinctRefNo(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                BondPeriodicSchedule[] bondperiodicschedule = _IFRSDataService.GetBondPeriodicScheduleDistinctRefNo();

                return request.CreateResponse<BondPeriodicSchedule[]>(HttpStatusCode.OK, bondperiodicschedule);
            });
        }

        [HttpGet]
        [Route("availablebondperiodicschedule")]
        public HttpResponseMessage GetAvailableBondPeriodicSchedules(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                BondPeriodicSchedule[] bondperiodicschedule = _IFRSDataService.GetAllBondPeriodicSchedules();

                return request.CreateResponse<BondPeriodicSchedule[]>(HttpStatusCode.OK, bondperiodicschedule);
            });
        }
    }
}
