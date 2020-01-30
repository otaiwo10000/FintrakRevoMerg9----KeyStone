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
    [RoutePrefix("api/teamacessread")]
    [UsesDisposableService]
    public class TeamAccessReadApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public TeamAccessReadApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }

        [HttpGet]
        [Route("getteamaccessread")]
        public HttpResponseMessage GetTeamAccessRead(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                TeamAccessReadMtd obj = new TeamAccessReadMtd();
                List<TeamAccessReadModel> dm = obj.GetTeamAccessRead().ToList();

                return request.CreateResponse(HttpStatusCode.OK, dm);

            });
        }

        [HttpGet]
        [Route("getteamaccessreadByParam/{search}")]
        public HttpResponseMessage GetTeamAccessReadByParam(HttpRequestMessage request, string search)
        {
            return GetHttpResponse(request, () =>
            {
                TeamAccessReadMtd obj = new TeamAccessReadMtd();
                List<TeamAccessReadModel> dm = obj.GetTeamAccessReadUsingParams(search).ToList();

                return request.CreateResponse(HttpStatusCode.OK, dm);

            });
        }


    }
}
