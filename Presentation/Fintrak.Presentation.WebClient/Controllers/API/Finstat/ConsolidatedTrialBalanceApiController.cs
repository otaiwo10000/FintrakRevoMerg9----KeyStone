using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Client.Core.Contracts;
using Fintrak.Client.Core.Entities;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.IFRS.Entities;
using Fintrak.Client.IFRS.Contracts;
using Fintrak.Presentation.WebClient.Models;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/consolidatedtrialbalance")]
    [UsesDisposableService]
    public class ConsolidatedTrialBalanceApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ConsolidatedTrialBalanceApiController(IFinstatService finstatService)
        {
            _FinstatService = finstatService;
        }

        IFinstatService _FinstatService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_FinstatService);
        }

        [HttpGet]
        [Route("getconsolidatedtrialbalanceGAAP")]
        public HttpResponseMessage GetAllTrialBalanceConsolidated(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                TrialBalanceConsolidated[] trialbalanceconsolidated = _FinstatService.GetAllTrialBalanceConsolidated();

                return request.CreateResponse<TrialBalanceConsolidated[]>(HttpStatusCode.OK, trialbalanceconsolidated);
            });
        }

        [HttpGet]
        [Route("getconsolidatedtrialbalanceIFRS")]
        public HttpResponseMessage GetAllTrialBalanceConsolidatedIFRS(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                TrialBalanceConsolidatedIFRS[] trialbalanceconsolidatedifrs = _FinstatService.GetAllTrialBalanceConsolidatedIFRS();

                return request.CreateResponse<TrialBalanceConsolidatedIFRS[]>(HttpStatusCode.OK, trialbalanceconsolidatedifrs);
            });
        }

    }
}
