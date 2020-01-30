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
    [RoutePrefix("api/incomeproductstableunit")]
    [UsesDisposableService]
    public class IncomeProductstableUnitApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomeProductstableUnitApiController(IMPRIncomeService mprincomeService)
        {
            _MPRIncomeService = mprincomeService;
        }

        IMPRIncomeService _MPRIncomeService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRIncomeService);
        }




        [HttpPost]
        [Route("updateincomeproductunit")]
        public HttpResponseMessage UpdateIncomeProductsTable(HttpRequestMessage request, [FromBody]IncomeProductstableUnit iptModel)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var ipt = _MPRIncomeService.UpdateIncomeProductstableUnit(iptModel);

                //var response =  request.CreateResponse<ScoreCardMetrics>(HttpStatusCode.OK, scm);
                response = request.CreateResponse<IncomeProductstableUnit>(HttpStatusCode.OK, ipt);

                return response;
            });
        }


        [HttpPost]
        [Route("deleteincomeproductunit")]
        public HttpResponseMessage DeleteIncomeProductsTable(HttpRequestMessage request, [FromBody]int iptUnitId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IncomeProductstableUnit ipt = _MPRIncomeService.GetIncomeProductstableUnit(iptUnitId);

                if (ipt != null)
                {
                    _MPRIncomeService.DeleteIncomeProductstableUnit(iptUnitId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No IncomeProduct found under the Id.");

                return response;
            });
        }


        [HttpGet]
        [Route("getincomeproductunit/{iptUnitId}")]
        public HttpResponseMessage GetIncomeProductstable(HttpRequestMessage request, int iptUnitId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IncomeProductstableUnit ipt = _MPRIncomeService.GetIncomeProductstableUnit(iptUnitId);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<IncomeProductstableUnit>(HttpStatusCode.OK, ipt);

                return response;
            });
        }


        [HttpGet]
        [Route("getallincomeproductunits")]
        public HttpResponseMessage GetAllIncomeProductsTable(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeProductstableUnit[] ipt = _MPRIncomeService.GetAllIncomeProductstableUnits();

                return request.CreateResponse<IncomeProductstableUnit[]>(HttpStatusCode.OK, ipt);
            });
        }

        [HttpGet]
        [Route("getincomeproductsunitusingsearch/{searchvalue}")]
        public HttpResponseMessage GetIncomeProductsTableUnitBySearch(HttpRequestMessage request, string searchvalue)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                IncomeProductstableUnit[] ipt = _MPRIncomeService.GetincomeproducttableunitUsingSearchValue(searchvalue);

                // notice no need to create a seperate model object since TeamDefinition entity will do just fine
                response = request.CreateResponse<IncomeProductstableUnit[]>(HttpStatusCode.OK, ipt);

                return response;
            });
        }


    }
}
