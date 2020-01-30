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
    [RoutePrefix("api/onebankao")]
    [UsesDisposableService]
    public class OneBankAOApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public OneBankAOApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }

        [HttpPost]
        [Route("updateonebankao")]
        public HttpResponseMessage UpdateOneBankAO(HttpRequestMessage request, [FromBody]OneBankAO onebankModel)
        {
            return GetHttpResponse(request, () =>
            {
                var onebank = _MPRCoreService.UpdateOneBankAO(onebankModel);

                return request.CreateResponse<OneBankAO>(HttpStatusCode.OK, onebank);
            });
        }


        [HttpPost]
        [Route("deleteonebankao")]
        public HttpResponseMessage DeleteAbcRatio(HttpRequestMessage request, [FromBody]int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                OneBankAO onebankao = _MPRCoreService.GetOneBankAO(Id);

                if (onebankao != null)
                {
                    _MPRCoreService.DeleteOneBankAO(Id);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No item found under that ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getonebankao/{Id}")]
        public HttpResponseMessage GetOneBankAO(HttpRequestMessage request, int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                OneBankAO onebankao = _MPRCoreService.GetOneBankAO(Id);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<OneBankAO>(HttpStatusCode.OK, onebankao);

                return response;
            });
        }


        [HttpGet]
        [Route("availableonebankao")]
        public HttpResponseMessage GetAvailableOneBankAO(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                OneBankAO[] onebankao = _MPRCoreService.GetAllOneBankAO();

                return request.CreateResponse<OneBankAO[]>(HttpStatusCode.OK, onebankao);
            });
        }

        [HttpGet]
        [Route("getonebankao/{SearchValue}/{year}/{period}")]
        public HttpResponseMessage GetAvailableOneBankAO(HttpRequestMessage request, string SearchValue, int year, int period)
        {
            return GetHttpResponse(request, () =>
            {
                OneBankAO[] onebankao = _MPRCoreService.GetOneBankAOByParams(SearchValue, year, period);

                return request.CreateResponse<OneBankAO[]>(HttpStatusCode.OK, onebankao);
            });
        }

    }
}
