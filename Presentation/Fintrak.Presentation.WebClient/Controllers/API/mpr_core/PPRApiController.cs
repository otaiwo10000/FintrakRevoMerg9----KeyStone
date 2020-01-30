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
    [RoutePrefix("api/ppr")]
    [UsesDisposableService]
    public class PPRApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public PPRApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }

        [HttpPost]
        [Route("updateppr")]
        public HttpResponseMessage UpdatePPR(HttpRequestMessage request, [FromBody]PPR pprModel)
        {
            return GetHttpResponse(request, () =>
            {
                var ppr = _MPRCoreService.UpdatePPR(pprModel);

                return request.CreateResponse<PPR>(HttpStatusCode.OK, ppr);
            });
        }


        [HttpPost]
        [Route("deleteppr")]
        public HttpResponseMessage DeletePPR(HttpRequestMessage request, [FromBody]int PPRId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                PPR ppr = _MPRCoreService.GetPPR(PPRId);

                if (ppr != null)
                {
                    _MPRCoreService.DeletePPR(PPRId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No PPR found under that ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getppr/{PPRId}")]
        public HttpResponseMessage GetPPR(HttpRequestMessage request, int PPRId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                PPR ppr = _MPRCoreService.GetPPR(PPRId);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<PPR>(HttpStatusCode.OK, ppr);

                return response;
            });
        }


        [HttpGet]
        [Route("availableppr")]
        public HttpResponseMessage GetAvailablePPR(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                PPR[] ppr = _MPRCoreService.GetAllPPR();

                return request.CreateResponse<PPR[]>(HttpStatusCode.OK, ppr);
            });
        }
    }
}
