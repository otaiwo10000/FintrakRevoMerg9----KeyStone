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
using Fintrak.Shared.Basic.Framework;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/incomebranches")]
    [UsesDisposableService]
    public class IncomeBranchesApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomeBranchesApiController(IMPRBSService mprBSService)
        {
            _MPRBSService = mprBSService;
        }

        IMPRBSService _MPRBSService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRBSService);
        }

        [HttpPost]
        [Route("updateincomebranches")]
        public HttpResponseMessage UpdateIncomeBranches(HttpRequestMessage request, [FromBody]IncomeBranches ibModel)
        {
            return GetHttpResponse(request, () =>
            {
                var ib = _MPRBSService.UpdateIncomeBranches(ibModel);

                return request.CreateResponse<IncomeBranches>(HttpStatusCode.OK, ib);
            });
        }

        [HttpPost]
        [Route("deleteincomebranches")]
        public HttpResponseMessage DeleteIncomeBranches(HttpRequestMessage request, [FromBody]int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IncomeBranches ib = _MPRBSService.GetIncomeBranches(Id);

                if (ib != null)
                {
                    _MPRBSService.DeleteIncomeBranches(Id);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No branch found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getincomebranches/{Id}")]
        public HttpResponseMessage GetIncomeBranches(HttpRequestMessage request, int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IncomeBranches ib = _MPRBSService.GetIncomeBranches(Id);

                // notice no need to create a seperate model object since MPRProduct entity will do just fine
                response = request.CreateResponse<IncomeBranches>(HttpStatusCode.OK, ib);

                return response;
            });
        }

        [HttpGet]
        [Route("availableincomebranches")]
        public HttpResponseMessage GetAvailableMPRProducts(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeBranches[] ib = _MPRBSService.GetAllIncomeBranches();

                return request.CreateResponse<IncomeBranches[]>(HttpStatusCode.OK, ib);
            });
        }

       
    }
}
