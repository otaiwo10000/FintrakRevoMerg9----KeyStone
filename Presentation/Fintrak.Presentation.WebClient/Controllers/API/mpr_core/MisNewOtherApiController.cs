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
    [RoutePrefix("api/misnewother")]
    [UsesDisposableService]
    public class MisNewOtherApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public MisNewOtherApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }

        [HttpGet]
        [Route("getmisnewother")]
        public HttpResponseMessage GetMisNewOther(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                MisNewOtherMtd obj = new MisNewOtherMtd();
                List<MisNewOtherModel> dm = obj.GetMisNewOther().ToList();

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
