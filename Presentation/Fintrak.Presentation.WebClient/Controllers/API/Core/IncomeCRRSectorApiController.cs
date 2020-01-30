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

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/incomecrrsector")]
    [UsesDisposableService]
    public class IncomeCRRSectorApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomeCRRSectorApiController(ICoreService coreService)
        {
            _CoreService = coreService;
        }

        ICoreService _CoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_CoreService);
        }

        [HttpPost]
        [Route("updateincomecrrsector")]
        public HttpResponseMessage UpdateIncomeCRRSector(HttpRequestMessage request, [FromBody]IncomeCRRSector icsModel)
        {
            return GetHttpResponse(request, () =>
            {
                var ics = _CoreService.UpdateIncomeCRRSector(icsModel);

                return request.CreateResponse<IncomeCRRSector>(HttpStatusCode.OK, ics);
            });
        }

        [HttpPost]
        [Route("deleteincomecrrsector")]
        public HttpResponseMessage DeleteIncomeCRRSector(HttpRequestMessage request, [FromBody]int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IncomeCRRSector ics = _CoreService.GetIncomeCRRSector(Id);

                if (ics != null)
                {
                    _CoreService.DeleteIncomeCRRSector(Id);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No IncomeCRRSector found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getincomecrrsector/{Id}")]
        public HttpResponseMessage GetIncomeCRRSector(HttpRequestMessage request,int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IncomeCRRSector ics = _CoreService.GetIncomeCRRSector(Id);

                // notice no need to create a seperate model object since Staff entity will do just fine
                response = request.CreateResponse<IncomeCRRSector>(HttpStatusCode.OK, ics);

                return response;
            });
        }

        [HttpGet]
        [Route("availableincomecrrsector")]
        public HttpResponseMessage GetAvailableIncomeCRRSector(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeCRRSector[] ics = _CoreService.GetAllIncomeCRRSector();

                return request.CreateResponse<IncomeCRRSector[]>(HttpStatusCode.OK, ics);
            });
        }
    }
}
