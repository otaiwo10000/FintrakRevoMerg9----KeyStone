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
    [RoutePrefix("api/incomenewdetailsTEMPstatus")]
    [UsesDisposableService]
    public class IncomeNewDetailsTEMPstatusApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomeNewDetailsTEMPstatusApiController(IMPRPLService mprPLService)
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
        [Route("incomenewdetailsTEMPawaiting")]
        public HttpResponseMessage IncomeOtherBreakdownTEMPAWAITING(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "AWAITINGAPPROVAL";
                IncomeNewDetailsTEMPStatus obj = new IncomeNewDetailsTEMPStatus();
                var ddb = obj.IncomeNewDetailsTEMP(approvalstatus);
               
                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        [HttpGet]
        [Route("incomenewdetailsTEMPusingparamsawaiting/{search}")]
        public HttpResponseMessage IncomeOtherBreakdownTEMPusingparamsAWAITING(HttpRequestMessage request, string search)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "AWAITINGAPPROVAL";
                IncomeNewDetailsTEMPStatus obj = new IncomeNewDetailsTEMPStatus();
                var ddb = obj.IncomeNewDetailsTEMPusingparams(approvalstatus, search);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }
        //======================== awaiting ends ================================================================================

        //======================== approved starts ================================================================================

        [HttpGet]
        [Route("incomenewdetailsTEMPapproved")]
        public HttpResponseMessage IncomeOtherBreakdownTEMPAPPROVED(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "APPROVED";
                IncomeNewDetailsTEMPStatus obj = new IncomeNewDetailsTEMPStatus();
                var ddb = obj.IncomeNewDetailsTEMP(approvalstatus);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        [HttpGet]
        [Route("incomenewdetailsTEMPusingparamsapproved/{search}")]
        public HttpResponseMessage IncomeOtherBreakdownTEMPusingparamsAPPROVED(HttpRequestMessage request, string search)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "APPROVED";
                IncomeNewDetailsTEMPStatus obj = new IncomeNewDetailsTEMPStatus();
                var ddb = obj.IncomeNewDetailsTEMPusingparams(approvalstatus, search);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }
        //======================== approved ends ================================================================================

        //======================== declined starts ================================================================================

        [HttpGet]
        [Route("incomenewdetailsTEMPdeclined")]
        public HttpResponseMessage IncomeOtherBreakdownTEMPDECLINED(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "DECLINED";
                IncomeNewDetailsTEMPStatus obj = new IncomeNewDetailsTEMPStatus();
                var ddb = obj.IncomeNewDetailsTEMP(approvalstatus);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        [HttpGet]
        [Route("incomenewdetailsTEMPusingparamsdeclined/{search}")]
        public HttpResponseMessage IncomeOtherBreakdownTEMPusingparamsDECLINED(HttpRequestMessage request, string search)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "DECLINED";
                IncomeNewDetailsTEMPStatus obj = new IncomeNewDetailsTEMPStatus();
                var ddb = obj.IncomeNewDetailsTEMPusingparams(approvalstatus, search);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }
        //======================== declined ends ================================================================================


        [HttpPost]
        [Route("incomenewdetailsTEMPapproval")]
        public HttpResponseMessage EditIncomeOtherBreakdownTEMPApproval(HttpRequestMessage request, [FromBody]string[] selectedIds2)
        {
            return GetHttpResponse(request, () =>
            {
                //IncomeNewDetailsTEMPStatus Obj = new IncomeNewDetailsTEMPStatus();
                //string selectedIds = String.Join(",", selectedIds2);
                //Obj.EditIncomeNewDetailsApproval(selectedIds);

                IncomeNewDetailsTEMPStatus Obj = new IncomeNewDetailsTEMPStatus();
                foreach (var batch in selectedIds2.Batch(2000))
                {
                    string selectedIds = String.Join(",", batch);
                    Obj.EditIncomeNewDetailsApproval(selectedIds);
                }

                return request.CreateResponse(HttpStatusCode.OK);
            });
        }

        [HttpPost]
        [Route("incomenewdetailsTEMPdecline")]
        public HttpResponseMessage EditIncomeOtherBreakdownTEMPDecline(HttpRequestMessage request, [FromBody]string[] selectedIds2)
        {
            return GetHttpResponse(request, () =>
            {
                //IncomeNewDetailsTEMPStatus Obj = new IncomeNewDetailsTEMPStatus();
                //string selectedIds = String.Join(",", selectedIds2);
                //Obj.EditIncomeNewDetailsDecline(selectedIds);

                IncomeNewDetailsTEMPStatus Obj = new IncomeNewDetailsTEMPStatus();
                foreach (var batch in selectedIds2.Batch(2000))
                {
                    string selectedIds = String.Join(",", batch);
                    Obj.EditIncomeNewDetailsDecline(selectedIds);
                }

                return request.CreateResponse(HttpStatusCode.OK);
            });
        }

      

    }
}
