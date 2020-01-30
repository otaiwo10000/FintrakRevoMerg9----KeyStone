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
    [RoutePrefix("api/incomeproductshare")]
    [UsesDisposableService]
    public class IncomeProductShareApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomeProductShareApiController(IMPROPEXService mprOpexService)
        {
            _MPROPEXService = mprOpexService;
        }

        IMPROPEXService _MPROPEXService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPROPEXService);
        }

        [HttpPost]
        [Route("updateincomeproductshare")]
        public HttpResponseMessage UpdateIncomeProductShare(HttpRequestMessage request, [FromBody]IncomeProductShare opexModel)
        {
            return GetHttpResponse(request, () =>
            {
                var opex = _MPROPEXService.UpdateIncomeProductShare(opexModel);

                return request.CreateResponse<IncomeProductShare>(HttpStatusCode.OK, opex);
            });
        }

        [HttpPost]
        [Route("deleteincomeproductshare")]
        public HttpResponseMessage DeleteIncomeProductShare(HttpRequestMessage request, [FromBody]int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IncomeProductShare opex = _MPROPEXService.GetIncomeProductShare(ID);

                if (opex != null)
                {
                    _MPROPEXService.DeleteIncomeProductShare(ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No IncomeProductShare found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getincomeproductshare/{ID}")]
        public HttpResponseMessage GetIncomeProductShare(HttpRequestMessage request, int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IncomeProductShare opex = _MPROPEXService.GetIncomeProductShare(ID);

                // notice no need to create a seperate model object since ActivityBase entity will do just fine
                response = request.CreateResponse<IncomeProductShare>(HttpStatusCode.OK, opex);

                return response;
            });
        }

        [HttpGet]
        [Route("availableincomeproductshare")]
        public HttpResponseMessage GetAvailableIncomeProductShare(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeProductShare[] opex = _MPROPEXService.GetAllIncomeProductShare();

                return request.CreateResponse<IncomeProductShare[]>(HttpStatusCode.OK, opex);
            });
        }
    }
}
