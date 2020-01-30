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
    [RoutePrefix("api/customertransferprice")]
    [UsesDisposableService]
    public class CustomerTransferPriceApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public CustomerTransferPriceApiController(IMPRBSService mprBSService)
        {
            _MPRBSService = mprBSService;
        }

        IMPRBSService _MPRBSService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRBSService);
        }

        [HttpPost]
        [Route("updatecustomertransferprice")]
        public HttpResponseMessage UpdateCustomerTransferPrice(HttpRequestMessage request, [FromBody]CustomerTransferPrice CustomerTransferPriceModel)
        {
            return GetHttpResponse(request, () =>
            {
                var CustomerTransferPrice = _MPRBSService.UpdateCustomerTransferPrice(CustomerTransferPriceModel);

                return request.CreateResponse<CustomerTransferPrice>(HttpStatusCode.OK, CustomerTransferPrice);
            });
        }

        [HttpPost]
        [Route("deletecustomertransferprice")]
        public HttpResponseMessage DeleteCustomerTransferPrice(HttpRequestMessage request, [FromBody]int customertransferpriceId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                CustomerTransferPrice CustomerTransferPrice = _MPRBSService.GetCustomerTransferPrice(customertransferpriceId);

                if (CustomerTransferPrice != null)
                {
                    _MPRBSService.DeleteCustomerTransferPrice(customertransferpriceId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Acquirere mapping found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getcustomertransferprice/{customertransferpriceId}")]
        public HttpResponseMessage GetCustomerTransferPrice(HttpRequestMessage request, int customertransferpriceId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                CustomerTransferPrice CustomerTransferPrice = _MPRBSService.GetCustomerTransferPrice(customertransferpriceId);

                // notice no need to create a seperate model object since CustomerTransferPrice entity will do just fine
                response = request.CreateResponse<CustomerTransferPrice>(HttpStatusCode.OK, CustomerTransferPrice);

                return response;
            });
        }

        [HttpGet]
        [Route("availablecustomertransferprice")]
        public HttpResponseMessage GetAvailableCustomerTransferPrice(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                CustomerTransferPrice[] CustomerTransferPrice = _MPRBSService.GetAllCustomerTransferPrices();


                return request.CreateResponse<CustomerTransferPrice[]>(HttpStatusCode.OK, CustomerTransferPrice);
            });
        }

        [HttpGet]
        [Route("getcustomertransferpricebysetup")]
        public HttpResponseMessage CustomerTransferPricebySetUp(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                CustomerTransferPriceData[] customerTransferPrice = _MPRBSService.GetCustomerTransferPricebySetUp();


                return request.CreateResponse<CustomerTransferPriceData[]>(HttpStatusCode.OK, customerTransferPrice);
            });
        }

        [HttpGet]
        [Route("getcustomertransferpricebysearch/{search}")]
        public HttpResponseMessage CustomerTransferPricebysearch(HttpRequestMessage request, string search)
        {
            return GetHttpResponse(request, () =>
            {
                CustomerTransferPriceData[] customerTransferPrice = _MPRBSService.GetCustomerTransferPricebysearch(search);


                return request.CreateResponse<CustomerTransferPriceData[]>(HttpStatusCode.OK, customerTransferPrice);
            });
        }

       
        [HttpGet]
        [Route("companycode")]
        public HttpResponseMessage CustomerCode(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string companycode = System.Configuration.ConfigurationManager.AppSettings["CompanyCode"];

                return request.CreateResponse<string>(companycode);
            });
        }
    }
}
