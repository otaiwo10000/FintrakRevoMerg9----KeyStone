using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Client.Core.Contracts;
using Fintrak.Client.Core.Entities;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/incomemonths")]
    [UsesDisposableService]
    public class IncomeMonthsApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomeMonthsApiController(ICoreService coreService)
        {
            _CoreService = coreService;
        }

        ICoreService _CoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_CoreService);
        }

        [HttpPost]
        [Route("updateincomemonths")]
        public HttpResponseMessage UpdateIncomeMonths(HttpRequestMessage request, [FromBody]IncomeMonths imModel)
        {
            return GetHttpResponse(request, () =>
            {
                var im = _CoreService.UpdateIncomeMonths(imModel);

                return request.CreateResponse<IncomeMonths>(HttpStatusCode.OK, im);
            });
        }

        [HttpPost]
        [Route("deleteincomemonths")]
        public HttpResponseMessage DeleteIncomeMonths(HttpRequestMessage request, [FromBody]int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IncomeMonths im = _CoreService.GetIncomeMonths(Id);

                if (im != null)
                {
                    _CoreService.DeleteIncomeMonths(Id);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No staff found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getincomemonths/{Id}")]
        public HttpResponseMessage GetIncomeMonths(HttpRequestMessage request,int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IncomeMonths im = _CoreService.GetIncomeMonths(Id);

                // notice no need to create a seperate model object since Staff entity will do just fine
                response = request.CreateResponse<IncomeMonths>(HttpStatusCode.OK, im);

                return response;
            });
        }

        [HttpGet]
        [Route("availableincomemonths")]
        public HttpResponseMessage GetAvailableIncomeMonths(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeMonths[] im = _CoreService.GetAllIncomeMonths();

                return request.CreateResponse<IncomeMonths[]>(HttpStatusCode.OK, im);
            });
        }
    }
}
