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
    [RoutePrefix("api/enums")]
    [UsesDisposableService]
    public class EnumsApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public EnumsApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);
        }
      

        [HttpGet]
        [Route("currencytype")]
        public List<System.Web.Mvc.SelectListItem> currencyType(HttpRequestMessage request)
        {           
                var currencytype = new List<System.Web.Mvc.SelectListItem>();

                 currencytype = (Enum.GetValues(typeof(Fintrak.Shared.MPR.Framework.CurrencyType)).Cast<int>().Select(e => new System.Web.Mvc.SelectListItem() { Text = Enum.GetName(typeof(Fintrak.Shared.MPR.Framework.CurrencyType), e), Value = e.ToString() })).ToList();

                // response = request.CreateResponse<System.Web.Mvc.SelectListItem>(HttpStatusCode.OK, currencytype);

                return currencytype;
                //return Json(currencyTypeDropDown, System.Web.Mvc.JsonRequestBehavior.AllowGet);         
        }

        [HttpGet]
        [Route("balancesheetcategory")]
        public List<System.Web.Mvc.SelectListItem> BalancesheetCategory(HttpRequestMessage request)
        {
            //var bscategory = new List<System.Web.Mvc.SelectListItem>();

            var bscategory = (Enum.GetValues(typeof(Fintrak.Shared.MPR.Framework.BalanceSheetCategory)).Cast<int>().Select(e => new System.Web.Mvc.SelectListItem() { Text = Enum.GetName(typeof(Fintrak.Shared.MPR.Framework.BalanceSheetCategory), e), Value = e.ToString() })).ToList();

            return bscategory;
            //return Json(currencyTypeDropDown, System.Web.Mvc.JsonRequestBehavior.AllowGet);         
        }




        //public ActionResult gender()
        //{
        //    var v = (Enum.GetValues(typeof(Gender)).Cast<int>().Select(e => new SelectListItem() { Text = Enum.GetName(typeof(Gender), e), Value = e.ToString() })).ToList();

        //    return Json(v, JsonRequestBehavior.AllowGet);
        //}

    }
}
