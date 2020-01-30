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
    [RoutePrefix("api/opextimeallocationmpr")]
    [UsesDisposableService]
    public class OpexTimeAllocationMPRApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public OpexTimeAllocationMPRApiController(IMPROPEXService mprOpexService)
        {
            _MPROPEXService = mprOpexService;
        }

        IMPROPEXService _MPROPEXService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPROPEXService);
        }

        [HttpPost]
        [Route("updateopextimeallocationmpr")]
        public HttpResponseMessage UpdateOpexTimeAllocationMPR(HttpRequestMessage request, [FromBody]OpexTimeAllocationMPR opexModel)
        {
            return GetHttpResponse(request, () =>
            {
                var opex = _MPROPEXService.UpdateOpexTimeAllocationMPR(opexModel);

                return request.CreateResponse<OpexTimeAllocationMPR>(HttpStatusCode.OK, opex);
            });
        }

        [HttpPost]
        [Route("deleteopextimeallocationmpr")]
        public HttpResponseMessage DeleteOpexTimeAllocationMPR(HttpRequestMessage request, [FromBody]int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                OpexTimeAllocationMPR opex = _MPROPEXService.GetOpexTimeAllocationMPR(ID);

                if (opex != null)
                {
                    //_MPROPEXService.DeleteActivityBase(ID);
                    _MPROPEXService.DeleteOpexTimeAllocationMPR(ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No OpexTimeAllocationMPR found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getopextimeallocationmpr/{ID}")]
        public HttpResponseMessage GetOpexTimeAllocationMPR(HttpRequestMessage request, int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                OpexTimeAllocationMPR opex = _MPROPEXService.GetOpexTimeAllocationMPR(ID);

                // notice no need to create a seperate model object since ActivityBase entity will do just fine
                response = request.CreateResponse<OpexTimeAllocationMPR>(HttpStatusCode.OK, opex);

                return response;
            });
        }

        [HttpGet]
        [Route("availableopextimeallocationmpr")]
        public HttpResponseMessage GetAvailableOpexTimeAllocationMPR(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                OpexTimeAllocationMPR[] opex = _MPROPEXService.GetAllOpexTimeAllocationMPR();

                return request.CreateResponse<OpexTimeAllocationMPR[]>(HttpStatusCode.OK, opex);
            });
        }
    }
}
