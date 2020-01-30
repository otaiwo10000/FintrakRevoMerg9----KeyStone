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
using Fintrak.Shared.Common.Extensions;
using Fintrak.Presentation.WebClient.Additionalmethods;
using Fintrak.Presentation.WebClient.Additionalmethods.ApprovalMtd;
using MoreLinq.Extensions;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/misnewTEMPstatus")]
    [UsesDisposableService]
    public class MISNewTEMPStatusApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public MISNewTEMPStatusApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);
        }



        //======================== awaiting starts ================================================================================

        [HttpGet]
        [Route("misnewTEMPstatusAWAITING")]
        public HttpResponseMessage MISNewAwaiting(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "AWAITINGAPPROVAL";
                MISNewTEMPStatus obj = new MISNewTEMPStatus();
                var ddb = obj.MISNewTEMP(approvalstatus);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }       

        [HttpGet]
        [Route("misnewTEMPstatususingparamsAWAITING/{search}")]
        public HttpResponseMessage MISNewUsingParamsAwaiting(HttpRequestMessage request, string search)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "AWAITINGAPPROVAL";
                MISNewTEMPStatus obj = new MISNewTEMPStatus();
                var ddb = obj.MISNewTEMPusingparams(approvalstatus, search);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }       

        //======================== awaiting ends ================================================================================

        //======================== approved starts ================================================================================

        [HttpGet]
        [Route("misnewTEMPstatusAPPROVED")]
        public HttpResponseMessage MISNewApproved(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "APPROVED";
                MISNewTEMPStatus obj = new MISNewTEMPStatus();
                var ddb = obj.MISNewTEMP(approvalstatus);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }      

        [HttpGet]
        [Route("misnewTEMPstatususingparamsAPPROVED/{search}")]
        public HttpResponseMessage MISNewUsingParamsApproved(HttpRequestMessage request, string search)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "APPROVED";
                MISNewTEMPStatus obj = new MISNewTEMPStatus();
                var ddb = obj.MISNewTEMPusingparams(approvalstatus, search);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        //======================== approved ends ================================================================================

        //======================== declined starts ================================================================================

        [HttpGet]
        [Route("misnewTEMPstatusDECLINED")]
        public HttpResponseMessage TeamDeclined(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "DECLINED";
                MISNewTEMPStatus obj = new MISNewTEMPStatus();
                var ddb = obj.MISNewTEMP(approvalstatus);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }     

        [HttpGet]
        [Route("misnewTEMPstatususingparamsDECLINED/{search}")]
        public HttpResponseMessage MISNewTEMPusingparamsDECLINED(HttpRequestMessage request, string search)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "DECLINED";
                MISNewTEMPStatus obj = new MISNewTEMPStatus();
                var ddb = obj.MISNewTEMPusingparams(approvalstatus, search);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }
        
        //======================== declined ends ================================================================================

        [HttpPost]
        //[Route("teamTEMPapproval/{selectedIds}")]
        [Route("misnewTEMPapproval")]
        public HttpResponseMessage EditMISNewTEMPApproval(HttpRequestMessage request, [FromBody]string[] selectedIds2)
        {
            return GetHttpResponse(request, () =>
            {
                //MISNewTEMPStatus Obj = new MISNewTEMPStatus();
                //string selectedIds = String.Join(",", selectedIds2);
                //Obj.EditMISNewApproval(selectedIds);

                MISNewTEMPStatus Obj = new MISNewTEMPStatus();
                foreach (var batch in selectedIds2.Batch(2000))
                {
                    string selectedIds = String.Join(",", batch);
                    Obj.EditMISNewApproval(selectedIds);
                }

                return request.CreateResponse(HttpStatusCode.OK);
            });
        }

        [HttpPost]
        //[Route("teamTEMPdecline/{selectedIds}")]
        [Route("misnewTEMPdecline")]
        public HttpResponseMessage EditMISNewTEMPDecline(HttpRequestMessage request, [FromBody]string[] selectedIds2)
        {
            return GetHttpResponse(request, () =>
            {
                //MISNewTEMPStatus tObj = new MISNewTEMPStatus();
                //string selectedIds = String.Join(",", selectedIds2);
                //tObj.EditMISNewDecline(selectedIds);

                MISNewTEMPStatus Obj = new MISNewTEMPStatus();
                foreach (var batch in selectedIds2.Batch(2000))
                {
                    string selectedIds = String.Join(",", batch);
                    Obj.EditMISNewDecline(selectedIds);
                }

                return request.CreateResponse(HttpStatusCode.OK);
            });
        }

    }
}
