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
    [RoutePrefix("api/incomeaccountstreemiscodes")]
    [UsesDisposableService]
    public class IncomeAccountsTreeMisCodesApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomeAccountsTreeMisCodesApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }

        [HttpPost]
        [Route("updateincomeaccountstreemiscodes")]
        public HttpResponseMessage UpdateIncomeAccountsTreeMisCodes(HttpRequestMessage request, [FromBody]IncomeAccountsTreeMisCodes incomeAccountsTreeMisCodesModel)
        {
            return GetHttpResponse(request, () =>
            {
                var incomeAccountsTreeMisCodes = _MPRCoreService.UpdateIncomeAccountsTreeMisCodes(incomeAccountsTreeMisCodesModel);

                return request.CreateResponse<IncomeAccountsTreeMisCodes>(HttpStatusCode.OK, incomeAccountsTreeMisCodes);
            });
        }


        [HttpPost]
        [Route("deleteincomeaccountstreemiscodes")]
        public HttpResponseMessage DeleteIncomeAccountsTreeMisCodes(HttpRequestMessage request, [FromBody]int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IncomeAccountsTreeMisCodes incomeAccountsTreeMisCodes = _MPRCoreService.GetIncomeAccountsTreeMisCodes(ID);

                if (incomeAccountsTreeMisCodes != null)
                {
                    _MPRCoreService.DeleteIncomeAccountsTreeMisCodes(ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No IncomeAccountsTreeMisCodes found under that ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getincomeaccountstreemiscodes/{ID}")]
        public HttpResponseMessage GetIncomeAccountsTreeMisCodes(HttpRequestMessage request, int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IncomeAccountsTreeMisCodes incomeAccountsTreeMisCodes = _MPRCoreService.GetIncomeAccountsTreeMisCodes(ID);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<IncomeAccountsTreeMisCodes>(HttpStatusCode.OK, incomeAccountsTreeMisCodes);

                return response;
            });
        }


        [HttpGet]
        [Route("availableincomeaccountstreemiscodes")]
        public HttpResponseMessage GetAvailableIncomeAccountsTreeMisCodes(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeAccountsTreeMisCodes[] incomeAccountsTreeMisCodes = _MPRCoreService.GetAllIncomeAccountsTreeMisCodes();

                return request.CreateResponse<IncomeAccountsTreeMisCodes[]>(HttpStatusCode.OK, incomeAccountsTreeMisCodes);
            });
        }

        [HttpGet]
        [Route("getincomeaccountstreemiscodesusingaccountnumber/{accountno}")]
        public HttpResponseMessage GetIncomeAccountsTreeMisCodesUsingAccountNumber(HttpRequestMessage request, string accountno)
        {
            return GetHttpResponse(request, () =>
            {
                accountno = accountno.Replace("FORWARDSLASHXTER", "/");
                accountno = accountno.Replace("DOTXTER", ".");

                IncomeAccountsTreeMisCodes[] incomeAccountsTreeMisCodes = _MPRCoreService.GetByAccountNumber(accountno);


                return request.CreateResponse<IncomeAccountsTreeMisCodes[]>(HttpStatusCode.OK, incomeAccountsTreeMisCodes);
            });
        }
    }
}
