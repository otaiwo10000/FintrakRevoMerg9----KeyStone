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
    [RoutePrefix("api/incomepoolratesbu")]
    [UsesDisposableService]
    public class IncomePoolRateSbuApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomePoolRateSbuApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }

        [HttpPost]
        [Route("updateincomepoolratesbu")]
        public HttpResponseMessage UpdateIncomePoolRateSbu(HttpRequestMessage request, [FromBody]IncomePoolRateSbu incomePoolRateSbuModel)
        {
            return GetHttpResponse(request, () =>
            {
                var incomePoolRateSbu = _MPRCoreService.UpdateIncomePoolRateSbu(incomePoolRateSbuModel);

                return request.CreateResponse<IncomePoolRateSbu>(HttpStatusCode.OK, incomePoolRateSbu);
            });
        }


        [HttpPost]
        [Route("deleteincomepoolratesbu")]
        public HttpResponseMessage DeleteIncomePoolRateSbu(HttpRequestMessage request, [FromBody]int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IncomePoolRateSbu incomePoolRateSbu = _MPRCoreService.GetIncomePoolRateSbu(ID);

                if (incomePoolRateSbu != null)
                {
                    _MPRCoreService.DeleteIncomePoolRateSbu(ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No IncomePoolRateSbu found under that ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getincomepoolratesbu/{ID}")]
        public HttpResponseMessage GetIncomePoolRateSbu(HttpRequestMessage request, int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IncomePoolRateSbu incomePoolRateSbu = _MPRCoreService.GetIncomePoolRateSbu(ID);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<IncomePoolRateSbu>(HttpStatusCode.OK, incomePoolRateSbu);

                return response;
            });
        }


        [HttpGet]
        [Route("availableincomepoolratesbu")]
        public HttpResponseMessage GetAvailableIncomePoolRateSbu(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IncomePoolRateSbu[] incomePoolRateSbu = _MPRCoreService.GetAllIncomePoolRateSbu();

                return request.CreateResponse<IncomePoolRateSbu[]>(HttpStatusCode.OK, incomePoolRateSbu);
            });
        }
    }
}
