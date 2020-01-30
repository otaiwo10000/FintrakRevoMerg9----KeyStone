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
    [RoutePrefix("api/incomefintrakaccountssegment")]
    [UsesDisposableService]
    public class IncomeFintrakAccountsSegmentApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomeFintrakAccountsSegmentApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }

        [HttpPost]
        [Route("updateincomefintrakaccountssegment")]
        public HttpResponseMessage UpdateIncomeFintrakAccountsSegment(HttpRequestMessage request, [FromBody]IncomeFintrakAccountsSegment ifasModel)
        {
            return GetHttpResponse(request, () =>
            {
                var ifas = _MPRCoreService.UpdateIncomeFintrakAccountsSegment(ifasModel);

                return request.CreateResponse<IncomeFintrakAccountsSegment>(HttpStatusCode.OK, ifas);
            });
        }


        [HttpPost]
        [Route("deleteincomefintrakaccountssegment")]
        public HttpResponseMessage DeleteIncomeFintrakAccountsSegment(HttpRequestMessage request, [FromBody]int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IncomeFintrakAccountsSegment ifas = _MPRCoreService.GetIncomeFintrakAccountsSegment(Id);

                if (ifas != null)
                {
                    _MPRCoreService.DeleteIncomeFintrakAccountsSegment(Id);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No IncomeFintrakAccountsSegment found under that ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getincomefintrakaccountssegment/{Id}")]
        public HttpResponseMessage GetIncomeFintrakAccountsSegment(HttpRequestMessage request, int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IncomeFintrakAccountsSegment ifas = _MPRCoreService.GetIncomeFintrakAccountsSegment(Id);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<IncomeFintrakAccountsSegment>(HttpStatusCode.OK, ifas);

                return response;
            });
        }


        [HttpGet]
        [Route("availableincomefintrakaccountssegment")]
        public HttpResponseMessage GetAvailableIncomeFintrakAccountsSegment(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeFintrakAccountsSegment[] ifas = _MPRCoreService.GetAllIncomeFintrakAccountsSegment();

                return request.CreateResponse<IncomeFintrakAccountsSegment[]>(HttpStatusCode.OK, ifas);
            });
        }

        [HttpGet]
        [Route("getincomefintrakaccountssegmentbycustomercodeandbank/{customerid}/{bank}")]
        public HttpResponseMessage GetAvailableIncomeFintrakAccountsSegment(HttpRequestMessage request, string customerid, string bank)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeFintrakAccountsSegment[] ifas = _MPRCoreService.GetAccountsSegmentByCustomerIdBank(customerid, bank);

                return request.CreateResponse<IncomeFintrakAccountsSegment[]>(HttpStatusCode.OK, ifas);
            });
        }


        [HttpGet]
        [Route("populateincomefintrakaccountssegment")]
        public HttpResponseMessage LoadIncomeFintrakAccountsSegment(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                var ifas = new Additionalmethods.IncomeFintrakAccountsSegmentMtd();

                string seg = ifas.LoadIncomeFintrakAccountsSegment();

                return request.CreateResponse(HttpStatusCode.OK, seg);
            });
        }

    }
}
