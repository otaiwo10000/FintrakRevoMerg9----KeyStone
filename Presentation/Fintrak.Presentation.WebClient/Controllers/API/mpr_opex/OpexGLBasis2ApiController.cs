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
    [RoutePrefix("api/opexglbasis2")]
    [UsesDisposableService]
    public class OpexGLBasis2ApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public OpexGLBasis2ApiController(IMPROPEXService mprOpexService)
        {
            _MPROPEXService = mprOpexService;
        }

        IMPROPEXService _MPROPEXService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPROPEXService);
        }

        [HttpPost]
        [Route("updateopexglbasis2")]
        public HttpResponseMessage UpdateOpexGLBasis2(HttpRequestMessage request, [FromBody]OpexGLBasis2 opexModel)
        {
            return GetHttpResponse(request, () =>
            {
                var opex = _MPROPEXService.UpdateOpexGLBasis2(opexModel);

                return request.CreateResponse<OpexGLBasis2>(HttpStatusCode.OK, opex);
            });
        }

        [HttpPost]
        [Route("deleteopexglbasis2")]
        public HttpResponseMessage DeleteOpexGLBasis2(HttpRequestMessage request, [FromBody]int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                OpexGLBasis2 opex = _MPROPEXService.GetOpexGLBasis2(ID);

                if (opex != null)
                {
                    _MPROPEXService.DeleteActivityBase(ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Opex gl basis found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getopexglbasis2/{ID}")]
        public HttpResponseMessage GetOpexGLBasis2(HttpRequestMessage request, int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                OpexGLBasis2 opex = _MPROPEXService.GetOpexGLBasis2(ID);

                // notice no need to create a seperate model object since ActivityBase entity will do just fine
                response = request.CreateResponse<OpexGLBasis2>(HttpStatusCode.OK, opex);

                return response;
            });
        }

        [HttpGet]
        [Route("availableopexglbasis2")]
        public HttpResponseMessage GetAvailableOpexGLBasis2(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                OpexGLBasis2[] opex = _MPROPEXService.GetAllOpexGLBasis2();

                return request.CreateResponse<OpexGLBasis2[]>(HttpStatusCode.OK, opex);
            });
        }
    }
}
