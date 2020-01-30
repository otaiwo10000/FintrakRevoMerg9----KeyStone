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
    [RoutePrefix("api/misnewothersTEMPstatus")]
    [UsesDisposableService]
    public class MISNewOthersTEMPStatusApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public MISNewOthersTEMPStatusApiController(IMPRCoreService mprCoreService)
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
        [Route("misnewothersTEMPstatusAWAITING")]
        public HttpResponseMessage MISNewOthersAwaiting(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "AWAITINGAPPROVAL";
                MISNewOthersTEMPStatus obj = new MISNewOthersTEMPStatus();
                var ddb = obj.MISNewOthersTEMP(approvalstatus);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }       

        [HttpGet]
        [Route("misnewothersTEMPstatususingparamsAWAITING/{search}")]
        public HttpResponseMessage MISNewOthersUsingParamsAwaiting(HttpRequestMessage request, string search)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "AWAITINGAPPROVAL";
                MISNewOthersTEMPStatus obj = new MISNewOthersTEMPStatus();
                var ddb = obj.MISNewOthersTEMPusingparams(approvalstatus, search);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }       

        //======================== awaiting ends ================================================================================

        //======================== approved starts ================================================================================

        [HttpGet]
        [Route("misnewothersTEMPstatusAPPROVED")]
        public HttpResponseMessage MISNewApproved(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "APPROVED";
                MISNewOthersTEMPStatus obj = new MISNewOthersTEMPStatus();
                var ddb = obj.MISNewOthersTEMP(approvalstatus);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }      

        [HttpGet]
        [Route("misnewothersTEMPstatususingparamsAPPROVED/{search}")]
        public HttpResponseMessage MISNewUsingParamsApproved(HttpRequestMessage request, string search)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "APPROVED";
                MISNewOthersTEMPStatus obj = new MISNewOthersTEMPStatus();
                var ddb = obj.MISNewOthersTEMPusingparams(approvalstatus, search);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        //======================== approved ends ================================================================================

        //======================== declined starts ================================================================================

        [HttpGet]
        [Route("misnewothersTEMPstatusDECLINED")]
        public HttpResponseMessage TeamDeclined(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "DECLINED";
                MISNewOthersTEMPStatus obj = new MISNewOthersTEMPStatus();
                var ddb = obj.MISNewOthersTEMP(approvalstatus);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }     

        [HttpGet]
        [Route("misnewothersTEMPstatususingparamsDECLINED/{search}")]
        public HttpResponseMessage MISNewTEMPusingparamsDECLINED(HttpRequestMessage request, string search)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "DECLINED";
                MISNewOthersTEMPStatus obj = new MISNewOthersTEMPStatus();
                var ddb = obj.MISNewOthersTEMPusingparams(approvalstatus, search);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }
        
        //======================== declined ends ================================================================================

        [HttpPost]
        //[Route("teamTEMPapproval/{selectedIds}")]
        [Route("misnewothersTEMPapproval")]
        public HttpResponseMessage EditMISNewTEMPApproval(HttpRequestMessage request, [FromBody]string[] selectedIds2)
        {
            return GetHttpResponse(request, () =>
            {
                //MISNewOthersTEMPStatus Obj = new MISNewOthersTEMPStatus();
                //string selectedIds = String.Join(",", selectedIds2);
                //Obj.EditMISNewOthersApproval(selectedIds);

                MISNewOthersTEMPStatus Obj = new MISNewOthersTEMPStatus();
                foreach (var batch in selectedIds2.Batch(2000))
                {
                    string selectedIds = String.Join(",", batch);
                    Obj.EditMISNewOthersApproval(selectedIds);
                }

                return request.CreateResponse(HttpStatusCode.OK);
            });
        }

        [HttpPost]
        //[Route("teamTEMPdecline/{selectedIds}")]
        [Route("misnewothersTEMPdecline")]
        public HttpResponseMessage EditMISNewTEMPDecline(HttpRequestMessage request, [FromBody]string[] selectedIds2)
        {
            return GetHttpResponse(request, () =>
            {
                //MISNewOthersTEMPStatus tObj = new MISNewOthersTEMPStatus();
                //string selectedIds = String.Join(",", selectedIds2);
                //tObj.EditMISNewOthersDecline(selectedIds);

                MISNewOthersTEMPStatus Obj = new MISNewOthersTEMPStatus();
                foreach (var batch in selectedIds2.Batch(2000))
                {
                    string selectedIds = String.Join(",", batch);
                    Obj.EditMISNewOthersDecline(selectedIds);
                }

                return request.CreateResponse(HttpStatusCode.OK);
            });
        }

    }
}
