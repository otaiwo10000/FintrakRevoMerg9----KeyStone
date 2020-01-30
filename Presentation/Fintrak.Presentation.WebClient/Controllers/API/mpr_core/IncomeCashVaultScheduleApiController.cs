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
    [RoutePrefix("api/incomecashvaultschedule")]
    [UsesDisposableService]
    public class IncomeCashVaultScheduleApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomeCashVaultScheduleApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }

        [HttpPost]
        [Route("updateincomecashvaultschedule")]
        public HttpResponseMessage UpdateIncomeCashVaultSchedule(HttpRequestMessage request, [FromBody]IncomeCashVaultSchedule icsModel)
        {
            return GetHttpResponse(request, () =>
            {
                var ics = _MPRCoreService.UpdateIncomeCashVaultSchedule(icsModel);

                return request.CreateResponse<IncomeCashVaultSchedule>(HttpStatusCode.OK, ics);
            });
        }


        [HttpPost]
        [Route("deleteincomecashvaultschedule")]
        public HttpResponseMessage DeleteIncomeCashVaultSchedule(HttpRequestMessage request, [FromBody]int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IncomeCashVaultSchedule ics = _MPRCoreService.GetIncomeCashVaultSchedule(ID);

                if (ics != null)
                {
                    _MPRCoreService.DeleteIncomeCashVaultSchedule(ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Income cash vault schedule found under that ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getincomecashvaultschedule/{ID}")]
        public HttpResponseMessage GetIncomeCashVaultSchedule(HttpRequestMessage request, int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IncomeCashVaultSchedule ics = _MPRCoreService.GetIncomeCashVaultSchedule(ID);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<IncomeCashVaultSchedule>(HttpStatusCode.OK, ics);

                return response;
            });
        }


        [HttpGet]
        [Route("availableincomecashvaultschedule")]
        public HttpResponseMessage GetAllIncomeCashVaultScheduleL(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeCashVaultSchedule[] ics = _MPRCoreService.GetAllIncomeCashVaultScheduleL();

                return request.CreateResponse<IncomeCashVaultSchedule[]>(HttpStatusCode.OK, ics);
            });
        }
    }
}
