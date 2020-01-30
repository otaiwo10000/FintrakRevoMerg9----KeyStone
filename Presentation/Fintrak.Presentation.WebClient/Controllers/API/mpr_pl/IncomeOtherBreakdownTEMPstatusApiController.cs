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
    [RoutePrefix("api/incomeotherbreakdownTEMPstatus")]
    [UsesDisposableService]
    public class IncomeOtherBreakdownTEMPApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomeOtherBreakdownTEMPApiController(IMPRPLService mprPLService)
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
        [Route("incomeotherbreakdownTEMPawaiting")]
        public HttpResponseMessage IncomeOtherBreakdownTEMPAWAITING(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "AWAITINGAPPROVAL";
                IncomeOtherBreakdownTEMPStatus obj = new IncomeOtherBreakdownTEMPStatus();
                var ddb = obj.IncomeOtherBreakdownTEMP(approvalstatus);
               
                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        [HttpGet]
        [Route("incomeotherbreakdownTEMPusingparamsawaiting/{search}")]
        public HttpResponseMessage IncomeOtherBreakdownTEMPusingparamsAWAITING(HttpRequestMessage request, string search)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "AWAITINGAPPROVAL";
                IncomeOtherBreakdownTEMPStatus obj = new IncomeOtherBreakdownTEMPStatus();
                var ddb = obj.IncomeOtherBreakdownTEMPusingparams(approvalstatus, search);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }
        //======================== awaiting ends ================================================================================

        //======================== approved starts ================================================================================

        [HttpGet]
        [Route("incomeotherbreakdownTEMPapproved")]
        public HttpResponseMessage IncomeOtherBreakdownTEMPAPPROVED(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "APPROVED";
                IncomeOtherBreakdownTEMPStatus obj = new IncomeOtherBreakdownTEMPStatus();
                var ddb = obj.IncomeOtherBreakdownTEMP(approvalstatus);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        [HttpGet]
        [Route("incomeotherbreakdownTEMPusingparamsapproved/{search}")]
        public HttpResponseMessage IncomeOtherBreakdownTEMPusingparamsAPPROVED(HttpRequestMessage request, string search)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "APPROVED";
                IncomeOtherBreakdownTEMPStatus obj = new IncomeOtherBreakdownTEMPStatus();
                var ddb = obj.IncomeOtherBreakdownTEMPusingparams(approvalstatus, search);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }
        //======================== approved ends ================================================================================

        //======================== declined starts ================================================================================

        [HttpGet]
        [Route("incomeotherbreakdownTEMPdeclined")]
        public HttpResponseMessage IncomeOtherBreakdownTEMPDECLINED(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "DECLINED";
                IncomeOtherBreakdownTEMPStatus obj = new IncomeOtherBreakdownTEMPStatus();
                var ddb = obj.IncomeOtherBreakdownTEMP(approvalstatus);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        [HttpGet]
        [Route("incomeotherbreakdownTEMPusingparamsdeclined/{search}")]
        public HttpResponseMessage IncomeOtherBreakdownTEMPusingparamsDECLINED(HttpRequestMessage request, string search)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "DECLINED";
                IncomeOtherBreakdownTEMPStatus obj = new IncomeOtherBreakdownTEMPStatus();
                var ddb = obj.IncomeOtherBreakdownTEMPusingparams(approvalstatus, search);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }
        //======================== declined ends ================================================================================


        [HttpPost]
        //[Route("incomeotherbreakdownTEMPapproval/{selectedIds}")]
        [Route("incomeotherbreakdownTEMPapproval")]
        public HttpResponseMessage EditIncomeOtherBreakdownTEMPApproval(HttpRequestMessage request, [FromBody]string[] selectedIds2)
        {
            return GetHttpResponse(request, () =>
            {
                //IncomeOtherBreakdownTEMPStatus Obj = new IncomeOtherBreakdownTEMPStatus();
                //string selectedIds = String.Join(",", selectedIds2);
                //Obj.EditIncomeOtherBreakdownApproval(selectedIds);

                IncomeOtherBreakdownTEMPStatus Obj = new IncomeOtherBreakdownTEMPStatus();
                foreach (var batch in selectedIds2.Batch(2000))
                {
                    string selectedIds = String.Join(",", batch);
                    Obj.EditIncomeOtherBreakdownApproval(selectedIds);
                }

                return request.CreateResponse(HttpStatusCode.OK);
            });
        }

        [HttpPost]
        //[Route("incomeotherbreakdownTEMPdecline/{selectedIds}")]
        [Route("incomeotherbreakdownTEMPdecline")]
        public HttpResponseMessage EditIncomeOtherBreakdownTEMPDecline(HttpRequestMessage request, [FromBody]string[] selectedIds2)
        {
            return GetHttpResponse(request, () =>
            {
                //IncomeOtherBreakdownTEMPStatus Obj = new IncomeOtherBreakdownTEMPStatus();
                //string selectedIds = String.Join(",", selectedIds2);
                //Obj.EditIncomeOtherBreakdownDecline(selectedIds);

                IncomeOtherBreakdownTEMPStatus Obj = new IncomeOtherBreakdownTEMPStatus();
                foreach (var batch in selectedIds2.Batch(2000))
                {
                    string selectedIds = String.Join(",", batch);
                    Obj.EditIncomeOtherBreakdownDecline(selectedIds);
                }

                return request.CreateResponse(HttpStatusCode.OK);
            });
        }

      

    }
}
