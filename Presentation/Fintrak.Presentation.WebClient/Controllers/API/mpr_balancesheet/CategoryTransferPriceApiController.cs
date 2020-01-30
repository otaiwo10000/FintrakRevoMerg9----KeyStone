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
    [RoutePrefix("api/categorytransferprice")]
    [UsesDisposableService]
    public class CategoryTransferPriceApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public CategoryTransferPriceApiController(IMPRBSService mprBSService)
        {
            _MPRBSService = mprBSService;
        }

        IMPRBSService _MPRBSService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRBSService);
        }

        [HttpPost]
        [Route("updatecategorytransferprice")]
        public HttpResponseMessage UpdateCategoryTransferPrice(HttpRequestMessage request, [FromBody]CategoryTransferPrice categoryTransferPriceModel)
        {
            return GetHttpResponse(request, () =>
            {
                var categoryTransferPrice = _MPRBSService.UpdateCategoryTransferPrice(categoryTransferPriceModel);

                return request.CreateResponse<CategoryTransferPrice>(HttpStatusCode.OK, categoryTransferPrice);
            });
        }

        [HttpPost]
        [Route("deletecategorytransferprice")]
        public HttpResponseMessage DeleteCategoryTransferPrice(HttpRequestMessage request, [FromBody]int CategoryTransferPriceId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                CategoryTransferPrice categoryTransferPrice = _MPRBSService.GetCategoryTransferPrice(CategoryTransferPriceId);

                if (categoryTransferPrice != null)
                {
                    _MPRBSService.DeleteCategoryTransferPrice(CategoryTransferPriceId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Opex Business Rule found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getcategorytransferprice/{CategoryTransferPriceId}")]
        public HttpResponseMessage GetCategoryTransferPrice(HttpRequestMessage request, int CategoryTransferPriceId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                CategoryTransferPrice categoryTransferPrice = _MPRBSService.GetCategoryTransferPrice(CategoryTransferPriceId);

                // notice no need to create a seperate model object since CategoryTransferPrice entity will do just fine
                response = request.CreateResponse<CategoryTransferPrice>(HttpStatusCode.OK, categoryTransferPrice);

                return response;
            });
        }

        [HttpGet]
        [Route("availablecategorytransferprice")]
        public HttpResponseMessage GetAvailableCategoryTransferPrice(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                CategoryTransferPrice[] categoryTransferPrice = _MPRBSService.GetAllCategoryTransferPrices();


                return request.CreateResponse<CategoryTransferPrice[]>(HttpStatusCode.OK, categoryTransferPrice);
            });
        }

        [HttpGet]
        [Route("getcategorytransferpricebysetup")]
        public HttpResponseMessage CategoryTransferPricebySetUp(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                CategoryTransferPriceData[] categoryTransferPrice = _MPRBSService.GetCategoryTransferPriceUsingSetUp();


                return request.CreateResponse<CategoryTransferPriceData[]>(HttpStatusCode.OK, categoryTransferPrice);
            });
        }

        [HttpGet]
        [Route("getcategorytransferpricebysearch/{search}")]
        public HttpResponseMessage CategoryTransferPricebysearch(HttpRequestMessage request, string search)
        {
            return GetHttpResponse(request, () =>
            {
                CategoryTransferPriceData[] categoryTransferPrice = _MPRBSService.GetCategoryTransferPriceUsingsearch(search);


                return request.CreateResponse<CategoryTransferPriceData[]>(HttpStatusCode.OK, categoryTransferPrice);
            });
        }
    }
}
