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
    [RoutePrefix("api/onebankbranch")]
    [UsesDisposableService]
    public class OneBankBranchApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public OneBankBranchApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }

        [HttpPost]
        [Route("updateonebankbranch")]
        public HttpResponseMessage UpdateOneBankBranch(HttpRequestMessage request, [FromBody]OneBankBranch onebankModel)
        {
            return GetHttpResponse(request, () =>
            {
                var onebank = _MPRCoreService.UpdateOneBankBranch(onebankModel);

                return request.CreateResponse<OneBankBranch>(HttpStatusCode.OK, onebank);
            });
        }


        [HttpPost]
        [Route("deleteonebankbranch")]
        public HttpResponseMessage DeleteAbcRatio(HttpRequestMessage request, [FromBody]int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                OneBankBranch onebank = _MPRCoreService.GetOneBankBranch(Id);

                if (onebank != null)
                {
                    _MPRCoreService.DeleteOneBankBranch(Id);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No item found under that ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getonebankbranch/{Id}")]
        public HttpResponseMessage GetOneBankBranch(HttpRequestMessage request, int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                OneBankBranch onebank = _MPRCoreService.GetOneBankBranch(Id);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<OneBankBranch>(HttpStatusCode.OK, onebank);

                return response;
            });
        }


        [HttpGet]
        [Route("availableonebankbranch")]
        public HttpResponseMessage GetAvailableOneBankAO(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                OneBankBranch[] onebank = _MPRCoreService.GetAllOneBankBranch();

                return request.CreateResponse<OneBankBranch[]>(HttpStatusCode.OK, onebank);
            });
        }

        [HttpGet]
        [Route("getonebankbranch/{SearchValue}/{year}/{period}")]
        public HttpResponseMessage GetAvailableOneBankBranch(HttpRequestMessage request, string SearchValue, int year, int period)
        {
            return GetHttpResponse(request, () =>
            {
                OneBankBranch[] onebank = _MPRCoreService.GetOneBankBranchByParams(SearchValue, year, period);

                return request.CreateResponse<OneBankBranch[]>(HttpStatusCode.OK, onebank);
            });
        }

    }
}
