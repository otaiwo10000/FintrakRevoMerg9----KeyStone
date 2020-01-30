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
using Fintrak.Presentation.WebClient.Models;
using Fintrak.Shared.Common.Extensions;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/producttransferprice")]
    [UsesDisposableService]
    public class ProductTransferPriceApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ProductTransferPriceApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);
        }


        [HttpPost]
        [Route("updateproducttransferprice")]
        public HttpResponseMessage UpdateProductTransferPrice(HttpRequestMessage request, [FromBody]ProductTransferPrice ptpModel)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var ptp = _MPRCoreService.Updateproducttransferprice(ptpModel);

                //var response =  request.CreateResponse<ProductTransferPrice>(HttpStatusCode.OK, scm);
                response = request.CreateResponse<ProductTransferPrice>(HttpStatusCode.OK, ptp);

                return response;
            });
        }


        [HttpPost]
        [Route("deleteproducttransferprice")]
        public HttpResponseMessage DeleteProductTransferPrice(HttpRequestMessage request, [FromBody]int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                ProductTransferPrice ptp = _MPRCoreService.Getproducttransferprice(ID);

                if (ptp != null)
                {
                    _MPRCoreService.Deleteproducttransferprice(ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No producttransferprice found under the ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getproducttransferprice/{ID}")]
        public HttpResponseMessage GetProductTransferPrice(HttpRequestMessage request, int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ProductTransferPrice ptp = _MPRCoreService.Getproducttransferprice(ID);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<ProductTransferPrice>(HttpStatusCode.OK, ptp);

                return response;
            });
        }


        [HttpGet]
        [Route("getallproducttransferprice")]
        public HttpResponseMessage GetAllProductTransferPrice(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ProductTransferPrice[] ptp = _MPRCoreService.GetAllProductTransferPrice();

                return request.CreateResponse<ProductTransferPrice[]>(HttpStatusCode.OK, ptp);
            });
        }

        [HttpGet]
        [Route("getproducttransferpriceusingsearch/{searchvalue}")]
        public HttpResponseMessage GetProductTransferPriceUsingSearch(HttpRequestMessage request, string searchvalue)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ProductTransferPriceData[] ptp = _MPRCoreService.GetProductTransferPriceUsingSearchValue(searchvalue);

                // notice no need to create a seperate model object since TeamDefinition entity will do just fine
                response = request.CreateResponse<ProductTransferPriceData[]>(HttpStatusCode.OK, ptp);

                return response;
            });
        }

    }
}
