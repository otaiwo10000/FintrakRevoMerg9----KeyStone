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
using Fintrak.Presentation.WebClient.Models;
using Fintrak.Shared.Common.Extensions;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/incomeproductstablealt")]
    [UsesDisposableService]
    public class IncomeProductsTableALTApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomeProductsTableALTApiController(IMPRIncomeService mprincomeService)
        {
            _MPRIncomeService = mprincomeService;
        }

        IMPRIncomeService _MPRIncomeService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRIncomeService);
        }


        [HttpPost]
        [Route("updateincomeproduct")]
        public HttpResponseMessage UpdateIncomeProductsTableALT(HttpRequestMessage request, [FromBody]IncomeProductsTableALT iptModel)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var ipt = _MPRIncomeService.Updateincomeproducttablealt(iptModel);

                //var response =  request.CreateResponse<ScoreCardMetrics>(HttpStatusCode.OK, scm);
                 response = request.CreateResponse<IncomeProductsTableALT>(HttpStatusCode.OK, ipt);

                return response;
            });
        }


        [HttpPost]
        [Route("deleteincomeproduct")]
        public HttpResponseMessage DeleteIncomeProductsTableALT(HttpRequestMessage request, [FromBody]int productid)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IncomeProductsTableALT ipt = _MPRIncomeService.Getincomeproducttablealt(productid);

                if (ipt != null)
                {
                    _MPRIncomeService.Deleteincomeproducttablealt(productid);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No IncomeProduct found under the productid.");

                return response;
            });
        }


        [HttpGet]
        [Route("getincomeproduct/{productid}")]
        public HttpResponseMessage GetIncomeProductstableALT(HttpRequestMessage request, int productid)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IncomeProductsTableALT ipt = _MPRIncomeService.Getincomeproducttablealt(productid);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<IncomeProductsTableALT>(HttpStatusCode.OK, ipt);

                return response;
            });
        }


        [HttpGet]
        [Route("getallincomeproducts")]
        public HttpResponseMessage GetAllIncomeProductsTableALT(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeProductsTableALT[] ipt = _MPRIncomeService.GetAllincomeproducttablealt();

                return request.CreateResponse<IncomeProductsTableALT[]>(HttpStatusCode.OK, ipt);
            });
        }

        [HttpGet]
        [Route("getincomeproductsusingsearch/{searchvalue}")]
        public HttpResponseMessage GetIncomeProductsTableALTUsingSearch(HttpRequestMessage request, string searchvalue)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                IncomeProductsTableALT[] ipt = _MPRIncomeService.GetincomeproducttablealtUsingSearchValue(searchvalue);

                // notice no need to create a seperate model object since TeamDefinition entity will do just fine
                response = request.CreateResponse<IncomeProductsTableALT[]>(HttpStatusCode.OK, ipt);

                return response;
            });
        }

    

    }
}
