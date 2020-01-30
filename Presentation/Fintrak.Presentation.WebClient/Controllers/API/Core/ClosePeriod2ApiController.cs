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

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/closeperiod2")]
    [UsesDisposableService]
    public class ClosePeriod2ApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ClosePeriod2ApiController(ICoreService coreService)
        {
            _CoreService = coreService;
        }

        ICoreService _CoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_CoreService);
        }

        [HttpPost]
        [Route("updatecloseperiod2")]
        public HttpResponseMessage UpdateClosePeriod2(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {

                int returnvalu = 0;
                HttpResponseMessage res = null;

                var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

                using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    var cmd = new System.Data.SqlClient.SqlCommand("Income_ClosePeriod_Process", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;

                    System.Data.SqlClient.SqlParameter returnValueParam= cmd.Parameters.Add("@ReturnVal", System.Data.SqlDbType.Int);
                    returnValueParam.Direction = System.Data.ParameterDirection.ReturnValue;

                    //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;
                   
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    returnvalu = (int)returnValueParam.Value;
                }

                //comm = "Operation Successful.";
                res = request.CreateResponse(HttpStatusCode.OK, returnvalu);

                return res;
            });
        }
    }
}
