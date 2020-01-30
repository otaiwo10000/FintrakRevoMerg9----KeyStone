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
using Fintrak.Presentation.WebClient.Additionalmethods;
using Fintrak.Presentation.WebClient.Models.MPR;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/volumeanalysisrundate")]
    [UsesDisposableService]
    public class VolumeAnalysisRundateApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public VolumeAnalysisRundateApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);
        }

        

        [HttpGet]
        [Route("updatevolumeanalysisrundate")]
        public HttpResponseMessage GetAccountTransferPrice(HttpRequestMessage request, [FromBody]VolumeAnalysisRundateRundateModel volmodel)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                VolumeAnalysisRundateMtd volObj = new VolumeAnalysisRundateMtd();
                volObj.UpdateVolumeAnalysisRundate(volmodel);

                return request.CreateResponse(HttpStatusCode.OK, response);
            });
        }


        [HttpGet]
        [Route("getvolumeanalysisrundate/{Id}")]
        public HttpResponseMessage TeamStructureRegion(HttpRequestMessage request, int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                VolumeAnalysisRundateMtd volObj = new VolumeAnalysisRundateMtd();
                VolumeAnalysisRundateRundateModel volObj2 = new VolumeAnalysisRundateRundateModel();
                volObj2 = volObj.GetVolumeAnalysisRundateById(Id);

                response = request.CreateResponse<VolumeAnalysisRundateRundateModel>(HttpStatusCode.OK, volObj2);

                return response;
            });
        }

        [HttpGet]
        [Route("getallvolumeanalysis")]
        public HttpResponseMessage GetAllVolumeAnalysis(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {            
                VolumeAnalysisRundateMtd volObj = new VolumeAnalysisRundateMtd();
                List<VolumeAnalysisRundateRundateModel> volList = new List<VolumeAnalysisRundateRundateModel>();
                volList = volObj.GetAllAnalysisRundate().ToList();

                return request.CreateResponse(HttpStatusCode.OK, volList);
            });
        }
    }
}
