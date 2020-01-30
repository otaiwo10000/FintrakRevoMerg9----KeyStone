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
    [RoutePrefix("api/incomeretailproductoverrideTEMP")]
    [UsesDisposableService]
    public class IncomeRetailProductOverrideTEMPApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomeRetailProductOverrideTEMPApiController(IMPRBSService mprBSService)
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
        public HttpResponseMessage UpdateIncomeRetailProductOverrideTEMP(HttpRequestMessage request, [FromBody]IncomeRetailProductOverrideTEMP icprbModel)
        {
            return GetHttpResponse(request, () =>
            {
                var icprb = _MPRBSService.UpdateIncomeRetailProductOverrideTEMP(icprbModel);

                return request.CreateResponse<IncomeRetailProductOverrideTEMP>(HttpStatusCode.OK, icprb);
            });
        }

        [HttpPost]
        [Route("deleteincomeretailproductoverride")]
        public HttpResponseMessage DeleteIncomeRetailProductOverrideTEMP(HttpRequestMessage request, [FromBody]int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IncomeRetailProductOverrideTEMP icprb = _MPRBSService.GetIncomeRetailProductOverrideTEMP(Id);

                if (icprb != null)
                {
                    _MPRBSService.DeleteIncomeRetailProductOverrideTEMP(Id);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getincomeretailproductoverride/{Id}")]
        public HttpResponseMessage GetIncomeRetailProductOverrideTEMP(HttpRequestMessage request, int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IncomeRetailProductOverrideTEMP icprb = _MPRBSService.GetIncomeRetailProductOverrideTEMP(Id);

                // notice no need to create a seperate model object since AcquirerMapping entity will do just fine
                response = request.CreateResponse<IncomeRetailProductOverrideTEMP>(HttpStatusCode.OK, icprb);

                return response;
            });
        }

        [HttpGet]
        [Route("availableincomeretailproductoverride")]
        public HttpResponseMessage GetAllIncomeRetailProductOverrideTEMP(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeRetailProductOverrideTEMP[] icprb = _MPRBSService.GetAllIncomeRetailProductOverrideTEMP();


                return request.CreateResponse<IncomeRetailProductOverrideTEMP[]>(HttpStatusCode.OK, icprb);
            });
        }

        [HttpGet]
        [Route("getincomeretailproductoverrideusingcustomeridandbank/{customerId}/{bank}")]
        public HttpResponseMessage GetIncomeRetailProductOverrideTEMPUsingCustomerIdandBank(HttpRequestMessage request, int customerId, string bank)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeRetailProductOverrideTEMP[] icprb = _MPRBSService.OverrideUsingCustomerIdAndBankTEMP(customerId, bank);


                return request.CreateResponse<IncomeRetailProductOverrideTEMP[]>(HttpStatusCode.OK, icprb);
            });
        }

        [HttpGet]
        [Route("getincomeretailproductoverrideusingparams/{search}")]
        public HttpResponseMessage GetIncomeRetailProductOverrideTEMPUsingCustomerIdandBank(HttpRequestMessage request, string search)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeRetailProductOverrideTEMP[] icprb = _MPRBSService.SearchByCustomerORMISORAcctOfficer(search);


                return request.CreateResponse<IncomeRetailProductOverrideTEMP[]>(HttpStatusCode.OK, icprb);
            });
        }

    }
}
