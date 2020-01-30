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
    [RoutePrefix("api/incomecurrency")]
    [UsesDisposableService]
    public class IncomeCurrencyApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomeCurrencyApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }

        [HttpPost]
        [Route("updateincomecurrency")]
        public HttpResponseMessage UpdateIncomeCurrency(HttpRequestMessage request, [FromBody]IncomeCurrency incomeCurrencyModel)
        {
            return GetHttpResponse(request, () =>
            {
                var incomeCurrency = _MPRCoreService.UpdateIncomeCurrency(incomeCurrencyModel);

                return request.CreateResponse<IncomeCurrency>(HttpStatusCode.OK, incomeCurrency);
            });
        }


        [HttpPost]
        [Route("deleteincomecurrency")]
        public HttpResponseMessage DeleteIncomeCurrency(HttpRequestMessage request, [FromBody]int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IncomeCurrency incomeCurrency = _MPRCoreService.GetIncomeCurrency(ID);

                if (incomeCurrency != null)
                {
                    _MPRCoreService.DeleteIncomeCurrency(ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No IncomeCurrency found under that ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getincomecurrency/{ID}")]
        public HttpResponseMessage GetIncomeCurrency(HttpRequestMessage request, int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IncomeCurrency incomeCurrency = _MPRCoreService.GetIncomeCurrency(ID);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<IncomeCurrency>(HttpStatusCode.OK, incomeCurrency);

                return response;
            });
        }


        [HttpGet]
        [Route("availableincomecurrency")]
        public HttpResponseMessage GetAvailableIncomeCurrency(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeCurrency[] incomeCurrency = _MPRCoreService.GetAllIncomeCurrency();

                return request.CreateResponse<IncomeCurrency[]>(HttpStatusCode.OK, incomeCurrency);
            });
        }
    }
}
