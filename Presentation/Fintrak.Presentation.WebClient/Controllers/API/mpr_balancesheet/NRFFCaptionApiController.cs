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
    [RoutePrefix("api/nrffcaption")]
    [UsesDisposableService]
    public class NRFFCaptionApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public NRFFCaptionApiController(IMPRBSService mprBSService)
        {
            _MPRBSService = mprBSService;
        }

        IMPRBSService _MPRBSService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRBSService);
        }

        [HttpPost]
        [Route("updatenrffcaption")]
        public HttpResponseMessage UpdateNRFFCaption(HttpRequestMessage request, [FromBody]NRFFCaption nRFFCaptionModel)
        {
            return GetHttpResponse(request, () =>
            {
                var nRFFCaption = _MPRBSService.UpdateNRFFCaption(nRFFCaptionModel);

                return request.CreateResponse<NRFFCaption>(HttpStatusCode.OK, nRFFCaption);
            });
        }

        [HttpPost]
        [Route("deletenRFFCaption")]
        public HttpResponseMessage DeleteNRFFCaption(HttpRequestMessage request, [FromBody]int NRFFCaption_Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                NRFFCaption nRFFCaption = _MPRBSService.GetNRFFCaption(NRFFCaption_Id);

                if (nRFFCaption != null)
                {
                    _MPRBSService.DeleteNRFFCaption(NRFFCaption_Id);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No NRFFCaption found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getnrffcaption/{NRFFCaption_Id}")]
        public HttpResponseMessage GetNRFFCaption(HttpRequestMessage request, int NRFFCaption_Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                NRFFCaption nRFFCaption = _MPRBSService.GetNRFFCaption(NRFFCaption_Id);

                // notice no need to create a seperate model object since NRFFCaption entity will do just fine
                response = request.CreateResponse<NRFFCaption>(HttpStatusCode.OK, nRFFCaption);

                return response;
            });
        }

        [HttpGet]
        [Route("availablenrffcaptions")]
        public HttpResponseMessage GetAvailableNRFFCaptions(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                NRFFCaption[] nRFFCaptions = _MPRBSService.GetAllNRFFCaptions();

                return request.CreateResponse<NRFFCaption[]>(HttpStatusCode.OK, nRFFCaptions);
            });
        }
    }
}
