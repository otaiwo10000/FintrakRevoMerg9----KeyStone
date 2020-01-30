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
using Fintrak.Client.IFRS.Entities;
using Fintrak.Client.IFRS.Contracts;
using Fintrak.Presentation.WebClient.Models;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/glmapping")]
    [UsesDisposableService]
    public class GLMappingApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public GLMappingApiController(IFinstatService finstatService)
        {
            _FinstatService = finstatService;
        }

        IFinstatService _FinstatService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_FinstatService);
        }

        [HttpPost]
        [Route("updateglMapping")]
        public HttpResponseMessage UpdateGLMapping(HttpRequestMessage request, [FromBody]GLMappingModel glMappingModel)
        {
            return GetHttpResponse(request, () =>
            {
                var glMapping = _FinstatService.UpdateGLMapping(glMappingModel.GLMapping, glMappingModel.Status);

                return request.CreateResponse<GLMapping>(HttpStatusCode.OK, glMapping);
                //vb
            });
        }

        [HttpPost]
        [Route("deleteglMapping")]
        public HttpResponseMessage DeleteGLMapping(HttpRequestMessage request, [FromBody]int glMappingId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                GLMapping glMapping = _FinstatService.GetGLMapping(glMappingId);

                if (glMapping != null)
                {
                    _FinstatService.DeleteGLMapping(glMappingId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No glMapping found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getglMapping/{glMappingId}")]
        public HttpResponseMessage GetGLMapping(HttpRequestMessage request, int glMappingId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                GLMapping glMapping = _FinstatService.GetGLMapping(glMappingId);

                // notice no need to create a seperate model object since GLMapping entity will do just fine
                response = request.CreateResponse<GLMapping>(HttpStatusCode.OK, glMapping);

                return response;
            });
        }

        [HttpGet]
        [Route("availableglMappings")]
        public HttpResponseMessage GetAvailableGLMappings(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                GLMappingData[] glMappings = _FinstatService.GetGLMappings();

                return request.CreateResponse<GLMappingData[]>(HttpStatusCode.OK, glMappings);
            });
        }
        ///{jobCode}/{startDate}
        [HttpGet]
        [Route("getsubcaptions/{level}/{captionCode}")]
        public HttpResponseMessage GetSubCaptionss(HttpRequestMessage request, int level, string captionCode)
        {
            return GetHttpResponse(request, () =>
            {
                GLMapping[] glMappings = _FinstatService.GetAllGLMappings();

                List<CaptionModel> captions = new List<CaptionModel>();

                List<string> distinctCaptions = null;

                if (level == 0)
                {
                    distinctCaptions = glMappings.Select(c => c.SubCaption).Distinct().ToList();
                }
                else if (level == 1)
                {
                    distinctCaptions = glMappings.Select(c => c.SubCaption1).Distinct().ToList();
                }
                else if (level == 2)
                {
                    distinctCaptions = glMappings.Select(c => c.SubCaption2).Distinct().ToList();
                }
                else if (level == 3)
                {
                    distinctCaptions = glMappings.Select(c => c.SubCaption3).Distinct().ToList();
                }
                else if (level == 4)
                {
                    distinctCaptions = glMappings.Select(c => c.SubCaption4).Distinct().ToList();
                }

                else if (level == 5)
                {
                    distinctCaptions = glMappings.Where(c => c.CaptionCode == captionCode).Select(c => c.SubCaption).Distinct().ToList();
                }

                foreach (var c in distinctCaptions)
                    captions.Add(new CaptionModel()
                    {
                        Code = c,
                        Name = c
                    });

                return request.CreateResponse<CaptionModel[]>(HttpStatusCode.OK, captions.ToArray());
            });
        }

        [HttpGet]
        [Route("getunmappedgl")]
        public HttpResponseMessage GetUnMappedGL(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                GLMappingData[] gls = _FinstatService.GetUnMappedGLs();

                return request.CreateResponse<GLMappingData[]>(HttpStatusCode.OK, gls);
            });
        }

        [HttpGet]
        [Route("getunmappedgl/{glCode}")]
        public HttpResponseMessage GetGLMapping(HttpRequestMessage request, string glCode)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                GLMappingData glMapping = _FinstatService.GetUnMappedGLbyGLCode(glCode).FirstOrDefault();

                // notice no need to create a seperate model object since GLMapping entity will do just fine
                response = request.CreateResponse<GLMappingData>(HttpStatusCode.OK, glMapping);

                return response;
            });
        }


        [HttpGet]
        [Route("getsubsubcaptions/{caption}")]
        public HttpResponseMessage GetReferenceNos(HttpRequestMessage request, string caption)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                GLMapping[] glMapping = _FinstatService.GetSubSubCaption(caption);
                List<SubSubCaptionModel> captions = new List<SubSubCaptionModel>();
                List<string> distinctsubsubCaptions = null;
                distinctsubsubCaptions = glMapping.Select(c => c.SubCaption1).Distinct().ToList();

                foreach (var c in distinctsubsubCaptions)
                    captions.Add(new SubSubCaptionModel()
                    {
                        SubCaption1 = c
                    });

                // notice no need to create a seperate model object since GLMapping entity will do just fine
                response = request.CreateResponse<SubSubCaptionModel[]>(HttpStatusCode.OK, captions.ToArray());

                return response;
            });
        }
    }
}
