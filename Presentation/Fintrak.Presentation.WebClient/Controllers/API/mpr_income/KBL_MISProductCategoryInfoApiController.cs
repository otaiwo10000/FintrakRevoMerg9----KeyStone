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
    [RoutePrefix("api/misproductcategoryinfo")]
    [UsesDisposableService]
    public class KBL_MISProductCategoryInfoApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public KBL_MISProductCategoryInfoApiController(IMPRIncomeService mprincomeService)
        {
            _MPRIncomeService = mprincomeService;
        }

        IMPRIncomeService _MPRIncomeService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRIncomeService);
        }


        //[HttpPost]
        //[Route("updateincomeproducttabletreasury")]
        //public HttpResponseMessage UpdateIncomeProductsTableTreasury(HttpRequestMessage request, [FromBody]IncomeProductsTableTreasury iptTreasuryModel)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;

        //        var iptTreasury = _MPRIncomeService.UpdateIncomeProductsTableTreasury(iptTreasuryModel);

        //         response = request.CreateResponse<IncomeProductsTableTreasury>(HttpStatusCode.OK, iptTreasury);

        //        return response;
        //    });
        //}


        //[HttpPost]
        //[Route("deleteincomeproducttabletreasury")]
        //public HttpResponseMessage DeleteIncomeProductsTableTreasury(HttpRequestMessage request, [FromBody]int iptTreasuryId)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;

        //        // not that calling the WCF service here will authenticate access to the data
        //        IncomeProductsTableTreasury ipt = _MPRIncomeService.GetIncomeProductsTableTreasury(iptTreasuryId);

        //        if (ipt != null)
        //        {
        //            _MPRIncomeService.DeleteIncomeProductsTableTreasury(iptTreasuryId);

        //            response = request.CreateResponse(HttpStatusCode.OK);
        //        }
        //        else
        //            response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No IncomeProduct table treasury found under the ID.");

        //        return response;
        //    });
        //}


        //[HttpGet]
        //[Route("getincomeproducttabletreasury/{iptTreasuryId}")]
        //public HttpResponseMessage GetIncomeProductstable(HttpRequestMessage request, int iptTreasuryId)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;

        //        IncomeProductsTableTreasury ipt = _MPRIncomeService.GetIncomeProductsTableTreasury(iptTreasuryId);

        //        // notice no need to create a seperate model object since CaptionMapping entity will do just fine
        //        response = request.CreateResponse<IncomeProductsTableTreasury>(HttpStatusCode.OK, ipt);

        //        return response;
        //    });
        //}


        [HttpGet]
        [Route("getallmisproductcategoryinfo")]
        public HttpResponseMessage GetAllKBL_MISProductCategoryInfo(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                KBL_MISProductCategoryInfo[] ipt = _MPRIncomeService.GetAllKBL_MISProductCategoryInfo();

                return request.CreateResponse<KBL_MISProductCategoryInfo[]>(HttpStatusCode.OK, ipt);
            });
        }


    }
}
