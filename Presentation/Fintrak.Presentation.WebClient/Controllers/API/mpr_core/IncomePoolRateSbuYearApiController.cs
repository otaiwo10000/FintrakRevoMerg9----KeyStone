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
    [RoutePrefix("api/incomepoolratesbuyear")]
    [UsesDisposableService]
    public class IncomePoolRateSbuYearApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomePoolRateSbuYearApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }

        [HttpPost]
        [Route("updateincomepoolratesbuyear")]
        public HttpResponseMessage UpdateIncomePoolRateSbuYear(HttpRequestMessage request, [FromBody]IncomePoolRateSbuYear incomePoolRateSbuYearModel)
        {
            return GetHttpResponse(request, () =>
            {
                var incomePoolRateSbuYear = _MPRCoreService.UpdateIncomePoolRateSbuYear(incomePoolRateSbuYearModel);

                return request.CreateResponse<IncomePoolRateSbuYear>(HttpStatusCode.OK, incomePoolRateSbuYear);
            });
        }


        [HttpPost]
        [Route("deleteincomepoolratesbuyear")]
        public HttpResponseMessage DeleteIncomePoolRateSbuYear(HttpRequestMessage request, [FromBody]int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IncomePoolRateSbuYear incomePoolRateSbuYear = _MPRCoreService.GetIncomePoolRateSbuYear(ID);

                if (incomePoolRateSbuYear != null)
                {
                    _MPRCoreService.DeleteIncomePoolRateSbuYear(ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No IncomePoolRateSbuYear found under that ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getincomepoolratesbuyear/{ID}")]
        public HttpResponseMessage GetIncomePoolRateSbuYear(HttpRequestMessage request, int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IncomePoolRateSbuYear incomePoolRateSbuYear = _MPRCoreService.GetIncomePoolRateSbuYear(ID);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<IncomePoolRateSbuYear>(HttpStatusCode.OK, incomePoolRateSbuYear);

                return response;
            });
        }


        [HttpGet]
        [Route("availableincomepoolratesbuyear")]
        public HttpResponseMessage GetAvailableIncomePoolRateSbuYear(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IncomePoolRateSbuYear[] incomePoolRateSbuYear = _MPRCoreService.GetAllIncomePoolRateSbuYear();

                return request.CreateResponse<IncomePoolRateSbuYear[]>(HttpStatusCode.OK, incomePoolRateSbuYear);
            });
        }
    }
}
