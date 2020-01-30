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
    [RoutePrefix("api/incomeaccountstreemiscodestemp")]
    [UsesDisposableService]
    public class IncomeAccountsTreeMisCodesTEMPApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomeAccountsTreeMisCodesTEMPApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }

        [HttpPost]
        [Route("updateincomeaccountstreemiscodestemp")]
        public HttpResponseMessage UpdateIncomeAccountsTreeMisCodesTEMP(HttpRequestMessage request, [FromBody]IncomeAccountsTreeMisCodesTEMP incomeAccountsTreeMisCodesModel)
        {
            return GetHttpResponse(request, () =>
            {
                var incomeAccountsTreeMisCodes = _MPRCoreService.UpdateIncomeAccountsTreeMisCodesTEMP(incomeAccountsTreeMisCodesModel);

                return request.CreateResponse<IncomeAccountsTreeMisCodesTEMP>(HttpStatusCode.OK, incomeAccountsTreeMisCodes);
            });
        }


        [HttpPost]
        [Route("deleteincomeaccountstreemiscodestemp")]
        public HttpResponseMessage DeleteIncomeAccountsTreeMisCodesTEMP(HttpRequestMessage request, [FromBody]int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IncomeAccountsTreeMisCodesTEMP incomeAccountsTreeMisCodes = _MPRCoreService.GetIncomeAccountsTreeMisCodesTEMP(ID);

                if (incomeAccountsTreeMisCodes != null)
                {
                    _MPRCoreService.DeleteIncomeAccountsTreeMisCodesTEMP(ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No IncomeAccountsTreeMisCodes found under that ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getincomeaccountstreemiscodestemp/{ID}")]
        public HttpResponseMessage GetIncomeAccountsTreeMisCodesTEMP(HttpRequestMessage request, int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IncomeAccountsTreeMisCodesTEMP incomeAccountsTreeMisCodes = _MPRCoreService.GetIncomeAccountsTreeMisCodesTEMP(ID);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<IncomeAccountsTreeMisCodesTEMP>(HttpStatusCode.OK, incomeAccountsTreeMisCodes);

                return response;
            });
        }


        [HttpGet]
        [Route("availableincomeaccountstreemiscodestemp")]
        public HttpResponseMessage GetAvailableIncomeAccountsTreeMisCodesTEMP(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeAccountsTreeMisCodesTEMP[] incomeAccountsTreeMisCodes = _MPRCoreService.GetAllIncomeAccountsTreeMisCodesTEMP();

                return request.CreateResponse<IncomeAccountsTreeMisCodesTEMP[]>(HttpStatusCode.OK, incomeAccountsTreeMisCodes);
            });
        }

        [HttpGet]
        [Route("incomeaccountstreemiscodestempbysearch/{search}")]
        public HttpResponseMessage GeIncomeAccountsTreeMisCodesTEMPUsingSearch(HttpRequestMessage request, string search)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeAccountsTreeMisCodesTEMP[] incomeAccountsTreeAccount = _MPRCoreService.GetIncomeAccountsTreeMisCodesTEMPBySearchVal(search);

                return request.CreateResponse<IncomeAccountsTreeMisCodesTEMP[]>(HttpStatusCode.OK, incomeAccountsTreeAccount);
            });
        }

        //======================== awaiting starts ================================================================================

        [HttpGet]
        [Route("incomeaccountstreemiscodesAWAITING")]
        public HttpResponseMessage IncomeAccountsTreeMisCodesTEMPAwaiting(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "AWAITINGAPPROVAL";
                IncomeAccountsTreeMisCodesTEMPStatus obj = new IncomeAccountsTreeMisCodesTEMPStatus();
                var ddb = obj.IncomeAccountsTreeMisCodesTEMP(approvalstatus);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        [HttpGet]
        [Route("incomeaccountstreemiscodesusingparamsAWAITING/{search}")]
        public HttpResponseMessage IncomeAccountMISOverrideUsingParamsAwaiting(HttpRequestMessage request, string search)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "AWAITINGAPPROVAL";
                IncomeAccountsTreeMisCodesTEMPStatus obj = new IncomeAccountsTreeMisCodesTEMPStatus();
                var ddb = obj.IncomeAccountsTreeMisCodesTEMPusingparams(approvalstatus, search);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        //======================== awaiting ends ================================================================================

        //======================== approved starts ================================================================================

        [HttpGet]
        [Route("incomeaccountstreemiscodesAPPROVED")]
        public HttpResponseMessage IncomeAccountMISOverrideApproved(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "APPROVED";
                IncomeAccountsTreeMisCodesTEMPStatus obj = new IncomeAccountsTreeMisCodesTEMPStatus();
                var ddb = obj.IncomeAccountsTreeMisCodesTEMP(approvalstatus);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        [HttpGet]
        [Route("incomeaccountstreemiscodesusingparamsAPPROVED/{search}")]
        public HttpResponseMessage TeamUsingParamsApproved(HttpRequestMessage request, string search)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "APPROVED";
                IncomeAccountsTreeMisCodesTEMPStatus obj = new IncomeAccountsTreeMisCodesTEMPStatus();
                var ddb = obj.IncomeAccountsTreeMisCodesTEMPusingparams(approvalstatus, search);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        //======================== approved ends ================================================================================

        //======================== declined starts ================================================================================

        [HttpGet]
        [Route("incomeaccountstreemiscodesDECLINED")]
        public HttpResponseMessage IncomeAccountsTreeMisCodesDECLINED(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "DECLINED";
                IncomeAccountsTreeMisCodesTEMPStatus obj = new IncomeAccountsTreeMisCodesTEMPStatus();
                var ddb = obj.IncomeAccountsTreeMisCodesTEMP(approvalstatus);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        [HttpGet]
        [Route("incomeaccountstreemiscodesusingparamsDECLINED/{search}")]
        public HttpResponseMessage IncomeAccountsTreeMisCodesusingparamsDECLINED(HttpRequestMessage request, string search)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "DECLINED";
                IncomeAccountsTreeMisCodesTEMPStatus obj = new IncomeAccountsTreeMisCodesTEMPStatus();
                var ddb = obj.IncomeAccountsTreeMisCodesTEMPusingparams(approvalstatus, search);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        //======================== declined ends ================================================================================

        [HttpPost]
        //[Route("incomeaccountstreemiscodesapproval/{selectedIds}")]
        [Route("incomeaccountstreemiscodesapproval")]
        public HttpResponseMessage EditIncomeAccountsTreeMisCodesApproval(HttpRequestMessage request, [FromBody]string[] selectedIds2)
       // public HttpResponseMessage EditIncomeAccountsTreeMisCodesApproval(HttpRequestMessage request, [FromBody]string selectedIds)
        {
            return GetHttpResponse(request, () =>
            {
                //IncomeAccountsTreeMisCodesTEMPStatus Obj = new IncomeAccountsTreeMisCodesTEMPStatus();
                //string selectedIds = String.Join(",", selectedIds2);
                //Obj.EditIncomeAccountsTreeMisCodesApproval(selectedIds);

                IncomeAccountsTreeMisCodesTEMPStatus Obj = new IncomeAccountsTreeMisCodesTEMPStatus();
                foreach (var batch in selectedIds2.Batch(2000))
                {
                    string selectedIds = String.Join(",", batch);
                    Obj.EditIncomeAccountsTreeMisCodesApproval(selectedIds);
                }

                return request.CreateResponse(HttpStatusCode.OK);
            });
        }

        [HttpPost]
        //[Route("incomeaccountstreemiscodesdecline/{selectedIds}")]
        [Route("incomeaccountstreemiscodesdecline")]
        public HttpResponseMessage EditIncomeAccountsTreeMisCodesDecline(HttpRequestMessage request, [FromBody]string[] selectedIds2)
        {
            return GetHttpResponse(request, () =>
            {
                //IncomeAccountsTreeMisCodesTEMPStatus tObj = new IncomeAccountsTreeMisCodesTEMPStatus();
                //string selectedIds = String.Join(",", selectedIds2);
                //tObj.EditIncomeAccountsTreeMisCodesDecline(selectedIds);

                IncomeAccountsTreeMisCodesTEMPStatus Obj = new IncomeAccountsTreeMisCodesTEMPStatus();
                foreach (var batch in selectedIds2.Batch(2000))
                {
                    string selectedIds = String.Join(",", batch);
                    Obj.EditIncomeAccountsTreeMisCodesDecline(selectedIds);
                }

                return request.CreateResponse(HttpStatusCode.OK);
            });
        }


    }
}
