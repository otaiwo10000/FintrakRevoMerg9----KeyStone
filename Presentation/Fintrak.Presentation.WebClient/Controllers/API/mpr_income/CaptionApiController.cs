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
using Fintrak.Presentation.WebClient.Models;
using Fintrak.Shared.Common.Extensions;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/caption")]
    [UsesDisposableService]
    public class CaptionApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public CaptionApiController(IMPRIncomeService mprincomeService)
        {
            _MPRIncomeService = mprincomeService;
        }

        IMPRIncomeService _MPRIncomeService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRIncomeService);
        }


        [HttpPost]
        [Route("updatecaption")]
        public HttpResponseMessage UpdateCaption(HttpRequestMessage request, [FromBody]Caption captionModel)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var caption = _MPRIncomeService.UpdateCaption(captionModel);

                //var response =  request.CreateResponse<ScoreCardMetrics>(HttpStatusCode.OK, scm);
                 response = request.CreateResponse<Caption>(HttpStatusCode.OK, caption);

                return response;
            });
        }


        [HttpPost]
        [Route("deletecaption")]
        public HttpResponseMessage DeleteCaption(HttpRequestMessage request, [FromBody]int CaptionId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                Caption caption = _MPRIncomeService.GetCaption(CaptionId);

                if (caption != null)
                {
                    _MPRIncomeService.DeleteCaption(CaptionId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No caption found under the captionid.");

                return response;
            });
        }


        [HttpGet]
        [Route("getcaption/{captionid}")]
        public HttpResponseMessage GetIncomeProductstable(HttpRequestMessage request, int CaptionId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                Caption caption = _MPRIncomeService.GetCaption(CaptionId);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<Caption>(HttpStatusCode.OK, caption);

                return response;
            });
        }

        [HttpGet]
        [Route("getallcaptions")]
        public HttpResponseMessage GetAllCaptions(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                Caption[] captions = _MPRIncomeService.GetAllCaptions();

                return request.CreateResponse<Caption[]>(HttpStatusCode.OK, captions);
            });
        }
    

    }
}
