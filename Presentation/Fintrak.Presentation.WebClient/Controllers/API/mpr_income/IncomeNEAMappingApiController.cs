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
    [RoutePrefix("api/incomeNEAmapping")]
    [UsesDisposableService]
    public class IncomeNEAMappingApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomeNEAMappingApiController(IMPRIncomeService mprincomeService)
        {
            _MPRIncomeService = mprincomeService;
        }

        IMPRIncomeService _MPRIncomeService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRIncomeService);
        }


        [HttpPost]
        [Route("updateincomeNEAmapping")]
        public HttpResponseMessage UpdateIncomeNEAMapping(HttpRequestMessage request, [FromBody]IncomeNEAMapping incomeNEAmappingModel)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var incomeNEAmapping = _MPRIncomeService.UpdateIncomeNEAMapping(incomeNEAmappingModel);

                 response = request.CreateResponse<IncomeNEAMapping>(HttpStatusCode.OK, incomeNEAmapping);

                return response;
            });
        }


        [HttpPost]
        [Route("deleteincomeNEAmapping")]
        public HttpResponseMessage DeleteIncomeNEAMapping(HttpRequestMessage request, [FromBody]int incomeNEAmappingId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IncomeNEAMapping inm = _MPRIncomeService.GetIncomeNEAMapping(incomeNEAmappingId);

                if (inm != null)
                {
                    _MPRIncomeService.DeleteIncomeNEAMapping(incomeNEAmappingId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No IncomeNEA Mapping found under the ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getincomeNEAmapping/{incomeNEAmappingId}")]
        public HttpResponseMessage GetIncomeProductstable(HttpRequestMessage request, int incomeNEAmappingId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IncomeNEAMapping ipt = _MPRIncomeService.GetIncomeNEAMapping(incomeNEAmappingId);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<IncomeNEAMapping>(HttpStatusCode.OK, ipt);

                return response;
            });
        }

        [HttpGet]
        [Route("getallincomeNEAmapping")]
        public HttpResponseMessage GetAllIncomeProductsTable(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeNEAMapping[] ipt = _MPRIncomeService.GetAllIncomeNEAMapping();

                return request.CreateResponse<IncomeNEAMapping[]>(HttpStatusCode.OK, ipt);
            });
        }

        [HttpGet]
        [Route("getincomeNEAmappingusingsearch/{searchvalue}")]
        public HttpResponseMessage GetIncomeProductsTableUsingSearch(HttpRequestMessage request, string searchvalue)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                IncomeNEAMappingData[] ipt = _MPRIncomeService.GetIncomeNEAMappingUsingSearchValue(searchvalue);

                // notice no need to create a seperate model object since TeamDefinition entity will do just fine
                response = request.CreateResponse<IncomeNEAMappingData[]>(HttpStatusCode.OK, ipt);

                return response;
            });
        }

        [HttpGet]
        [Route("getfullincomeNEAmapping")]
        public HttpResponseMessage GetFullIncomeNEAMapping(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                IncomeNEAMappingData[] ipt = _MPRIncomeService.GetFullIncomeNEAMapping();

                // notice no need to create a seperate model object since TeamDefinition entity will do just fine
                response = request.CreateResponse<IncomeNEAMappingData[]>(HttpStatusCode.OK, ipt);

                return response;
            });
        }

    }
}
