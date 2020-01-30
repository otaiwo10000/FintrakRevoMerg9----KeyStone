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

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/slaryschedule")]
    [UsesDisposableService]
    public class SlaryScheduleApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public SlaryScheduleApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }

        [HttpPost]
        [Route("updateslaryschedule")]
        public HttpResponseMessage UpdateSlarySchedule(HttpRequestMessage request, [FromBody]SlarySchedule slaryScheduleModel)
        {
            return GetHttpResponse(request, () =>
            {
                var slarySchedule = _MPRCoreService.UpdateSlarySchedule(slaryScheduleModel);

                return request.CreateResponse<SlarySchedule>(HttpStatusCode.OK, slarySchedule);
            });
        }

        [HttpPost]
        [Route("deleteslaryschedule")]
        public HttpResponseMessage DeleteSlarySchedule(HttpRequestMessage request, [FromBody]int slaryScheduleId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                SlarySchedule slarySchedule = _MPRCoreService.GetSlarySchedule(slaryScheduleId);

                if (slarySchedule != null)
                {
                    _MPRCoreService.DeleteSlarySchedule(slaryScheduleId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No slarySchedule found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getslaryschedule/{slaryscheduleId}")]
        public HttpResponseMessage GetSlarySchedule(HttpRequestMessage request, int slaryscheduleId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                SlarySchedule slarySchedule = _MPRCoreService.GetSlarySchedule(slaryscheduleId);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<SlarySchedule>(HttpStatusCode.OK, slarySchedule);

                return response;
            });
        }


        [HttpGet]
        [Route("availableslaryschedule")]
        public HttpResponseMessage GetAvailableAbcRatio(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                SlarySchedule[] slarySchedule = _MPRCoreService.GetAllSlarySchedule();

                return request.CreateResponse<SlarySchedule[]>(HttpStatusCode.OK, slarySchedule);
            });
        }
    }
}
