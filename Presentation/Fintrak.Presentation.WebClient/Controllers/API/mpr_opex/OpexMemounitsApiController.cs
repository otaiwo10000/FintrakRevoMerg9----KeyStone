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
    [RoutePrefix("api/opexmemounits")]
    [UsesDisposableService]
    public class OpexMemounitsApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public OpexMemounitsApiController(IMPROPEXService mprOpexService)
        {
            _MPROPEXService = mprOpexService;
        }

        IMPROPEXService _MPROPEXService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPROPEXService);
        }

        [HttpPost]
        [Route("updateopexmemounits")]
        public HttpResponseMessage UpdateOpexMemounits(HttpRequestMessage request, [FromBody]OpexMemounits opexModel)
        {
            return GetHttpResponse(request, () =>
            {
                var opex = _MPROPEXService.UpdateOpexMemounits(opexModel);

                return request.CreateResponse<OpexMemounits>(HttpStatusCode.OK, opex);
            });
        }

        [HttpPost]
        [Route("deleteopexmemounits")]
        public HttpResponseMessage DeleteOpexMemounits(HttpRequestMessage request, [FromBody]int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                OpexMemounits opex = _MPROPEXService.GetOpexMemounits(ID);

                if (opex != null)
                {
                    _MPROPEXService.DeleteOpexMemounits(ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No OpexMemounits found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getopexmemounits/{ID}")]
        public HttpResponseMessage GetOpexMemounits(HttpRequestMessage request, int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                OpexMemounits opex = _MPROPEXService.GetOpexMemounits(ID);

                // notice no need to create a seperate model object since ActivityBase entity will do just fine
                response = request.CreateResponse<OpexMemounits>(HttpStatusCode.OK, opex);

                return response;
            });
        }

        [HttpGet]
        [Route("availableopexmemounits")]
        public HttpResponseMessage GetAvailableOpexMemounits(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                OpexMemounits[] opex = _MPROPEXService.GetAllOpexMemounits();

                return request.CreateResponse<OpexMemounits[]>(HttpStatusCode.OK, opex);
            });
        }
    }
}
