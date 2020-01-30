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
    [RoutePrefix("api/trialbalance")]
    [UsesDisposableService]
    public class TrialBalanceApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public TrialBalanceApiController(IFinstatService finstatService)
        {
            _FinstatService = finstatService;
        }

        IFinstatService _FinstatService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_FinstatService);
        }

        [HttpGet]
        [Route("getifrstrialbalance")]
        public HttpResponseMessage GetGetIFRSTrialBalance(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                var item = new TrialBalanceModel();
                TrialBalance[] trialBalances = _FinstatService.GetAllTrialBalances();
                decimal totalbal = 0;
                foreach (var tbal in trialBalances)
                {
                    totalbal = totalbal + tbal.LCY_Balance;
                }
                item.TrialBalance = trialBalances;
                item.TranslatedBalance = totalbal;

                return request.CreateResponse<TrialBalanceModel>(HttpStatusCode.OK, item);
            });
        }

        [HttpGet]
        [Route("gettrialbalancebygl/{glCode}")]
        public HttpResponseMessage GetGetTrialBalanceByGL(HttpRequestMessage request, string glCode)
        {
            return GetHttpResponse(request, () =>
            {
                TrialBalance[] trialBalances = _FinstatService.GetTrialBalanceByGL(glCode);

                return request.CreateResponse<TrialBalance[]>(HttpStatusCode.OK, trialBalances);
            });
        }

        [HttpGet]
        [Route("getgaptrialbalance")]
        public HttpResponseMessage GetGetGAPTrialBalance(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                var item = new TrialBalanceGapModel();
                TrialBalanceGap[] trialBalances = _FinstatService.GetAllTrialBalanceGaps();
               decimal totalbal = 0;
                 foreach (var tbal in trialBalances)
                 {
                     totalbal = totalbal + tbal.LCY_Balance;
                 }
                 item.TrialBalance = trialBalances;
                 item.TranslatedBalance = totalbal;

                 return request.CreateResponse<TrialBalanceGapModel>(HttpStatusCode.OK, item);
            });
        }

        [HttpGet]
        [Route("getgaptrialbalancebygl/{glCode}")]
        public HttpResponseMessage GetGetGAPTrialBalanceByGL(HttpRequestMessage request,string glCode)
        {
            return GetHttpResponse(request, () =>
            {
                TrialBalanceGap[] trialBalances = _FinstatService.GetTrialBalanceGapByGL(glCode);

                return request.CreateResponse<TrialBalanceGap[]>(HttpStatusCode.OK, trialBalances);
            });
        }

    }
}
