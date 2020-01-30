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
    [RoutePrefix("api/onboardinguser")]
    [UsesDisposableService]
    public class OnBoardingUserApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public OnBoardingUserApiController(ICoreService coreService)
        {
            _CoreService = coreService;
        }

        ICoreService _CoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_CoreService);
        }

        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;


        //[HttpPost]
        //[Route("updatestaff")]
        //public HttpResponseMessage UpdateStaff(HttpRequestMessage request, [FromBody]Staff staffModel)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        var staff = _CoreService.UpdateStaff(staffModel);

        //        return request.CreateResponse<Staff>(HttpStatusCode.OK, staff);
        //    });
        //}

        //[HttpGet]
        //[Route("availableonboardingusers")]
        //public HttpResponseMessage GetAvailableStaffs(HttpRequestMessage request)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        Staff[] staffs = _CoreService.GetAllStaffs();

        //        return request.CreateResponse<Staff[]>(HttpStatusCode.OK, staffs);
        //    });
        //}

        //[HttpGet]
        //[Route("availableonboardingusers")]
        //public HttpResponseMessage GetAvailableOnBoardingUsers(HttpRequestMessage request)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        //string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;
        //        List<Models.OnBoardingUserModel> obuList = new List<Models.OnBoardingUserModel>();

        //        using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
        //        {
        //            var cmd = new System.Data.SqlClient.SqlCommand("spp_availableonboardingusers", con);
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            cmd.CommandTimeout = 0;

        //            //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

        //            con.Open();
        //            //cmd.ExecuteNonQuery();
        //            //cmd2.ExecuteNonQuery();

        //            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                var obu = new Models.OnBoardingUserModel();

        //                obu.Id = reader["Id"] != DBNull.Value ? int.Parse(reader["Id"].ToString()) : 0;

        //                obu.UserName = reader["UserName"] != DBNull.Value ? reader["UserName"].ToString() : "";
        //                obu.FirstName = reader["FirstName"] != DBNull.Value ? reader["FirstName"].ToString() : "";
        //                obu.LastName = reader["LastName"] != DBNull.Value ? reader["LastName"].ToString() : "";
        //                obu.Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : "";
        //                obu.StaffId = reader["StaffId"] != DBNull.Value ? reader["StaffId"].ToString() : "";
        //                obu.TeamDefinitionCode = reader["TeamDefinitionCode"] != DBNull.Value ? reader["TeamDefinitionCode"].ToString() : "";
        //                obu.MISCode = reader["MISCode"] != DBNull.Value ? reader["MISCode"].ToString() : "";
        //                obu.Status = reader["Status"] != DBNull.Value ? reader["Status"].ToString() : "";

        //                obuList.Add(obu);
        //            }
        //            con.Close();
        //        }

        //        //Staff[] staffs = _CoreService.GetAllStaffs();

        //        return request.CreateResponse(HttpStatusCode.OK, obuList);
        //    });
        //}

        [HttpGet]
        [Route("availableonboardingusers")]
        public HttpResponseMessage GetAvailableOnBoardingUsers2(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                List<Models.OnBoardingUserModel> obuList = new List<Models.OnBoardingUserModel>();

                using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    var cmd = new System.Data.SqlClient.SqlCommand("", con);

                    //cmd.CommandText = "select * from Names where Id=@Id";
                    //cmd.Parameters.AddWithValue("@Id", id);

                    con.Open();
                    cmd.CommandText = "select * from OnBoardingUsers";
                    System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var obu = new Models.OnBoardingUserModel();

                        obu.Id = reader["Id"] != DBNull.Value ? int.Parse(reader["Id"].ToString()) : 0;

                        obu.UserName = reader["UserName"] != DBNull.Value ? reader["UserName"].ToString() : "";
                        obu.FirstName = reader["FirstName"] != DBNull.Value ? reader["FirstName"].ToString() : "";
                        obu.LastName = reader["LastName"] != DBNull.Value ? reader["LastName"].ToString() : "";
                        obu.Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : "";
                        obu.StaffId = reader["StaffId"] != DBNull.Value ? reader["StaffId"].ToString() : "";
                        obu.TeamDefinitionCode = reader["TeamDefinitionCode"] != DBNull.Value ? reader["TeamDefinitionCode"].ToString() : "";
                        obu.MISCode = reader["MISCode"] != DBNull.Value ? reader["MISCode"].ToString() : "";
                        obu.Status = reader["Status"] != DBNull.Value ? reader["Status"].ToString() : "";

                        obuList.Add(obu);
                    }
                    con.Close();

                }

                //Staff[] staffs = _CoreService.GetAllStaffs();

                return request.CreateResponse(HttpStatusCode.OK, obuList);
            });
        }

        [HttpPost]
        [Route("onboardingusersapprovalanddecline/{selectedIds}")]
        //public HttpResponseMessage EditOnboardingUsersApprovalAndDecline(HttpRequestMessage request, [FromBody]int[] model)
        public HttpResponseMessage EditOnboardingUsersApprovalAndDecline(HttpRequestMessage request, string selectedIds)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage res = null;

                using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    //var cmd = new System.Data.SqlClient.SqlCommand("", con);
                    ////cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    ////cmd.CommandTimeout = 0;

                    //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;


                    //cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter
                    //{
                    //    ParameterName = "@Ids",
                    //    Value = selectedIds,
                    //});
                    //cmd.Parameters["@Ids"].Value = Convert.ToInt32(selectedIds);


                    //////con.Open();
                    //////cmd.CommandText = "update OnBoardingUsers set status='Awaiting Approval3' where id in (@Ids)";
                    //////cmd.Parameters.AddWithValue("@Ids", selectedIds);
                    ////////cmd.Parameters.AddWithValue("@Ids", System.Data.SqlDbType.Int);
                    ////////cmd.Parameters["@Ids"].Value = Convert.ToInt32(selectedIds);
                    //////cmd.ExecuteNonQuery();
                    //////con.Close();
                    ///

                    using (var cmd = new System.Data.SqlClient.SqlCommand("", con))
                    {
                        var userInput = selectedIds;
                        var values = userInput.Split(',');

                        con.Open();
                        var sql = "update OnBoardingUsers set status='Awaiting Approval3' where id IN(";
                        for (int i = 0; i < values.Length; i++)
                        {
                            sql = $"{sql} @{i},";
                            cmd.Parameters.Add($"@{i}", System.Data.Odbc.OdbcType.Int).Value = values[i];
                        }
                        cmd.CommandText = sql.TrimEnd(',') + ");";
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                    using (var cmd = new System.Data.SqlClient.SqlCommand("", con))
                    {
                        var userInput = selectedIds;
                        var values = userInput.Split(',');

                        con.Open();
                        var sql2 = "select UserName, Email, FirstName, LastName, StaffId, TeamDefinitionCode, MISCode from OnBoardingUsers where id IN(";
                        for (int i = 0; i < values.Length; i++)
                        {
                            sql2 = $"{sql2} @{i},";
                            cmd.Parameters.Add($"@{i}", System.Data.Odbc.OdbcType.Int).Value = values[i];
                        }
                        cmd.CommandText = sql2.TrimEnd(',') + ");";
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    //con.Close();

                }

                return request.CreateResponse(HttpStatusCode.OK, res);
            });
        }


    }
}
