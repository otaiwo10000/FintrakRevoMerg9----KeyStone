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
    [RoutePrefix("api/incomeaccountpoolrate")]
    [UsesDisposableService]
    public class IncomeAccountPoolRateApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomeAccountPoolRateApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }

        [HttpPost]
        [Route("updateincomeaccountpoolrate")]
        public HttpResponseMessage UpdateIncomeAccountPoolRate(HttpRequestMessage request, [FromBody]IncomeAccountPoolRate incomeAccountPoolRateModel)
        {
            return GetHttpResponse(request, () =>
            {
                var incomeAccountPoolRate = _MPRCoreService.UpdateIncomeAccountPoolRate(incomeAccountPoolRateModel);

                return request.CreateResponse<IncomeAccountPoolRate>(HttpStatusCode.OK, incomeAccountPoolRate);
            });
        }

        [HttpPost]
        [Route("deleteincomeaccountpoolrate")]
        public HttpResponseMessage DeleteIncomeAccountPoolRate(HttpRequestMessage request, [FromBody]int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IncomeAccountPoolRate incomeAccountPoolRate = _MPRCoreService.GetIncomeAccountPoolRate(Id);

                if (incomeAccountPoolRate != null)
                {
                    _MPRCoreService.DeleteIncomeAccountPoolRate(Id);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No IncomeAccountPoolRate found under that ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getincomeaccountpoolrate/{Id}")]
        public HttpResponseMessage GetIncomeAccountPoolRate(HttpRequestMessage request, int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IncomeAccountPoolRate incomeAccountPoolRate = _MPRCoreService.GetIncomeAccountPoolRate(Id);

                // notice no need to create a seperate model object since CustomerMapping entity will do just fine
                response = request.CreateResponse<IncomeAccountPoolRate>(HttpStatusCode.OK, incomeAccountPoolRate);

                return response;
            });
        }


        [HttpGet]
        [Route("availableincomeaccountpoolrate")]
        public HttpResponseMessage GetAvailableIncomeAccountPoolRate(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeAccountPoolRate[] incomeAccountPoolRate = _MPRCoreService.GetAllIncomeAccountPoolRate();

                return request.CreateResponse<IncomeAccountPoolRate[]>(HttpStatusCode.OK, incomeAccountPoolRate);
            });
        }

        [HttpGet]
        [Route("getincomeaccountpoolrateusingsearch/{searchvalue}")]
        public HttpResponseMessage GetIncomeAccountPoolRateUsingSearchValue(HttpRequestMessage request, string searchvalue)
        {
            return GetHttpResponse(request, () =>
            {
                searchvalue = searchvalue.Replace("FORWARDSLASHXTER", "/");
                searchvalue = searchvalue.Replace("DOTXTER", ".");

                HttpResponseMessage response = null;
                IncomeAccountPoolRate[] incomeAccountPoolRate = _MPRCoreService.GetIncomeAccountPoolRateUsingSearchValue(searchvalue);

                // notice no need to create a seperate model object since TeamDefinition entity will do just fine
                response = request.CreateResponse<IncomeAccountPoolRate[]>(HttpStatusCode.OK, incomeAccountPoolRate);

                return response;
            });
        }
    }
}
