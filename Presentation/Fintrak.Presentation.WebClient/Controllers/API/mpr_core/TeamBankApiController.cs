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
    [RoutePrefix("api/teambank")]
    [UsesDisposableService]
    public class TeamBankApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public TeamBankApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);
        }


        [HttpPost]
        [Route("updateteambank")]
        public HttpResponseMessage UpdateTeamStructure(HttpRequestMessage request, [FromBody]TeamBank tsModel)
        {
            return GetHttpResponse(request, () =>
            {
                var ts = _MPRCoreService.UpdateTeamBank(tsModel);

                return request.CreateResponse<TeamBank>(HttpStatusCode.OK, ts);
            });
        }


        [HttpPost]
        [Route("deleteteambank")]
        public HttpResponseMessage DeleteTeamBank(HttpRequestMessage request, [FromBody]int teambankId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                TeamBank ts = _MPRCoreService.GetTeamBank(teambankId);

                if (ts != null)
                {
                    _MPRCoreService.DeleteTeamBank(teambankId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Team Structure found under the ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getteambank/{teambankid}")]
        public HttpResponseMessage GetRatios(HttpRequestMessage request, int Team_StructureId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                TeamStructure ts = _MPRCoreService.GetTeamStructure(Team_StructureId);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<TeamStructure>(HttpStatusCode.OK, ts);

                return response;
            });
        }


        [HttpGet]
        [Route("GetAllData")]
        public HttpResponseMessage GetData(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                TeamBank[] ts = _MPRCoreService.GetAllTeamBanks();

                return request.CreateResponse<TeamBank[]>(HttpStatusCode.OK, ts);
            });
        }

        [HttpGet]
        [Route("getteambankusingparams/{searchvalue}/{year}")]
        public HttpResponseMessage GetTeamBanksUsinSearchValueAndYear(HttpRequestMessage request, string searchvalue, int year)
        {
            return GetHttpResponse(request, () =>
            {
                TeamBank[] teambank = _MPRCoreService.GetTeamBanksUsingParams(searchvalue, year);

                // notice no need to create a seperate model object since TeamDefinition entity will do just fine
               var response = request.CreateResponse<TeamBank[]>(HttpStatusCode.OK, teambank);

                return response;
            });
        }

        [HttpGet]
        [Route("getteambankusingdefcode/{code}")]
        public HttpResponseMessage GetTeamStructureByDefCode(HttpRequestMessage request, string code)
        {
            return GetHttpResponse(request, () =>
            {
                TeamBank[] teambank= _MPRCoreService.GetTeamBankUsingDefinitionCode(code);

                // notice no need to create a seperate model object since TeamDefinition entity will do just fine
                var response = request.CreateResponse<TeamBank[]>(HttpStatusCode.OK, teambank);

                return response;
            });
        }

    }
}
