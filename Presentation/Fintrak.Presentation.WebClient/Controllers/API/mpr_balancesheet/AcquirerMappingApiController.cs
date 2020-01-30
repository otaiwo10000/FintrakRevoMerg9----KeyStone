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
    [RoutePrefix("api/acquirermapping")]
    [UsesDisposableService]
    public class AcquirerMappingApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public AcquirerMappingApiController(IMPRBSService mprBSService)
        {
            _MPRBSService = mprBSService;
        }

        IMPRBSService _MPRBSService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRBSService);
        }

        [HttpPost]
        [Route("updateacquirermapping")]
        public HttpResponseMessage UpdateAcquirerMapping(HttpRequestMessage request, [FromBody]AcquirerMapping AcquirerMappingModel)
        {
            return GetHttpResponse(request, () =>
            {
                var AcquirerMapping = _MPRBSService.UpdateAcquirerMapping(AcquirerMappingModel);

                return request.CreateResponse<AcquirerMapping>(HttpStatusCode.OK, AcquirerMapping);
            });
        }

        [HttpPost]
        [Route("deleteacquirermapping")]
        public HttpResponseMessage DeleteAcquirerMapping(HttpRequestMessage request, [FromBody]int mpr_Acquirer_Mapping_Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                AcquirerMapping AcquirerMapping = _MPRBSService.GetAcquirerMapping(mpr_Acquirer_Mapping_Id);

                if (AcquirerMapping != null)
                {
                    _MPRBSService.DeleteAcquirerMapping(mpr_Acquirer_Mapping_Id);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Acquirere mapping found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getacquirermapping/{mpr_acquirer_mapping_Id}")]
        public HttpResponseMessage GetAcquirerMapping(HttpRequestMessage request, int mpr_Acquirer_Mapping_Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                AcquirerMapping AcquirerMapping = _MPRBSService.GetAcquirerMapping(mpr_Acquirer_Mapping_Id);

                // notice no need to create a seperate model object since AcquirerMapping entity will do just fine
                response = request.CreateResponse<AcquirerMapping>(HttpStatusCode.OK, AcquirerMapping);

                return response;
            });
        }

        [HttpGet]
        [Route("availableacquirermapping")]
        public HttpResponseMessage GetAvailableAcquirerMapping(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                AcquirerMapping[] AcquirerMapping = _MPRBSService.GetAllAcquirerMappings();


                return request.CreateResponse<AcquirerMapping[]>(HttpStatusCode.OK, AcquirerMapping);
            });
        }
    }
}
