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
    [RoutePrefix("api/finstatmapping")]
    [UsesDisposableService]
    public class FinstatMappingApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public FinstatMappingApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }

        [HttpPost]
        [Route("updatefinstatmapping")]
        public HttpResponseMessage UpdateFinstatMapping(HttpRequestMessage request, [FromBody]FinstatMapping finstatMappingModel)
        {
            return GetHttpResponse(request, () =>
            {
                var finstatMapping = _MPRCoreService.UpdateFinstatMapping(finstatMappingModel);

                return request.CreateResponse<FinstatMapping>(HttpStatusCode.OK, finstatMapping);
            });
        }


        [HttpPost]
        [Route("deletefinstatmapping")]
        public HttpResponseMessage DeleteFinstatMapping(HttpRequestMessage request, [FromBody]int finstatMappingId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                FinstatMapping finstatMapping = _MPRCoreService.GetFinstatMapping(finstatMappingId);

                if (finstatMapping != null)
                {
                    _MPRCoreService.DeleteAbcRatio(finstatMappingId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No FinstatMapping found under that ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getfinstatmapping/{finstatMappingId}")]
        public HttpResponseMessage GetFinstatMapping(HttpRequestMessage request, int finstatMappingId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                FinstatMapping finstatMapping = _MPRCoreService.GetFinstatMapping(finstatMappingId);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<FinstatMapping>(HttpStatusCode.OK, finstatMapping);

                return response;
            });
        }


        [HttpGet]
        [Route("availablefinstatmapping")]
        public HttpResponseMessage GetAvailableFinstatMapping(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                FinstatMapping[] finstatMapping = _MPRCoreService.GetAllFinstatMapping();

                return request.CreateResponse<FinstatMapping[]>(HttpStatusCode.OK, finstatMapping);
            });
        }
    }
}
