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
    [RoutePrefix("api/incomecustomerpoolrate")]
    [UsesDisposableService]
    public class IncomeCustomerPoolRateApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomeCustomerPoolRateApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }

        [HttpPost]
        [Route("updateincomecustomerpoolrate")]
        public HttpResponseMessage UpdateIncomeCustomerPoolRate(HttpRequestMessage request, [FromBody]IncomeCustomerPoolRate incomeCustomerPoolRateModel)
        {
            return GetHttpResponse(request, () =>
            {
                var incomeCustomerPoolRate = _MPRCoreService.UpdateIncomeCustomerPoolRate(incomeCustomerPoolRateModel);

                return request.CreateResponse<IncomeCustomerPoolRate>(HttpStatusCode.OK, incomeCustomerPoolRate);
            });
        }

        [HttpPost]
        [Route("deleteincomecustomerpoolrate")]
        public HttpResponseMessage DeleteIncomeCustomerPoolRate(HttpRequestMessage request, [FromBody]int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IncomeCustomerPoolRate incomeCustomerPoolRate = _MPRCoreService.GetIncomeCustomerPoolRate(Id);

                if (incomeCustomerPoolRate != null)
                {
                    _MPRCoreService.DeleteIncomeCustomerPoolRate(Id);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No IncomeCustomerPoolRate found under that ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getincomecustomerpoolrate/{Id}")]
        public HttpResponseMessage GetIncomeCustomerPoolRate(HttpRequestMessage request, int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IncomeCustomerPoolRate incomeCustomerPoolRate = _MPRCoreService.GetIncomeCustomerPoolRate(Id);

                // notice no need to create a seperate model object since CustomerMapping entity will do just fine
                response = request.CreateResponse<IncomeCustomerPoolRate>(HttpStatusCode.OK, incomeCustomerPoolRate);

                return response;
            });
        }


        [HttpGet]
        [Route("availableincomecustomerpoolrate")]
        public HttpResponseMessage GetAvailableIncomeCustomerPoolRate(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeCustomerPoolRate[] incomeCustomerPoolRate = _MPRCoreService.GetAllIncomeCustomerPoolRate();

                return request.CreateResponse<IncomeCustomerPoolRate[]>(HttpStatusCode.OK, incomeCustomerPoolRate);
            });
        }

        [HttpGet]
        [Route("getincomecustomerpoolrateusingsearch/{searchvalue}")]
        public HttpResponseMessage GetIncomeCustomerPoolRateUsingSearchValue(HttpRequestMessage request, string searchvalue)
        {
            return GetHttpResponse(request, () =>
            {
                searchvalue = searchvalue.Replace("FORWARDSLASHXTER", "/");
                searchvalue = searchvalue.Replace("DOTXTER", ".");

                HttpResponseMessage response = null;
                IncomeCustomerPoolRate[] incomeCustomerPoolRate = _MPRCoreService.GetIncomeCustomerPoolRateUsingSearchValue(searchvalue);

                // notice no need to create a seperate model object since TeamDefinition entity will do just fine
                response = request.CreateResponse<IncomeCustomerPoolRate[]>(HttpStatusCode.OK, incomeCustomerPoolRate);

                return response;
            });
        }
    }
}
