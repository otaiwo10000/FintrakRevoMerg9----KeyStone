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
    [RoutePrefix("api/incomeaccountmisoverride")]
    [UsesDisposableService]
    public class IncomeAccountMisOverrideApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomeAccountMisOverrideApiController(IMPRBSService mprBSService)
        {
            _MPRBSService = mprBSService;
        }

        IMPRBSService _MPRBSService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRBSService);

        }

        [HttpGet]
        [Route("getincomeaccountmisoverride")]
        public HttpResponseMessage GetIncomeAccountMisOverride(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeAccountMisOverrideMtd obj = new IncomeAccountMisOverrideMtd();
                List<IncomeAccountMisOverrideModel> dm = obj.GetIncomeAccountMisOverride().ToList();

                return request.CreateResponse(HttpStatusCode.OK, dm);

            });
        }

        [HttpGet]
        [Route("getincomeaccountmisoverrideByParam/{search}")]
        public HttpResponseMessage GetIncomeAccountMisOverride(HttpRequestMessage request, string search)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeAccountMisOverrideMtd obj = new IncomeAccountMisOverrideMtd();
                List<IncomeAccountMisOverrideModel> dm = obj.GetIncomeAccountMisOverrideUsingParams(search).ToList();

                return request.CreateResponse(HttpStatusCode.OK, dm);

            });
        }


    }
}
