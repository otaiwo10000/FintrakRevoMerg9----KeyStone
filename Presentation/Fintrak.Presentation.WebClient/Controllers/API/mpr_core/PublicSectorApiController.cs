
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
    [RoutePrefix("api/publicsector")]
    [UsesDisposableService]
    public class PublicSectorApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public PublicSectorApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);
        }

        [HttpGet]
        [Route("publicsectors")]
        public HttpResponseMessage GetPublicSectorsView(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IEnumerable<PublicSectorData> publicsectors = _MPRCoreService.GetAllPublicSectorData();

                return request.CreateResponse<IEnumerable<PublicSectorData>>(HttpStatusCode.OK, publicsectors);
            });
        }
    }
}
