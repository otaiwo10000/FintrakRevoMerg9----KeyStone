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
    [RoutePrefix("api/incomesplitpoolsratesandbasis")]
    [UsesDisposableService]
    public class IncomeSplitPoolsRatesAndBasisApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomeSplitPoolsRatesAndBasisApiController(IMPRBSService mprBSService)
        {
            _MPRBSService = mprBSService;
        }

        IMPRBSService _MPRBSService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRBSService);
        }

        [HttpPost]
        [Route("updateincomesplitpoolsratesandbasis")]
        public HttpResponseMessage UpdateIncomeSplitPoolsRatesAndBasis(HttpRequestMessage request, [FromBody]IncomeSplitPoolsRatesAndBasis icprbModel)
        {
            return GetHttpResponse(request, () =>
            {
                var icprb = _MPRBSService.UpdateIncomeSplitPoolsRatesAndBasis(icprbModel);

                return request.CreateResponse<IncomeSplitPoolsRatesAndBasis>(HttpStatusCode.OK, icprb);
            });
        }

        [HttpPost]
        [Route("deleteincomesplitpoolsratesandbasis")]
        public HttpResponseMessage DeleteIncomeSplitPoolsRatesAndBasis(HttpRequestMessage request, [FromBody]int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IncomeSplitPoolsRatesAndBasis icprb = _MPRBSService.GetIncomeSplitPoolsRatesAndBasis(Id);

                if (icprb != null)
                {
                    _MPRBSService.DeleteIncomeSplitPoolsRatesAndBasis(Id);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No income split pool rate and basis found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getincomesplitpoolsratesandbasis/{Id}")]
        public HttpResponseMessage GetIncomeSplitPoolsRatesAndBasis(HttpRequestMessage request, int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IncomeSplitPoolsRatesAndBasis icprb = _MPRBSService.GetIncomeSplitPoolsRatesAndBasis(Id);

                // notice no need to create a seperate model object since AcquirerMapping entity will do just fine
                response = request.CreateResponse<IncomeSplitPoolsRatesAndBasis>(HttpStatusCode.OK, icprb);

                return response;
            });
        }

        [HttpGet]
        [Route("availableincomesplitpoolsratesandbasis")]
        public HttpResponseMessage GetAvailableIncomeSplitPoolsRatesAndBasis(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeSplitPoolsRatesAndBasis[] icprb = _MPRBSService.GetAllIncomeSplitPoolsRatesAndBasis();


                return request.CreateResponse<IncomeSplitPoolsRatesAndBasis[]>(HttpStatusCode.OK, icprb);
            });
        }

        [HttpGet]
        [Route("getincomesplitpoolsratesandbasisusingyearmonth/{year}/{period}")]
        public HttpResponseMessage GetIncomeSplitPoolsRatesAndBasisUsingyearmonth(HttpRequestMessage request, int year, int period)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeSplitPoolsRatesAndBasis[] icprb = _MPRBSService.IncomeSplitUsingYearAndPeriod(year, period);


                return request.CreateResponse<IncomeSplitPoolsRatesAndBasis[]>(HttpStatusCode.OK, icprb);
            });
        }

    }
}
