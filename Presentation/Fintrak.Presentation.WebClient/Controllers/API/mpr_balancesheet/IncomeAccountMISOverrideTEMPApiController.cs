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
    [RoutePrefix("api/incomeaccountMISoverrideTEMP")]
    [UsesDisposableService]
    public class IncomeAccountMISOverrideTEMPApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomeAccountMISOverrideTEMPApiController(IMPRBSService mprBSService)
        {
            _MPRBSService = mprBSService;
        }

        IMPRBSService _MPRBSService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRBSService);
        }


        [HttpPost]
        [Route("updateincomeaccountMISoverride")]
        public HttpResponseMessage UpdateIncomeAccountMISOverrideTEMP(HttpRequestMessage request, [FromBody]IncomeAccountMISOverrideTEMP icprbModel)
        {
            return GetHttpResponse(request, () =>
            {
                var icprb = _MPRBSService.UpdateIncomeAccountMISOverrideTEMP(icprbModel);

                return request.CreateResponse<IncomeAccountMISOverrideTEMP>(HttpStatusCode.OK, icprb);
            });
        }

        [HttpPost]
        [Route("deleteincomeaccountMISoverride")]
        public HttpResponseMessage DeleteIncomeAccountMISOverrideTEMP(HttpRequestMessage request, [FromBody]int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IncomeAccountMISOverrideTEMP icprb = _MPRBSService.GetIncomeAccountMISOverrideTEMP(Id);

                if (icprb != null)
                {
                    _MPRBSService.DeleteIncomeAccountMISOverrideTEMP(Id);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getincomeaccountMISoverride/{Id}")]
        public HttpResponseMessage GetIncomeAccountMISOverrideTEMP(HttpRequestMessage request, int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IncomeAccountMISOverrideTEMP icprb = _MPRBSService.GetIncomeAccountMISOverrideTEMP(Id);

                // notice no need to create a seperate model object since AcquirerMapping entity will do just fine
                response = request.CreateResponse<IncomeAccountMISOverrideTEMP>(HttpStatusCode.OK, icprb);

                return response;
            });
        }

        [HttpGet]
        [Route("availableincomeaccountMISoverride")]
        public HttpResponseMessage GetAllIncomeAccountMISOverrideTEMP(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeAccountMISOverrideTEMP[] icprb = _MPRBSService.GetAllIncomeAccountMISOverrideTEMP();


                return request.CreateResponse<IncomeAccountMISOverrideTEMP[]>(HttpStatusCode.OK, icprb);
            });
        }

        [HttpGet]
        [Route("getincomeaccountMISoverrideusingaccountnumber/{accountno}")]
        public HttpResponseMessage GetIncomeAccountMISOverrideTEMPUsingAcctNo(HttpRequestMessage request, string accountno)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeAccountMISOverrideTEMP[] icprb = _MPRBSService.OverrideUsingAccountNumberTEMP(accountno);


                return request.CreateResponse<IncomeAccountMISOverrideTEMP[]>(HttpStatusCode.OK, icprb);
            });
        }

        [HttpGet]
        [Route("getincomeaccountMISoverrideusingaccountnoORmisORacctofficer/{search}")]
        public HttpResponseMessage GetIncomeAccountMISOverrideTEMPUsingAcctNoorMISorAcctOfficer(HttpRequestMessage request, string search)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeAccountMISOverrideTEMP[] icprb = _MPRBSService.OverrideUsingAccountNumberTEMP(search);


                return request.CreateResponse<IncomeAccountMISOverrideTEMP[]>(HttpStatusCode.OK, icprb);
            });
        }

    }
}
