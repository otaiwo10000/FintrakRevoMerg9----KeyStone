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
using Fintrak.Presentation.WebClient.Additionalmethods;
using Fintrak.Presentation.WebClient.Models;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/incomeaccountslisting")]
    [UsesDisposableService]
    public class IncomeAccountsListingApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomeAccountsListingApiController(IMPRBSService mprBSService)
        {
            _MPRBSService = mprBSService;
        }

        IMPRBSService _MPRBSService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRBSService);
        }


        [HttpPost]
        [Route("updateincomeaccountslisting")]
        public HttpResponseMessage UpdateIncomeAccountsListing(HttpRequestMessage request, [FromBody]IncomeAccountsListing ialModel)
        {
            return GetHttpResponse(request, () =>
            {
                var ial = _MPRBSService.UpdateIncomeAccountsListing(ialModel);

                return request.CreateResponse<IncomeAccountsListing>(HttpStatusCode.OK, ial);
            });
        }

        [HttpPost]
        [Route("deleteincomeaccountslisting")]
        public HttpResponseMessage DeleteIncomeAccountsListing(HttpRequestMessage request, [FromBody]int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IncomeAccountsListing ial = _MPRBSService.GetIncomeAccountsListing(Id);

                if (ial != null)
                {
                    _MPRBSService.DeleteIncomeAccountsListing(Id);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getincomeaccountslisting/{Id}")]
        public HttpResponseMessage GetIncomeAccountsListing(HttpRequestMessage request, int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IncomeAccountsListing ial = _MPRBSService.GetIncomeAccountsListing(Id);

                // notice no need to create a seperate model object since AcquirerMapping entity will do just fine
                response = request.CreateResponse<IncomeAccountsListing>(HttpStatusCode.OK, ial);

                return response;
            });
        }

        [HttpGet]
        [Route("availableincomeaccountslisting")]
        public HttpResponseMessage GetIncomeAccountsListing(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                //IncomeAccountsListing[] ial = _MPRBSService.GetAllIncomeAccountsListing();
                //return request.CreateResponse<IncomeAccountsListing[]>(HttpStatusCode.OK, ial);

                IncomeAccountsListingMtd obj = new IncomeAccountsListingMtd();
                List<IncomeAccountsListingModel> dm = obj.GetIncomeAccountsListing().ToList();

                return request.CreateResponse(HttpStatusCode.OK, dm);
            });
        }

        [HttpGet]
        [Route("getincomeaccountslistingusingaccountnumber/{search}")]
        public HttpResponseMessage GetIncomeAccountsListingUsingParam(HttpRequestMessage request, string search)
        {
            return GetHttpResponse(request, () =>
            {
                //accountno = accountno.Replace("FORWARDSLASHXTER", "/");
                //accountno = accountno.Replace("DOTXTER", ".");

                //IncomeAccountsListing[] ial = _MPRBSService.FilterByAccountNumber(accountno);


                //return request.CreateResponse<IncomeAccountsListing[]>(HttpStatusCode.OK, ial);

                IncomeAccountsListingMtd obj = new IncomeAccountsListingMtd();
                List<IncomeAccountsListingModel> dm = obj.GetIncomeAccountsListingUsingParams(search).ToList();

                return request.CreateResponse(HttpStatusCode.OK, dm);

            });
        }

    }
}
