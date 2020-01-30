using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Presentation.WebClient.Models;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.MPR.Contracts;
using Fintrak.Client.MPR.Entities;
using Fintrak.Presentation.WebClient.Additionalmethods;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/downloadbasefintrakfinalmemomanual")]
    [UsesDisposableService]
    public class MPRDownLoadBaseFintrakFinalMemoManualApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public MPRDownLoadBaseFintrakFinalMemoManualApiController(IMPRPLService mprPLService)
        {
            _MPRPLService = mprPLService;
        }

        IMPRPLService _MPRPLService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRPLService);
        }

        [HttpGet]
        [Route("ddbfintrakmemomanual")]
        public HttpResponseMessage GetDDBCaption(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                MPRDownLoadBaseFintrakFinalMemoManualMtd obj = new MPRDownLoadBaseFintrakFinalMemoManualMtd();
                List<MPRDownLoadBaseFintrakFinalMemoManualModel> ddb = obj.GetMPRDownloadBaseFintrakFinalManual().ToList();

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        [HttpGet]
        [Route("ddbfintrakmemomanualusingparams/{year}/{period}/{search}")]
        public HttpResponseMessage GetDDBUsingParams(HttpRequestMessage request, int year, int period, string search)
        {
            return GetHttpResponse(request, () =>
            {
                MPRDownLoadBaseFintrakFinalMemoManualMtd obj = new MPRDownLoadBaseFintrakFinalMemoManualMtd();
                List<MPRDownLoadBaseFintrakFinalMemoManualModel> ddb = obj.GetMPRDownloadBaseFintrakFinalManualUsingYearPeriod(year, period, search).ToList();

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

    }
}
