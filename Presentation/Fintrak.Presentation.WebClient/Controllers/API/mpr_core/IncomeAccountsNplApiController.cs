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
    [RoutePrefix("api/incomeaccountsnpl")]
    [UsesDisposableService]
    public class IncomeAccountsNplApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomeAccountsNplApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }

        [HttpPost]
        [Route("updateincomeaccountsnpl")]
        public HttpResponseMessage UpdateIncomeAccountsNpl(HttpRequestMessage request, [FromBody]IncomeAccountsNpl incomeAccountsNplModel)
        {
            return GetHttpResponse(request, () =>
            {
                var incomeAccountsNpl = _MPRCoreService.UpdateIncomeAccountsNpl(incomeAccountsNplModel);

                return request.CreateResponse<IncomeAccountsNpl>(HttpStatusCode.OK, incomeAccountsNpl);
            });
        }


        [HttpPost]
        [Route("deleteincomeaccountsnpl")]
        public HttpResponseMessage DeleteIncomeAccountsNpl(HttpRequestMessage request, [FromBody]int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IncomeAccountsNpl incomeAccountsNpl = _MPRCoreService.GetIncomeAccountsNpl(ID);

                if (incomeAccountsNpl != null)
                {
                    _MPRCoreService.DeleteIncomeAccountsNpl(ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No IncomeAccountsNpl found under that ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getincomeaccountsnpl/{ID}")]
        public HttpResponseMessage GetIncomeAccountsNpl(HttpRequestMessage request, int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IncomeAccountsNpl incomeAccountsNpl = _MPRCoreService.GetIncomeAccountsNpl(ID);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<IncomeAccountsNpl>(HttpStatusCode.OK, incomeAccountsNpl);

                return response;
            });
        }


        [HttpGet]
        [Route("availableincomeaccountsnpl")]
        public HttpResponseMessage GetAvailableIncomeAccountsNpl(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeAccountsNpl[] incomeAccountsNpl = _MPRCoreService.GetAllIncomeAccountsNpl();

                return request.CreateResponse<IncomeAccountsNpl[]>(HttpStatusCode.OK, incomeAccountsNpl);
            });
        }

        [HttpGet]
        [Route("availableincomeaccountscustomers")]
        public HttpResponseMessage GetAvailableIncomeAccountsCustomers(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeAccountsNplData[] incomeAccountscustomers = _MPRCoreService.GetAllIncomeAccountsCustomers();

                return request.CreateResponse<IncomeAccountsNplData[]>(HttpStatusCode.OK, incomeAccountscustomers);
            });
        }

    }
}
