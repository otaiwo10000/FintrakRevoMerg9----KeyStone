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
    [RoutePrefix("api/incomesplitpoolsrate")]
    [UsesDisposableService]
    public class IncomeSplitPoolsRateApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomeSplitPoolsRateApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }

        [HttpPost]
        [Route("updateincomesplitpoolsrate")]
        public HttpResponseMessage UpdateIncomeSplitPoolsRate(HttpRequestMessage request, [FromBody]IncomeSplitPoolsRate incomeSplitPoolsRateModel)
        {
            return GetHttpResponse(request, () =>
            {
                var incomeSplitPoolsRate = _MPRCoreService.UpdateIncomeSplitPoolsRate(incomeSplitPoolsRateModel);

                return request.CreateResponse<IncomeSplitPoolsRate>(HttpStatusCode.OK, incomeSplitPoolsRate);
            });
        }


        [HttpPost]
        [Route("deleteincomesplitpoolsrate")]
        public HttpResponseMessage DeleteIncomeSplitPoolsRate(HttpRequestMessage request, [FromBody]int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IncomeSplitPoolsRate incomeSplitPoolsRate = _MPRCoreService.GetIncomeSplitPoolsRate(ID);

                if (incomeSplitPoolsRate != null)
                {
                    _MPRCoreService.DeleteIncomeSplitPoolsRate(ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No IncomeSplitPoolsRate found under that ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getincomesplitpoolsrate/{ID}")]
        public HttpResponseMessage GetIncomeSplitPoolsRate(HttpRequestMessage request, int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IncomeSplitPoolsRate incomeSplitPoolsRate = _MPRCoreService.GetIncomeSplitPoolsRate(ID);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<IncomeSplitPoolsRate>(HttpStatusCode.OK, incomeSplitPoolsRate);

                return response;
            });
        }


        [HttpGet]
        [Route("availableincomesplitpoolsrate")]
        public HttpResponseMessage GetAvailableIncomeSplitPoolsRate(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeSplitPoolsRate[] incomeSplitPoolsRate = _MPRCoreService.GetAllIncomeSplitPoolsRate();

                return request.CreateResponse<IncomeSplitPoolsRate[]>(HttpStatusCode.OK, incomeSplitPoolsRate);
            });
        }
    }
}
