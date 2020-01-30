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
    [RoutePrefix("api/mprinterestmapping")]
    [UsesDisposableService]
    public class MprInterestMappingApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public MprInterestMappingApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }

        [HttpPost]
        [Route("updatemprinterestmapping")]
        public HttpResponseMessage UpdateMprInterestMapping(HttpRequestMessage request, [FromBody]MprInterestMapping mprInterestMappingModel)
        {
            return GetHttpResponse(request, () =>
            {
                var mprInterestMapping = _MPRCoreService.UpdateMprInterestMapping(mprInterestMappingModel);

                return request.CreateResponse<MprInterestMapping>(HttpStatusCode.OK, mprInterestMapping);
            });
        }


        [HttpPost]
        [Route("deletemprinterestmapping")]
        public HttpResponseMessage DeleteMprInterestMMapping(HttpRequestMessage request, [FromBody]int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                MprInterestMapping mprInterestMapping = _MPRCoreService.GetMprInterestMapping(ID);

                if (mprInterestMapping != null)
                {
                    _MPRCoreService.DeleteMprInterestMapping(ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No MprInterestMapping found under that ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getmprinterestmapping/{ID}")]
        public HttpResponseMessage GetMprInterestMapping(HttpRequestMessage request, int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                MprInterestMapping mprInterestMapping = _MPRCoreService.GetMprInterestMapping(ID);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<MprInterestMapping>(HttpStatusCode.OK, mprInterestMapping);

                return response;
            });
        }


        [HttpGet]
        [Route("availablemprinterestmapping")]
        public HttpResponseMessage GetAvailableMprInterestMapping(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                MprInterestMapping[] mprInterestMapping = _MPRCoreService.GetAllMprInterestMapping();

                return request.CreateResponse<MprInterestMapping[]>(HttpStatusCode.OK, mprInterestMapping);
            });
        }
    }
}
