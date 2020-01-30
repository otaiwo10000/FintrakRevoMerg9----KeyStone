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
    [RoutePrefix("api/opexupdate")]
    [UsesDisposableService]
    public class OpexUpdateApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public OpexUpdateApiController(IMPROPEXService mprOpexService)
        {
            _MPROPEXService = mprOpexService;
        }

        IMPROPEXService _MPROPEXService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPROPEXService);
        }

        [HttpPost]
        [Route("updateopexupdate")]
        public HttpResponseMessage UpdateOpexUpdate(HttpRequestMessage request, [FromBody]OpexUpdate opexModel)
        {
            return GetHttpResponse(request, () =>
            {
                var opex = _MPROPEXService.UpdateOpexUpdate(opexModel);

                return request.CreateResponse<OpexUpdate>(HttpStatusCode.OK, opex);
            });
        }

        [HttpPost]
        [Route("deleteopexupdate")]
        public HttpResponseMessage DeleteOpexUpdate(HttpRequestMessage request, [FromBody]int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                OpexUpdate opex = _MPROPEXService.GetOpexUpdate(ID);

                if (opex != null)
                {
                    _MPROPEXService.DeleteOpexUpdate(ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No OpexUpdate found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getopexupdate/{ID}")]
        public HttpResponseMessage GetOpexUpdate(HttpRequestMessage request, int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                OpexUpdate opex = _MPROPEXService.GetOpexUpdate(ID);

                // notice no need to create a seperate model object since ActivityBase entity will do just fine
                response = request.CreateResponse<OpexUpdate>(HttpStatusCode.OK, opex);

                return response;
            });
        }

        [HttpGet]
        [Route("availableopexupdate")]
        public HttpResponseMessage GetAvailableOpexUpdate(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                OpexUpdate[] opex = _MPROPEXService.GetAllOpexUpdate();

                return request.CreateResponse<OpexUpdate[]>(HttpStatusCode.OK, opex);
            });
        }
    }
}
