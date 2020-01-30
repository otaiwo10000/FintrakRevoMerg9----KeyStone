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
    [RoutePrefix("api/ftpriskratings")]
    [UsesDisposableService]
    public class FTPRiskRatingsApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public FTPRiskRatingsApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }

        [HttpPost]
        [Route("updateftpriskratings")]
        public HttpResponseMessage UpdateFTPRiskRatings(HttpRequestMessage request, [FromBody]FTPRiskRatings fTPRiskRatingsModel)
        {
            return GetHttpResponse(request, () =>
            {
                var fTPRiskRatings = _MPRCoreService.UpdateFTPRiskRatings(fTPRiskRatingsModel);

                return request.CreateResponse<FTPRiskRatings>(HttpStatusCode.OK, fTPRiskRatings);
            });
        }


        [HttpPost]
        [Route("deleteftpriskratings")]
        public HttpResponseMessage DeleteFTPRiskRatings(HttpRequestMessage request, [FromBody]int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                FTPRiskRatings fTPRiskRatings = _MPRCoreService.GetFTPRiskRatings(ID);

                if (fTPRiskRatings != null)
                {
                    _MPRCoreService.DeleteFTPRiskRatings(ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No FTPRiskRatings found under that ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getftpriskratings/{ID}")]
        public HttpResponseMessage GetFTPRiskRatings(HttpRequestMessage request, int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                FTPRiskRatings fTPRiskRatings = _MPRCoreService.GetFTPRiskRatings(ID);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<FTPRiskRatings>(HttpStatusCode.OK, fTPRiskRatings);

                return response;
            });
        }


        [HttpGet]
        [Route("availableftpriskratings")]
        public HttpResponseMessage GetAvailableFTPRiskRatings(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                FTPRiskRatings[] fTPRiskRatings = _MPRCoreService.GetAllFTPRiskRatings();

                return request.CreateResponse<FTPRiskRatings[]>(HttpStatusCode.OK, fTPRiskRatings);
            });
        }

        [HttpGet]
        [Route("getftpriskratingsusingsearch/{searchvalue}")]
        public HttpResponseMessage GetFTPRiskRatingsUsingSearchValue(HttpRequestMessage request, string searchvalue)
        {
            return GetHttpResponse(request, () =>
            {
                searchvalue = searchvalue.Replace("FORWARDSLASHXTER", "/");
                searchvalue = searchvalue.Replace("DOTXTER", ".");

                HttpResponseMessage response = null;
                FTPRiskRatings[] fTPRiskRatings = _MPRCoreService.GetFTPRiskRatingsUsingSearchValue(searchvalue);

                // notice no need to create a seperate model object since TeamDefinition entity will do just fine
                response = request.CreateResponse<FTPRiskRatings[]>(HttpStatusCode.OK, fTPRiskRatings);

                return response;
            });
        }
    }
}
