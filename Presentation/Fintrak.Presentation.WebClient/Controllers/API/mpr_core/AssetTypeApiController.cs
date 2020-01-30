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
    [RoutePrefix("api/assettype")]
    [UsesDisposableService]
    public class AssetTypeApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public AssetTypeApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }

        [HttpPost]
        [Route("updateassettype")]
        public HttpResponseMessage UpdateRatios(HttpRequestMessage request, [FromBody]Ratios ratiosModel)
        {
            return GetHttpResponse(request, () =>
            {
                var ratios = _MPRCoreService.UpdateRatios(ratiosModel);

                return request.CreateResponse<Ratios>(HttpStatusCode.OK, ratios);
            });
        }


        [HttpPost]
        [Route("deleteassettype")]
        public HttpResponseMessage DeleteRatios(HttpRequestMessage request, [FromBody]int ratiosId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                Ratios ratios = _MPRCoreService.GetRatios(ratiosId);

                if (ratios != null)
                {
                    _MPRCoreService.DeleteRatios(ratiosId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Ratios found under that ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getassettype/{AssetType_Id}")]
        public HttpResponseMessage GetAssetType(HttpRequestMessage request, int AssetType_Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                AssetType assettype = _MPRCoreService.GetAssetType(AssetType_Id);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<AssetType>(HttpStatusCode.OK, assettype);

                return response;
            });
        }


        [HttpGet]
        [Route("availableassettype")]
        public HttpResponseMessage GetAvailableAssettype(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                AssetType[] assettype = _MPRCoreService.GetAllAssetType();

                return request.CreateResponse<AssetType[]>(HttpStatusCode.OK, assettype);
            });
        }
    }
}
