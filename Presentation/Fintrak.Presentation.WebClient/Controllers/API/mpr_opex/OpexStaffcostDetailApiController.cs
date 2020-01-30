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
    [RoutePrefix("api/opexstaffcostdetail")]
    [UsesDisposableService]
    public class OpexOpexStaffcostDetailApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public OpexOpexStaffcostDetailApiController(IMPROPEXService mprOpexService)
        {
            _MPROPEXService = mprOpexService;
        }

        IMPROPEXService _MPROPEXService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPROPEXService);
        }

        [HttpPost]
        [Route("updateopexstaffcostdetail")]
        public HttpResponseMessage UpdateOpexStaffcostDetail(HttpRequestMessage request, [FromBody]OpexStaffcostDetail opexModel)
        {
            return GetHttpResponse(request, () =>
            {
                var opex = _MPROPEXService.UpdateOpexStaffcostDetail(opexModel);

                return request.CreateResponse<OpexStaffcostDetail>(HttpStatusCode.OK, opex);
            });
        }

        [HttpPost]
        [Route("deleteopexstaffcostdetail")]
        public HttpResponseMessage DeleteOpexStaffcostDetail(HttpRequestMessage request, [FromBody]int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                OpexStaffcostDetail opex = _MPROPEXService.GetOpexStaffcostDetail(ID);

                if (opex != null)
                {
                    //_MPROPEXService.DeleteActivityBase(ID);
                    _MPROPEXService.DeleteOpexStaffcostDetail(ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No OpexStaffcostDetail found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getopexstaffcostdetail/{ID}")]
        public HttpResponseMessage GetOpexStaffcostDetail(HttpRequestMessage request, int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                OpexStaffcostDetail opex = _MPROPEXService.GetOpexStaffcostDetail(ID);

                // notice no need to create a seperate model object since ActivityBase entity will do just fine
                response = request.CreateResponse<OpexStaffcostDetail>(HttpStatusCode.OK, opex);

                return response;
            });
        }

        [HttpGet]
        [Route("availableopexstaffcostdetail")]
        public HttpResponseMessage GetAvailableOpexStaffcostDetail(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                OpexStaffcostDetail[] opex = _MPROPEXService.GetAllOpexStaffcostDetail();

                return request.CreateResponse<OpexStaffcostDetail[]>(HttpStatusCode.OK, opex);
            });
        }
    }
}
