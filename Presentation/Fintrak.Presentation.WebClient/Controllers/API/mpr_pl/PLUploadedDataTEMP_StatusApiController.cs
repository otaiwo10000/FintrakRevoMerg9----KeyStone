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
    [RoutePrefix("api/pluploadedataTEMPstatus")]
    [UsesDisposableService]
    public class PLUploadedDataTEMPStatusApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public PLUploadedDataTEMPStatusApiController(IMPRPLService mprPLService)
        {
            _MPRPLService = mprPLService;
        }

        IMPRPLService _MPRPLService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRPLService);
        }

        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

        //======================== awaiting starts ================================================================================

        [HttpGet]
        [Route("pluploadeddataAWAITING")]
        public HttpResponseMessage PLUploadedDataAwaiting(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "AWAITINGAPPROVAL";
                PLUploadedDataTEMPStatus obj = new PLUploadedDataTEMPStatus();
                var ddb = obj.PLUploadedDataTEMP(approvalstatus);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        [HttpGet]
        [Route("pluploadeddatausingparamsAWAITING/{search}")]
        public HttpResponseMessage PLUploadedDataUsingParamsAwaiting(HttpRequestMessage request, string search)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "AWAITINGAPPROVAL";
                PLUploadedDataTEMPStatus obj = new PLUploadedDataTEMPStatus();
                var ddb = obj.PLUploadedDataTEMPusingparams(approvalstatus, search);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        //======================== awaiting ends ================================================================================

        //======================== approved starts ================================================================================

        [HttpGet]
        [Route("pluploadeddataAPPROVED")]
        public HttpResponseMessage PLUploadedDataApproved(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "APPROVED";
                PLUploadedDataTEMPStatus obj = new PLUploadedDataTEMPStatus();
                var ddb = obj.PLUploadedDataTEMP(approvalstatus);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        [HttpGet]
        [Route("pluploadeddatausingparamsAPPROVED/{search}")]
        public HttpResponseMessage PLUploadedDataUsingParamsApproved(HttpRequestMessage request, string search)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "APPROVED";
                PLUploadedDataTEMPStatus obj = new PLUploadedDataTEMPStatus();
                var ddb = obj.PLUploadedDataTEMPusingparams(approvalstatus, search);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        //======================== approved ends ================================================================================

        //======================== declined starts ================================================================================

        [HttpGet]
        [Route("pluploadeddataDECLINED")]
        public HttpResponseMessage PLUploadedDataDECLINED(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "DECLINED";
                PLUploadedDataTEMPStatus obj = new PLUploadedDataTEMPStatus();
                var ddb = obj.PLUploadedDataTEMP(approvalstatus);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        [HttpGet]
        [Route("pluploadeddatausingparamsDECLINED/{search}")]
        public HttpResponseMessage PLUploadedDataTEMPusingparamsDECLINED(HttpRequestMessage request, string search)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "DECLINED";
                PLUploadedDataTEMPStatus obj = new PLUploadedDataTEMPStatus();
                var ddb = obj.PLUploadedDataTEMPusingparams(approvalstatus, search);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        //======================== declined ends ================================================================================

        [HttpPost]
        //[Route("incomeaccountMISoverrideapproval/{selectedIds}")]
        [Route("pluploadeddataapproval")]
        public HttpResponseMessage EditPLUploadedDataApproval(HttpRequestMessage request, [FromBody]string[] selectedIds2)
        {
            return GetHttpResponse(request, () =>
            {
                //IncomeAccountMISOverrideTEMPStatus Obj = new IncomeAccountMISOverrideTEMPStatus();
                //string selectedIds = String.Join(",", selectedIds2);
                //Obj.EditIncomeAccountMISOverrideApproval(selectedIds);

                //int counter = 1;

                PLUploadedDataTEMPStatus Obj = new PLUploadedDataTEMPStatus();
                foreach (var batch in selectedIds2.Batch(2000))
                {
                    //foreach (var eachId in batch)
                    //{
                    //Console.WriteLine("Batch: {0}, Id: {1}", counter, eachId);
                    //}
                    // counter++;

                    string selectedIds = String.Join(",", batch);
                    Obj.EditPLUploadedDataApproval(selectedIds);
                }


                return request.CreateResponse(HttpStatusCode.OK);
            });
        }

        [HttpPost]
        //[Route("incomeaccountMISoverridedecline/{selectedIds}")]
        [Route("pluploadeddatadecline")]
        public HttpResponseMessage EditPLUploadedDataDecline(HttpRequestMessage request, [FromBody]string[] selectedIds2)
        {
            return GetHttpResponse(request, () =>
            {
                //IncomeAccountMISOverrideTEMPStatus tObj = new IncomeAccountMISOverrideTEMPStatus();
                //string selectedIds = String.Join(",", selectedIds2);
                //tObj.EditIncomeAccountMISOverrideDecline(selectedIds);

                PLUploadedDataTEMPStatus Obj = new PLUploadedDataTEMPStatus();
                foreach (var batch in selectedIds2.Batch(2000))
                {
                    string selectedIds = String.Join(",", batch);
                    Obj.EditPLUploadedDataDecline(selectedIds);
                }

                return request.CreateResponse(HttpStatusCode.OK);
            });
        }


        


    }
}
