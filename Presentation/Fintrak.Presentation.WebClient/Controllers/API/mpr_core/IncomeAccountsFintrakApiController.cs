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
    [RoutePrefix("api/incomeaccountsfintrak")]
    [UsesDisposableService]
    public class IncomeAccountsFintrakApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomeAccountsFintrakApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }

        [HttpPost]
        [Route("updateincomeaccountsfintrak")]
        public HttpResponseMessage UpdateIncomeAccountsFintrak(HttpRequestMessage request, [FromBody]IncomeAccountsFintrak incomeAccountsFintrakModel)
        {
            return GetHttpResponse(request, () =>
            {
                var incomeAccountsFintrak = _MPRCoreService.UpdateIncomeAccountsFintrak(incomeAccountsFintrakModel);

                return request.CreateResponse<IncomeAccountsFintrak>(HttpStatusCode.OK, incomeAccountsFintrak);
            });
        }


        [HttpPost]
        [Route("deleteincomeaccountsfintrak")]
        public HttpResponseMessage DeleteIncomeAccountsFintrak(HttpRequestMessage request, [FromBody]int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IncomeAccountsFintrak incomeAccountsFintrak = _MPRCoreService.GetIncomeAccountsFintrak(ID);

                if (incomeAccountsFintrak != null)
                {
                    _MPRCoreService.DeleteIncomeAccountsFintrak(ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No IncomeAccountsFintrak found under that ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getincomeaccountsfintrak/{ID}")]
        public HttpResponseMessage GetIncomeAccountsFintrak(HttpRequestMessage request, int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IncomeAccountsFintrak incomeAccountsFintrak = _MPRCoreService.GetIncomeAccountsFintrak(ID);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<IncomeAccountsFintrak>(HttpStatusCode.OK, incomeAccountsFintrak);

                return response;
            });
        }


        [HttpGet]
        [Route("availableincomeaccountsfintrak")]
        public HttpResponseMessage GetAvailableIncomeAccountsFintrak(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeAccountsFintrak[] incomeAccountsFintrak = _MPRCoreService.GetAllIncomeAccountsFintrak();

                return request.CreateResponse<IncomeAccountsFintrak[]>(HttpStatusCode.OK, incomeAccountsFintrak);
            });
        }
    }
}
