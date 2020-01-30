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
using Fintrak.Client.MPR.Entities;
using Fintrak.Client.MPR.Contracts;
using Fintrak.Presentation.WebClient.Additionalmethods;
using Fintrak.Presentation.WebClient.Models;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/opexrawexpensesfixed")]
    [UsesDisposableService]
    public class OpexRawExpensesFixedApiController : ApiControllerBase
    {
        //[ImportingConstructor]
        //public OpexRawExpensesFixedApiController(IMPROPEXService mprOPEXService)
        //{
        //    _MPROPEXService = mprOPEXService;
        //}

        IMPROPEXService _MPROPEXService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPROPEXService);
        }

        [HttpPost]
        [Route("updateopexRawExpensesFixed")]
        public HttpResponseMessage UpdateOpexRawExpensesFixed(HttpRequestMessage request, string glcode, string diff, string miscode)
        {
            return GetHttpResponse(request, () =>
            {
                //var ratios = _MPRCoreService.UpdateRatios(ratiosModel);

                //return request.CreateResponse<Ratios>(HttpStatusCode.OK, ratios);

                var comm = "";
                HttpResponseMessage res = null;

                OpexMtd obj = new OpexMtd();

                obj.insertfromOpexStaffExpDiffINTORawExpensesFixed(glcode, diff, miscode);

                comm = "Operation Successful.";
                res = request.CreateResponse(HttpStatusCode.OK, comm);

                return res;
            });
        }

        [HttpPost]
        [Route("updateopexRawExpensesFixed_2")]
        public HttpResponseMessage UpdateOpexRawExpensesFixed_2(HttpRequestMessage request, [FromBody]OpexStaffExpDiffModel_2 opexstaffexpdeiff)
        {
            return GetHttpResponse(request, () =>
            {
                //var ratios = _MPRCoreService.UpdateRatios(ratiosModel);

                //return request.CreateResponse<Ratios>(HttpStatusCode.OK, ratios);

                var comm = "";
                HttpResponseMessage res = null;

                OpexMtd obj = new OpexMtd();

                obj.insertfromOpexStaffExpDiffINTORawExpensesFixed_2(opexstaffexpdeiff);

                comm = "Operation Successful.";
                res = request.CreateResponse(HttpStatusCode.OK, comm);

                return res;
            });
        }

        [HttpGet]
        [Route("getopexstaffexpensesdiff/{id}")]
        public HttpResponseMessage GetOpexRawExpense(HttpRequestMessage request, int id)
        {           
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage res = null;

                //OpexStaffExpDiffModel opobj = new OpexStaffExpDiffModel();
                OpexMtd obj = new OpexMtd();

                OpexStaffExpDiffModel opObj = obj.GetOpexStaffExpDiffMtd(id);

                res = request.CreateResponse(HttpStatusCode.OK, opObj);

                return res;
            });
        }

        //[HttpPost]
        //[Route("deleteopexstaffexpdiff")]
        //public HttpResponseMessage DeleteOpexRawExpense(HttpRequestMessage request, int id)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        //var ratios = _MPRCoreService.UpdateRatios(ratiosModel);

        //        //return request.CreateResponse<Ratios>(HttpStatusCode.OK, ratios);

        //        var comm = "";
        //        HttpResponseMessage res = null;

        //        OpexMtd obj = new OpexMtd();

        //        obj.DeleteOpexStaffExpDiffMtd(id);

        //        comm = "Operation Successful.";
        //        res = request.CreateResponse(HttpStatusCode.OK, comm);

        //        return res;
        //    });
        //}

        //[HttpGet]
        //[Route("getopexRawExpensesFixed/{opexRawExpenseId}")]
        //public HttpResponseMessage GetOpexRawExpense(HttpRequestMessage request, int opexRawExpenseId)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;

        //        OpexRawExpense opexRawExpense = _MPROPEXService.GetOpexRawExpense(opexRawExpenseId);

        //        // notice no need to create a seperate model object since OpexRawExpense entity will do just fine
        //        response = request.CreateResponse<OpexRawExpense>(HttpStatusCode.OK, opexRawExpense);

        //        return response;
        //    });
        //}

        [HttpGet]
        [Route("availableopexstaffexpdiff")]
        public HttpResponseMessage GetOpexStaffExpDiff(HttpRequestMessage request)
        {           
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage res = null;

                List<OpexStaffExpDiffModel> objList = new List<OpexStaffExpDiffModel>();
                OpexMtd obj = new OpexMtd();

                objList = obj.GetTopOpexStaffExpDiffMtd().ToList();

                res = request.CreateResponse(HttpStatusCode.OK, objList);

                return res;
            });
        }

        [HttpGet]
        [Route("availableopexrawexpensesfixed")]
        public HttpResponseMessage GetOpexRawExpensesFixed(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage res = null;

                List<RawExpensesFixedModel> objList = new List<RawExpensesFixedModel>();
                OpexMtd obj = new OpexMtd();

                objList = obj.GetAllRawExpensesFixedModelMtd().ToList();

                res = request.CreateResponse(HttpStatusCode.OK, objList);

                return res;
            });
        }

        [HttpGet]
        [Route("availableopexrawexpensesfixed_2/{svalue}")]
        public HttpResponseMessage GetOpexRawExpensesFixed_2(HttpRequestMessage request, string svalue)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage res = null;

                List<RawExpensesFixedModel> objList = new List<RawExpensesFixedModel>();
                OpexMtd obj = new OpexMtd();

                objList = obj.GetAllRawExpensesFixedModelMtd_2(svalue).ToList();

                res = request.CreateResponse(HttpStatusCode.OK, objList);

                return res;
            });
        }

        [HttpGet]
        [Route("getrawexpensesfixedbyid/{Id}")]
        public HttpResponseMessage GetRawExpensesFixedUsingId(HttpRequestMessage request, int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage res = null;

                OpexMtd obj = new OpexMtd();
                RawExpensesFixedModel opex = obj.GetRawExpensesFixedById(Id);

                res = request.CreateResponse(HttpStatusCode.OK, opex);

                return res;
            });
        }

        [HttpPost]
        [Route("updaterawexpensesfixed")]
        public HttpResponseMessage UpdateRawExpensesFixed(HttpRequestMessage request, [FromBody]RawExpensesFixedModel updateopexmodel)
        {
            return GetHttpResponse(request, () =>
            {
                OpexMtd obj = new OpexMtd();
                obj.UpdateRawExpensesFixed(updateopexmodel);

                return request.CreateResponse(HttpStatusCode.OK);
            });
        }

        [HttpGet]
        [Route("availableopexdetailshareBUD")]
        public HttpResponseMessage GetAllOPEXDetailShareBUDMtd(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage res = null;

                List<OPEX_DETAIL_SHARE_BUD> objList = new List<OPEX_DETAIL_SHARE_BUD>();
                OpexMtd obj = new OpexMtd();

                objList = obj.GetAllOPEXDetailShareBUDMtd().ToList();

                res = request.CreateResponse(HttpStatusCode.OK, objList);

                return res;
            });
        }

        [HttpGet]
        [Route("availablesharedcostCC")]
        public HttpResponseMessage GetAllsharedCostCCMtd(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage res = null;

                List<sharedCostCC> objList = new List<sharedCostCC>();
                OpexMtd obj = new OpexMtd();

                objList = obj.GetAllsharedCostCCMtd().ToList();

                res = request.CreateResponse(HttpStatusCode.OK, objList);

                return res;
            });
        }

        [HttpGet]
        [Route("availableopexdetailshareBUD_2/{svalue}")]
        public HttpResponseMessage GetAllOPEXDetailShareBUDMtd_2(HttpRequestMessage request, string svalue)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage res = null;

                List<OPEX_DETAIL_SHARE_BUD> objList = new List<OPEX_DETAIL_SHARE_BUD>();
                OpexMtd obj = new OpexMtd();

                objList = obj.GetAllOPEXDetailShareBUDMtd_2(svalue).ToList();

                res = request.CreateResponse(HttpStatusCode.OK, objList);

                return res;
            });
        }

    }
}
