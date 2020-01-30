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
    [RoutePrefix("api/incomecommfeebusinessrule")]
    [UsesDisposableService]
    public class IncomeCommFeeBusinessRuleApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomeCommFeeBusinessRuleApiController(IMPRBSService mprBSService)
        {
            _MPRBSService = mprBSService;
        }

        IMPRBSService _MPRBSService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRBSService);
        }

        [HttpPost]
        [Route("updateincomecommfeebusinessrule")]
        public HttpResponseMessage UpdateIncomeCommFeeBusinessRule(HttpRequestMessage request, [FromBody]IncomeCommFeeBusinessRule incomeCommFeeBusinessRuleModel)
        {
            return GetHttpResponse(request, () =>
            {
                var incomeCommFeeBusinessRule = _MPRBSService.UpdateIncomeCommFeeBusinessRule(incomeCommFeeBusinessRuleModel);

                return request.CreateResponse<IncomeCommFeeBusinessRule>(HttpStatusCode.OK, incomeCommFeeBusinessRule);
            });
        }

        [HttpPost]
        [Route("deleteincomecommfeebusinessrule")]
        public HttpResponseMessage DeleteIncomeCommFeeBusinessRule(HttpRequestMessage request, [FromBody]int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IncomeCommFeeBusinessRule incomeCommFeeBusinessRule = _MPRBSService.GetIncomeCommFeeBusinessRule(ID);

                if (incomeCommFeeBusinessRule != null)
                {
                    _MPRBSService.DeleteIncomeCommFeeBusinessRule(ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No incomeCommFeeBusinessRule found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getincomecommfeebusinessrule/{ID}")]
        public HttpResponseMessage GetIncomeCommFeeBusinessRule(HttpRequestMessage request, int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IncomeCommFeeBusinessRule incomeCommFeeBusinessRule = _MPRBSService.GetIncomeCommFeeBusinessRule(ID);

                // notice no need to create a seperate model object since ProductMIS entity will do just fine
                response = request.CreateResponse<IncomeCommFeeBusinessRule>(HttpStatusCode.OK, incomeCommFeeBusinessRule);

                return response;
            });
        }

        [HttpGet]
        [Route("availableincomecommfeebusinessrule")]
        public HttpResponseMessage GetAvailableIncomeCommFeeBusinessRule(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeCommFeeBusinessRule[] incomeCommFeeBusinessRule = _MPRBSService.GetAllIncomeCommFeeBusinessRule();

                return request.CreateResponse<IncomeCommFeeBusinessRule[]>(HttpStatusCode.OK, incomeCommFeeBusinessRule);
            });
        }

        [HttpGet]
        [Route("getincomecommfeebusinessruleusingsearch/{searchvalue}")]
        public HttpResponseMessage GetIncomeCommFeeBusinessRuleUsingSearchValue(HttpRequestMessage request, string searchvalue)
        {
            return GetHttpResponse(request, () =>
            {
                searchvalue = searchvalue.Replace("FORWARDSLASHXTER", "/");
                searchvalue = searchvalue.Replace("DOTXTER", ".");

                HttpResponseMessage response = null;
                IncomeCommFeeBusinessRule[] incomeCommFeeBusinessRule = _MPRBSService.GetIncomeCommFeeBusinessRuleUsingSearchValue(searchvalue);

                // notice no need to create a seperate model object since TeamDefinition entity will do just fine
                response = request.CreateResponse<IncomeCommFeeBusinessRule[]>(HttpStatusCode.OK, incomeCommFeeBusinessRule);

                return response;
            });
        }

    }
}
