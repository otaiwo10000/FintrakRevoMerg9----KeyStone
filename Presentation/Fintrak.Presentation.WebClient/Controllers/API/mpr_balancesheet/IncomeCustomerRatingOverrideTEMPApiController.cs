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
    [RoutePrefix("api/incomecustomerratingoverrideTEMP")]
    [UsesDisposableService]
    public class IncomeCustomerRatingOverrideTEMPApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomeCustomerRatingOverrideTEMPApiController(IMPRBSService mprBSService)
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
        public HttpResponseMessage UpdateIncomeCustomerRatingOverride(HttpRequestMessage request, [FromBody]IncomeCustomerRatingOverrideTEMP icroModel)
        {
            return GetHttpResponse(request, () =>
            {
                var icro = _MPRBSService.UpdateIncomeCustomerRatingOverrideTEMP(icroModel);

                return request.CreateResponse<IncomeCustomerRatingOverrideTEMP>(HttpStatusCode.OK, icro);
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
                IncomeCustomerRatingOverrideTEMP icro = _MPRBSService.GetIncomeCustomerRatingOverrideTEMP(Id);

                if (icro != null)
                {
                    _MPRBSService.DeleteIncomeCustomerRatingOverrideTEMP(Id);

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

                IncomeCustomerRatingOverrideTEMP icro = _MPRBSService.GetIncomeCustomerRatingOverrideTEMP(Id);

                // notice no need to create a seperate model object since AcquirerMapping entity will do just fine
                response = request.CreateResponse<IncomeCustomerRatingOverrideTEMP>(HttpStatusCode.OK, icro);

                return response;
            });
        }

        [HttpGet]
        [Route("availableincomecustomerratingoverride")]
        public HttpResponseMessage GetAllIncomeCustomerRatingOverride(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeCustomerRatingOverrideTEMP[] icro = _MPRBSService.GetAllIncomeCustomerRatingOverrideTEMP();


                return request.CreateResponse<IncomeCustomerRatingOverrideTEMP[]>(HttpStatusCode.OK, icro);
            });
        }

        [HttpGet]
        [Route("getoverridebyrefnumber/{refnumber}")]
        public HttpResponseMessage GetOverrideByRefNumber(HttpRequestMessage request, string refnumber)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeCustomerRatingOverrideTEMP[] icro = _MPRBSService.GetOverrideByRefNumberTEMP(refnumber);


                return request.CreateResponse<IncomeCustomerRatingOverrideTEMP[]>(HttpStatusCode.OK, icro);
            });
        }

    }
}
