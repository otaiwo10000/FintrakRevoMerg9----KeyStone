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
    [RoutePrefix("api/incomecaptionpoolrate")]
    [UsesDisposableService]
    public class IncomeCaptionPoolRateApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomeCaptionPoolRateApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }

        [HttpPost]
        [Route("updateincomecaptionpoolrate")]
        public HttpResponseMessage UpdateIncomeCaptionPoolRate(HttpRequestMessage request, [FromBody]IncomeCaptionPoolRate incomeCaptionPoolRateModel)
        {
            return GetHttpResponse(request, () =>
            {
                var incomeCaptionPoolRate = _MPRCoreService.UpdateIncomeCaptionPoolRate(incomeCaptionPoolRateModel);

                return request.CreateResponse<IncomeCaptionPoolRate>(HttpStatusCode.OK, incomeCaptionPoolRate);
            });
        }


        [HttpPost]
        [Route("deleteincomecaptionpoolrate")]
        public HttpResponseMessage DeleteIncomeCaptionPoolRate(HttpRequestMessage request, [FromBody]int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IncomeCaptionPoolRate incomeCaptionPoolRate = _MPRCoreService.GetIncomeCaptionPoolRate(ID);

                if (incomeCaptionPoolRate != null)
                {
                    _MPRCoreService.DeleteIncomeCaptionPoolRate(ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No IncomeCaptionPoolRate found under that ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getincomecaptionpoolrate/{ID}")]
        public HttpResponseMessage GetIncomeCaptionPoolRate(HttpRequestMessage request, int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IncomeCaptionPoolRate incomeCaptionPoolRate = _MPRCoreService.GetIncomeCaptionPoolRate(ID);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<IncomeCaptionPoolRate>(HttpStatusCode.OK, incomeCaptionPoolRate);

                return response;
            });
        }


        [HttpGet]
        [Route("availableincomecaptionpoolrate")]
        public HttpResponseMessage GetAvailableIncomeCaptionPoolRate(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeCaptionPoolRate[] incomeCaptionPoolRate = _MPRCoreService.GetAllIncomeCaptionPoolRate();

                return request.CreateResponse<IncomeCaptionPoolRate[]>(HttpStatusCode.OK, incomeCaptionPoolRate);
            });
        }

        [HttpGet]
        [Route("getincomecaptionpoolrateusingsearch/{searchvalue}")]
        public HttpResponseMessage GetIncomeCaptionPoolRateUsingSearchValue(HttpRequestMessage request, string searchvalue)
        {
            return GetHttpResponse(request, () =>
            {
                searchvalue = searchvalue.Replace("FORWARDSLASHXTER", "/");
                searchvalue = searchvalue.Replace("DOTXTER", ".");

                HttpResponseMessage response = null;
                IncomeCaptionPoolRate[] incomeCaptionPoolRate = _MPRCoreService.GetIncomeCaptionPoolRateUsingSearchValue(searchvalue);

                // notice no need to create a seperate model object since TeamDefinition entity will do just fine
                response = request.CreateResponse<IncomeCaptionPoolRate[]>(HttpStatusCode.OK, incomeCaptionPoolRate);

                return response;
            });
        }
    }
}
