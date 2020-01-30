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
    [RoutePrefix("api/incomecommfeemis")]
    [UsesDisposableService]
    public class IncomeCommFeeMisApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomeCommFeeMisApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }

        [HttpPost]
        [Route("updateincomecommfeemis")]
        public HttpResponseMessage UpdateIncomeCommFeeMis(HttpRequestMessage request, [FromBody]IncomeCommFeeMis incomeCommFeeMisModel)
        {
            return GetHttpResponse(request, () =>
            {
                var incomeCommFeeMis = _MPRCoreService.UpdateIncomeCommFeeMis(incomeCommFeeMisModel);

                return request.CreateResponse<IncomeCommFeeMis>(HttpStatusCode.OK, incomeCommFeeMis);
            });
        }


        [HttpPost]
        [Route("deleteincomecommfeemis")]
        public HttpResponseMessage DeleteIncomeCommFeeMis(HttpRequestMessage request, [FromBody]int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IncomeCommFeeMis incomeCommFeeMis = _MPRCoreService.GetIncomeCommFeeMis(ID);

                if (incomeCommFeeMis != null)
                {
                    _MPRCoreService.DeleteIncomeCommFeeMis(ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No IncomeCommFeeMis found under that ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getincomecommfeemis/{ID}")]
        public HttpResponseMessage GetIncomeCommFeeMis(HttpRequestMessage request, int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IncomeCommFeeMis incomeCommFeeMis = _MPRCoreService.GetIncomeCommFeeMis(ID);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<IncomeCommFeeMis>(HttpStatusCode.OK, incomeCommFeeMis);

                return response;
            });
        }


        [HttpGet]
        [Route("availableincomecommfeemis")]
        public HttpResponseMessage GetAvailableIncomeCommFeeMis(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeCommFeeMis[] incomeCommFeeMis = _MPRCoreService.GetAllIncomeCommFeeMis();

                return request.CreateResponse<IncomeCommFeeMis[]>(HttpStatusCode.OK, incomeCommFeeMis);
            });
        }
    }
}
