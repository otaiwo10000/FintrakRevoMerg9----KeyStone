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
    [RoutePrefix("api/opexsbubasecost")]
    [UsesDisposableService]
    public class OpexSBUBaseCostApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public OpexSBUBaseCostApiController(IMPROPEXService mprOpexService)
        {
            _MPROPEXService = mprOpexService;
        }

        IMPROPEXService _MPROPEXService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPROPEXService);
        }

        [HttpPost]
        [Route("updateopexsbubasecost")]
        public HttpResponseMessage UpdateOpexSBUBaseCost(HttpRequestMessage request, [FromBody]OpexSBUBaseCost opexModel)
        {
            return GetHttpResponse(request, () =>
            {
                var opex = _MPROPEXService.UpdateOpexSBUBaseCost(opexModel);

                return request.CreateResponse<OpexSBUBaseCost>(HttpStatusCode.OK, opex);
            });
        }

        [HttpPost]
        [Route("deleteopexsbubasecost")]
        public HttpResponseMessage DeleteOpexSBUBaseCost(HttpRequestMessage request, [FromBody]int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                OpexSBUBaseCost opex = _MPROPEXService.GetOpexSBUBaseCost(ID);

                if (opex != null)
                {
                    _MPROPEXService.DeleteOpexSBUBaseCost(ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No OpexSBUBaseCost found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getopexsbubasecost/{ID}")]
        public HttpResponseMessage GetOpexSBUBaseCost(HttpRequestMessage request, int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                OpexSBUBaseCost opex = _MPROPEXService.GetOpexSBUBaseCost(ID);

                // notice no need to create a seperate model object since ActivityBase entity will do just fine
                response = request.CreateResponse<OpexSBUBaseCost>(HttpStatusCode.OK, opex);

                return response;
            });
        }

        [HttpGet]
        [Route("availableopexsbubasecost")]
        public HttpResponseMessage GetAvailableOpexSBUBaseCost(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                OpexSBUBaseCost[] opex = _MPROPEXService.GetAllOpexSBUBaseCost();

                return request.CreateResponse<OpexSBUBaseCost[]>(HttpStatusCode.OK, opex);
            });
        }
    }
}
