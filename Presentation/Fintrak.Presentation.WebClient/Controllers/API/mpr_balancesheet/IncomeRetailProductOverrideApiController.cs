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
    [RoutePrefix("api/incomeretailproductoverride")]
    [UsesDisposableService]
    public class IncomeRetailProductOverrideApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomeRetailProductOverrideApiController(IMPRBSService mprBSService)
        {
            _MPRBSService = mprBSService;
        }

        IMPRBSService _MPRBSService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRBSService);
        }


        [HttpPost]
        [Route("updateincomeretailproductoverride")]
        public HttpResponseMessage UpdateIncomeRetailProductOverride(HttpRequestMessage request, [FromBody]IncomeRetailProductOverride icprbModel)
        {
            return GetHttpResponse(request, () =>
            {
                var icprb = _MPRBSService.UpdateIncomeRetailProductOverride(icprbModel);

                return request.CreateResponse<IncomeRetailProductOverride>(HttpStatusCode.OK, icprb);
            });
        }

        [HttpPost]
        [Route("deleteincomeretailproductoverride")]
        public HttpResponseMessage DeleteIncomeRetailProductOverride(HttpRequestMessage request, [FromBody]int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IncomeRetailProductOverride icprb = _MPRBSService.GetIncomeRetailProductOverride(Id);

                if (icprb != null)
                {
                    _MPRBSService.DeleteIncomeRetailProductOverride(Id);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getincomeretailproductoverride/{Id}")]
        public HttpResponseMessage GetIncomeRetailProductOverride(HttpRequestMessage request, int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IncomeRetailProductOverride icprb = _MPRBSService.GetIncomeRetailProductOverride(Id);

                // notice no need to create a seperate model object since AcquirerMapping entity will do just fine
                response = request.CreateResponse<IncomeRetailProductOverride>(HttpStatusCode.OK, icprb);

                return response;
            });
        }

        [HttpGet]
        [Route("availableincomeretailproductoverride")]
        public HttpResponseMessage GetAllIncomeRetailProductOverride(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeRetailProductOverride[] icprb = _MPRBSService.GetAllIncomeRetailProductOverride();

                return request.CreateResponse<IncomeRetailProductOverride[]>(HttpStatusCode.OK, icprb);
            });
        }

        [HttpGet]
        [Route("getincomeretailproductoverrideusingcustomeridandbank/{customerId}/{bank}")]
        public HttpResponseMessage GetIncomeRetailProductOverrideUsingCustomerIdandBank(HttpRequestMessage request, int customerId, string bank)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeRetailProductOverride[] icprb = _MPRBSService.OverrideUsingCustomerIdAndBank(customerId, bank);

                return request.CreateResponse<IncomeRetailProductOverride[]>(HttpStatusCode.OK, icprb);
            });
        }

    }
}
