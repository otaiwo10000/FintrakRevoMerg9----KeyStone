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
    [RoutePrefix("api/riskadjustedcharge")]
    [UsesDisposableService]
    public class RiskAdjustedChargeApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public RiskAdjustedChargeApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }

        [HttpPost]
        [Route("updaterac")]
        public HttpResponseMessage UpdateRiskAdjustedCharge(HttpRequestMessage request, [FromBody]RiskAdjustedCharge racModel)
        {
            return GetHttpResponse(request, () =>
            {
                var rac = _MPRCoreService.UpdateRiskAdjustedCharge(racModel);

                return request.CreateResponse<RiskAdjustedCharge>(HttpStatusCode.OK, rac);
            });
        }


        [HttpPost]
        [Route("deleterac")]
        public HttpResponseMessage DeleteRiskAdjustedCharge(HttpRequestMessage request, [FromBody]int RiskAdjustedChargeId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                RiskAdjustedCharge rac = _MPRCoreService.GetRiskAdjustedCharge(RiskAdjustedChargeId);

                if (rac != null)
                {
                    _MPRCoreService.DeleteRiskAdjustedCharge(RiskAdjustedChargeId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Risk Adjusted Charge found under that ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getrac/{RiskAdjustedChargeId}")]
        public HttpResponseMessage GetRiskAdjustedCharge(HttpRequestMessage request, int RiskAdjustedChargeId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                RiskAdjustedCharge rac = _MPRCoreService.GetRiskAdjustedCharge(RiskAdjustedChargeId);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<RiskAdjustedCharge>(HttpStatusCode.OK, rac);

                return response;
            });
        }


        [HttpGet]
        [Route("availablerac")]
        public HttpResponseMessage GetAvailableRiskAdjustedCharge(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                RiskAdjustedCharge[] rac = _MPRCoreService.GetAllRiskAdjustedCharge();

                return request.CreateResponse<RiskAdjustedCharge[]>(HttpStatusCode.OK, rac);
            });
        }
    }
}
