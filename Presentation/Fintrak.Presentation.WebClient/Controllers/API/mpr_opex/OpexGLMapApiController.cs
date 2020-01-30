using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Client.Core.Contracts;
using Fintrak.Client.Core.Entities;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.MPR.Entities;
using Fintrak.Client.MPR.Contracts;
using Fintrak.Presentation.WebClient.Models;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/opexglmap")]
    [UsesDisposableService]
    public class OpexGLMapApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public OpexGLMapApiController(IMPROPEXService mprOPEXService)
        {
            _MPROPEXService = mprOPEXService;
        }

        IMPROPEXService _MPROPEXService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPROPEXService);
        }

        [HttpPost]
        [Route("updateglMap")]
        public HttpResponseMessage UpdateGLMap(HttpRequestMessage request, [FromBody]OpexGLMap glMapModel)
        {
            return GetHttpResponse(request, () =>
            {
                var glMap = _MPROPEXService.UpdateOpexGLMap(glMapModel);

                return request.CreateResponse<OpexGLMap>(HttpStatusCode.OK, glMap);
            });
        }

        [HttpPost]
        [Route("deleteglMap")]
        public HttpResponseMessage DeleteGLMap(HttpRequestMessage request, [FromBody]int glMapId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                OpexGLMap glMap = _MPROPEXService.GetOpexGLMap(glMapId);

                if (glMap != null)
                {
                    _MPROPEXService.DeleteOpexGLMapping(glMapId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No glMap found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getglMap/{glMapId}")]
        public HttpResponseMessage GetGLMap(HttpRequestMessage request,int glMapId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                OpexGLMap glMap = _MPROPEXService.GetOpexGLMap(glMapId);

                // notice no need to create a seperate model object since GLMapping entity will do just fine
                response = request.CreateResponse<OpexGLMap>(HttpStatusCode.OK, glMap);

                return response;
            });
        }

        [HttpGet]
        [Route("availableglMaps")]
        public HttpResponseMessage GetAvailableGLMaps(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                OpexGLMap[] glMaps = _MPROPEXService.GetAllOpexGLMap();

                return request.CreateResponse<OpexGLMap[]>(HttpStatusCode.OK, glMaps);
            });
        }

        //[HttpGet]
        //[Route("getsubcaptions/{level}")]
        //public HttpResponseMessage GetSubCaptionss(HttpRequestMessage request,int level)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        OpexGLMapping[] glMappings = _MPROPEXService.GetAllOpexGLMappings();

        //        List<CaptionModel> captions = new List<CaptionModel>();

        //        List<string> distinctCaptions = null;

        //        if (level == 0){
        //            distinctCaptions = glMappings.Select(c => c.Caption).Distinct().ToList();
        //        }
        //        else if (level == 1)
        //        {
        //            distinctCaptions = glMappings.Select(c => c.SubCaption).Distinct().ToList();
        //        }
        //        else if (level == 2)
        //        {
        //            distinctCaptions = glMappings.Select(c => c.SubCaption1).Distinct().ToList();
        //        }
        //        else if (level == 3)
        //        {
        //            distinctCaptions = glMappings.Select(c => c.SubCaption2).Distinct().ToList();
        //        }
        //        else if (level == 4)
        //        {
        //            distinctCaptions = glMappings.Select(c => c.SubCaption3).Distinct().ToList();
        //        }
        //        else if (level == 5)
        //        {
        //            distinctCaptions = glMappings.Select(c => c.SubCaption4).Distinct().ToList();
        //        }
                
        //        foreach (var c in distinctCaptions)
        //            captions.Add(new CaptionModel() {
        //                Code = c,
        //                Name = c
        //            });

        //        return request.CreateResponse<CaptionModel[]>(HttpStatusCode.OK, captions.ToArray());
        //    });
        //}

       
      
    }
}
