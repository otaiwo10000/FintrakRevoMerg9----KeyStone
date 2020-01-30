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
    [RoutePrefix("api/incomeaccountstreeaccount")]
    [UsesDisposableService]
    public class IncomeAccountsTreeAccountApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomeAccountsTreeAccountApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }

        [HttpPost]
        [Route("updateincomeaccountstreeaccount")]
        public HttpResponseMessage UpdateIncomeAccountsTreeAccount(HttpRequestMessage request, [FromBody]IncomeAccountsTreeAccount incomeAccountsTreeAccountModel)
        {
            return GetHttpResponse(request, () =>
            {
                var incomeAccountsTreeAccount = _MPRCoreService.UpdateIncomeAccountsTreeAccount(incomeAccountsTreeAccountModel);

                return request.CreateResponse<IncomeAccountsTreeAccount>(HttpStatusCode.OK, incomeAccountsTreeAccount);
            });
        }


        [HttpPost]
        [Route("deleteincomeaccountstreeaccount")]
        public HttpResponseMessage DeleteIncomeAccountsTreeAccount(HttpRequestMessage request, [FromBody]int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IncomeAccountsTreeAccount incomeAccountsTreeAccount = _MPRCoreService.GetIncomeAccountsTreeAccount(ID);

                if (incomeAccountsTreeAccount != null)
                {
                    _MPRCoreService.DeleteIncomeAccountsTreeAccount(ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No IncomeAccountsTreeAccount found under that ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getincomeaccountstreeaccount/{ID}")]
        public HttpResponseMessage GetIncomeAccountsTreeAccount(HttpRequestMessage request, int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IncomeAccountsTreeAccount incomeAccountsTreeAccount = _MPRCoreService.GetIncomeAccountsTreeAccount(ID);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<IncomeAccountsTreeAccount>(HttpStatusCode.OK, incomeAccountsTreeAccount);

                return response;
            });
        }


        [HttpGet]
        [Route("availableincomeaccountstreeaccount")]
        public HttpResponseMessage GetAvailableIncomeAccountsTreeAccount(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeAccountsTreeAccount[] incomeAccountsTreeAccount = _MPRCoreService.GetAllIncomeAccountsTreeAccount();

                return request.CreateResponse<IncomeAccountsTreeAccount[]>(HttpStatusCode.OK, incomeAccountsTreeAccount);
            });
        }

        [HttpGet]
        [Route("getincomeaccountstreeaccountusingaccountnumber/{accountno}")]
        public HttpResponseMessage GetIncomeAccountsTreeAccountUsingAccountNumber(HttpRequestMessage request, string accountno)
        {
            return GetHttpResponse(request, () =>
            {
                accountno = accountno.Replace("FORWARDSLASHXTER", "/");
                accountno = accountno.Replace("DOTXTER", ".");

                IncomeAccountsTreeAccount[] incomeAccountsTreeAccount = _MPRCoreService.FilterByAccountNumber(accountno);


                return request.CreateResponse<IncomeAccountsTreeAccount[]>(HttpStatusCode.OK, incomeAccountsTreeAccount);
            });
        }

    }
}
