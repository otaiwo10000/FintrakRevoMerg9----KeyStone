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
    [RoutePrefix("api/ctp")]
    [UsesDisposableService]
    public class CaptionTransferPriceApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public CaptionTransferPriceApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }

        [HttpPost]
        [Route("updatectp")]
        public HttpResponseMessage UpdateCaptionTransferPrice(HttpRequestMessage request, [FromBody]caption_transfer_price ctpModel)
        {
            return GetHttpResponse(request, () =>
            {
                var ctp = _MPRCoreService.UpdateCaptionTransferPrice(ctpModel);

                return request.CreateResponse<caption_transfer_price>(HttpStatusCode.OK, ctp);
            });
        }


        [HttpPost]
        [Route("deletectp")]
        public HttpResponseMessage DeleteCaptionTransferPrice(HttpRequestMessage request, [FromBody]int caption_transfer_price_Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                caption_transfer_price ctp = _MPRCoreService.GetCaptionTransferPrice(caption_transfer_price_Id);

                if (ctp != null)
                {
                    _MPRCoreService.DeleteCaptionTransferPrice(caption_transfer_price_Id);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No caption transfer price found under that ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getctp/{caption_transfer_price_Id}")]
        public HttpResponseMessage GetRatios(HttpRequestMessage request, int caption_transfer_price_Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                caption_transfer_price ctp = _MPRCoreService.GetCaptionTransferPrice(caption_transfer_price_Id);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<caption_transfer_price>(HttpStatusCode.OK, ctp);

                return response;
            });
        }


        [HttpGet]
        [Route("availableCTP")]
        public HttpResponseMessage GetAvailableCTP(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                caption_transfer_price[] ctp = _MPRCoreService.GetAllCaptionTransferPrice();

                return request.CreateResponse<caption_transfer_price[]>(HttpStatusCode.OK, ctp);
            });
        }
    }
}
