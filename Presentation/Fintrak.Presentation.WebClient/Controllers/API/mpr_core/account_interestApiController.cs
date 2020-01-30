
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
    [RoutePrefix("api/account_interest")]
    [UsesDisposableService]
    public class account_interestApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public account_interestApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);
        }

        [HttpGet]
        [Route("accountinterestData")]
        public HttpResponseMessage GetAccountInterestData(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IEnumerable<account_interest> ai = _MPRCoreService.GellALlAccountInterest();

                return request.CreateResponse<IEnumerable<account_interest>>(HttpStatusCode.OK, ai);
            });
        }
    }
}
