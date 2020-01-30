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
    [RoutePrefix("api/incomeadjustmentsummary")]
    [UsesDisposableService]
    public class IncomeAdjustmentSummaryApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomeAdjustmentSummaryApiController(IMPRPLService mprPLService)
        {
            _MPRPLService = mprPLService;
        }

        IMPRPLService _MPRPLService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRPLService);
        }

        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

        [HttpGet]
        [Route("volsummary/{period}/{year}")]
        public HttpResponseMessage VolumeSummary(HttpRequestMessage request, int period, int year)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeAdjustmentSummaryMtd obj = new IncomeAdjustmentSummaryMtd();
                List<IncomeAdjustmentSummaryModel> vsummary = obj.GetIncomeAdjustmentVolSummary(period, year).ToList();

                return request.CreateResponse(HttpStatusCode.OK, vsummary);
            });
        }

        [HttpGet]
        [Route("sbusummary/{period}/{year}/{caption}")]
        public HttpResponseMessage SBUSummary(HttpRequestMessage request, int period, int year, string caption)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeAdjustmentSummaryMtd obj = new IncomeAdjustmentSummaryMtd();
                List<IncomeAdjustmentSummaryModel> sbusummary = obj.IncomeAdjustmentGetSBUSummary(period, year, caption).ToList();

                return request.CreateResponse(HttpStatusCode.OK, sbusummary);
            });
        }

        [HttpGet]
        [Route("volcaptionsummary/{period}/{year}/{caption}")]
        public HttpResponseMessage VolCaptionSummary(HttpRequestMessage request, int period, int year, string caption)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeAdjustmentSummaryMtd obj = new IncomeAdjustmentSummaryMtd();
                List<IncomeAdjustmentSummaryModel> vcaptionsummary = obj.IncomeAdjustmentVolCaptionSummary(period, year, caption).ToList();

                return request.CreateResponse(HttpStatusCode.OK, vcaptionsummary);
            });
        }

        [HttpGet]
        [Route("commfeesbucaptionsummary/{period}/{year}/{caption}")]
        public HttpResponseMessage CommFessSBUCaptionSummary(HttpRequestMessage request, int period, int year, string caption)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeAdjustmentSummaryMtd obj = new IncomeAdjustmentSummaryMtd();
                List<IncomeAdjustmentSummaryModel> cfsbucaptionsummary = obj.IncomeAdjustmentCommFeesSBUCaptionSummary(period, year, caption).ToList();

                return request.CreateResponse(HttpStatusCode.OK, cfsbucaptionsummary);
            });
        }

        [HttpGet]
        [Route("commfeecaptionsummary/{period}/{year}/{caption}")]
        public HttpResponseMessage CommFessCaptionSummary(HttpRequestMessage request, int period, int year, string caption)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeAdjustmentSummaryMtd obj = new IncomeAdjustmentSummaryMtd();
                List<IncomeAdjustmentSummaryModel> cfcaptionsummary = obj.IncomeAdjustmentCommFeesCaptionSummary(period, year, caption).ToList();

                return request.CreateResponse(HttpStatusCode.OK, cfcaptionsummary);
            });
        }

        [HttpGet]
        [Route("volprodsummary/{period}/{year}")]
        public HttpResponseMessage VolumeProductSummary(HttpRequestMessage request, int period, int year)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeAdjustmentSummaryMtd obj = new IncomeAdjustmentSummaryMtd();
                List<IncomeAdjustmentSummaryModel> vpsummary = obj.GetIncomeAdjustmentVolProdSummary(period, year).ToList();

                return request.CreateResponse(HttpStatusCode.OK, vpsummary);
            });
        }

    }
}
