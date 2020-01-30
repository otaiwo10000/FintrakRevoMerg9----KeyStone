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
using Fintrak.Presentation.WebClient.Models;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/volumeanalysisrundates")]
    [UsesDisposableService]
    public class VolumeAnalysisRundatesApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public VolumeAnalysisRundatesApiController(IMPRBSService mprBSService)
        {
            _MPRBSService = mprBSService;
        }

        IMPRBSService _MPRBSService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRBSService);
        }

        [HttpPost]
        [Route("updatevolumeanalysisrundates")]
        //public HttpResponseMessage UpdateVolumeAnalysisRundates(HttpRequestMessage request, [FromBody]VolumeAnalysisRundates vrdModel)
        public HttpResponseMessage InsertVolumeAnalysisRundates(HttpRequestMessage request, [FromBody]VolumeAnalysisRundatesModel vrdModel)
        {
            return GetHttpResponse(request, () =>
            {
                //var vrd = _MPRBSService.UpdateVolumeAnalysisRundates(vrdModel);
                //return request.CreateResponse<VolumeAnalysisRundates>(HttpStatusCode.OK, vrd);

                VolumeAnalysisRundatesMtd ob = new VolumeAnalysisRundatesMtd();

                if (vrdModel.Id == 0)
                {
                    ob.InsertVolumeAnalysisRundates(vrdModel);
                }
                else
                {
                    ob.UpdateVolumeAnalysisRundates(vrdModel);
                }
                return request.CreateResponse(HttpStatusCode.OK);
            });
        }

        

        [HttpPost]
        [Route("deletevolumeanalysisrundates")]
        public HttpResponseMessage DeleteVolumeAnalysisRundates(HttpRequestMessage request, [FromBody]int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                VolumeAnalysisRundatesMtd ob = new VolumeAnalysisRundatesMtd();
                ob.DeleteVolumeAnalysisRundates(Id);

                response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No volume analysis rundate found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getvolumeanalysisrundates/{Id}")]
        public HttpResponseMessage GetVolumeAnalysisRundates(HttpRequestMessage request, int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                //VolumeAnalysisRundates vrd = _MPRBSService.GetVolumeAnalysisRundates(Id);
                //response = request.CreateResponse<VolumeAnalysisRundates>(HttpStatusCode.OK, vrd);

                VolumeAnalysisRundatesModel vrd = new VolumeAnalysisRundatesModel();
                VolumeAnalysisRundatesMtd ob = new VolumeAnalysisRundatesMtd();

                vrd = ob.GetVolumeAnalysisRundates(Id);
                response = request.CreateResponse<VolumeAnalysisRundatesModel>(HttpStatusCode.OK, vrd);

                return response;
            });
        }

        [HttpGet]
        [Route("getallvolumeanalysisrundates")]
        public HttpResponseMessage GetAllVolumeAnalysisRundates(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                //VolumeAnalysisRundates[] vrd = _MPRBSService.GetAllVolumeAnalysisRundates();
                //return request.CreateResponse<VolumeAnalysisRundates[]>(HttpStatusCode.OK, vrd);


                List<VolumeAnalysisRundatesModel> vrd = new List<VolumeAnalysisRundatesModel>();
                VolumeAnalysisRundatesMtd ob = new VolumeAnalysisRundatesMtd();

                vrd = ob.GetAllVolumeAnalysisRundates();

                return request.CreateResponse(HttpStatusCode.OK, vrd);
            });
        }

        
    }
}
