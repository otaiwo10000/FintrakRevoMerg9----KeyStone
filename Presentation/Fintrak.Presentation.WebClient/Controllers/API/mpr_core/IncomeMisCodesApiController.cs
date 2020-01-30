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
    [RoutePrefix("api/incomemiscodes")]
    [UsesDisposableService]
    public class IncomeMisCodesApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomeMisCodesApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }

        [HttpPost]
        [Route("updateincomemiscodes")]
        public HttpResponseMessage UpdateIncomeMisCodes(HttpRequestMessage request, [FromBody]IncomeMisCodes incomeMisCodesModel)
        {
            return GetHttpResponse(request, () =>
            {
                var incomeMisCodes = _MPRCoreService.UpdateIncomeMisCodes(incomeMisCodesModel);

                return request.CreateResponse<IncomeMisCodes>(HttpStatusCode.OK, incomeMisCodes);
            });
        }


        [HttpPost]
        [Route("deleteincomemiscodes")]
        public HttpResponseMessage DeleteIncomeMisCodes(HttpRequestMessage request, [FromBody]int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IncomeMisCodes incomeMisCodes = _MPRCoreService.GetIncomeMisCodes(ID);

                if (incomeMisCodes != null)
                {
                    _MPRCoreService.DeleteIncomeMisCodes(ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No IncomeMisCode found under that ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getincomemiscodes/{ID}")]
        public HttpResponseMessage GetIncomeMisCodes(HttpRequestMessage request, int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IncomeMisCodes incomeMisCodes = _MPRCoreService.GetIncomeMisCodes(ID);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<IncomeMisCodes>(HttpStatusCode.OK, incomeMisCodes);

                return response;
            });
        }


        [HttpGet]
        [Route("availableincomemiscodes")]
        public HttpResponseMessage GetAvailableIncomeMisCodes(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeMisCodes[] incomeMisCodes = _MPRCoreService.GetAllIncomeMisCodes();

                return request.CreateResponse<IncomeMisCodes[]>(HttpStatusCode.OK, incomeMisCodes);
            });
        }
    }
}
