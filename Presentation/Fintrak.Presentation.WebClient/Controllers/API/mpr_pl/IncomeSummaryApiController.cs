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
using Fintrak.Presentation.WebClient.Additionalmethods;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/incomesummary")]
    [UsesDisposableService]
    public class IncomeSummaryApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomeSummaryApiController(IMPRPLService mprPLService)
        {
            _MPRPLService = mprPLService;
        }

        IMPRPLService _MPRPLService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRPLService);
        }

        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

        //[HttpGet]
        //[Route("getallincomesummary")]
        //public HttpResponseMessage GetAllIncomeSummary(HttpRequestMessage request)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        YearPeriodMtd ob1 = new YearPeriodMtd();
        //        int year = ob1.Year();

        //        YearPeriodMtd ob2 = new YearPeriodMtd();
        //        int period = ob1.Period();

        //        ProfitORLosspMtd obj = new ProfitORLosspMtd();
        //        List<IncomeSummaryModel> incomesummary = obj.GetIncomeSummary(year, period).ToList();

        //        return request.CreateResponse(HttpStatusCode.OK, incomesummary);
        //    });
        //}

        [HttpGet]
        [Route("getallincomesummaryusingparams/{yr}/{pr}")]
        public HttpResponseMessage GetAllIncomeSummaryUsingParams(HttpRequestMessage request, int yr, int pr)
        {
            return GetHttpResponse(request, () =>
            {
                ProfitORLosspMtd obj = new ProfitORLosspMtd();
                List<IncomeSummaryModel> incomesummary = obj.GetIncomeSummary(yr, pr).ToList();

                return request.CreateResponse(HttpStatusCode.OK, incomesummary);
            });
        }

    }
}
