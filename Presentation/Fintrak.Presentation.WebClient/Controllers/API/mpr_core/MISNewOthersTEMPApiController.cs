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

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/misnewothersTEMP")]
    [UsesDisposableService]
    public class MISNewOthersApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public MISNewOthersApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }

        [HttpPost]
        [Route("updatemisnewothers")]
        public HttpResponseMessage UpdateMISNewOthersTEMP(HttpRequestMessage request, [FromBody]MISNewOthersTEMP icprbModel)
        {
            return GetHttpResponse(request, () =>
            {
                var icprb = _MPRCoreService.UpdateMISNewOthersTEMP(icprbModel);

                return request.CreateResponse<MISNewOthersTEMP>(HttpStatusCode.OK, icprb);
            });
        }

        [HttpPost]
        [Route("deletemisnewothers")]
        public HttpResponseMessage DeleteMISNewOthersTEMP(HttpRequestMessage request, [FromBody]int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                MISNewOthersTEMP icprb = _MPRCoreService.GetMISNewOthersTEMP(Id);

                if (icprb != null)
                {
                    _MPRCoreService.DeleteMISNewOthersTEMP(Id);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getmisnewothers/{Id}")]
        public HttpResponseMessage GetIncomeAccountMISOverrideTEMP(HttpRequestMessage request, int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                MISNewOthersTEMP icprb = _MPRCoreService.GetMISNewOthersTEMP(Id);

                // notice no need to create a seperate model object since AcquirerMapping entity will do just fine
                response = request.CreateResponse<MISNewOthersTEMP>(HttpStatusCode.OK, icprb);

                return response;
            });
        }

        [HttpGet]
        [Route("availablemisnewothers")]
        public HttpResponseMessage GetAllMISNewOthersTEMP(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                MISNewOthersTEMP[] icprb = _MPRCoreService.GetAllMISNewOthersTEMP();


                return request.CreateResponse<MISNewOthersTEMP[]>(HttpStatusCode.OK, icprb);
            });
        }

        [HttpGet]
        [Route("getMISnewothersTEMPUsingSearchVal/{search}")]
        public HttpResponseMessage GetMISNewOthersTEMPUsingSearchVal(HttpRequestMessage request, string search)
        {
            return GetHttpResponse(request, () =>
            {
                MISNewOthersTEMP[] icprb = _MPRCoreService.GetMISNewOthersTEMPBySearchVal(search);


                return request.CreateResponse<MISNewOthersTEMP[]>(HttpStatusCode.OK, icprb);
            });
        }
    }
}
