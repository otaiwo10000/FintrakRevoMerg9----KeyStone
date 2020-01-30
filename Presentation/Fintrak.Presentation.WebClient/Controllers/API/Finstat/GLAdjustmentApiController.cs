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
using Fintrak.Shared.IFRS.Framework;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/gladjustment")]
    [UsesDisposableService]
    public class GLAdjustmentApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public GLAdjustmentApiController(IFinstatService finstatService)
        {
            _FinstatService = finstatService;
        }

        IFinstatService _FinstatService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_FinstatService);
        }

        [HttpPost]
        [Route("updateglAdjustment")]
        public HttpResponseMessage UpdateGLAdjustment(HttpRequestMessage request, [FromBody]GLAdjustment glAdjustmentModel)
        {
            return GetHttpResponse(request, () =>
            {
                var glAdjustment = _FinstatService.UpdateGLAdjustment(glAdjustmentModel);

                return request.CreateResponse<GLAdjustment>(HttpStatusCode.OK, glAdjustment);
            });
        }

        [HttpPost]
        [Route("postadjustment/{adjustmentType}")]
        public HttpResponseMessage PostGLAdjustment(HttpRequestMessage request, int adjustmentType)
        {
            return GetHttpResponse(request, () =>
            {
                if (adjustmentType == 1)
                  _FinstatService.PostGLAdjustment(AdjustmentType.GAAP);
                else if (adjustmentType == 2)
                    _FinstatService.PostGLAdjustment(AdjustmentType.IFRS);

                return request.CreateResponse(HttpStatusCode.OK);
            });
        }

        [HttpPost]
        [Route("deleteglAdjustment")]
        public HttpResponseMessage DeleteGLAdjustment(HttpRequestMessage request, [FromBody]int glAdjustmentId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                GLAdjustment glAdjustment = _FinstatService.GetGLAdjustment(glAdjustmentId);

                if (glAdjustment != null)
                {
                    _FinstatService.DeleteGLAdjustment(glAdjustmentId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No GL Adjustment found under that ID.");

                return response;
            });
        }

        [HttpPost]
        [Route("deletegladjustmentbycode/{adjustmentType}/{adjustmentCode}")]
        public HttpResponseMessage DeleteGLAdjustment(HttpRequestMessage request, int adjustmentType, string adjustmentCode)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (adjustmentType == 1)
                    _FinstatService.DeleteGLAdjustmentByCode(AdjustmentType.GAAP, adjustmentCode);
                else if (adjustmentType == 2)
                    _FinstatService.DeleteGLAdjustmentByCode(AdjustmentType.IFRS, adjustmentCode);

                return response;
            });
        }

        [HttpPost]
        [Route("reversegladjustmentbycode/{adjustmentType}/{adjustmentCode}")]
        public HttpResponseMessage ReverseGLAdjustment(HttpRequestMessage request, int adjustmentType, string adjustmentCode)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (adjustmentType == 1)
                    _FinstatService.ReverseGLAdjustmentByCode(AdjustmentType.GAAP, adjustmentCode);
                else if (adjustmentType == 2)
                    _FinstatService.ReverseGLAdjustmentByCode(AdjustmentType.IFRS, adjustmentCode);

                return request.CreateResponse(HttpStatusCode.OK);
            });
        }

        [HttpPost]
        [Route("purgegladjustmentbycode/{adjustmentType}")]
        public HttpResponseMessage ReverseGLAdjustment(HttpRequestMessage request, int adjustmentType)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (adjustmentType == 1)
                    _FinstatService.PurgeGLAdjustment(AdjustmentType.GAAP);
                else if (adjustmentType == 2)
                    _FinstatService.PurgeGLAdjustment(AdjustmentType.IFRS);

                return request.CreateResponse(HttpStatusCode.OK);
            });
        }

        [HttpGet]
        [Route("getglAdjustment/{glAdjustmentId}")]
        public HttpResponseMessage GetGLAdjustment(HttpRequestMessage request,int glAdjustmentId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                GLAdjustment glAdjustment = _FinstatService.GetGLAdjustment(glAdjustmentId);

                // notice no need to create a seperate model object since GLAdjustment entity will do just fine
                response = request.CreateResponse<GLAdjustment>(HttpStatusCode.OK, glAdjustment);

                return response;
            });
        }

        [HttpGet]
        [Route("getgladjustments/{adjustmentType}")]
        public HttpResponseMessage GetAvailableGLAdjustments(HttpRequestMessage request, int adjustmentType)
        {
            return GetHttpResponse(request, () =>
            {
                GLAdjustmentData[] glAdjustments = null;

                if (adjustmentType == 1)
                    glAdjustments = _FinstatService.GetGLAdjustments(AdjustmentType.GAAP);
                else if (adjustmentType == 2)
                    glAdjustments = _FinstatService.GetGLAdjustments(AdjustmentType.IFRS);
               
                return request.CreateResponse<GLAdjustmentData[]>(HttpStatusCode.OK, glAdjustments);
            });
        }

        [HttpGet]
        [Route("getgladjustmentbystatus/{adjustmentType}/{status}")]
        public HttpResponseMessage GetGLAdjustmentByStatus(HttpRequestMessage request, int adjustmentType,bool status)
        {
            return GetHttpResponse(request, () =>
            {
                GLAdjustmentData[] glAdjustments = null;

                if (adjustmentType == 1)
                    glAdjustments = _FinstatService.GetGLAdjustmentByStatus(AdjustmentType.GAAP, status);
                else if (adjustmentType == 2)
                    glAdjustments = _FinstatService.GetGLAdjustmentByStatus(AdjustmentType.IFRS, status);

                return request.CreateResponse<GLAdjustmentData[]>(HttpStatusCode.OK, glAdjustments);
            });
        }
    }
}
