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
    [RoutePrefix("api/incomelinecapton")]
    [UsesDisposableService]
    public class IncomeLineCaptonApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomeLineCaptonApiController(IMPRIncomeService mprincomeService)
        {
            _MPRIncomeService = mprincomeService;
        }

        IMPRIncomeService _MPRIncomeService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRIncomeService);
        }



        [HttpGet]
        [Route("getallincomelinecaptons")]
        public HttpResponseMessage GetAllIncomeLineCaptons(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeLineCapton[] ilCaptons = _MPRIncomeService.GetAllIncomeLineCaptons();

                return request.CreateResponse<IncomeLineCapton[]>(HttpStatusCode.OK, ilCaptons);
            });
        }
    

    }
}
