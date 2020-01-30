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
using Fintrak.Presentation.WebClient.Additionalmethods;
using System.Data.SqlClient;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/teamstructure")]
    [UsesDisposableService]
    public class TeamStructureApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public TeamStructureApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);
        }

        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;


        [HttpPost]
        [Route("updateteamstructure")]
        public HttpResponseMessage UpdateTeamStructure(HttpRequestMessage request, [FromBody]TeamStructure tsModel)
        {
            return GetHttpResponse(request, () =>
            {
                var ts = _MPRCoreService.UpdateTeamStructure(tsModel);

                return request.CreateResponse<TeamStructure>(HttpStatusCode.OK, ts);
            });
        }


        [HttpPost]
        [Route("deleteteamstructure")]
        public HttpResponseMessage DeleteTeamStructure(HttpRequestMessage request, [FromBody]int Team_StructureId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                TeamStructure ts = _MPRCoreService.GetTeamStructure(Team_StructureId);

                if (ts != null)
                {
                    _MPRCoreService.DeleteTeamStructure(Team_StructureId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Team Structure found under the ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getteamstructure/{Team_StructureId}")]
        public HttpResponseMessage GetRatios(HttpRequestMessage request, int Team_StructureId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                TeamStructure ts = _MPRCoreService.GetTeamStructure(Team_StructureId);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<TeamStructure>(HttpStatusCode.OK, ts);

                return response;
            });
        }


        [HttpGet]
        [Route("GetAllData")]
        public HttpResponseMessage GetData(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                TeamStructure[] ts = _MPRCoreService.GetAllTeamStructure();

                return request.CreateResponse<TeamStructure[]>(HttpStatusCode.OK, ts);
            });
        }

        [HttpGet]
        [Route("getteamstructureusingparams/{SearchValue}/{year}")]
        public HttpResponseMessage GetTeamStructureByParams(HttpRequestMessage request, string SearchValue, string year)
        {
            return GetHttpResponse(request, () =>
            {
                TeamStructure[] teamstructure = _MPRCoreService.GetTeamStructureUsingParams(SearchValue, year);

                // notice no need to create a seperate model object since TeamDefinition entity will do just fine
               var response = request.CreateResponse<TeamStructure[]>(HttpStatusCode.OK, teamstructure);

                return response;
            });
        }

        [HttpGet]
        [Route("teamstructureusingparameters/{selectedDefinitionCode}/{SearchValue}/{year}")]
        public HttpResponseMessage TeamStructureByParameters(HttpRequestMessage request, string selectedDefinitionCode, string SearchValue, string year)
        {
            return GetHttpResponse(request, () =>
            {
                TeamStructure[] teamstructure = _MPRCoreService.TeamstructureByParameters(selectedDefinitionCode, SearchValue, year);

                // notice no need to create a seperate model object since TeamDefinition entity will do just fine
                var response = request.CreateResponse<TeamStructure[]>(HttpStatusCode.OK, teamstructure);

                return response;
            });
        }

        [HttpGet]
        [Route("getteamstructureusingparamsANDsetup/{code}/{SearchValue}")]
        public HttpResponseMessage GetTDByParamsAndSetUp(HttpRequestMessage request, string code, string SearchValue)
        {
            return GetHttpResponse(request, () =>
            {
                TeamStructure[] teamstructure = _MPRCoreService.GetTeamstructureByParamsAndeSetUp(code, SearchValue);

                // notice no need to create a seperate model object since TeamDefinition entity will do just fine
                var response = request.CreateResponse<TeamStructure[]>(HttpStatusCode.OK, teamstructure);

                return response;
            });
        }

        [HttpGet]
        [Route("getteamstructureusingsetup")]
        public HttpResponseMessage GetTeamDefinitionUsingSetup(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                TeamStructure[] teamstructure = _MPRCoreService.GetTeamStructureUsingSetUp();

                // notice no need to create a seperate model object since TeamDefinition entity will do just fine
                var response = request.CreateResponse<TeamStructure[]>(HttpStatusCode.OK, teamstructure);

                return response;
            });
        }

        [HttpGet]
        [Route("getteamstructureusingdefcode/{code}")]
        public HttpResponseMessage GetTeamStructureByDefCode(HttpRequestMessage request, string code)
        {
            return GetHttpResponse(request, () =>
            {
                TeamStructure[] teamstructure = _MPRCoreService.GetTeamStructureUsingDefinitionCode(code);

                // notice no need to create a seperate model object since TeamDefinition entity will do just fine
                var response = request.CreateResponse<TeamStructure[]>(HttpStatusCode.OK, teamstructure);

                return response;
            });
        }

        [HttpGet]
        [Route("getteamstructureusingdefcodemonthly/{code}")]
        public HttpResponseMessage GetTeamStructureByDefCodeMonthly(HttpRequestMessage request, string code)
        {
            return GetHttpResponse(request, () =>
            {
                TeamStructure[] teamstructure = _MPRCoreService.GetTeamStructureUsingDefinitionCodeMonthly(code);

                // notice no need to create a seperate model object since TeamDefinition entity will do just fine
                var response = request.CreateResponse<TeamStructure[]>(HttpStatusCode.OK, teamstructure);

                return response;
            });
        }

        [HttpGet]
        [Route("getteamstructureusingparamsANDsetupmonthly/{code}/{SearchValue}")]
        public HttpResponseMessage GetTDByParamsAndSetUpMonthly(HttpRequestMessage request, string code, string SearchValue)
        {
            return GetHttpResponse(request, () =>
            {
                TeamStructure[] teamstructure = _MPRCoreService.GetTeamstructureByParamsAndeSetUpMonthly(code, SearchValue);

                // notice no need to create a seperate model object since TeamDefinition entity will do just fine
                var response = request.CreateResponse<TeamStructure[]>(HttpStatusCode.OK, teamstructure);

                return response;
            });
        }

        [HttpGet]
        [Route("getteamstructureusingsetupmonthly")]
        public HttpResponseMessage GetTeamDefinitionUsingSetupMonthly(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                TeamStructure[] teamstructure = _MPRCoreService.GetTeamStructureUsingSetUpMonthly();

                // notice no need to create a seperate model object since TeamDefinition entity will do just fine
                var response = request.CreateResponse<TeamStructure[]>(HttpStatusCode.OK, teamstructure);

                return response;
            });
        }

        //[HttpGet]
        //[Route("getteamstructureusingdefcode/{code}")]
        //public HttpResponseMessage GetTeamStructureByDefCode(HttpRequestMessage request, string code)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        var res = "something went wrong";

        //        if (System.Configuration.ConfigurationManager.AppSettings["CompanyCodeTeam"].ToString() == "STB")
        //        {
        //            TeamStructure[] teamstructure = _MPRCoreService.GetTeamStructureUsingDefinitionCode(code);

        //            // notice no need to create a seperate model object since TeamDefinition entity will do just fine
        //             var response = request.CreateResponse<TeamStructure[]>(HttpStatusCode.OK, teamstructure);
        //            return response;
        //        }
        //        else if (System.Configuration.ConfigurationManager.AppSettings["CompanyCodeTeam"].ToString() == "KBL")
        //        {
        //            List<TeamStructureModel> tsList = new List<TeamStructureModel>();
        //            TeamBankGroupMtd tsListObj = new TeamBankGroupMtd();

        //           List<teamBankGroupNameCode> teamstructure = tsListObj.GetTeamstructureByDefinitionCode(code).ToList();

        //           var response = request.CreateResponse(HttpStatusCode.OK, teamstructure);
        //            return response;
        //        }

        //        return request.CreateResponse(HttpStatusCode.OK, res);
        //    });
        //}

        [HttpGet]
        [Route("getteamstructureusingdefcodeKBL/{code}")]
        public HttpResponseMessage GetTeamStructureByDefCodeKBL(HttpRequestMessage request, string code)
        {
            return GetHttpResponse(request, () =>
            {               
                List<TeamStructureModel> tsList = new List<TeamStructureModel>();
                 TeamBankGroupMtd tsListObj = new TeamBankGroupMtd();

               List<teamBankGroupDropDownModel> teamstructure = tsListObj.GetTeamstructureByDefinitionCode(code).ToList();

                var response = request.CreateResponse(HttpStatusCode.OK, teamstructure);

                return response;
            });
        }

        [HttpGet]
        [Route("getteamstructureusingacctofficerKBL/{code}/{search}")]
        public HttpResponseMessage GetTeamStructureByAcctKBL(HttpRequestMessage request, string code, string search)
        {
            return GetHttpResponse(request, () =>
            {
                List<AccountOfficer> tsList = new List<AccountOfficer>();
                TeamBankGroupMtd tsListObj = new TeamBankGroupMtd();

                List<AccountOfficer> accountofficer = tsListObj.GetTeamstructureByAcct(code, search).ToList();

                var response = request.CreateResponse(HttpStatusCode.OK, accountofficer);

                return response;
            });
        }

        //[HttpGet]
        //[Route("getteamstructuretop1/{branch}/{year}")]
        //public HttpResponseMessage TeamStructureTop1(HttpRequestMessage request, string branch, string year)
        //{           
        //    return GetHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;

        //        TeamStructure ts = _MPRCoreService.GetTeamStructureTop1(branch, year);

        //        response = request.CreateResponse<TeamStructure>(HttpStatusCode.OK, ts);

        //        return response;
        //    });
        //}

        [HttpGet]
        [Route("getteamstructuretop1/{branch}/{defcode}/{year}")]
        public HttpResponseMessage TeamStructureTop1(HttpRequestMessage request, string branch, string defcode, string year)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                TeamStructure ts = _MPRCoreService.GetTeamStructureTop1(branch, defcode, year);

                response = request.CreateResponse<TeamStructure>(HttpStatusCode.OK, ts);

                return response;
            });
        }

        [HttpGet]
        [Route("getdetailstop1/{teamcode}/{yr}")]
        public HttpResponseMessage GetDetailsTop1(HttpRequestMessage request, string teamcode, int yr)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage res = null;

                TeamStructureModel tb = new TeamStructureModel();

                using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    var cmd = new System.Data.SqlClient.SqlCommand("getdetailstop1", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;

                    //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "TeamCode",
                        Value = teamcode,
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "Year",
                        Value = yr,
                    });

                    con.Open();
                    //cmd.ExecuteNonQuery();
                    //cmd2.ExecuteNonQuery();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        tb.TeamName = reader["TeamName"] != DBNull.Value ? reader["TeamName"].ToString() : "default";
                        tb.Branch_Code = reader["Branch_Code"] != DBNull.Value ? reader["Branch_Code"].ToString() : "default";
                        tb.BranchName = reader["BranchName"] != DBNull.Value ? reader["BranchName"].ToString() : "default";
                        tb.Region_Code = reader["Region_Code"] != DBNull.Value ? reader["Region_Code"].ToString() : "default";
                        tb.RegionName = reader["RegionName"] != DBNull.Value ? reader["RegionName"].ToString() : "default";
                        tb.Zone_Code = reader["Zone_Code"] != DBNull.Value ? reader["Zone_Code"].ToString() : "default";
                        tb.ZoneName = reader["ZoneName"] != DBNull.Value ? reader["ZoneName"].ToString() : "default";
                        tb.DIRECTORATECODE = reader["DIRECTORATECODE"] != DBNull.Value ? reader["DIRECTORATECODE"].ToString() : "default";
                        tb.DIRECTORATENAME = reader["DIRECTORATENAME"] != DBNull.Value ? reader["DIRECTORATENAME"].ToString() : "default";
                    }
                    con.Close();
                }

                //comm = "Operation Successful.";
                res = request.CreateResponse(HttpStatusCode.OK, tb);

                return res;
            });
        }

        [HttpPost]
        [Route("updatedetailstop1")]
        public HttpResponseMessage UpdateDetailsTop1(HttpRequestMessage request, [FromBody]TeamStructureModel teamstructureModel)
        {
            return GetHttpResponse(request, () =>
            {
                var comm = "";
                HttpResponseMessage res = null;

                //var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

                using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    var cmd = new System.Data.SqlClient.SqlCommand("updatedetailstop1", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;

                    //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "Accountofficer_Code",
                        Value = teamstructureModel.Accountofficer_Code,
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "AccountofficerName",
                        Value = teamstructureModel.AccountofficerName,
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "Team_Code",
                        Value = teamstructureModel.Team_Code,
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "staff_id",
                        Value = teamstructureModel.staff_id,
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "Year",
                        Value = teamstructureModel.Year,
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "TeamName",
                        Value = teamstructureModel.TeamName,
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "Branch_Code",
                        Value = teamstructureModel.Branch_Code,
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "BranchName",
                        Value = teamstructureModel.BranchName,
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "Region_Code",
                        Value = teamstructureModel.Region_Code,
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "RegionName",
                        Value = teamstructureModel.RegionName,
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "DIRECTORATECODE",
                        Value = teamstructureModel.DIRECTORATECODE,
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "DIRECTORATENAME",
                        Value = teamstructureModel.DIRECTORATENAME,
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "Zone_Code",
                        Value = teamstructureModel.Zone_Code,
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "ZoneName",
                        Value = teamstructureModel.ZoneName,
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "Period",
                        Value = teamstructureModel.Period,
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "Team_StructureId",
                        Value = teamstructureModel.Team_StructureId,
                    });

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                comm = "Operation Successful.";
                res = request.CreateResponse(HttpStatusCode.OK, comm);

                return res;
            });
        }


    }
}
