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
    [RoutePrefix("api/onebankteamtable")]
    [UsesDisposableService]
    public class OneBankTeamTableApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public OneBankTeamTableApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }

        [HttpPost]
        [Route("updateonebankteamtable")]
        public HttpResponseMessage UpdateOneBankTeamTable(HttpRequestMessage request, [FromBody]OneBankTeamTable oneBankTeamTableModel)
        {
            return GetHttpResponse(request, () =>
            {
                var oneBankTeamTable = _MPRCoreService.UpdateOneBankTeamTable(oneBankTeamTableModel);

                return request.CreateResponse<OneBankTeamTable>(HttpStatusCode.OK, oneBankTeamTable);
            });
        }


        [HttpPost]
        [Route("deleteonebankteamtable")]
        public HttpResponseMessage DeleteOneBankTeamTable(HttpRequestMessage request, [FromBody]int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                OneBankTeamTable oneBankTeamTable = _MPRCoreService.GetOneBankTeamTable(ID);

                if (oneBankTeamTable != null)
                {
                    _MPRCoreService.DeleteOneBankTeamTable(ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No OneBankTeamTable found under that ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getonebankteamtable/{ID}")]
        public HttpResponseMessage GetOneBankTeamTable(HttpRequestMessage request, int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                OneBankTeamTable oneBankTeamTable = _MPRCoreService.GetOneBankTeamTable(ID);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<OneBankTeamTable>(HttpStatusCode.OK, oneBankTeamTable);

                return response;
            });
        }


        [HttpGet]
        [Route("availableonebankteamtable")]
        public HttpResponseMessage GetAvailableOneBankTeamTable(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                OneBankTeamTable[] oneBankTeamTable = _MPRCoreService.GetAllOneBankTeamTable();

                return request.CreateResponse<OneBankTeamTable[]>(HttpStatusCode.OK, oneBankTeamTable);
            });
        }
    }
}
