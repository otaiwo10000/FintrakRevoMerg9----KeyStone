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
using Fintrak.Presentation.WebClient.Additionalmethods;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/TeamStructureALL")]
    [UsesDisposableService]
    public class TeamStructureALLApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public TeamStructureALLApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);
        }


        [HttpPost]
        [Route("updateTeamStructureALL")]
        public HttpResponseMessage UpdateTeamStructureALL(HttpRequestMessage request, [FromBody]TeamStructureALL tsModel)
        {
            return GetHttpResponse(request, () =>
            {
                var ts = _MPRCoreService.UpdateTeamStructureALL(tsModel);

                return request.CreateResponse<TeamStructureALL>(HttpStatusCode.OK, ts);
            });
        }


        [HttpPost]
        [Route("deleteTeamStructureALL")]
        public HttpResponseMessage DeleteTeamStructureALL(HttpRequestMessage request, [FromBody]int Team_StructureId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                TeamStructureALL ts = _MPRCoreService.GetTeamStructureALL(Team_StructureId);

                if (ts != null)
                {
                    _MPRCoreService.DeleteTeamStructureALL(Team_StructureId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Team Structure found under the ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getTeamStructureALL/{Team_StructureId}")]
        public HttpResponseMessage GetRatios(HttpRequestMessage request, int Team_StructureId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                TeamStructureALL ts = _MPRCoreService.GetTeamStructureALL(Team_StructureId);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<TeamStructureALL>(HttpStatusCode.OK, ts);

                return response;
            });
        }


        [HttpGet]
        [Route("GetAllData")]
        public HttpResponseMessage GetData(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                TeamStructureALL[] ts = _MPRCoreService.GetAllTeamStructureALL();

                return request.CreateResponse<TeamStructureALL[]>(HttpStatusCode.OK, ts);
            });
        }

        [HttpGet]
        [Route("getTeamStructureALLusingparams/{SearchValue}/{year}")]
        public HttpResponseMessage GetTeamStructureALLByParams(HttpRequestMessage request, string SearchValue, int year)
        {
            return GetHttpResponse(request, () =>
            {
                TeamStructureALL[] TeamStructureALL = _MPRCoreService.GetTeamStructureALLUsingParams(SearchValue, year);

                // notice no need to create a seperate model object since TeamDefinition entity will do just fine
                var response = request.CreateResponse<TeamStructureALL[]>(HttpStatusCode.OK, TeamStructureALL);

                return response;
            });
        }

        [HttpGet]
        [Route("TeamStructureALLusingparameters/{selectedDefinitionCode}/{SearchValue}/{year}")]
        public HttpResponseMessage TeamStructureALLByParameters(HttpRequestMessage request, string selectedDefinitionCode, string SearchValue, int year)
        {
            return GetHttpResponse(request, () =>
            {
                TeamStructureALL[] TeamStructureALL = _MPRCoreService.TeamStructureALLByParameters(selectedDefinitionCode, SearchValue, year);

                // notice no need to create a seperate model object since TeamDefinition entity will do just fine
                var response = request.CreateResponse<TeamStructureALL[]>(HttpStatusCode.OK, TeamStructureALL);

                return response;
            });
        }

        [HttpGet]
        [Route("getTeamStructureALLusingparamsANDsetup/{code}/{SearchValue}")]
        public HttpResponseMessage GetTDByParamsAndSetUp(HttpRequestMessage request, string code, string SearchValue)
        {
            return GetHttpResponse(request, () =>
            {
                TeamStructureALL[] TeamStructureALL = _MPRCoreService.GetTeamStructureALLByParamsAndeSetUp(code, SearchValue);

                // notice no need to create a seperate model object since TeamDefinition entity will do just fine
                var response = request.CreateResponse<TeamStructureALL[]>(HttpStatusCode.OK, TeamStructureALL);

                return response;
            });
        }

        [HttpGet]
        [Route("getTeamStructureALLusingsetup")]
        public HttpResponseMessage GetTeamDefinitionUsingSetup(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                TeamStructureALL[] TeamStructureALL = _MPRCoreService.GetTeamStructureALLUsingSetUp();

                // notice no need to create a seperate model object since TeamDefinition entity will do just fine
                var response = request.CreateResponse<TeamStructureALL[]>(HttpStatusCode.OK, TeamStructureALL);

                return response;
            });
        }

        [HttpGet]
        [Route("getTeamStructureALLusingdefcode/{code}")]
        public HttpResponseMessage GetTeamStructureALLByDefCode(HttpRequestMessage request, string code)
        {
            return GetHttpResponse(request, () =>
            {
                TeamStructureALL[] TeamStructureALL = _MPRCoreService.GetTeamStructureALLUsingDefinitionCode(code);

                // notice no need to create a seperate model object since TeamDefinition entity will do just fine
                var response = request.CreateResponse<TeamStructureALL[]>(HttpStatusCode.OK, TeamStructureALL);

                return response;
            });
        }

        [HttpGet]
        [Route("getTeamStructureALLusingdefcodemonthly/{code}")]
        public HttpResponseMessage GetTeamStructureALLByDefCodeMonthly(HttpRequestMessage request, string code)
        {
            return GetHttpResponse(request, () =>
            {
                TeamStructureALL[] TeamStructureALL = _MPRCoreService.GetTeamStructureALLUsingDefinitionCodeMonthly(code);

                // notice no need to create a seperate model object since TeamDefinition entity will do just fine
                var response = request.CreateResponse<TeamStructureALL[]>(HttpStatusCode.OK, TeamStructureALL);

                return response;
            });
        }

        [HttpGet]
        [Route("getTeamStructureALLusingparamsANDsetupmonthly/{code}/{SearchValue}")]
        public HttpResponseMessage GetTDByParamsAndSetUpMonthly(HttpRequestMessage request, string code, string SearchValue)
        {
            return GetHttpResponse(request, () =>
            {
                TeamStructureALL[] TeamStructureALL = _MPRCoreService.GetTeamStructureALLByParamsAndeSetUpMonthly(code, SearchValue);

                // notice no need to create a seperate model object since TeamDefinition entity will do just fine
                var response = request.CreateResponse<TeamStructureALL[]>(HttpStatusCode.OK, TeamStructureALL);

                return response;
            });
        }

        [HttpGet]
        [Route("getTeamStructureALLusingsetupmonthly")]
        public HttpResponseMessage GetTeamDefinitionUsingSetupMonthly(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                TeamStructureALL[] TeamStructureALL = _MPRCoreService.GetTeamStructureALLUsingSetUpMonthly();

                // notice no need to create a seperate model object since TeamDefinition entity will do just fine
                var response = request.CreateResponse<TeamStructureALL[]>(HttpStatusCode.OK, TeamStructureALL);

                return response;
            });
        }
        
        [HttpGet]
        [Route("getTeamStructureALLtop1/{branch}/{defcode}/{year}")]
        public HttpResponseMessage TeamStructureALLTop1(HttpRequestMessage request, string branch, string defcode, int year)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                TeamStructureALL ts = _MPRCoreService.GetTeamStructureALLTop1(branch, defcode, year);

                response = request.CreateResponse<TeamStructureALL>(HttpStatusCode.OK, ts);

                return response;
            });
        }


    }
}
