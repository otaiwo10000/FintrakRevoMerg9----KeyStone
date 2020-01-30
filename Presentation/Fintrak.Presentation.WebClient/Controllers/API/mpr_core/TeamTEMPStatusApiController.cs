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
    [RoutePrefix("api/teamTEMPstatus")]
    [UsesDisposableService]
    public class TeamTEMPStatusApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public TeamTEMPStatusApiController(IMPRCoreService mprCoreService)
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
        [Route("teamTEMPstatusAWAITING")]
        public HttpResponseMessage TeamAwaiting(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "AWAITINGAPPROVAL";
                TeamTEMPStatus obj = new TeamTEMPStatus();
                var ddb = obj.TeamStructureTEMP(approvalstatus);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }       

        [HttpGet]
        [Route("teamTEMPstatususingparamsAWAITING/{search}")]
        public HttpResponseMessage TeamUsingParamsAwaiting(HttpRequestMessage request, string search)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "AWAITINGAPPROVAL";
                TeamTEMPStatus obj = new TeamTEMPStatus();
                var ddb = obj.TeamStructureusingparamsTEMP(approvalstatus, search);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }       

        //======================== awaiting ends ================================================================================

        //======================== approved starts ================================================================================

        [HttpGet]
        [Route("teamTEMPstatusAPPROVED")]
        public HttpResponseMessage TeamApproved(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "APPROVED";
                TeamTEMPStatus obj = new TeamTEMPStatus();
                var ddb = obj.TeamStructureTEMP(approvalstatus);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }      

        [HttpGet]
        [Route("teamTEMPstatususingparamsAPPROVED/{search}")]
        public HttpResponseMessage TeamUsingParamsApproved(HttpRequestMessage request, string search)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "APPROVED";
                TeamTEMPStatus obj = new TeamTEMPStatus();
                var ddb = obj.TeamStructureusingparamsTEMP(approvalstatus, search);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        //======================== approved ends ================================================================================

        //======================== declined starts ================================================================================

        [HttpGet]
        [Route("teamTEMPstatusDECLINED")]
        public HttpResponseMessage TeamTEMPDeclined(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "DECLINED";
                TeamTEMPStatus obj = new TeamTEMPStatus();
                var ddb = obj.TeamStructureTEMP(approvalstatus);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }     

        [HttpGet]
        [Route("teamTEMPstatususingparamsDECLINED/{search}")]
        public HttpResponseMessage TeamTEMPusingparamsDECLINED(HttpRequestMessage request, string search)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "DECLINED";
                TeamTEMPStatus obj = new TeamTEMPStatus();
                var ddb = obj.TeamStructureusingparamsTEMP(approvalstatus, search);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }
        
        //======================== declined ends ================================================================================

        [HttpPost]
        //[Route("teamTEMPapproval/{selectedIds}")]
        [Route("teamTEMPapproval")]
        public HttpResponseMessage EditTeamTEMPApproval(HttpRequestMessage request, [FromBody]string[] selectedIds2)
        {
            return GetHttpResponse(request, () =>
            {
                //TeamTEMPStatus Obj = new TeamTEMPStatus();
                //string selectedIds = String.Join(",", selectedIds2);
                //Obj.EditTeamApproval(selectedIds);

                TeamTEMPStatus Obj = new TeamTEMPStatus();
                foreach (var batch in selectedIds2.Batch(2000))
                {
                    string selectedIds = String.Join(",", batch);
                    Obj.EditTeamApproval(selectedIds);
                }

                return request.CreateResponse(HttpStatusCode.OK);
            });
        }

        [HttpPost]
        //[Route("teamTEMPdecline/{selectedIds}")]
        [Route("teamTEMPdecline")]
        public HttpResponseMessage EditTeamTEMPDecline(HttpRequestMessage request, [FromBody]string[] selectedIds2)
        {
            return GetHttpResponse(request, () =>
            {
                //TeamTEMPStatus tObj = new TeamTEMPStatus();
                //string selectedIds = String.Join(",", selectedIds2);
                //tObj.EditTeamDecline(selectedIds);

                TeamTEMPStatus Obj = new TeamTEMPStatus();
                foreach (var batch in selectedIds2.Batch(2000))
                {
                    string selectedIds = String.Join(",", batch);
                    Obj.EditTeamDecline(selectedIds);
                }

                return request.CreateResponse(HttpStatusCode.OK);
            });
        }

    }
}
