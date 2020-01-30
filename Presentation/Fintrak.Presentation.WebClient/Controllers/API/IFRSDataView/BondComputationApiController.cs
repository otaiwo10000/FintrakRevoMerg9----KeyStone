using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Presentation.WebClient.Models;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.IFRS.Contracts;
using Fintrak.Client.IFRS.Entities;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/bondcomputation")]
    [UsesDisposableService]
    public class BondComputationApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public BondComputationApiController(IIFRSDataViewService ifrsDataService)
        {
            _IFRSDataService = ifrsDataService;
        }

        IIFRSDataViewService _IFRSDataService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRSDataService);
        }



        [HttpGet]
        [Route("getbondcomputation/{refno}")]
        public HttpResponseMessage GetBondComputation(HttpRequestMessage request, string refno)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                BondComputation[] bondcomputation = _IFRSDataService.GetBondComputationResultbyRefNo(refno);

                // notice no need to create a seperate model object since BondComputation entity will do just fine
                response = request.CreateResponse<BondComputation[]>(HttpStatusCode.OK, bondcomputation);

                return response;
            });
        }


        [HttpGet]
        [Route("getbondcomputationdistinctrefno2")]
        public HttpResponseMessage GetBondComputationResultDistinctRefNo(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                BondComputation[] bondcomputation = _IFRSDataService.GetBondComputationResultDistinctRefNo();

                return request.CreateResponse<BondComputation[]>(HttpStatusCode.OK, bondcomputation);
            });
        }

        [HttpGet]
        [Route("availablebondcomputation")]
        public HttpResponseMessage GetAvailableBondComputations(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                BondComputation[] bondcomputation = _IFRSDataService.GetAllBondComputations();

                return request.CreateResponse<BondComputation[]>(HttpStatusCode.OK, bondcomputation);
            });
        }


        [HttpGet]
        [Route("getrefnos")]
        public HttpResponseMessage GetReferenceNos(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                //BondComputation[] bondcomputations = _IFRSDataService.GetRefNoBondComputation();

                //return request.CreateResponse<BondComputation[]>(HttpStatusCode.OK, bondcomputations);
                string[] refNo = _IFRSDataService.GetDistinctRefNo();
                List<ReferenceNoModel> refNos = new List<ReferenceNoModel>();
                foreach (var c in refNo)
                    refNos.Add(new ReferenceNoModel()
                    {
                        RefNo = c

                    });

                return request.CreateResponse<ReferenceNoModel[]>(HttpStatusCode.OK, refNos.ToArray());
            });
        }
    }
}
