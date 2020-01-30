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
    [RoutePrefix("api/yearperiod")]
    [UsesDisposableService]
    public class YearPeriodApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public YearPeriodApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }

        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;


        [HttpGet]
        [Route("years")]
        public HttpResponseMessage Years(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage res = null;

                var newList = new List<YearModel>();
                //List<int> newList = new List<int>();

                using (var con = new SqlConnection(connectionString))
                {
                    var cmd = new SqlCommand("spp_years", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;

                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var yearObj = new YearModel();

                        //int year = reader["year"] != DBNull.Value ? Convert.ToInt32(reader["year"]) : 0;

                        yearObj.value = reader["year"] != DBNull.Value ? Convert.ToInt32(reader["year"]) : 0;
                        yearObj.name = reader["year"] != DBNull.Value ? Convert.ToInt32(reader["year"]) : 0;

                        //yearObj.value = reader["year"] != DBNull.Value ? Convert.ToString(reader["year"]) : null;
                        //yearObj.name = reader["year"] != DBNull.Value ? Convert.ToString(reader["year"]) : null;

                        newList.Add(yearObj);
                    }
                    con.Close();
                }  // using               


                //res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, newList);
                res = request.CreateResponse(HttpStatusCode.OK, newList);

                return res;
            });
        }

    }
}
