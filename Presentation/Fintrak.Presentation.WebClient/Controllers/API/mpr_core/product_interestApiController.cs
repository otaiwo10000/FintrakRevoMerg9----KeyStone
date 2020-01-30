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
    [RoutePrefix("api/productInterest")]
    [UsesDisposableService]
    public class ProductInterestApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ProductInterestApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);
        }

        //[HttpGet]
        //[Route("getAllData")]
        //public HttpResponseMessage GetPI(HttpRequestMessage request)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        IEnumerable<product_interest> prodInt = _MPRCoreService.GetAllProductInterest();

        //        return request.CreateResponse<IEnumerable<product_interest>>(HttpStatusCode.OK, prodInt);
        //    });
        //}

        [HttpGet]
        [Route("getAllData")]
        public HttpResponseMessage GetAvailableProductInterest(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                product_interestData[] productinterests = _MPRCoreService.GetAllProductInterest();

                return request.CreateResponse<product_interestData[]>(HttpStatusCode.OK, productinterests);
            });
        }
    }
}
