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
using Fintrak.Presentation.WebClient.Additionalmethods.ApprovalMtd;
using MoreLinq.Extensions;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/incomeaccountslistingTEMPstatus")]
    [UsesDisposableService]
    public class IncomeAccountsListingTEMPStatusApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomeAccountsListingTEMPStatusApiController(IMPRBSService mprBSService)
        {
            _MPRBSService = mprBSService;
        }

        IMPRBSService _MPRBSService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRBSService);
        }

        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

        //======================== awaiting starts ================================================================================

        [HttpGet]
        [Route("incomeaccountslistingAWAITING")]
        public HttpResponseMessage IncomeAccountsListingAwaiting(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "AWAITINGAPPROVAL";
                IncomeAccountsListingTEMPStatus obj = new IncomeAccountsListingTEMPStatus();
                var ddb = obj.IncomeAccountsListingTEMP(approvalstatus);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        [HttpGet]
        [Route("incomeaccountslistingusingparamsAWAITING/{search}")]
        public HttpResponseMessage IncomeAccountsListingUsingParamsAwaiting(HttpRequestMessage request, string search)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "AWAITINGAPPROVAL";
                IncomeAccountsListingTEMPStatus obj = new IncomeAccountsListingTEMPStatus();
                var ddb = obj.IncomeAccountsListingTEMPusingparams(approvalstatus, search);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        //======================== awaiting ends ================================================================================

        //======================== approved starts ================================================================================

        [HttpGet]
        [Route("incomeaccountslistingAPPROVED")]
        public HttpResponseMessage IncomeAccountsListingApproved(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "APPROVED";
                IncomeAccountsListingTEMPStatus obj = new IncomeAccountsListingTEMPStatus();
                var ddb = obj.IncomeAccountsListingTEMP(approvalstatus);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        [HttpGet]
        [Route("incomeaccountslistingusingparamsAPPROVED/{search}")]
        public HttpResponseMessage IncomeAccountsListingUsingParamsApproved(HttpRequestMessage request, string search)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "APPROVED";
                IncomeAccountsListingTEMPStatus obj = new IncomeAccountsListingTEMPStatus();
                var ddb = obj.IncomeAccountsListingTEMPusingparams(approvalstatus, search);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        //======================== approved ends ================================================================================

        //======================== declined starts ================================================================================

        [HttpGet]
        [Route("incomeaccountslistingDECLINED")]
        public HttpResponseMessage IncomeAccountsListingDECLINED(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "DECLINED";
                IncomeAccountsListingTEMPStatus obj = new IncomeAccountsListingTEMPStatus();
                var ddb = obj.IncomeAccountsListingTEMP(approvalstatus);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        [HttpGet]
        [Route("incomeaccountslistingusingparamsDECLINED/{search}")]
        public HttpResponseMessage IncomeAccountsListingTEMPusingparamsDECLINED(HttpRequestMessage request, string search)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "DECLINED";
                IncomeAccountsListingTEMPStatus obj = new IncomeAccountsListingTEMPStatus();
                var ddb = obj.IncomeAccountsListingTEMPusingparams(approvalstatus, search);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        //======================== declined ends ================================================================================

        [HttpPost]
        //[Route("incomeaccountMISoverrideapproval/{selectedIds}")]
        [Route("incomeaccountslistingapproval")]
        public HttpResponseMessage EditIncomeAccountsListingApproval(HttpRequestMessage request, [FromBody]string[] selectedIds2)
        {
            return GetHttpResponse(request, () =>
            {
                //IncomeAccountsListingTEMPStatus Obj = new IncomeAccountsListingTEMPStatus();
                //string selectedIds = String.Join(",", selectedIds2);
                //Obj.EditIncomeAccountsListingApproval(selectedIds);

                IncomeAccountsListingTEMPStatus Obj = new IncomeAccountsListingTEMPStatus();
                foreach (var batch in selectedIds2.Batch(2000))
                {
                    string selectedIds = String.Join(",", batch);
                    Obj.EditIncomeAccountsListingApproval(selectedIds);
                }

                return request.CreateResponse(HttpStatusCode.OK);
            });
        }

        [HttpPost]
        //[Route("incomeaccountMISoverridedecline/{selectedIds}")]
        [Route("incomeaccountslistingdecline")]
        public HttpResponseMessage EditIncomeAccountsListingDecline(HttpRequestMessage request, [FromBody]string[] selectedIds2)
        {
            return GetHttpResponse(request, () =>
            {
                //IncomeAccountsListingTEMPStatus tObj = new IncomeAccountsListingTEMPStatus();
                //string selectedIds = String.Join(",", selectedIds2);
                //tObj.EditIncomeAccountsListingDecline(selectedIds);

                IncomeAccountsListingTEMPStatus Obj = new IncomeAccountsListingTEMPStatus();
                foreach (var batch in selectedIds2.Batch(2000))
                {
                    string selectedIds = String.Join(",", batch);
                    Obj.EditIncomeAccountsListingDecline(selectedIds);
                }

                return request.CreateResponse(HttpStatusCode.OK);
            });
        }


    }
}
