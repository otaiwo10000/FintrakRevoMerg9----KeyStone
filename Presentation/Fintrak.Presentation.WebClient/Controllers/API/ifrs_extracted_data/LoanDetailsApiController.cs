using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.IFRS.Contracts;
using Fintrak.Client.IFRS.Entities;
using Fintrak.Presentation.WebClient.Models;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/rawloandetail")]
    [UsesDisposableService]
    public class RawLoanDetailsApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public RawLoanDetailsApiController(IExtractedDataService ifrsDataService)
        {
            _IFRSDataService = ifrsDataService;
        }

        IExtractedDataService _IFRSDataService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRSDataService);
        }

        [HttpPost]
        [Route("updaterawloandetail")]
        public HttpResponseMessage UpdateloanDetail(HttpRequestMessage request, [FromBody]RawLoanDetails loanDetailModel)
        {
            return GetHttpResponse(request, () =>
            {
                var loanDetail = _IFRSDataService.UpdateRawLoanDetails(loanDetailModel);

                return request.CreateResponse<RawLoanDetails>(HttpStatusCode.OK, loanDetail);
            });
        }



        [HttpPost]
        [Route("deleteloandetail")]
        public HttpResponseMessage DeleteloanDetail(HttpRequestMessage request, [FromBody]int loanDetails)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                RawLoanDetails rawloandetail = _IFRSDataService.GetRawLoanDetails(loanDetails);

                if (rawloandetail != null)
                {
                    _IFRSDataService.DeleteRawLoanDetails(loanDetails);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No LoanPrimaryData found under that ID.");

                return response;
            });
        }

        

        [HttpPost]
        [Route("updateloanclassnotch")]
        public HttpResponseMessage UpdateLoanClassNotch(HttpRequestMessage request, [FromBody] MaturityParam param)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                _IFRSDataService.UpdateLoanClassNotch(param.RefNo, param.Rating,param.Stage);

                response = request.CreateResponse(HttpStatusCode.OK);

                return response;
            });
        }


        [HttpGet]
        [Route("availablerawloandetail")]
        public HttpResponseMessage GetAvailablerawloandetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                RawLoanDetails[] rawloandetail = _IFRSDataService.GetAllRawLoanDetails().ToArray();

                return request.CreateResponse<RawLoanDetails[]>(HttpStatusCode.OK, rawloandetail.ToArray());
            });
        }

        [HttpGet]
        [Route("getrawloandetail/{loanDetails}")]
        public HttpResponseMessage GetLoanDetails(HttpRequestMessage request, int loanDetails)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                RawLoanDetails rawloandetail = _IFRSDataService.GetRawLoanDetails(loanDetails);

                // notice no need to create a seperate model object since Setup entity will do just fine
                response = request.CreateResponse<RawLoanDetails>(HttpStatusCode.OK, rawloandetail);

                return response;
            });
        }

    }
}
