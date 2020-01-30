using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Presentation.WebClient.Models;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.MPR.Contracts;
using Fintrak.Client.MPR.Entities;
using Fintrak.Presentation.WebClient.Additionalmethods;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/incomeadjustmentvolumesearch")]
    [UsesDisposableService]
    public class IncomeAdjustmentVolumeSearchApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomeAdjustmentVolumeSearchApiController(IMPRPLService mprPLService)
        {
            _MPRPLService = mprPLService;
        }

        IMPRPLService _MPRPLService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRPLService);
        }



        [HttpGet]
        [Route("incomeadjustmentvolumesearchusingparams/{year}/{period}/{search}")]
        public HttpResponseMessage GetIncomeAdjustmentVolUsingParams(HttpRequestMessage request, int year, int period, string search)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeAdjustmentVolumeSearchMtd obj = new IncomeAdjustmentVolumeSearchMtd();
                List<IncomeAdjustmentVolumeSearchModel> ddb = obj.GetIncomeAdjustmentVolumeSearchUsingYearPeriod(year, period, search).ToList();

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        [HttpGet]
        [Route("incomeadjustmentvolumesearchusingid/{Id}")]
        public HttpResponseMessage GetIncomeAdjustmentVolUsingParams(HttpRequestMessage request, int Id)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeAdjustmentVolumeSearchMtd obj = new IncomeAdjustmentVolumeSearchMtd();
                IncomeAdjustmentVolumeSearchModel ddb = obj.GetIncomeAdjustmentById(Id);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        [HttpPost]
        [Route("addincomeadjustmentvol")]
        //public HttpResponseMessage AddIncomeAdjustmentVol(HttpRequestMessage request, string MISCODE, string ACCTCODE, string ACCOUNTNUMBER, string CUSTNAME, string BALANCE, string AVERAGE, string INTEREST, string PRODUCTCODE, string CATEGORY, string CURRENCY)
        public HttpResponseMessage AddIncomeAdjustmentVol(HttpRequestMessage request, [FromBody]AddIncomeAdjustmentVolumeSearchModel addvolumemodel)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeAdjustmentVolumeSearchMtd obj = new IncomeAdjustmentVolumeSearchMtd();
                //obj.AddIncomeAdjustmentVolumeSearch(MISCODE, ACCTCODE, ACCOUNTNUMBER, CUSTNAME, BALANCE, AVERAGE, INTEREST, PRODUCTCODE, CATEGORY, CURRENCY);
                obj.AddIncomeAdjustmentVolumeSearch(addvolumemodel);

                return request.CreateResponse(HttpStatusCode.OK);
            });
        }

        [HttpPost]
        [Route("updateincomeadjustmentvol")]
        //public HttpResponseMessage UpdateIncomeAdjustmentVol(HttpRequestMessage request, int ID, string MISCODE, string ACCTCODE, int PERIOD, int YEAR, string ACCOUNTNUMBER, string PRODUCTCODE, string CATEGORY, string CURRENCY, string CUSTNAME, string CAPTION, string ACCOUNTNUMBER1)
        public HttpResponseMessage UpdateIncomeAdjustmentVol(HttpRequestMessage request, [FromBody]UpdateIncomeAdjustmentVolumeSearchModel updatevolumemodel)
        {
            return GetHttpResponse(request, () =>
            {
                //IncomeAdjustmentVolumeSearchMtd obj = new IncomeAdjustmentVolumeSearchMtd();
                //obj.UpdateIncomeAdjustmentVolumeSearch(updatevolumemodel);

                OtherInfo latestsetup = new OtherInfo();

                int currentyear = latestsetup.GetLatestIncomeSetUp().Year;
                int currentperiod = latestsetup.GetLatestIncomeSetUp().CurrentPeriod;

                if(updatevolumemodel.YEAR == currentyear && updatevolumemodel.PERIOD == currentperiod)
                {
                    IncomeAdjustmentVolumeSearchMtd obj = new IncomeAdjustmentVolumeSearchMtd();
                    obj.UpdateIncomeAdjustmentVolumeSearch(updatevolumemodel);
                }

                return request.CreateResponse(HttpStatusCode.OK);
            });
        }

        [HttpPost]
        [Route("deleteincomeadjustmentvol")]
        public HttpResponseMessage DeleteIncomeAdjustmentVol(HttpRequestMessage request, [FromBody]int Id, [FromBody]UpdateIncomeAdjustmentVolumeSearchModel updatevolumemodel)
        {
            return GetHttpResponse(request, () =>
            {
                //IncomeAdjustmentVolumeSearchMtd obj = new IncomeAdjustmentVolumeSearchMtd();
                //obj.DeleteIncomeAdjustmentVolumeSearch(Id);

                OtherInfo latestsetup = new OtherInfo();

                int currentyear = latestsetup.GetLatestIncomeSetUp().Year;
                int currentperiod = latestsetup.GetLatestIncomeSetUp().CurrentPeriod;

                if (updatevolumemodel.YEAR == currentyear && updatevolumemodel.PERIOD == currentperiod)
                {
                    IncomeAdjustmentVolumeSearchMtd obj = new IncomeAdjustmentVolumeSearchMtd();
                    obj.DeleteIncomeAdjustmentVolumeSearch(Id);
                }

                return request.CreateResponse(HttpStatusCode.OK);
            });
        }

    }
}
