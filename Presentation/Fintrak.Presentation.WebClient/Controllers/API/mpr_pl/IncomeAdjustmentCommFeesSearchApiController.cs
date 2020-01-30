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
using Fintrak.Presentation.WebClient.Additionalmethods;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/incomeadjustmentcommfeessearch")]
    [UsesDisposableService]
    public class IncomeAdjustmentCommFeesSearchApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomeAdjustmentCommFeesSearchApiController(IMPRPLService mprPLService)
        {
            _MPRPLService = mprPLService;
        }

        IMPRPLService _MPRPLService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRPLService);
        }

        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

        [HttpGet]
        [Route("getallincomecommfeeusingparams/{yr}/{pr}/{search}")]
        public HttpResponseMessage GetAllIncomeCommFeeUsingParams(HttpRequestMessage request, int yr, int pr, string search)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeAdjustmentCommFeesSearchMtd obj = new IncomeAdjustmentCommFeesSearchMtd();
                List<IncomeAdjustmentCommFeesSearchModel> cf = obj.GetCommFees(yr, pr, search).ToList();

                return request.CreateResponse(HttpStatusCode.OK, cf);
            });
        }

        [HttpGet]
        [Route("incomeadjustmentcommfeessearchusingid/{Id}")]
        public HttpResponseMessage GetIncomeAdjustmentCommFeesUsingParams(HttpRequestMessage request, int Id)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeAdjustmentCommFeesSearchMtd obj = new IncomeAdjustmentCommFeesSearchMtd();
                IncomeAdjustmentCommFeesSearchModel ddb = obj.GetCommFeeById(Id);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        [HttpPost]
        [Route("addincomeadjustmentcommfeessearch")]
        //public HttpResponseMessage AddIncomeAdjustmentVol(HttpRequestMessage request, string MISCODE, string ACCTCODE, string ACCOUNTNUMBER, string CUSTNAME, string BALANCE, string AVERAGE, string INTEREST, string PRODUCTCODE, string CATEGORY, string CURRENCY)
        public HttpResponseMessage AddIncomeAdjustmentCommFessSearch(HttpRequestMessage request, [FromBody]IncomeAdjustmentCommFeesSearchModel addcfmodel)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeAdjustmentCommFeesSearchMtd obj = new IncomeAdjustmentCommFeesSearchMtd();
                obj.AddIncomeAdjustmentCommFeesSearch(addcfmodel);

                return request.CreateResponse(HttpStatusCode.OK);
            });
        }

        [HttpPost]
        [Route("updateincomeadjustmentcommfeessearch")]
        //public HttpResponseMessage UpdateIncomeAdjustmentVol(HttpRequestMessage request, int ID, string MISCODE, string ACCTCODE, int PERIOD, int YEAR, string ACCOUNTNUMBER, string PRODUCTCODE, string CATEGORY, string CURRENCY, string CUSTNAME, string CAPTION, string ACCOUNTNUMBER1)
        public HttpResponseMessage UpdateIncomeAdjustmentCommFessSearch(HttpRequestMessage request, [FromBody]IncomeAdjustmentCommFeesSearchModel updatecfmodel)
        {
            return GetHttpResponse(request, () =>
            {
                // OtherInfo latestsetup = new OtherInfo();

                //int currentyear = latestsetup.GetLatestIncomeSetUp().Year;
                //int currentperiod = latestsetup.GetLatestIncomeSetUp().CurrentPeriod;

                //if (updatecfmodel.Year == currentyear && updatecfmodel.Period == currentperiod)
                //{
                //    IncomeAdjustmentCommFeesSearchMtd obj = new IncomeAdjustmentCommFeesSearchMtd();
                //    obj.UpdateIncomeAdjustmentCommFeesSearch(updatecfmodel);
                //}

                IncomeAdjustmentCommFeesSearchMtd obj = new IncomeAdjustmentCommFeesSearchMtd();
                if(updatecfmodel.Year == DateTime.Now.Year)
                obj.UpdateIncomeAdjustmentCommFeesSearch(updatecfmodel);

                return request.CreateResponse(HttpStatusCode.OK);
            });
        }

        [HttpPost]
        [Route("updateincomeadjustmentcommfeessearchACCESS")]
        //public HttpResponseMessage UpdateIncomeAdjustmentVol(HttpRequestMessage request, int ID, string MISCODE, string ACCTCODE, int PERIOD, int YEAR, string ACCOUNTNUMBER, string PRODUCTCODE, string CATEGORY, string CURRENCY, string CUSTNAME, string CAPTION, string ACCOUNTNUMBER1)
        public HttpResponseMessage UpdateIncomeAdjustmentCommFessSearchACCESS(HttpRequestMessage request, [FromBody]IncomeAdjustmentCommFeesSearchModel updatecfmodel)
        {
            return GetHttpResponse(request, () =>
            {               
                IncomeAdjustmentCommFeesSearchMtd obj = new IncomeAdjustmentCommFeesSearchMtd();
                if (updatecfmodel.Year == DateTime.Now.Year)
                    obj.UpdateIncomeAdjustmentCommFeesSearchACCESS(updatecfmodel);

                return request.CreateResponse(HttpStatusCode.OK);
            });
        }

        [HttpPost]
        [Route("deleteincomeadjustmentcommfeessearch")]
        public HttpResponseMessage DeleteIncomeAdjustmentCommFeesSearch(HttpRequestMessage request, [FromBody]int Id, [FromBody]IncomeAdjustmentCommFeesSearchModel updatecfmodel)
        {
            return GetHttpResponse(request, () =>
            {
                //IncomeAdjustmentVolumeSearchMtd obj = new IncomeAdjustmentVolumeSearchMtd();
                //obj.DeleteIncomeAdjustmentVolumeSearch(Id);

                OtherInfo latestsetup = new OtherInfo();

                int currentyear = latestsetup.GetLatestIncomeSetUp().Year;
                int currentperiod = latestsetup.GetLatestIncomeSetUp().CurrentPeriod;

                if (updatecfmodel.Year == currentyear && updatecfmodel.Period == currentperiod)
                {
                    IncomeAdjustmentCommFeesSearchMtd obj = new IncomeAdjustmentCommFeesSearchMtd();
                    if (updatecfmodel.Year == DateTime.Now.Year)
                        obj.DeleteIncomeAdjustmentCommFeesSearch(Id);
                }

                return request.CreateResponse(HttpStatusCode.OK);
            });
        }

    }
}
