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
using Fintrak.Presentation.WebClient.Additionalmethods.ApprovalMtd;
using MoreLinq.Extensions;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/downloadbasefintrakfinalmanualTEMPstatus")]
    [UsesDisposableService]
    public class MPRDownloadBaseFintrakFinalManualTEMPStatusApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public MPRDownloadBaseFintrakFinalManualTEMPStatusApiController(IMPRPLService mprPLService)
        {
            _MPRPLService = mprPLService;
        }

        IMPRPLService _MPRPLService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRPLService);
        }


        //======================== awaiting starts ================================================================================

        [HttpGet]
        [Route("downloadbasefintrakfinalmanualTEMPawaiting")]
        public HttpResponseMessage DownloadbaseFintrakFinalmanualTEMPAWAITING(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "AWAITINGAPPROVAL";
                MPRDownloadBaseFintrakFinalManualTEMPStatus obj = new MPRDownloadBaseFintrakFinalManualTEMPStatus();
                var ddb = obj.MPRDownloadBaseFintrakFinalManualTEMP(approvalstatus);
               
                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        [HttpGet]
        [Route("downloadbasefintrakfinalmanualTEMPusingparamsawaiting/{search}")]
        public HttpResponseMessage DownloadbaseFintrakFinalmanualTEMPusingparamsAWAITING(HttpRequestMessage request, string search)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "AWAITINGAPPROVAL";
                MPRDownloadBaseFintrakFinalManualTEMPStatus obj = new MPRDownloadBaseFintrakFinalManualTEMPStatus();
                var ddb = obj.MPRDownloadBaseFintrakFinalManualTEMPusingparams(approvalstatus, search);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }
        //======================== awaiting ends ================================================================================

        //======================== approved starts ================================================================================

        [HttpGet]
        [Route("downloadbasefintrakfinalmanualTEMPapproved")]
        public HttpResponseMessage DownloadbaseFintrakFinalmanualTEMPAPPROVED(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "APPROVED";
                MPRDownloadBaseFintrakFinalManualTEMPStatus obj = new MPRDownloadBaseFintrakFinalManualTEMPStatus();
                var ddb = obj.MPRDownloadBaseFintrakFinalManualTEMP(approvalstatus);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        [HttpGet]
        [Route("downloadbasefintrakfinalmanualTEMPusingparamsapproved/{search}")]
        public HttpResponseMessage DownloadbaseFintrakFinalmanualTEMPusingparamsAPPROVED(HttpRequestMessage request, string search)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "APPROVED";
                MPRDownloadBaseFintrakFinalManualTEMPStatus obj = new MPRDownloadBaseFintrakFinalManualTEMPStatus();
                var ddb = obj.MPRDownloadBaseFintrakFinalManualTEMPusingparams(approvalstatus, search);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }
        //======================== approved ends ================================================================================

        //======================== declined starts ================================================================================

        [HttpGet]
        [Route("downloadbasefintrakfinalmanualTEMPdeclined")]
        public HttpResponseMessage DownloadbaseFintrakFinalmanualTEMPDECLINED(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "DECLINED";
                MPRDownloadBaseFintrakFinalManualTEMPStatus obj = new MPRDownloadBaseFintrakFinalManualTEMPStatus();
                var ddb = obj.MPRDownloadBaseFintrakFinalManualTEMP(approvalstatus);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        [HttpGet]
        [Route("downloadbasefintrakfinalmanualTEMPusingparamsdeclined/{search}")]
        public HttpResponseMessage DownloadbaseFintrakFinalmanualTEMPusingparamsDECLINED(HttpRequestMessage request, string search)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "DECLINED";
                MPRDownloadBaseFintrakFinalManualTEMPStatus obj = new MPRDownloadBaseFintrakFinalManualTEMPStatus();
                var ddb = obj.MPRDownloadBaseFintrakFinalManualTEMPusingparams(approvalstatus, search);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }
        //======================== declined ends ================================================================================


        [HttpPost]
        [Route("downloadbasefintrakfinalmanualTEMPapproval")]
        public HttpResponseMessage EditDownloadbaseFintrakFinalmanualTEMPApproval(HttpRequestMessage request, [FromBody]string[] selectedIds2)
        {
            return GetHttpResponse(request, () =>
            {
                //MPRDownloadBaseFintrakFinalManualTEMPStatus Obj = new MPRDownloadBaseFintrakFinalManualTEMPStatus();
                //string selectedIds = String.Join(",", selectedIds2);
                //Obj.EditDownloadbasefintrakfinalmanualApproval(selectedIds);

                MPRDownloadBaseFintrakFinalManualTEMPStatus Obj = new MPRDownloadBaseFintrakFinalManualTEMPStatus();
                foreach (var batch in selectedIds2.Batch(2000))
                {
                    string selectedIds = String.Join(",", batch);
                    Obj.EditDownloadbasefintrakfinalmanualApproval(selectedIds);
                }

                return request.CreateResponse(HttpStatusCode.OK);
            });
        }

        [HttpPost]
        [Route("downloadbasefintrakfinalmanualTEMPdecline")]
        public HttpResponseMessage EditDownloadbaseFintrakFinalmanualTEMPDecline(HttpRequestMessage request, [FromBody]string[] selectedIds2)
        {
            return GetHttpResponse(request, () =>
            {
                //MPRDownloadBaseFintrakFinalManualTEMPStatus Obj = new MPRDownloadBaseFintrakFinalManualTEMPStatus();
                //string selectedIds = String.Join(",", selectedIds2);
                //Obj.EditDownloadbasefintrakfinalmanualDecline(selectedIds);

                MPRDownloadBaseFintrakFinalManualTEMPStatus Obj = new MPRDownloadBaseFintrakFinalManualTEMPStatus();
                foreach (var batch in selectedIds2.Batch(2000))
                {
                    string selectedIds = String.Join(",", batch);
                    Obj.EditDownloadbasefintrakfinalmanualDecline(selectedIds);
                }

                return request.CreateResponse(HttpStatusCode.OK);
            });
        }

      

    }
}
