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
    [RoutePrefix("api/employeetable")]
    [UsesDisposableService]
    public class EmployeeTableModelApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public EmployeeTableModelApiController(IMPRPLService mprPLService)
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
        [Route("employees")]
        public HttpResponseMessage Employees(HttpRequestMessage request, List<EmployeeTableModel> employeeList)
        {
            return GetHttpResponse(request, () =>
            {
                //MPRDownLoadBaseFintrakFinalCaptionOnlyMtd obj = new MPRDownLoadBaseFintrakFinalCaptionOnlyMtd();
                //List<EmployeeTableModel> ddb = obj.GetDDBFintrakManual().ToList();

                return request.CreateResponse(HttpStatusCode.OK);
            });
        }

       

    }
}
