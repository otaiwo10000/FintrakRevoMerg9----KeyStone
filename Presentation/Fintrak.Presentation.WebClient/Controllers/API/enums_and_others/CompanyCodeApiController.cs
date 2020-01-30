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
    [RoutePrefix("api/codes")]
    [UsesDisposableService]
    public class CompanyCodeApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public CompanyCodeApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);
        }
      

        //[HttpGet]
        //[Route("companycode")]
        //public HttpResponseMessage CustomerCode(HttpRequestMessage request)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        string companycode = System.Configuration.ConfigurationManager.AppSettings["CompanyCode"].ToString();

        //        return request.CreateResponse<string>(companycode);
        //    });
        //}

        //[HttpGet]
        //[Route("companycode")]
        //public string CustomerCode()
        //{
        //    string companycode = System.Configuration.ConfigurationManager.AppSettings["CompanyCode"].ToString();

        //    //return request.CreateResponse<string>(companycode);
        //    return companycode;
        //}

        //public ActionResult gender()
        //{
        //    var v = (Enum.GetValues(typeof(Gender)).Cast<int>().Select(e => new SelectListItem() { Text = Enum.GetName(typeof(Gender), e), Value = e.ToString() })).ToList();

        //    return Json(v, JsonRequestBehavior.AllowGet);
        //}

    }
}
