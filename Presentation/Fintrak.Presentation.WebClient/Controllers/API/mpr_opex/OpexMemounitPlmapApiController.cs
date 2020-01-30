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
    [RoutePrefix("api/opexmemounitplmap")]
    [UsesDisposableService]
    public class OpexMemounitPlmapApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public OpexMemounitPlmapApiController(IMPROPEXService mprOpexService)
        {
            _MPROPEXService = mprOpexService;
        }

        IMPROPEXService _MPROPEXService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPROPEXService);
        }

        [HttpPost]
        [Route("updateopexmemounitplmap")]
        public HttpResponseMessage UpdateOpexMemounitplmap(HttpRequestMessage request, [FromBody]OpexMemounitPlmap opexModel)
        {
            return GetHttpResponse(request, () =>
            {
                var opex = _MPROPEXService.UpdateOpexMemounitPlmap(opexModel);

                return request.CreateResponse<OpexMemounitPlmap>(HttpStatusCode.OK, opex);
            });
        }

        [HttpPost]
        [Route("deleteopexmemounitplmap")]
        public HttpResponseMessage DeleteOpexMemounitPlmap(HttpRequestMessage request, [FromBody]int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                OpexMemounitPlmap opex = _MPROPEXService.GetOpexMemounitPlmap(ID);

                if (opex != null)
                {
                    _MPROPEXService.DeleteOpexMemounitPlmap(ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No OpexMemounitPlmap found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getopexmemounitplmap/{ID}")]
        public HttpResponseMessage GetOpexMemounitPlmap(HttpRequestMessage request, int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                OpexMemounitPlmap opex = _MPROPEXService.GetOpexMemounitPlmap(ID);

                // notice no need to create a seperate model object since ActivityBase entity will do just fine
                response = request.CreateResponse<OpexMemounitPlmap>(HttpStatusCode.OK, opex);

                return response;
            });
        }

        [HttpGet]
        [Route("availableopexmemounitplmap")]
        public HttpResponseMessage GetAvailableOpexMemounitPlmap(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                OpexMemounitPlmap[] opex = _MPROPEXService.GetAllOpexMemounitPlmap();

                return request.CreateResponse<OpexMemounitPlmap[]>(HttpStatusCode.OK, opex);
            });
        }
    }
}
