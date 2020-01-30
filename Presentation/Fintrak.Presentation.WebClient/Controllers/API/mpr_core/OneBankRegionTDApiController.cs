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
    [RoutePrefix("api/onebankregiontd")]
    [UsesDisposableService]
    public class OneBankRegionTDApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public OneBankRegionTDApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }

        [HttpPost]
        [Route("updateonebankregiontd")]
        public HttpResponseMessage UpdateOneBankRegionTD(HttpRequestMessage request, [FromBody]OneBankRegionTD oneBankRegionTDModel)
        {
            return GetHttpResponse(request, () =>
            {
                var oneBankRegionTD = _MPRCoreService.UpdateOneBankRegionTD(oneBankRegionTDModel);

                return request.CreateResponse<OneBankRegionTD>(HttpStatusCode.OK, oneBankRegionTD);
            });
        }


        [HttpPost]
        [Route("deleteonebankregiontd")]
        public HttpResponseMessage DeleteOneBankRegionTD(HttpRequestMessage request, [FromBody]int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                OneBankRegionTD oneBankRegionTD = _MPRCoreService.GetOneBankRegionTD(ID);

                if (oneBankRegionTD != null)
                {
                    _MPRCoreService.DeleteOneBankRegionTD(ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No OneBankRegionTD found under that ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getonebankregiontd/{ID}")]
        public HttpResponseMessage GetOneBankRegionTD(HttpRequestMessage request, int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                OneBankRegionTD oneBankRegionTD = _MPRCoreService.GetOneBankRegionTD(ID);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<OneBankRegionTD>(HttpStatusCode.OK, oneBankRegionTD);

                return response;
            });
        }


        [HttpGet]
        [Route("availableonebankregiontd")]
        public HttpResponseMessage GetAvailableOneBankRegionTD(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                OneBankRegionTD[] oneBankRegionTD = _MPRCoreService.GetAllOneBankRegionTD();

                return request.CreateResponse<OneBankRegionTD[]>(HttpStatusCode.OK, oneBankRegionTD);
            });
        }
    }
}
