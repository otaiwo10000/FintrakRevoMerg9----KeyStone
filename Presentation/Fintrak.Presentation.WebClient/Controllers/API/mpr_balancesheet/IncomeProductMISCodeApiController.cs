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
using Fintrak.Presentation.WebClient.Additionalmethods;
using Fintrak.Presentation.WebClient.Models;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/incomeproductmiscode")]
    [UsesDisposableService]
    public class IncomeProductMISCodeApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomeProductMISCodeApiController(IMPRBSService mprBSService)
        {
            _MPRBSService = mprBSService;
        }

        IMPRBSService _MPRBSService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRBSService);
        }

        [HttpGet]
        [Route("availableproductmiscodes")]
        public HttpResponseMessage GetProductMISCodes(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeProductMISCodeGetMtd obj = new IncomeProductMISCodeGetMtd();
                List<IncomeProductMISCodeGetModel> pmis = obj.GetProductMISCode().ToList();

                return request.CreateResponse(HttpStatusCode.OK, pmis);
            });
        }

    }
}
