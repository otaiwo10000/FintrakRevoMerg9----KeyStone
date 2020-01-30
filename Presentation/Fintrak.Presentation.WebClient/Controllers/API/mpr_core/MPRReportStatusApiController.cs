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
using Fintrak.Presentation.WebClient.Additionalmethods;
using Fintrak.Presentation.WebClient.Models;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/mprreportstatus")]
    [UsesDisposableService]
    public class MPRReportStatusApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public MPRReportStatusApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }

        string clientcode = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["ClientCode"]).Trim().ToUpper();

        [HttpPost]
        [Route("updatemprreportstatus")]
        public HttpResponseMessage UpdateMPRReportStatus(HttpRequestMessage request, [FromBody]MPRReportStatus mprreportstatusModel)
        {
            return GetHttpResponse(request, () =>
            {
                MPRReportStatus mprreportstatus = null;

                if (clientcode == "ABP")
                {
                    MPRReportStatusMtd ob = new MPRReportStatusMtd();
                    ob.UpdateMPRReportStatus(mprreportstatusModel);
                }
                else
                {
                    mprreportstatus = _MPRCoreService.UpdateMPRReportStatus(mprreportstatusModel);
                }

                //return request.CreateResponse<MPRReportStatus>(HttpStatusCode.OK, mprreportstatus);
                return request.CreateResponse(HttpStatusCode.OK);
            });
        }


        //[HttpPost]
        //[Route("deleteabcratio")]
        //public HttpResponseMessage DeleteAbcRatio(HttpRequestMessage request, [FromBody]int abcRatioId)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;

        //        // not that calling the WCF service here will authenticate access to the data
        //        AbcRatio abcRatio = _MPRCoreService.GetAbcRatio(abcRatioId);

        //        if (abcRatio != null)
        //        {
        //            _MPRCoreService.DeleteAbcRatio(abcRatioId);

        //            response = request.CreateResponse(HttpStatusCode.OK);
        //        }
        //        else
        //            response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No AbcRatio found under that ID.");

        //        return response;
        //    });
        //}


        [HttpGet]
        [Route("getmprreportstatus/{MPRReportStatusId}")]
        public HttpResponseMessage GetMPRReportStatus(HttpRequestMessage request, int MPRReportStatusId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                MPRReportStatus mprreportstatus = null;

                if (clientcode == "ABP")
                {
                    MPRReportStatusMtd ob = new MPRReportStatusMtd();
                    mprreportstatus = ob.GetMPRReportStatus(MPRReportStatusId);
                }
                else
                {
                    mprreportstatus = _MPRCoreService.GetMPRReportStatus(MPRReportStatusId);
                }
                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<MPRReportStatus>(HttpStatusCode.OK, mprreportstatus);

                return response;
            });
        }


        [HttpGet]
        [Route("availablemprreportstatus")]
        public HttpResponseMessage GetAvailableMPRReportStatus(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                List<MPRReportStatus> mprreportstatus = null;

                if (clientcode == "ABP")
                {
                    MPRReportStatusMtd ob = new MPRReportStatusMtd();
                    mprreportstatus = ob.GetAllMPRReportStatus();
                }
                else
                {
                    mprreportstatus = _MPRCoreService.GetAllMPRReportStatus().ToList();
                }
                //MPRReportStatus[] mprreportstatus = _MPRCoreService.GetAllMPRReportStatus();

                //return request.CreateResponse<MPRReportStatus[]>(HttpStatusCode.OK, mprreportstatus);
                return request.CreateResponse<List<MPRReportStatus>>(HttpStatusCode.OK, mprreportstatus);

            });
        }
    }
}
