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
    [RoutePrefix("api/corporateadjustment")]
    [UsesDisposableService]
    public class CorporateAdjustmentApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public CorporateAdjustmentApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }

        [HttpPost]
        [Route("updatecorporateadjustment")]
        public HttpResponseMessage UpdateCorporateAdjustment(HttpRequestMessage request, [FromBody]CorporateAdjustment corporateAdjustmentModel)
        {
            return GetHttpResponse(request, () =>
            {
                var corporateadjustment = _MPRCoreService.UpdateCorporateAdjustment(corporateAdjustmentModel);

                return request.CreateResponse<CorporateAdjustment>(HttpStatusCode.OK, corporateadjustment);
            });
        }


        [HttpPost]
        [Route("deletecorporateadjustment")]
        public HttpResponseMessage DeleteRatios(HttpRequestMessage request, [FromBody]int CorporateAdjustmentId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                CorporateAdjustment corporateadjustment = _MPRCoreService.GetCorporateAdjustment(CorporateAdjustmentId);

                if (corporateadjustment != null)
                {
                    _MPRCoreService.DeleteRatios(CorporateAdjustmentId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Corporate Adjustment found under that ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getcorporateadjustment/{CorporateAdjustmentId}")]
        public HttpResponseMessage GetRatios(HttpRequestMessage request, int CorporateAdjustmentId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                CorporateAdjustment corporateadjustment = _MPRCoreService.GetCorporateAdjustment(CorporateAdjustmentId);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<CorporateAdjustment>(HttpStatusCode.OK, corporateadjustment);

                return response;
            });
        }


        [HttpGet]
        [Route("availablecorporateadjustment")]
        public HttpResponseMessage GetAvailableCorporateAdjustment(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                CorporateAdjustment[] corporateadjustment = _MPRCoreService.GetAllCorporateAdjustment();
               // IEnumerable<CorporateAdjustment> corporateadjustment = _MPRCoreService.GetAllCorporateAdjustment();

               return request.CreateResponse<CorporateAdjustment[]>(HttpStatusCode.OK, corporateadjustment);
                //return request.CreateResponse<IEnumerable<CorporateAdjustment>>(HttpStatusCode.OK, corporateadjustment);
            });
        }
    }
}
