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
    [RoutePrefix("api/accountcustomermodel")]
    [UsesDisposableService]
    public class AccountCustomerModelApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public AccountCustomerModelApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }

       
        [HttpGet]
        [Route("getaccountnumbercustomername/{SearchValue}")]
        public HttpResponseMessage AccountNumberCustomerName(HttpRequestMessage request, string SearchValue)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage res = null;

                OtherInfo obj = new OtherInfo();
                List<AccountCustomerModel> acctcus = obj.GetAccountCustomerInfo(SearchValue);

                res = request.CreateResponse(HttpStatusCode.OK, acctcus);

                return res;
            });
        }
    }
}
