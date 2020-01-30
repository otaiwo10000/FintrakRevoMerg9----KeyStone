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
    [RoutePrefix("api/incomeproductstable")]
    [UsesDisposableService]
    public class IncomeProductsTableApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomeProductsTableApiController(IMPRIncomeService mprincomeService)
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
        public HttpResponseMessage UpdateIncomeProductsTable(HttpRequestMessage request, [FromBody]IncomeProductsTable iptModel)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var ipt = _MPRIncomeService.Updateincomeproducttable(iptModel);

                //var response =  request.CreateResponse<ScoreCardMetrics>(HttpStatusCode.OK, scm);
                 response = request.CreateResponse<IncomeProductsTable>(HttpStatusCode.OK, ipt);

                return response;
            });
        }


        [HttpPost]
        [Route("deleteincomeproduct")]
        public HttpResponseMessage DeleteIncomeProductsTable(HttpRequestMessage request, [FromBody]int productid)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IncomeProductsTable ipt = _MPRIncomeService.Getincomeproducttable(productid);

                if (ipt != null)
                {
                    _MPRIncomeService.Deleteincomeproducttable(productid);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No IncomeProduct found under the productid.");

                return response;
            });
        }


        [HttpGet]
        [Route("getincomeproduct/{productid}")]
        public HttpResponseMessage GetIncomeProductstable(HttpRequestMessage request, int productid)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IncomeProductsTable ipt = _MPRIncomeService.Getincomeproducttable(productid);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<IncomeProductsTable>(HttpStatusCode.OK, ipt);

                return response;
            });
        }


        [HttpGet]
        [Route("getallincomeproducts")]
        public HttpResponseMessage GetAllIncomeProductsTable(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeProductsTable[] ipt = _MPRIncomeService.GetAllincomeproducttable();

                return request.CreateResponse<IncomeProductsTable[]>(HttpStatusCode.OK, ipt);
            });
        }

        [HttpGet]
        [Route("getincomeproductsusingsearch/{searchvalue}")]
        public HttpResponseMessage GetIncomeProductsTableUsingSearch(HttpRequestMessage request, string searchvalue)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                IncomeProductsTable[] ipt = _MPRIncomeService.GetincomeproducttableUsingSearchValue(searchvalue);

                // notice no need to create a seperate model object since TeamDefinition entity will do just fine
                response = request.CreateResponse<IncomeProductsTable[]>(HttpStatusCode.OK, ipt);

                return response;
            });
        }

    

    }
}
