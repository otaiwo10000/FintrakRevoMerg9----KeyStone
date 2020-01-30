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
    [RoutePrefix("api/teamstructurekbl")]
    [UsesDisposableService]
    public class TeamStructure_KBL_ApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public TeamStructure_KBL_ApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;


        //[HttpPost]
        //[Route("updateteamstructure")]
        //public HttpResponseMessage UpdateTeamStructure(HttpRequestMessage request, [FromBody]TeamStructureModel teamstructureModel)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        //var ratios = _MPRCoreService.UpdateRatios(ratiosModel);

        //        //return request.CreateResponse<Ratios>(HttpStatusCode.OK, ratios);

        //        var comm = "";
        //        HttpResponseMessage res = null;

        //        //var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

        //        using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
        //        {
        //            var cmd = new System.Data.SqlClient.SqlCommand("spp_updateteamstructurekbl", con);
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            cmd.CommandTimeout = 0;

        //            //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

        //            cmd.Parameters.Add(new SqlParameter
        //            {
        //                ParameterName = "Team_Code",
        //                Value = teamstructureModel.Team_Code,
        //            });

        //            cmd.Parameters.Add(new SqlParameter
        //            {
        //                ParameterName = "TeamName",
        //                Value = teamstructureModel.TeamName,
        //            });

        //            cmd.Parameters.Add(new SqlParameter
        //            {
        //                ParameterName = "Branch_Code",
        //                Value = teamstructureModel.Branch_Code,
        //            });

        //            cmd.Parameters.Add(new SqlParameter
        //            {
        //                ParameterName = "BranchName",
        //                Value = teamstructureModel.BranchName,
        //            });

        //            cmd.Parameters.Add(new SqlParameter
        //            {
        //                ParameterName = "Region_Code",
        //                Value = teamstructureModel.Region_Code,
        //            });

        //            cmd.Parameters.Add(new SqlParameter
        //            {
        //                ParameterName = "RegionName",
        //                Value = teamstructureModel.RegionName,
        //            });

        //            cmd.Parameters.Add(new SqlParameter
        //            {
        //                ParameterName = "Division_Code",
        //                Value = teamstructureModel.Division_Code,
        //            });

        //            cmd.Parameters.Add(new SqlParameter
        //            {
        //                ParameterName = "DivisionName",
        //                Value = teamstructureModel.DivisionName,
        //            });

        //            cmd.Parameters.Add(new SqlParameter
        //            {
        //                ParameterName = "AccountOfficer_Code",
        //                Value = teamstructureModel.AccountOfficer_Code,
        //            });

        //            cmd.Parameters.Add(new SqlParameter
        //            {
        //                ParameterName = "AccountOfficer_Name",
        //                Value = teamstructureModel.AccountOfficer_Name,
        //            });

        //            cmd.Parameters.Add(new SqlParameter
        //            {
        //                ParameterName = "StaffID",
        //                Value = teamstructureModel.StaffID,
        //            });

        //            cmd.Parameters.Add(new SqlParameter
        //            {
        //                ParameterName = "Year",
        //                Value = teamstructureModel.Year,
        //            });

        //            cmd.Parameters.Add(new SqlParameter
        //            {
        //                ParameterName = "teambankid",
        //                Value = teamstructureModel.TeamBankId,
        //            });

        //            cmd.Parameters.Add(new SqlParameter
        //            {
        //                ParameterName = "teamgroupid",
        //                Value = teamstructureModel.TeamGroupId,
        //            });

        //            con.Open();
        //            cmd.ExecuteNonQuery();
        //            con.Close();                   
        //        }

        //        comm = "Operation Successful.";
        //        res = request.CreateResponse(HttpStatusCode.OK, comm);

        //        return res;
        //    });
        //}

        [HttpPost]
        [Route("updateteambank")]
        public HttpResponseMessage UpdateTeamBank(HttpRequestMessage request, [FromBody]TeamStructureModel teamstructureModel)
        {
            return GetHttpResponse(request, () =>
            {               
                var comm = "";
                HttpResponseMessage res = null;

                //var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

                using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    var cmd = new System.Data.SqlClient.SqlCommand("spp_updateteambankkbl", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;

                    //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "Team_Code",
                        Value = teamstructureModel.Team_Code,
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

                    //cmd.Parameters.Add(new SqlParameter
                    //{
                    //    ParameterName = "BranchName",
                    //    Value = teamstructureModel.BranchName,
                    //});                  

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "AccountOfficer_Code",
                        Value = teamstructureModel.AccountOfficer_Code,
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "AccountOfficer_Name",
                        Value = teamstructureModel.AccountOfficer_Name,
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "StaffID",
                        Value = teamstructureModel.StaffID,
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "Year",
                        Value = teamstructureModel.Year,
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "teambankid",
                        Value = teamstructureModel.TeamBankId,
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


        [HttpPost]
        [Route("updateteamgroup")]
        public HttpResponseMessage UpdateTeamGroup(HttpRequestMessage request, [FromBody]TeamStructureModel teamstructureModel)
        {
            return GetHttpResponse(request, () =>
            {
                var comm = "";
                HttpResponseMessage res = null;

                //var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

                using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    var cmd = new System.Data.SqlClient.SqlCommand("spp_updateteamgroupkbl", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;

                    //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

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
                        ParameterName = "Division_Code",
                        Value = teamstructureModel.Division_Code,
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "DivisionName",
                        Value = teamstructureModel.DivisionName,
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "Year",
                        Value = teamstructureModel.Year,
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "teamgroupid",
                        Value = teamstructureModel.TeamGroupId,
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


        //[HttpPost]
        //[Route("deleteteamstructure/{year}/{teambankid}/{teamgroupid}")]
        //public HttpResponseMessage DeleteTeamStructure(HttpRequestMessage request, int year, int teambankid, int teamgroupid)
        ////public HttpResponseMessage DeleteTeamStructure(HttpRequestMessage request, [FromBody]TeamStructureModel teamstructureModel)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        //var comm = "";
        //        HttpResponseMessage res = null;

        //        using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
        //        {
        //            var cmd = new System.Data.SqlClient.SqlCommand("spp_deleteteamstructurekbl", con);
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            cmd.CommandTimeout = 0;

        //            //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

        //            cmd.Parameters.Add(new SqlParameter
        //            {
        //                ParameterName = "teambankid",
        //                Value = teambankid,
        //                //Value = teamstructureModel.TeamBankId,
        //            });

        //            cmd.Parameters.Add(new SqlParameter
        //            {
        //                ParameterName = "teamgroupid",
        //                Value = teamgroupid,
        //               // Value = teamgroupid,
        //            });

        //            cmd.Parameters.Add(new SqlParameter
        //            {
        //                ParameterName = "Year",
        //                Value = year,
        //                //Value = year,
        //            });


        //            con.Open();
        //            cmd.ExecuteNonQuery();
        //            con.Close();
        //        }

        //        //comm = "Operation Successful.";
        //        //res = request.CreateResponse(HttpStatusCode.OK, comm);
        //        res = request.CreateResponse(HttpStatusCode.OK);

        //        return res;
        //    });
        //}


        [HttpPost]
        [Route("deleteteambankid/{teambankid}")]
        public HttpResponseMessage DeleteTeamBankId(HttpRequestMessage request, int teambankid)
        //public HttpResponseMessage DeleteTeamStructure(HttpRequestMessage request, [FromBody]TeamStructureModel teamstructureModel)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage res = null;

                using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    var cmd = new System.Data.SqlClient.SqlCommand("spp_deleteteambankidkbl", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;

                    //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "teambankid",
                        Value = teambankid,
                    });

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                //res = request.CreateResponse(HttpStatusCode.OK, comm);
                res = request.CreateResponse(HttpStatusCode.OK);

                return res;
            });
        }


        [HttpPost]
        [Route("deleteteamgroupid/{teamgroupid}")]
        public HttpResponseMessage DeleteTeamGroupId(HttpRequestMessage request, int teamgroupid)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage res = null;

                using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    var cmd = new System.Data.SqlClient.SqlCommand("spp_deleteteamgroupidkbl", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;

                    //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "teamgroupid",
                        Value = teamgroupid,
                    });

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                //res = request.CreateResponse(HttpStatusCode.OK, comm);
                res = request.CreateResponse(HttpStatusCode.OK);

                return res;
            });
        }


        //[HttpGet]
        //[Route("getteamstructure/{teambankid}/{teamgroupid}")]
        //public HttpResponseMessage GetTeamStructure(HttpRequestMessage request, int teambankid, int teamgroupid)
        ////public HttpResponseMessage GetRatios(HttpRequestMessage request)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        //int teambankid = 0;
        //        //int teamgroupid = 5;

        //        HttpResponseMessage res = null;

        //        //var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

        //       TeamStructureModel tb = new TeamStructureModel();

        //        using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
        //        {
        //            var cmd = new System.Data.SqlClient.SqlCommand("spp_getteambankgroup", con);
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            cmd.CommandTimeout = 0;

        //            //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

        //            cmd.Parameters.Add(new SqlParameter
        //            {
        //                ParameterName = "teambankid",
        //                Value = teambankid,
        //            });

        //            cmd.Parameters.Add(new SqlParameter
        //            {
        //                ParameterName = "teamgroupid",
        //                Value = teamgroupid,
        //            });

        //            con.Open();
        //            //cmd.ExecuteNonQuery();
        //            //cmd2.ExecuteNonQuery();

        //            SqlDataReader reader = cmd.ExecuteReader();

        //            // if the record exists for both tables (team_bank and team_group )
        //            if (teambankid > 0 && teamgroupid > 0)
        //            {
        //                while (reader.Read())
        //                {
        //                    tb.TeamBankId = reader["teambankid"] != DBNull.Value ? int.Parse(reader["teambankid"].ToString()) : 0;
        //                    tb.TeamGroupId = reader["teamgroupid"] != DBNull.Value ? int.Parse(reader["teamgroupid"].ToString()) : 0;

        //                    tb.Team_Code = reader["Team_Code"] != DBNull.Value ? reader["Team_Code"].ToString() : "default";
        //                    tb.TeamName = reader["TeamName"] != DBNull.Value ? reader["TeamName"].ToString() : "default";
        //                    tb.Branch_Code = reader["Branch_Code"] != DBNull.Value ? reader["Branch_Code"].ToString() : "default";
        //                    tb.BranchName = reader["BranchName"] != DBNull.Value ? reader["BranchName"].ToString() : "default";
        //                    tb.Region_Code = reader["Region_Code"] != DBNull.Value ? reader["Region_Code"].ToString() : "default";
        //                    tb.RegionName = reader["RegionName"] != DBNull.Value ? reader["RegionName"].ToString() : "default";
        //                    tb.Division_Code = reader["Division_Code"] != DBNull.Value ? reader["Division_Code"].ToString() : "default";
        //                    tb.DivisionName = reader["DivisionName"] != DBNull.Value ? reader["DivisionName"].ToString() : "default";
        //                    tb.AccountOfficer_Code = reader["AccountOfficer_Code"] != DBNull.Value ? reader["AccountOfficer_Code"].ToString() : "default";
        //                    tb.AccountOfficer_Name = reader["AccountOfficer_Name"] != DBNull.Value ? reader["AccountOfficer_Name"].ToString() : "default";
        //                    tb.StaffID = reader["StaffID"] != DBNull.Value ? reader["StaffID"].ToString() : "default";
        //                    tb.Year = reader["Year"] != DBNull.Value ? int.Parse(reader["Year"].ToString()) : 0;
        //                }
        //            }

        //            // if the record exists for only team_bank table
        //            else if (teambankid > 0 && teamgroupid == 0)
        //            {
        //                while (reader.Read())
        //                {
        //                    tb.TeamBankId = reader["teambankid"] != DBNull.Value ? int.Parse(reader["teambankid"].ToString()) : 0;
        //                    tb.TeamGroupId = 0;

        //                    tb.Team_Code = reader["Team_Code"] != DBNull.Value ? reader["Team_Code"].ToString() : "default";
        //                    tb.TeamName = reader["TeamName"] != DBNull.Value ? reader["TeamName"].ToString() : "default";
        //                    tb.Branch_Code = reader["Branch_Code"] != DBNull.Value ? reader["Branch_Code"].ToString() : "default";
        //                    tb.BranchName = reader["BranchName"] != DBNull.Value ? reader["BranchName"].ToString() : "default";
        //                    tb.Region_Code = reader["Region_Code"] != DBNull.Value ? reader["Region_Code"].ToString() : "default";
        //                    tb.RegionName = reader["RegionName"] != DBNull.Value ? reader["RegionName"].ToString() : "default";
        //                    tb.Division_Code = reader["Division_Code"] != DBNull.Value ? reader["Division_Code"].ToString() : "default";
        //                    tb.DivisionName = reader["DivisionName"] != DBNull.Value ? reader["DivisionName"].ToString() : "default";
        //                    tb.AccountOfficer_Code = reader["AccountOfficer_Code"] != DBNull.Value ? reader["AccountOfficer_Code"].ToString() : "default";
        //                    tb.AccountOfficer_Name = reader["AccountOfficer_Name"] != DBNull.Value ? reader["AccountOfficer_Name"].ToString() : "default";
        //                    tb.StaffID = reader["StaffID"] != DBNull.Value ? reader["StaffID"].ToString() : "default";
        //                    tb.Year = reader["Year"] != DBNull.Value ? int.Parse(reader["Year"].ToString()) : 0;
        //                }
        //            }

        //            // if the record exists for only team_group table
        //            else if (teambankid == 0 && teamgroupid > 0)
        //            {
        //                while (reader.Read())
        //                {
        //                    tb.TeamBankId = 0;
        //                    tb.TeamGroupId = reader["teamgroupid"] != DBNull.Value ? int.Parse(reader["teamgroupid"].ToString()) : 0;

        //                    tb.Team_Code = "default";
        //                    tb.TeamName = "default";
        //                    tb.Branch_Code = reader["Branch_Code"] != DBNull.Value ? reader["Branch_Code"].ToString() : "default";
        //                    tb.BranchName = reader["BranchName"] != DBNull.Value ? reader["BranchName"].ToString() : "default";
        //                    tb.Region_Code = reader["Region_Code"] != DBNull.Value ? reader["Region_Code"].ToString() : "default";
        //                    tb.RegionName = reader["RegionName"] != DBNull.Value ? reader["RegionName"].ToString() : "default";
        //                    tb.Division_Code = reader["Division_Code"] != DBNull.Value ? reader["Division_Code"].ToString() : "default";
        //                    tb.DivisionName = reader["DivisionName"] != DBNull.Value ? reader["DivisionName"].ToString() : "default";
        //                    tb.AccountOfficer_Code = "default";
        //                    tb.AccountOfficer_Name = "default";
        //                    tb.StaffID = "default";
        //                    tb.Year = reader["Year"] != DBNull.Value ? int.Parse(reader["Year"].ToString()) : 0;
        //                }
        //            }

        //            con.Close();
        //        }

        //        //comm = "Operation Successful.";
        //        res = request.CreateResponse(HttpStatusCode.OK, tb);

        //        return res;
        //    });
        //}

        [HttpGet]
        [Route("getteambank/{teambankid}")]
        public HttpResponseMessage GetTeamBank(HttpRequestMessage request, int teambankid)
        //public HttpResponseMessage GetRatios(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage res = null;

                //var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

                TeamBankModel tb = new TeamBankModel();

                using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    var cmd = new System.Data.SqlClient.SqlCommand("spp_getsingleteambank", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;

                    //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "teambankid",
                        Value = teambankid,
                    });


                    con.Open();
                    //cmd.ExecuteNonQuery();
                    //cmd2.ExecuteNonQuery();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        tb.TeamBankId = reader["teambankid"] != DBNull.Value ? int.Parse(reader["teambankid"].ToString()) : 0;
                        tb.Team_Code = reader["Team_Code"] != DBNull.Value ? reader["Team_Code"].ToString() : "default";
                        tb.TeamName = reader["TeamName"] != DBNull.Value ? reader["TeamName"].ToString() : "default";
                        tb.Branch_Code = reader["Branch_Code"] != DBNull.Value ? reader["Branch_Code"].ToString() : "default";
                        tb.BranchName = reader["BranchName"] != DBNull.Value ? reader["BranchName"].ToString() : "default";                      
                        tb.AccountOfficer_Code = reader["AccountOfficer_Code"] != DBNull.Value ? reader["AccountOfficer_Code"].ToString() : "default";
                        tb.AccountOfficer_Name = reader["AccountOfficer_Name"] != DBNull.Value ? reader["AccountOfficer_Name"].ToString() : "default";
                        tb.StaffID = reader["StaffID"] != DBNull.Value ? reader["StaffID"].ToString() : "default";
                        tb.Year = reader["Year"] != DBNull.Value ? int.Parse(reader["Year"].ToString()) : 0;
                    }
                    con.Close();
                }

                //comm = "Operation Successful.";
                res = request.CreateResponse(HttpStatusCode.OK, tb);

            return res;
        });
        }

        [HttpGet]
        [Route("getteamgroup/{teamgroupid}")]
        public HttpResponseMessage GetTeamGroup(HttpRequestMessage request, int teamgroupid)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage res = null;

                //var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

                TeamGroupModel tb = new TeamGroupModel();

                using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    var cmd = new System.Data.SqlClient.SqlCommand("spp_getsingleteamgroup", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;

                    //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "teamgroupid",
                        Value = teamgroupid,
                    });


                    con.Open();
                    //cmd.ExecuteNonQuery();
                    //cmd2.ExecuteNonQuery();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        tb.TeamGroupId = reader["teamgroupid"] != DBNull.Value ? int.Parse(reader["teamgroupid"].ToString()) : 0;
                        tb.Branch_Code = reader["Branch_Code"] != DBNull.Value ? reader["Branch_Code"].ToString() : "default";
                        tb.BranchName = reader["BranchName"] != DBNull.Value ? reader["BranchName"].ToString() : "default";
                        tb.Region_Code = reader["Region_Code"] != DBNull.Value ? reader["Region_Code"].ToString() : "default";
                        tb.RegionName = reader["RegionName"] != DBNull.Value ? reader["RegionName"].ToString() : "default";
                        tb.Division_Code = reader["Division_Code"] != DBNull.Value ? reader["Division_Code"].ToString() : "default";
                        tb.DivisionName = reader["DivisionName"] != DBNull.Value ? reader["DivisionName"].ToString() : "default";
                        tb.Year = reader["Year"] != DBNull.Value ? int.Parse(reader["Year"].ToString()) : 0;
                    }
                    con.Close();
                }

                //comm = "Operation Successful.";
                res = request.CreateResponse(HttpStatusCode.OK, tb);

                return res;
            });
        }

        //[HttpGet]
        //[Route("availableteamstructure")]
        //public HttpResponseMessage GetAvailableRatios(HttpRequestMessage request)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage res = null;

        //        List<TeamStructureModel> teambankgroupList = new List<TeamStructureModel>();

        //        using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
        //        {
        //            var cmd = new System.Data.SqlClient.SqlCommand("spp_gettopteambankgroups", con);
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            cmd.CommandTimeout = 0;

        //            //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

        //            //cmd.Parameters.Add(new SqlParameter
        //            //{
        //            //    ParameterName = "teambankid",
        //            //    Value = teambankid,
        //            //});                   

        //            con.Open();
        //            //cmd.ExecuteNonQuery();
        //            //cmd2.ExecuteNonQuery();

        //            SqlDataReader reader = cmd.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                var tb = new TeamStructureModel();

        //                //Map_Location_Id = dr["MapLocationId"] != DBNull.Value ? System.Convert.ToInt32(dr["MapLocationId"]) : default(int),
        //                // Location_Latitude = decimal.Parse(dr["Latitude"].ToString()),
        //                //tb.TeamBankId = int.Parse(reader["teambankid"].ToString());
        //                //tb.TeamGroupId = int.Parse(reader["teamgroupid"].ToString());

        //                tb.TeamBankId = reader["teambankid"] != DBNull.Value ? int.Parse(reader["teambankid"].ToString()) : 0;
        //                tb.TeamGroupId = reader["teamgroupid"] != DBNull.Value ? int.Parse(reader["teamgroupid"].ToString()) : 0;
        //                tb.Team_Code = reader["Team_Code"] != DBNull.Value ? reader["Team_Code"].ToString() : "default";
        //                tb.TeamName = reader["TeamName"] != DBNull.Value ? reader["TeamName"].ToString() : "default";
        //                tb.Branch_Code = reader["Branch_Code"] != DBNull.Value ? reader["Branch_Code"].ToString() : "default";
        //                tb.BranchName = reader["BranchName"] != DBNull.Value ? reader["BranchName"].ToString() : "default";
        //                tb.Region_Code = reader["Region_Code"] != DBNull.Value ? reader["Region_Code"].ToString() : "default";
        //                tb.RegionName = reader["RegionName"] != DBNull.Value ? reader["RegionName"].ToString() : "default";
        //                tb.Division_Code = reader["Division_Code"] != DBNull.Value ? reader["Division_Code"].ToString() : "default";
        //                tb.DivisionName = reader["DivisionName"] != DBNull.Value ? reader["DivisionName"].ToString() : "default";
        //                tb.AccountOfficer_Code = reader["AccountOfficer_Code"] != DBNull.Value ? reader["AccountOfficer_Code"].ToString() : "default";
        //                tb.AccountOfficer_Name = reader["AccountOfficer_Name"] != DBNull.Value ? reader["AccountOfficer_Name"].ToString() : "default";
        //                tb.StaffID = reader["StaffID"] != DBNull.Value ? reader["StaffID"].ToString() : "default";
        //                tb.Year = reader["Year"] != DBNull.Value ? int.Parse(reader["Year"].ToString()) : 0;

        //                teambankgroupList.Add(tb);
        //            }
        //            con.Close();                 
        //        }

        //        //comm = "Operation Successful.";
        //        res = request.CreateResponse(HttpStatusCode.OK, teambankgroupList);

        //        return res;
        //    });
        //}

        [HttpGet]
        [Route("availableteambank")]
        public HttpResponseMessage GetAvailableBank(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage res = null;

                List<TeamBankModel> teambankList = new List<TeamBankModel>();

                using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    var cmd = new System.Data.SqlClient.SqlCommand("spp_getteambankList", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;

                    //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

                    con.Open();
                    
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var tb = new TeamBankModel();

                        tb.TeamBankId = reader["teambankid"] != DBNull.Value ? int.Parse(reader["teambankid"].ToString()) : 0;
                        tb.Team_Code = reader["Team_Code"] != DBNull.Value ? reader["Team_Code"].ToString() : "default";
                        tb.TeamName = reader["TeamName"] != DBNull.Value ? reader["TeamName"].ToString() : "default";
                        tb.Branch_Code = reader["Branch_Code"] != DBNull.Value ? reader["Branch_Code"].ToString() : "default";
                        tb.BranchName = reader["BranchName"] != DBNull.Value ? reader["BranchName"].ToString() : "default";                       
                        tb.AccountOfficer_Code = reader["AccountOfficer_Code"] != DBNull.Value ? reader["AccountOfficer_Code"].ToString() : "default";
                        tb.AccountOfficer_Name = reader["AccountOfficer_Name"] != DBNull.Value ? reader["AccountOfficer_Name"].ToString() : "default";
                        tb.StaffID = reader["StaffID"] != DBNull.Value ? reader["StaffID"].ToString() : "default";
                        tb.Year = reader["Year"] != DBNull.Value ? int.Parse(reader["Year"].ToString()) : 0;

                        teambankList.Add(tb);
                    }
                    con.Close();
                }

                //comm = "Operation Successful.";
                res = request.CreateResponse(HttpStatusCode.OK, teambankList);

                return res;
            });
        }

        [HttpGet]
        [Route("availableteamgroup")]
        public HttpResponseMessage GetAvailableGroup(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage res = null;

                List<TeamGroupModel> teamgroupList = new List<TeamGroupModel>();

                using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    var cmd = new System.Data.SqlClient.SqlCommand("spp_getteamgroupList", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;

                    //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var tb = new TeamGroupModel();

                        tb.TeamGroupId = reader["teamgroupid"] != DBNull.Value ? int.Parse(reader["teamgroupid"].ToString()) : 0;
                        tb.Branch_Code = reader["Branch_Code"] != DBNull.Value ? reader["Branch_Code"].ToString() : "default";
                        tb.BranchName = reader["BranchName"] != DBNull.Value ? reader["BranchName"].ToString() : "default";
                        tb.Region_Code = reader["Region_Code"] != DBNull.Value ? reader["Region_Code"].ToString() : "default";
                        tb.RegionName = reader["RegionName"] != DBNull.Value ? reader["RegionName"].ToString() : "default";
                        tb.Division_Code = reader["Division_Code"] != DBNull.Value ? reader["Division_Code"].ToString() : "default";
                        tb.DivisionName = reader["DivisionName"] != DBNull.Value ? reader["DivisionName"].ToString() : "default";                       
                        tb.Year = reader["Year"] != DBNull.Value ? int.Parse(reader["Year"].ToString()) : 0;

                        teamgroupList.Add(tb);
                    }
                    con.Close();
                }

                //comm = "Operation Successful.";
                res = request.CreateResponse(HttpStatusCode.OK, teamgroupList);

                return res;
            });
        }


        [HttpGet]
        [Route("getteamstructurebyparams/{svalue}/{yr}")]
        public HttpResponseMessage GetTeamBankGroupByParams(HttpRequestMessage request, string svalue, int yr)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage res = null;

                List<TeamStructureModel> teambankgroupList = new List<TeamStructureModel>();

                using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    var cmd = new System.Data.SqlClient.SqlCommand("spp_getteambankgroupsbyparams", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;

                    //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "searchvalue",
                        Value = svalue,
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "year",
                        Value = yr,
                    });

                    con.Open();
                    //cmd.ExecuteNonQuery();
                    //cmd2.ExecuteNonQuery();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var tb = new TeamStructureModel();

                        //Map_Location_Id = dr["MapLocationId"] != DBNull.Value ? System.Convert.ToInt32(dr["MapLocationId"]) : default(int),
                        // Location_Latitude = decimal.Parse(dr["Latitude"].ToString()),
                        //tb.TeamBankId = int.Parse(reader["teambankid"].ToString());
                        //tb.TeamGroupId = int.Parse(reader["teamgroupid"].ToString());

                        tb.TeamBankId = reader["teambankid"] != DBNull.Value ? int.Parse(reader["teambankid"].ToString()) : 0;
                        tb.TeamGroupId = reader["teamgroupid"] != DBNull.Value ? int.Parse(reader["teamgroupid"].ToString()) : 0;
                        tb.Team_Code = reader["Team_Code"] != DBNull.Value ? reader["Team_Code"].ToString() : "default";
                        tb.TeamName = reader["TeamName"] != DBNull.Value ? reader["TeamName"].ToString() : "default";
                        tb.Branch_Code = reader["Branch_Code"] != DBNull.Value ? reader["Branch_Code"].ToString() : "default";
                        tb.BranchName = reader["BranchName"] != DBNull.Value ? reader["BranchName"].ToString() : "default";
                        tb.Region_Code = reader["Region_Code"] != DBNull.Value ? reader["Region_Code"].ToString() : "default";
                        tb.RegionName = reader["RegionName"] != DBNull.Value ? reader["RegionName"].ToString() : "default";
                        tb.Division_Code = reader["Division_Code"] != DBNull.Value ? reader["Division_Code"].ToString() : "default";
                        tb.DivisionName = reader["DivisionName"] != DBNull.Value ? reader["DivisionName"].ToString() : "default";
                        tb.AccountOfficer_Code = reader["AccountOfficer_Code"] != DBNull.Value ? reader["AccountOfficer_Code"].ToString() : "default";
                        tb.AccountOfficer_Name = reader["AccountOfficer_Name"] != DBNull.Value ? reader["AccountOfficer_Name"].ToString() : "default";
                        tb.StaffID = reader["StaffID"] != DBNull.Value ? reader["StaffID"].ToString() : "default";
                        tb.Year = reader["Year"] != DBNull.Value ? int.Parse(reader["Year"].ToString()) : 0;

                        teambankgroupList.Add(tb);
                    }
                    con.Close();
                }

                //comm = "Operation Successful.";
                res = request.CreateResponse(HttpStatusCode.OK, teambankgroupList);

                return res;
            });
        }

        //This part will retrieve the information about the entered branchedcode i.e info about who the branch report to like the division
        [HttpGet]
        [Route("getteambanktop1/{branchcode}/{yr}")]
        public HttpResponseMessage GetTeamBankTop1(HttpRequestMessage request, string branchcode, int yr)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage res = null;

                TeamStructureModel tb = new TeamStructureModel();

                using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    var cmd = new System.Data.SqlClient.SqlCommand("spp_gettop_1_teambankvalidate", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;

                    //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "Branchcode",
                        Value = branchcode,
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
                        //tb.Region_Code = reader["Region_Code"] != DBNull.Value ? reader["Region_Code"].ToString() : "default";
                        //tb.RegionName = reader["RegionName"] != DBNull.Value ? reader["RegionName"].ToString() : "default";
                        //tb.Division_Code = reader["Division_Code"] != DBNull.Value ? reader["Division_Code"].ToString() : "default";
                        //tb.DivisionName = reader["DivisionName"] != DBNull.Value ? reader["DivisionName"].ToString() : "default";
                                      
                    }
                    con.Close();
                }

                //comm = "Operation Successful.";
                res = request.CreateResponse(HttpStatusCode.OK, tb);

                return res;
            });
        }

        //This part will retrieve the information about the entered regioncode i.e who the region report to like directorate
        [HttpGet]
        [Route("getteamgrouptop1/{regioncode}/{yr}")]
        public HttpResponseMessage GetTeamBankGroupTop1(HttpRequestMessage request, string regioncode, int yr)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage res = null;

                TeamStructureModel tb = new TeamStructureModel();

                using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    var cmd = new System.Data.SqlClient.SqlCommand("spp_gettop_1_teamgroupvalidate", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;

                    //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "Regioncode",
                        Value = regioncode,
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
                        //tb.TeamName = reader["TeamName"] != DBNull.Value ? reader["TeamName"].ToString() : "default";
                        //tb.Branch_Code = reader["Branch_Code"] != DBNull.Value ? reader["Branch_Code"].ToString() : "default";
                        //tb.BranchName = reader["BranchName"] != DBNull.Value ? reader["BranchName"].ToString() : "default";
                        //tb.Region_Code = reader["Region_Code"] != DBNull.Value ? reader["Region_Code"].ToString() : "default";
                        tb.RegionName = reader["RegionName"] != DBNull.Value ? reader["RegionName"].ToString() : "default";
                        tb.Division_Code = reader["Division_Code"] != DBNull.Value ? reader["Division_Code"].ToString() : "default";
                        tb.DivisionName = reader["DivisionName"] != DBNull.Value ? reader["DivisionName"].ToString() : "default";

                    }
                    con.Close();
                }

                //comm = "Operation Successful.";
                res = request.CreateResponse(HttpStatusCode.OK, tb);

                return res;
            });
        }


        [HttpGet]
        [Route("teamstructureusingparameters/{selecteddefinitiocode}/{SearchValue}/{year}")]
        public HttpResponseMessage TeamStructureByParameters(HttpRequestMessage request, string selecteddefinitiocode, string SearchValue, int year)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage res = null;

                List<TeamStructureModel> teambankgroupList = new List<TeamStructureModel>();

                using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    var cmd = new System.Data.SqlClient.SqlCommand("spp_getteambankgroupbydefcodevalyr", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;

                    //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "selectedDefinitionCode",
                        Value = selecteddefinitiocode,
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "codeORname",
                        Value = SearchValue,
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "Year",
                        Value = year,
                    });

                    con.Open();
                    //cmd.ExecuteNonQuery();
                    //cmd2.ExecuteNonQuery();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var tb = new TeamStructureModel();

                        //Map_Location_Id = dr["MapLocationId"] != DBNull.Value ? System.Convert.ToInt32(dr["MapLocationId"]) : default(int),
                        // Location_Latitude = decimal.Parse(dr["Latitude"].ToString()),
                        //tb.TeamBankId = int.Parse(reader["teambankid"].ToString());
                        //tb.TeamGroupId = int.Parse(reader["teamgroupid"].ToString());

                        tb.TeamBankId = reader["teambankid"] != DBNull.Value ? int.Parse(reader["teambankid"].ToString()) : 0;
                        tb.TeamGroupId = reader["teamgroupid"] != DBNull.Value ? int.Parse(reader["teamgroupid"].ToString()) : 0;
                        tb.Team_Code = reader["Team_Code"] != DBNull.Value ? reader["Team_Code"].ToString() : "default";
                        tb.TeamName = reader["TeamName"] != DBNull.Value ? reader["TeamName"].ToString() : "default";
                        tb.Branch_Code = reader["Branch_Code"] != DBNull.Value ? reader["Branch_Code"].ToString() : "default";
                        tb.BranchName = reader["BranchName"] != DBNull.Value ? reader["BranchName"].ToString() : "default";
                        tb.Region_Code = reader["Region_Code"] != DBNull.Value ? reader["Region_Code"].ToString() : "default";
                        tb.RegionName = reader["RegionName"] != DBNull.Value ? reader["RegionName"].ToString() : "default";
                        tb.Division_Code = reader["Division_Code"] != DBNull.Value ? reader["Division_Code"].ToString() : "default";
                        tb.DivisionName = reader["DivisionName"] != DBNull.Value ? reader["DivisionName"].ToString() : "default";
                        tb.AccountOfficer_Code = reader["AccountOfficer_Code"] != DBNull.Value ? reader["AccountOfficer_Code"].ToString() : "default";
                        tb.AccountOfficer_Name = reader["AccountOfficer_Name"] != DBNull.Value ? reader["AccountOfficer_Name"].ToString() : "default";
                        tb.StaffID = reader["StaffID"] != DBNull.Value ? reader["StaffID"].ToString() : "default";
                        tb.Year = reader["Year"] != DBNull.Value ? int.Parse(reader["Year"].ToString()) : 0;

                        teambankgroupList.Add(tb);
                    }
                    con.Close();
                }

                //comm = "Operation Successful.";
                res = request.CreateResponse(HttpStatusCode.OK, teambankgroupList);

                return res;
            });
        }

        //[HttpPost]
        ////[Route("mappingofteamstructure/{selectedlowerlevelcode}/{selectedlowerlevelmiscode}/{selectedhigherlevelcode}/{selectedhigherlevelmiscode}/{selectedhigherlevelmisname}/{year}")]
        //[Route("mappingofteamstructure")]

        ////public HttpResponseMessage MappingBranchToDivision(HttpRequestMessage request, string selectedlowerlevelcode, string selectedlowerlevelmiscode, string selectedhigherlevelcode, string selectedhigherlevelmiscode, string selectedhigherlevelmisname, int year)
        //public HttpResponseMessage MappingBranchToDivision(HttpRequestMessage request, [FromBody]TeamStructureMappingModel tsmapping)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage res = null;

        //        using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
        //        {
        //            using (var cmd = new System.Data.SqlClient.SqlCommand("", con))
        //            {
        //                string selectedlowerlevelcode = tsmapping.lowerlevelcode;
        //                string selectedhigherlevelcode = tsmapping.higherlevelcode;
        //                string selectedlowerlevelmiscode = tsmapping.lowerlevelmiscode;
        //                string selectedhigherlevelmiscode = tsmapping.higherlevelmiscode;
        //                string selectedhigherlevelmisname = tsmapping.higherlevelmisname;
        //                int year = tsmapping.year;

        //                string query = null;

        //                if (selectedlowerlevelcode.Trim().ToUpper() == "BRH")  //i.e team in the team_bank table
        //                { 
        //                    query = "update Team_Bank set branch_code=@selectedHIGHERlevelmiscode, branchname=@selectedHIGHERlevelmisname where team_code=@selectedLOWERlevelmiscode and year=@YEAR";
        //                }

        //                else if (selectedlowerlevelcode.Trim().ToUpper() == "DIV")   //i.e branch in the team_group table
        //                {
        //                    query = "update Team_Group set region_code=@selectedHIGHERlevelmiscode, regionname=@selectedHIGHERlevelmisname where branch_code=@selectedLOWERlevelmiscode and year=@YEAR";
        //                }

        //                else if (selectedlowerlevelcode.Trim().ToUpper() == "REG")   //i.e region in the team_group table
        //                {
        //                    query = "update Team_Group set division_code=@selectedHIGHERlevelmiscode, divisioname=@selectedHIGHERlevelmisname where region_code=@selectedLOWERlevelmiscode and year=@YEAR";
        //                }

        //                con.Open();
        //                cmd.CommandText = query;
        //                cmd.Parameters.AddWithValue("@selectedHIGHERlevelmiscode", selectedhigherlevelmiscode);
        //                cmd.Parameters.AddWithValue("@selectedHIGHERlevelmisname", selectedhigherlevelmisname);
        //                cmd.Parameters.AddWithValue("@selectedLOWERlevelmiscode", selectedlowerlevelmiscode);
        //                cmd.Parameters.AddWithValue("@YEAR", year);
        //                cmd.ExecuteNonQuery();
        //                con.Close();
        //            }
        //        }
        //        return request.CreateResponse(HttpStatusCode.OK, res);
        //    });
        //}

        [HttpGet]
        [Route("teamstructurebranch/{yr}")]
        public HttpResponseMessage TeamStructureBranch(HttpRequestMessage request, int yr)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage res = null;
                var branchList = new List<teamBankGroupNameCodeModel>();

                using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    using (var cmd = new System.Data.SqlClient.SqlCommand("", con))
                    {
                        string  query = "select distinct branch_code, branchname from vw_TeamBankGroupReport where year=@YEAR";
                        
                        con.Open();
                        cmd.CommandText = query;
                        cmd.Parameters.AddWithValue("@YEAR", yr);
                        //cmd.ExecuteNonQuery();
                        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            var obj = new Models.teamBankGroupNameCodeModel();

                            obj.Code = reader["branch_code"] != DBNull.Value ? reader["branch_code"].ToString() : "";
                            obj.Name = reader["branchname"] != DBNull.Value ? reader["branchname"].ToString() : "";

                            branchList.Add(obj);
                        }                       
                        con.Close();
                    }
                    //branchList = branchList.OrderBy(x => x.Name).GroupBy(x => x.Code).ToList();
                }
                return request.CreateResponse(HttpStatusCode.OK, branchList);
            });
        }

        [HttpGet]
        [Route("teamstructuredivision/{yr}")]
        public HttpResponseMessage TeamStructureDivision(HttpRequestMessage request, int yr)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage res = null;
                var divisionList = new List<teamBankGroupNameCodeModel>();

                using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    using (var cmd = new System.Data.SqlClient.SqlCommand("", con))
                    {
                        string query = "select distinct division_code, divisionname from vw_TeamBankGroupReport where year=@YEAR";

                        con.Open();
                        cmd.CommandText = query;
                        cmd.Parameters.AddWithValue("@YEAR", yr);
                        //cmd.ExecuteNonQuery();
                        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            var obj = new Models.teamBankGroupNameCodeModel();

                            obj.Code = reader["division_code"] != DBNull.Value ? reader["division_code"].ToString() : "";
                            obj.Name = reader["divisionname"] != DBNull.Value ? reader["divisionname"].ToString() : "";

                            divisionList.Add(obj);
                        }
                        con.Close();
                    }
                }
                return request.CreateResponse(HttpStatusCode.OK, divisionList);
            });
        }

        [HttpGet]
        [Route("teamstructureregion/{yr}")]
        public HttpResponseMessage TeamStructureRegion(HttpRequestMessage request, int yr)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage res = null;
                var regionList = new List<teamBankGroupNameCodeModel>();

                using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    using (var cmd = new System.Data.SqlClient.SqlCommand("", con))
                    {
                        string query = "select distinct region_code, regionname from vw_TeamBankGroupReport where year=@YEAR";

                        con.Open();
                        cmd.CommandText = query;
                        cmd.Parameters.AddWithValue("@YEAR", yr);
                        //cmd.ExecuteNonQuery();
                        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            var obj = new Models.teamBankGroupNameCodeModel();

                            obj.Code = reader["region_code"] != DBNull.Value ? reader["region_code"].ToString() : "";
                            obj.Name = reader["regionname"] != DBNull.Value ? reader["regionname"].ToString() : "";

                            regionList.Add(obj);
                        }
                        con.Close();
                    }
                }
                return request.CreateResponse(HttpStatusCode.OK, regionList);
            });
        }

        [HttpGet]
        [Route("teamstructuredirectorate/{yr}")]
        public HttpResponseMessage TeamStructureDirectorate(HttpRequestMessage request, int yr)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage res = null;
                var directorateList = new List<teamBankGroupNameCodeModel>();

                using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    using (var cmd = new System.Data.SqlClient.SqlCommand("", con))
                    {
                        string query = "select distinct directoratecode, directoratename from vw_TeamBankGroupReport where year=@YEAR";

                        con.Open();
                        cmd.CommandText = query;
                        cmd.Parameters.AddWithValue("@YEAR", yr);
                        //cmd.ExecuteNonQuery();
                        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            var obj = new Models.teamBankGroupNameCodeModel();

                            obj.Code = reader["directoratecode"] != DBNull.Value ? reader["directoratecode"].ToString() : "";
                            obj.Name = reader["directoratename"] != DBNull.Value ? reader["directoratename"].ToString() : "";

                            directorateList.Add(obj);
                        }
                        con.Close();
                    }
                }
                return request.CreateResponse(HttpStatusCode.OK, directorateList);
            });
        }

        [HttpPost]
        //[Route("mappingofteamstructure/{selectedlowerlevelcode}/{selectedlowerlevelmiscode}/{selectedhigherlevelcode}/{selectedhigherlevelmiscode}/{selectedhigherlevelmisname}/{year}")]
        [Route("mappingofteambank")]

        //public HttpResponseMessage MappingBranchToDivision(HttpRequestMessage request, string selectedlowerlevelcode, string selectedlowerlevelmiscode, string selectedhigherlevelcode, string selectedhigherlevelmiscode, string selectedhigherlevelmisname, int year)
        public HttpResponseMessage MappingTeamToBranch(HttpRequestMessage request, [FromBody]TeamStructureMapping2Model tsmapping)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage res = null;

                using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    using (var cmd = new System.Data.SqlClient.SqlCommand("", con))
                    {
                        //string query = "select top 1 branch_code from team_bank where team_code=@teammiscode2 and year=@YEAR2";
                        //string currentbranchcode = "";

                        //con.Open();
                        //cmd.CommandText = query;
                        //cmd.Parameters.AddWithValue("@teammiscode2", tsmapping.Team_Code);
                        //cmd.Parameters.AddWithValue("@YEAR2", tsmapping.Year);
                        ////cmd.ExecuteNonQuery();
                        //System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                        //while (reader.Read())
                        //{
                        //    currentbranchcode = reader["branch_code"] != DBNull.Value ? reader["branch_code"].ToString() : "";
                        //}
                        //con.Close();


                        //var query2 = "begin " +
                        //"update Team_Bank set branch_code=@branchmiscode, branchname=@branchmisname where team_code=@teammiscode and year=@YEAR " +
                        //"update Team_Group set region_code=@regionmiscode, regionname=@regionmisname where branch_code=@currentbranchmiscode and year=@YEAR " +
                        //"update Team_Group set division_code=@divisionmiscode, divisionname=@divisionmisname where branch_code=@currentbranchmiscode and year=@YEAR " +
                        //"end";

                        var query2 = "update Team_Bank set branch_code=@branchmiscode, branchname=@branchmisname where team_code=@teammiscode and year=@YEAR ";

                        con.Open();
                        cmd.CommandText = query2;

                        cmd.Parameters.AddWithValue("@teammiscode", tsmapping.Team_Code);
                        cmd.Parameters.AddWithValue("@branchmiscode", tsmapping.Branch_Code);
                        cmd.Parameters.AddWithValue("@branchmisname", tsmapping.BranchName);

                        cmd.Parameters.AddWithValue("@YEAR", tsmapping.Year);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                return request.CreateResponse(HttpStatusCode.OK, res);
            });
        }


        [HttpPost]
        [Route("mappingofteamgroupbrhreg")]
        public HttpResponseMessage MappingBranchToRegion(HttpRequestMessage request, [FromBody]TeamStructureMapping2Model tsmapping)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage res = null;

                using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    using (var cmd = new System.Data.SqlClient.SqlCommand("", con))
                    {                       
                        var query2 = "update Team_Group set region_code=@regionmiscode, regionname=@regionmisname where branch_code=@branchmiscode2 and year=@YEAR ";

                        con.Open();
                        cmd.CommandText = query2;

                        cmd.Parameters.AddWithValue("@branchmiscode2", tsmapping.Branch_Code2);
                        cmd.Parameters.AddWithValue("@regionmiscode", tsmapping.Region_Code);
                        cmd.Parameters.AddWithValue("@regionmisname", tsmapping.RegionName);

                        cmd.Parameters.AddWithValue("@YEAR", tsmapping.Year);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                return request.CreateResponse(HttpStatusCode.OK, res);
            });
        }

        [HttpPost]
        [Route("mappingofteamgroupregdiv")]
        public HttpResponseMessage MappingRegionToDivision(HttpRequestMessage request, [FromBody]TeamStructureMapping2Model tsmapping)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage res = null;

                using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    using (var cmd = new System.Data.SqlClient.SqlCommand("", con))
                    {
                        var query2 = "update Team_Group set division_code=@divisionmiscode, divisionname=@divisionmisname where region_code=@regionmiscode2 and year=@YEAR ";

                        con.Open();
                        cmd.CommandText = query2;

                        cmd.Parameters.AddWithValue("@regionmiscode2", tsmapping.Region_Code2);
                        cmd.Parameters.AddWithValue("@divisionmiscode", tsmapping.Division_Code);
                        cmd.Parameters.AddWithValue("@divisionmisname", tsmapping.DivisionName);

                        cmd.Parameters.AddWithValue("@YEAR", tsmapping.Year);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                return request.CreateResponse(HttpStatusCode.OK, res);
            });
        }


        [HttpPost]
        //[Route("mappingofteamstructure/{selectedlowerlevelcode}/{selectedlowerlevelmiscode}/{selectedhigherlevelcode}/{selectedhigherlevelmiscode}/{selectedhigherlevelmisname}/{year}")]
        [Route("mappingofteamgroup")]

        //public HttpResponseMessage MappingBranchToDivision(HttpRequestMessage request, string selectedlowerlevelcode, string selectedlowerlevelmiscode, string selectedhigherlevelcode, string selectedhigherlevelmiscode, string selectedhigherlevelmisname, int year)
        public HttpResponseMessage MappingBranchToRegionToDivision(HttpRequestMessage request, [FromBody]TeamStructureMapping2Model tsmapping)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage res = null;

                using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    using (var cmd = new System.Data.SqlClient.SqlCommand("", con))
                    {                      

                        var query2 = "begin " +
                        "update Team_Group set region_code=@regionmiscode, regionname=@regionmisname, division_code=@divisionmiscode, divisionname=@divisionmisname " +
                        "where branch_code=@branchmiscode and year=@YEAR  " +
                        "end";

                        con.Open();
                        cmd.CommandText = query2;

                        cmd.Parameters.AddWithValue("@branchmiscode", tsmapping.Branch_Code);
                        cmd.Parameters.AddWithValue("@regionmiscode", tsmapping.Region_Code);
                        cmd.Parameters.AddWithValue("@regionmisname", tsmapping.RegionName);
                        cmd.Parameters.AddWithValue("@divisionmiscode", tsmapping.Division_Code);
                        cmd.Parameters.AddWithValue("@divisionmisname", tsmapping.DivisionName);

                        cmd.Parameters.AddWithValue("@YEAR", tsmapping.Year);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                return request.CreateResponse(HttpStatusCode.OK, res);
            });
        }



    }
}
