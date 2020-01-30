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
    [RoutePrefix("api/customermis")]
    [UsesDisposableService]
    public class CustomermisApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public CustomermisApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }

        [HttpPost]
        [Route("updatecustomermis")]
        public HttpResponseMessage UpdateCustomermis(HttpRequestMessage request, [FromBody]Customermis customermisModel)
        {
            return GetHttpResponse(request, () =>
            {
                var customermis = _MPRCoreService.UpdateCustomermis(customermisModel);

                return request.CreateResponse<Customermis>(HttpStatusCode.OK, customermis);
            });
        }


        [HttpPost]
        [Route("deletecustomermis")]
        public HttpResponseMessage DeleteCustomermis(HttpRequestMessage request, [FromBody]int CustomermisId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                Customermis customermis = _MPRCoreService.GetCustomermis(CustomermisId);

                if (customermis != null)
                {
                    _MPRCoreService.DeleteCustomermis(CustomermisId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Customer MIS found under that ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getcustomermis/{CustomermisId}")]
        public HttpResponseMessage GetRatios(HttpRequestMessage request, int CustomermisId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                Customermis customermis = _MPRCoreService.GetCustomermis(CustomermisId);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<Customermis>(HttpStatusCode.OK, customermis);

                return response;
            });
        }


        [HttpGet]
        [Route("availablecmis")]
        public HttpResponseMessage GetAvailableCMIS(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                Customermis[] customermis = _MPRCoreService.GetAllCustomermis();

                return request.CreateResponse<Customermis[]>(HttpStatusCode.OK, customermis);
            });
        }
    }
}
