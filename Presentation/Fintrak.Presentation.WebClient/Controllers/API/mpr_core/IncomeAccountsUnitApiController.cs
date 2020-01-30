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
using System.Data.SqlClient;
using Fintrak.Presentation.WebClient.Models;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/incomeaccountsunit")]
    [UsesDisposableService]
    public class IncomeAccountsUnitApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomeAccountsUnitApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }

        [HttpPost]
        [Route("updateincomeaccountsunit")]
        public HttpResponseMessage UpdateIncomeAccountsUnit(HttpRequestMessage request, [FromBody]IncomeAccountsUnit incomeAccountsUnitModel)
        {
            return GetHttpResponse(request, () =>
            {
                var incomeAccountsUnit = _MPRCoreService.UpdateIncomeAccountsUnit(incomeAccountsUnitModel);

                return request.CreateResponse<IncomeAccountsUnit>(HttpStatusCode.OK, incomeAccountsUnit);
            });
        }


        [HttpPost]
        [Route("deleteincomeaccountsunit")]
        public HttpResponseMessage DeleteIncomeAccountsUnit(HttpRequestMessage request, [FromBody]int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IncomeAccountsUnit incomeAccountsUnit = _MPRCoreService.GetIncomeAccountsUnit(ID);

                if (incomeAccountsUnit != null)
                {
                    _MPRCoreService.DeleteIncomeAccountsUnit(ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No IncomeAccountsUnit found under that ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getincomeaccountsunit/{ID}")]
        public HttpResponseMessage GetIncomeIncomeAccountsUnit(HttpRequestMessage request, int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IncomeAccountsUnit incomeAccountsUnit = _MPRCoreService.GetIncomeAccountsUnit(ID);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<IncomeAccountsUnit>(HttpStatusCode.OK, incomeAccountsUnit);

                return response;
            });
        }


        [HttpGet]
        [Route("availableincomeaccountsunit")]
        public HttpResponseMessage GetAvailableIncomeAccountsUnit(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage res = null;

                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

                List<IncomeAccountsUnitModel> IncomeAccountsUnitList = new List<IncomeAccountsUnitModel>();

                using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    var cmd = new System.Data.SqlClient.SqlCommand("proc_Income_Accounts_Units_Get", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;

                    //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var tb = new IncomeAccountsUnitModel();

                        tb.ID = reader["ID"] != DBNull.Value ? int.Parse(reader["ID"].ToString()) : 0;
                        tb.AccountNumber = reader["AccountNumber"] != DBNull.Value ? reader["AccountNumber"].ToString() : "default";
                        tb.CustomerName= reader["CustomerName"] != DBNull.Value ? reader["CustomerName"].ToString() : "default";
                        tb.MIS_Code = reader["MIS_Code"] != DBNull.Value ? reader["MIS_Code"].ToString() : "default";
         
                        IncomeAccountsUnitList.Add(tb);
                    }
                    con.Close();
                }

                //comm = "Operation Successful.";
                res = request.CreateResponse(HttpStatusCode.OK, IncomeAccountsUnitList);

                return res;
            });
        }
    }
}
