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
    [RoutePrefix("api/incomesetup")]
    [UsesDisposableService]
    public class IncomeSetupApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomeSetupApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }

        [HttpPost]
        [Route("updateincomesetup")]
        public HttpResponseMessage UpdateIncomeSetup(HttpRequestMessage request, [FromBody]IncomeSetup incomeSetupModel)
        {
            return GetHttpResponse(request, () =>
            {
                var incomeSetup = _MPRCoreService.UpdateIncomeSetup(incomeSetupModel);

                return request.CreateResponse<IncomeSetup>(HttpStatusCode.OK, incomeSetup);
            });
        }


        [HttpPost]
        [Route("deleteincomesetup")]
        public HttpResponseMessage DeleteIncomeSetup(HttpRequestMessage request, [FromBody]int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IncomeSetup incomeSetup = _MPRCoreService.GetIncomeSetup(ID);

                if (incomeSetup != null)
                {
                    _MPRCoreService.DeleteIncomeSetup(ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No IncomeSetup found under that ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getincomesetup/{ID}")]
        public HttpResponseMessage GetIncomeSetup(HttpRequestMessage request, int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IncomeSetup incomeSetup = _MPRCoreService.GetIncomeSetup(ID);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<IncomeSetup>(HttpStatusCode.OK, incomeSetup);

                return response;
            });
        }


        [HttpGet]
        [Route("availableincomesetup")]
        public HttpResponseMessage GetAvailableIncomeSetup(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeSetup[] incomeSetup = _MPRCoreService.GetAllIncomeSetup();

                return request.CreateResponse<IncomeSetup[]>(HttpStatusCode.OK, incomeSetup);
            });
        }
        //    .GroupBy(x => new { x.Segment, x.OtherCaption, x.MainCaption, x.Currency
        //}).Select(o => o.FirstOrDefault());
        //[HttpGet]
        //[Route("latestincomesetup")]
        //public HttpResponseMessage GetLatestIncomeSetup(HttpRequestMessage request)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        var incomeSetup = _MPRCoreService.GetAllIncomeSetup().OrderByDescending(x=>new { x.Year, x.CurrentPeriod});
        //        IncomeSetup latestincomesetup = incomeSetup.FirstOrDefault();

        //        return request.CreateResponse<IncomeSetup>(HttpStatusCode.OK, latestincomesetup);
        //    });
        //}

        [HttpGet]
        [Route("latestincomesetup")]
        public HttpResponseMessage GetLatestIncomeSetup(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeSetup latestincomesetup = _MPRCoreService.GetLatestIncomeSetup();

                return request.CreateResponse<IncomeSetup>(HttpStatusCode.OK, latestincomesetup);
            });
        }

    }
}
