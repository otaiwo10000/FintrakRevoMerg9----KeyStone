
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
    [RoutePrefix("api/crb_Data")]
    [UsesDisposableService]
    public class crb_DataApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public crb_DataApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);
        }

        [HttpGet]
        [Route("availablecrb_Data")]
        public HttpResponseMessage GetAvailablecrb_Data(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IEnumerable<crb_Data> crbdata = _MPRCoreService.GetAllCrbData();

                return request.CreateResponse<IEnumerable<crb_Data>>(HttpStatusCode.OK, crbdata);
            });
        }
    }
}
