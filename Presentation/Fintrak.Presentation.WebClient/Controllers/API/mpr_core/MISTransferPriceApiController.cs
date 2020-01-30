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
    [RoutePrefix("api/mistransferprice")]
    [UsesDisposableService]
    public class MISTransferPriceApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public MISTransferPriceApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);
        }


        [HttpPost]
        [Route("updatemistransferprice")]
        public HttpResponseMessage UpdateMISTP(HttpRequestMessage request, [FromBody]MISTransferPrice mistpModel)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var mistp = _MPRCoreService.UpdateMISTransferPrice(mistpModel);

                 response = request.CreateResponse<MISTransferPrice>(HttpStatusCode.OK, mistp);

                return response;
            });
        }


        [HttpPost]
        [Route("deletemistransferprice")]
        public HttpResponseMessage DeleteMISTP(HttpRequestMessage request, [FromBody]int mistransferpriceId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                MISTransferPrice mistp = _MPRCoreService.GetMISTransferPrice(mistransferpriceId);

                if (mistp != null)
                {
                    _MPRCoreService.DeleteMISTransferPrice(mistransferpriceId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No MIS Transfer Price found under the ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getmistransferprice/{mistransferpriceId}")]
        public HttpResponseMessage GetMISTP(HttpRequestMessage request, int mistransferpriceId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                MISTransferPrice mistp = _MPRCoreService.GetMISTransferPrice(mistransferpriceId);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<MISTransferPrice>(HttpStatusCode.OK, mistp);

                return response;
            });
        }


        [HttpGet]
        [Route("getallmistransferprice")]
        public HttpResponseMessage GetAllMISTP(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                MISTransferPrice[] mistp = _MPRCoreService.GetAllMISTransferPrice();

                return request.CreateResponse<MISTransferPrice[]>(HttpStatusCode.OK, mistp);
            });
        }

        [HttpGet]
        [Route("getmistransferpriceusingsetup")]
        public HttpResponseMessage GetMISTPUsingSetup(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                MISTransferPriceData[] mistp = _MPRCoreService.GetMISTransferPriceUsingSetUp();

                // notice no need to create a seperate model object since TeamDefinition entity will do just fine
                response = request.CreateResponse<MISTransferPriceData[]>(HttpStatusCode.OK, mistp);

                return response;
            });
        }

        [HttpGet] 
        [Route("getmistransferpriceusingparams/{defCode}/{miscode}/{category}/{currency}/{year}/{period}")]
        public HttpResponseMessage GetMISTPUsingParams(HttpRequestMessage request, string defCode, string miscode, string category, string currency, int year, int period)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                MISTransferPriceData[] mistp = _MPRCoreService.GetMISTransferPricebyParams( defCode,  miscode,  category,  currency,  year,  period);

                // notice no need to create a seperate model object since TeamDefinition entity will do just fine
                response = request.CreateResponse<MISTransferPriceData[]>(HttpStatusCode.OK, mistp);

                return response;
            });
        }
    }
}
