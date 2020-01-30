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
    [RoutePrefix("api/acquirersharing")]
    [UsesDisposableService]
    public class AcquirerSharingApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public AcquirerSharingApiController(IMPRBSService mprBSService)
        {
            _MPRBSService = mprBSService;
        }

        IMPRBSService _MPRBSService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRBSService);
        }

        [HttpPost]
        [Route("updateacquirersharing")]
        public HttpResponseMessage UpdateAcquirerSharing(HttpRequestMessage request, [FromBody]AcquirerSharing AcquirerSharingModel)
        {
            return GetHttpResponse(request, () =>
            {
                var AcquirerSharing = _MPRBSService.UpdateAcquirerSharing(AcquirerSharingModel);

                return request.CreateResponse<AcquirerSharing>(HttpStatusCode.OK, AcquirerSharing);
            });
        }

        [HttpPost]
        [Route("deleteacquirersharing")]
        public HttpResponseMessage DeleteAcquirerSharing(HttpRequestMessage request, [FromBody]int mpr_Acquirer_Sharing_Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                AcquirerSharing AcquirerSharing = _MPRBSService.GetAcquirerSharing(mpr_Acquirer_Sharing_Id);

                if (AcquirerSharing != null)
                {
                    _MPRBSService.DeleteAcquirerSharing(mpr_Acquirer_Sharing_Id);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Acquirere mapping found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getacquirersharing/{mpr_acquirer_sharing_id}")]
        public HttpResponseMessage GetAcquirerSharing(HttpRequestMessage request, int mpr_Acquirer_Sharing_Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                AcquirerSharing AcquirerSharing = _MPRBSService.GetAcquirerSharing(mpr_Acquirer_Sharing_Id);

                // notice no need to create a seperate model object since AcquirerSharing entity will do just fine
                response = request.CreateResponse<AcquirerSharing>(HttpStatusCode.OK, AcquirerSharing);

                return response;
            });
        }

        [HttpGet]
        [Route("availableacquirersharing")]
        public HttpResponseMessage GetAvailableAcquirerSharing(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                AcquirerSharing[] AcquirerSharing = _MPRBSService.GetAllAcquirerSharings();


                return request.CreateResponse<AcquirerSharing[]>(HttpStatusCode.OK, AcquirerSharing);
            });
        }
    }
}
