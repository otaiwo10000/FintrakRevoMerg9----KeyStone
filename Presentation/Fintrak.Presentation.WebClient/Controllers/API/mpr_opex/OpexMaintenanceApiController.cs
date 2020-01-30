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
    [RoutePrefix("api/opexmaintenance")]
    [UsesDisposableService]
    public class OpexMaintenanceApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public OpexMaintenanceApiController(IMPROPEXService mprOpexService)
        {
            _MPROPEXService = mprOpexService;
        }

        IMPROPEXService _MPROPEXService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPROPEXService);
        }

        [HttpPost]
        [Route("updateopexmaintenance")]
        public HttpResponseMessage UpdateOpexMaintenance(HttpRequestMessage request, [FromBody]OpexMaintenance opexModel)
        {
            return GetHttpResponse(request, () =>
            {
                var opex = _MPROPEXService.UpdateOpexMaintenance(opexModel);

                return request.CreateResponse<OpexMaintenance>(HttpStatusCode.OK, opex);
            });
        }

        [HttpPost]
        [Route("deleteopexmaintenance")]
        public HttpResponseMessage DeleteOpexMaintenance(HttpRequestMessage request, [FromBody]int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                OpexMaintenance opex = _MPROPEXService.GetOpexMaintenance(ID);

                if (opex != null)
                {
                    _MPROPEXService.DeleteOpexMaintenance(ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No OpexMaintenance found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getopexmaintenance/{ID}")]
        public HttpResponseMessage GetOpexMaintenance(HttpRequestMessage request, int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                OpexMaintenance opex = _MPROPEXService.GetOpexMaintenance(ID);

                // notice no need to create a seperate model object since ActivityBase entity will do just fine
                response = request.CreateResponse<OpexMaintenance>(HttpStatusCode.OK, opex);

                return response;
            });
        }

        [HttpGet]
        [Route("availableopexmaintenance")]
        public HttpResponseMessage GetAvailableOpexMaintenance(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                OpexMaintenance[] opex = _MPROPEXService.GetAllOpexMaintenance();

                return request.CreateResponse<OpexMaintenance[]>(HttpStatusCode.OK, opex);
            });
        }
    }
}
