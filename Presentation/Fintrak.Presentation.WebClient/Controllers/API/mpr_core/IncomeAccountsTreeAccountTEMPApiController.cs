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
    [RoutePrefix("api/incomeaccountstreeaccounttemp")]
    [UsesDisposableService]
    public class IncomeAccountsTreeAccountTEMPApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomeAccountsTreeAccountTEMPApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }

        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

        [HttpPost]
        [Route("updateincomeaccountstreeaccounttemp")]
        public HttpResponseMessage UpdateIncomeAccountsTreeAccountTEMP(HttpRequestMessage request, [FromBody]IncomeAccountsTreeAccountTEMP incomeAccountsTreeAccountModel)
        {
            return GetHttpResponse(request, () =>
            {
                var incomeAccountsTreeAccount = _MPRCoreService.UpdateIncomeAccountsTreeAccountTEMP(incomeAccountsTreeAccountModel);

                return request.CreateResponse<IncomeAccountsTreeAccountTEMP>(HttpStatusCode.OK, incomeAccountsTreeAccount);
            });
        }


        [HttpPost]
        [Route("deleteincomeaccountstreeaccounttemp")]
        public HttpResponseMessage DeleteIncomeAccountsTreeAccountTEMP(HttpRequestMessage request, [FromBody]int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IncomeAccountsTreeAccountTEMP incomeAccountsTreeAccount = _MPRCoreService.GetIncomeAccountsTreeAccountTEMP(ID);

                if (incomeAccountsTreeAccount != null)
                {
                    _MPRCoreService.DeleteIncomeAccountsTreeAccountTEMP(ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No IncomeAccountsTreeAccountTemp found under that ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getincomeaccountstreeaccounttemp/{ID}")]
        public HttpResponseMessage GetIncomeAccountsTreeAccount(HttpRequestMessage request, int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IncomeAccountsTreeAccountTEMP incomeAccountsTreeAccount = _MPRCoreService.GetIncomeAccountsTreeAccountTEMP(ID);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<IncomeAccountsTreeAccountTEMP>(HttpStatusCode.OK, incomeAccountsTreeAccount);

                return response;
            });
        }


        [HttpGet]
        [Route("availableincomeaccountstreeaccounttemp")]
        public HttpResponseMessage GetAvailableIncomeAccountsTreeAccountTEMP(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeAccountsTreeAccountTEMP[] incomeAccountsTreeAccount = _MPRCoreService.GetAllIncomeAccountsTreeAccountTEMP();

                return request.CreateResponse<IncomeAccountsTreeAccountTEMP[]>(HttpStatusCode.OK, incomeAccountsTreeAccount);
            });
        }

        [HttpGet]
        [Route("incomeaccountstreeaccounttempbysearch/{search}")]
        public HttpResponseMessage GetAvailableIncomeAccountsTreeAccountTEMPUsingSearch(HttpRequestMessage request, string search)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeAccountsTreeAccountTEMP[] incomeAccountsTreeAccount = _MPRCoreService.GetIncomeAccountsTreeAccountTEMPBySearchVal(search);

                return request.CreateResponse<IncomeAccountsTreeAccountTEMP[]>(HttpStatusCode.OK, incomeAccountsTreeAccount);
            });
        }


        //======================== awaiting starts ================================================================================

        [HttpGet]
        [Route("incomeaccountstreeaccountAWAITING")]
        public HttpResponseMessage IncomeAccountsTreeAccountTEMPAwaiting(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "AWAITINGAPPROVAL";
                IncomeAccountsTreeAccountTEMPStatus obj = new IncomeAccountsTreeAccountTEMPStatus();
                var ddb = obj.IncomeAccountsTreeAccountTEMP(approvalstatus);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        [HttpGet]
        [Route("incomeaccountstreeaccountusingparamsAWAITING/{search}")]
        public HttpResponseMessage IncomeAccountMISOverrideUsingParamsAwaiting(HttpRequestMessage request, string search)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "AWAITINGAPPROVAL";
                IncomeAccountsTreeAccountTEMPStatus obj = new IncomeAccountsTreeAccountTEMPStatus();
                var ddb = obj.IncomeAccountsTreeAccountTEMPusingparams(approvalstatus, search);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        //======================== awaiting ends ================================================================================

        //======================== approved starts ================================================================================

        [HttpGet]
        [Route("incomeaccountstreeaccountAPPROVED")]
        public HttpResponseMessage IncomeAccountMISOverrideApproved(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "APPROVED";
                IncomeAccountsTreeAccountTEMPStatus obj = new IncomeAccountsTreeAccountTEMPStatus();
                var ddb = obj.IncomeAccountsTreeAccountTEMP(approvalstatus);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        [HttpGet]
        [Route("incomeaccountstreeaccountusingparamsAPPROVED/{search}")]
        public HttpResponseMessage TeamUsingParamsApproved(HttpRequestMessage request, string search)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "APPROVED";
                IncomeAccountsTreeAccountTEMPStatus obj = new IncomeAccountsTreeAccountTEMPStatus();
                var ddb = obj.IncomeAccountsTreeAccountTEMPusingparams(approvalstatus, search);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        //======================== approved ends ================================================================================

        //======================== declined starts ================================================================================

        [HttpGet]
        [Route("incomeaccountstreeaccountDECLINED")]
        public HttpResponseMessage IncomeAccountMISOverrideDECLINED(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "DECLINED";
                IncomeAccountsTreeAccountTEMPStatus obj = new IncomeAccountsTreeAccountTEMPStatus();
                var ddb = obj.IncomeAccountsTreeAccountTEMP(approvalstatus);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        [HttpGet]
        [Route("incomeaccountstreeaccountusingparamsDECLINED/{search}")]
        public HttpResponseMessage IncomeAccountsTreeAccountTEMPusingparamsDECLINED(HttpRequestMessage request, string search)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "DECLINED";
                IncomeAccountsTreeAccountTEMPStatus obj = new IncomeAccountsTreeAccountTEMPStatus();
                var ddb = obj.IncomeAccountsTreeAccountTEMPusingparams(approvalstatus, search);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        //======================== declined ends ================================================================================

        [HttpPost]
        //[Route("incomeaccountstreeaccountapproval/{selectedIds}")]
        [Route("incomeaccountstreeaccountapproval")]
        public HttpResponseMessage EditIncomeAccountsTreeAccountApproval(HttpRequestMessage request, [FromBody]string[] selectedIds2)
        {
            return GetHttpResponse(request, () =>
            {
                //IncomeAccountsTreeAccountTEMPStatus Obj = new IncomeAccountsTreeAccountTEMPStatus();
                //string selectedIds = String.Join(",", selectedIds2);
                //Obj.EditIncomeAccountsTreeAccountApproval(selectedIds);

                IncomeAccountsTreeAccountTEMPStatus Obj = new IncomeAccountsTreeAccountTEMPStatus();
                foreach (var batch in selectedIds2.Batch(2000))
                {
                    string selectedIds = String.Join(",", batch);
                    Obj.EditIncomeAccountsTreeAccountApproval(selectedIds);
                }

                return request.CreateResponse(HttpStatusCode.OK);
            });
        }

        [HttpPost]
        //[Route("incomeaccountstreeaccountdecline/{selectedIds}")]
        [Route("incomeaccountstreeaccountdecline")]
        public HttpResponseMessage EditIncomeAccountsTreeAccountDecline(HttpRequestMessage request, [FromBody]string[] selectedIds2)
        {
            return GetHttpResponse(request, () =>
            {
                //IncomeAccountsTreeAccountTEMPStatus tObj = new IncomeAccountsTreeAccountTEMPStatus();
                //string selectedIds = String.Join(",", selectedIds2);
                //tObj.EditIncomeAccountsTreeAccountDecline(selectedIds);

                IncomeAccountsTreeAccountTEMPStatus Obj = new IncomeAccountsTreeAccountTEMPStatus();
                foreach (var batch in selectedIds2.Batch(2000))
                {
                    string selectedIds = String.Join(",", batch);
                    Obj.EditIncomeAccountsTreeAccountDecline(selectedIds);
                }

                return request.CreateResponse(HttpStatusCode.OK);
            });
        }

    }
}
