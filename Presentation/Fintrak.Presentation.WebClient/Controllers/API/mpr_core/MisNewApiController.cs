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
    [RoutePrefix("api/misnew")]
    [UsesDisposableService]
    public class MisNewApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public MisNewApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }

        [HttpGet]
        [Route("getmisnew")]
        public HttpResponseMessage GetMisNew(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                MisNewMtd obj = new MisNewMtd();
                List<MisNewModel> dm = obj.GetMisNew().ToList();

                return request.CreateResponse(HttpStatusCode.OK, dm);

            });
        }

        //[HttpGet]
        //[Route("getteamaccessreadByParam/{search}")]
        //public HttpResponseMessage GetDimAudByParam(HttpRequestMessage request, string search)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        TeamAccessReadMtd obj = new TeamAccessReadMtd();
        //        List<TeamAccessReadModel> dm = obj.GetTeamAccessReadUsingParams(search).ToList();

        //        return request.CreateResponse(HttpStatusCode.OK, dm);

        //    });
        //}


    }
}
