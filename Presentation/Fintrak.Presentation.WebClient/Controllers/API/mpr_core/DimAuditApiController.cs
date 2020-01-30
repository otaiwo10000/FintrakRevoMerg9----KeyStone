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
    [RoutePrefix("api/dimaudit")]
    [UsesDisposableService]
    public class DimAuditApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public DimAuditApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }

        [HttpGet]
        [Route("getdimaudit")]
        public HttpResponseMessage GetDimAud(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                DimAuditMtd obj = new DimAuditMtd();
                List<DimAuditModel> dm = obj.GetDimAudit().ToList();

                return request.CreateResponse(HttpStatusCode.OK, dm);

            });
        }

        [HttpGet]
        [Route("getdimauditByParam/{search}")]
        public HttpResponseMessage GetDimAudByParam(HttpRequestMessage request, string search)
        {
            return GetHttpResponse(request, () =>
            {
                DimAuditMtd obj = new DimAuditMtd();
                List<DimAuditModel> dm = obj.GetDimAuditUsingParams(search).ToList();

                return request.CreateResponse(HttpStatusCode.OK, dm);

            });
        }


    }
}
