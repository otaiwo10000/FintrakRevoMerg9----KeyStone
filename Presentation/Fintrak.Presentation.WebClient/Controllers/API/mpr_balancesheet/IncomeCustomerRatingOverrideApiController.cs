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
    [RoutePrefix("api/incomecustomerratingoverride")]
    [UsesDisposableService]
    public class IncomeCustomerRatingOverrideApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomeCustomerRatingOverrideApiController(IMPRBSService mprBSService)
        {
            _MPRBSService = mprBSService;
        }

        IMPRBSService _MPRBSService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRBSService);
        }


        [HttpPost]
        [Route("updateincomecustomerratingoverride")]
        public HttpResponseMessage UpdateIncomeCustomerRatingOverride(HttpRequestMessage request, [FromBody]IncomeCustomerRatingOverride icroModel)
        {
            return GetHttpResponse(request, () =>
            {
                var icro = _MPRBSService.UpdateIncomeCustomerRatingOverride(icroModel);

                return request.CreateResponse<IncomeCustomerRatingOverride>(HttpStatusCode.OK, icro);
            });
        }

        [HttpPost]
        [Route("deleteincomecustomerratingoverride")]
        public HttpResponseMessage DeleteIncomeCustomerRatingOverride(HttpRequestMessage request, [FromBody]int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IncomeCustomerRatingOverride icro = _MPRBSService.GetIncomeCustomerRatingOverride(Id);

                if (icro != null)
                {
                    _MPRBSService.DeleteIncomeCustomerRatingOverride(Id);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getincomecustomerratingoverride/{Id}")]
        public HttpResponseMessage GetIncomeCustomerRatingOverride(HttpRequestMessage request, int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IncomeCustomerRatingOverride icro = _MPRBSService.GetIncomeCustomerRatingOverride(Id);

                // notice no need to create a seperate model object since AcquirerMapping entity will do just fine
                response = request.CreateResponse<IncomeCustomerRatingOverride>(HttpStatusCode.OK, icro);

                return response;
            });
        }

        [HttpGet]
        [Route("availableincomecustomerratingoverride")]
        public HttpResponseMessage GetAllIncomeCustomerRatingOverride(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeCustomerRatingOverride[] icro = _MPRBSService.GetAllIncomeCustomerRatingOverride();


                return request.CreateResponse<IncomeCustomerRatingOverride[]>(HttpStatusCode.OK, icro);
            });
        }

        [HttpGet]
        [Route("getoverridebyrefnumber/{refnumber}")]
        public HttpResponseMessage GetOverrideByRefNumber(HttpRequestMessage request, string refnumber)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeCustomerRatingOverride[] icro = _MPRBSService.GetOverrideByRefNumber(refnumber);


                return request.CreateResponse<IncomeCustomerRatingOverride[]>(HttpStatusCode.OK, icro);
            });
        }

    }
}
