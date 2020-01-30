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
    [RoutePrefix("api/incomememorep")]
    [UsesDisposableService]
    public class IncomeMemorepApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomeMemorepApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }

        [HttpPost]
        [Route("updateincomememorep")]
        public HttpResponseMessage UpdateIncomeMemorep(HttpRequestMessage request, [FromBody]IncomeMemorep incomeMemorepModel)
        {
            return GetHttpResponse(request, () =>
            {
                var incomeMemorep = _MPRCoreService.UpdateIncomeMemorep(incomeMemorepModel);

                return request.CreateResponse<IncomeMemorep>(HttpStatusCode.OK, incomeMemorep);
            });
        }


        [HttpPost]
        [Route("deleteincomememorep")]
        public HttpResponseMessage DeleteIncomeMemorep(HttpRequestMessage request, [FromBody]int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IncomeMemorep incomeMemorep = _MPRCoreService.GetIncomeMemorep(ID);

                if (incomeMemorep != null)
                {
                    _MPRCoreService.DeleteIncomeMemorep(ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No IncomeMemorep found under that ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getincomememorep/{ID}")]
        public HttpResponseMessage GetIncomeMemorep(HttpRequestMessage request, int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IncomeMemorep incomeMemorep = _MPRCoreService.GetIncomeMemorep(ID);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<IncomeMemorep>(HttpStatusCode.OK, incomeMemorep);

                return response;
            });
        }


        [HttpGet]
        [Route("availableincomememorep")]
        public HttpResponseMessage GetAvailableIncomeMemorep(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeMemorep[] incomeMemorep = _MPRCoreService.GetAllIncomeMemorep();

                return request.CreateResponse<IncomeMemorep[]>(HttpStatusCode.OK, incomeMemorep);
            });
        }
    }
}
