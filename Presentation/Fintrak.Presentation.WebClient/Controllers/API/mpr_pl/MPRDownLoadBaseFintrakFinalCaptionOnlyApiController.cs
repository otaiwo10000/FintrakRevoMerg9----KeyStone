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
    [RoutePrefix("api/downloadbasefintrakfinalCaptiononly")]
    [UsesDisposableService]
    public class DownLoadBaseFintrakFinalCaptionOnlyApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public DownLoadBaseFintrakFinalCaptionOnlyApiController(IMPRPLService mprPLService)
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
        [Route("ddbfintrakmanualcaptiononly")]
        public HttpResponseMessage GetDDBCaption(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                MPRDownLoadBaseFintrakFinalCaptionOnlyMtd obj = new MPRDownLoadBaseFintrakFinalCaptionOnlyMtd();
                List<MPRDownLoadBaseFintrakFinalCaptionOnlyModel> ddb = obj.GetDDBFintrakManual().ToList();

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        [HttpGet]
        [Route("incomelinecaptiononly")]
        public HttpResponseMessage GetIncomeLineCaption(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                MPRDownLoadBaseFintrakFinalCaptionOnlyMtd obj = new MPRDownLoadBaseFintrakFinalCaptionOnlyMtd();
                List<MPRDownLoadBaseFintrakFinalCaptionOnlyModel> ddb = obj.GetIncomeLineCaption().ToList();

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

    }
}
