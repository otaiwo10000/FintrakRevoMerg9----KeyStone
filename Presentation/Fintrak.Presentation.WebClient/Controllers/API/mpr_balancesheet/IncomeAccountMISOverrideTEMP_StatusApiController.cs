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
using Fintrak.Presentation.WebClient.Additionalmethods.ApprovalMtd;
using MoreLinq.Extensions;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/incomeaccountMISoverrideTEMPstatus")]
    [UsesDisposableService]
    public class IncomeAccountMISOverrideTEMPStatusApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomeAccountMISOverrideTEMPStatusApiController(IMPRBSService mprBSService)
        {
            _MPRBSService = mprBSService;
        }

        IMPRBSService _MPRBSService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRBSService);
        }

        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

        //======================== awaiting starts ================================================================================

        [HttpGet]
        [Route("incomeaccountMISoverrideAWAITING")]
        public HttpResponseMessage IncomeAccountMISOverrideAwaiting(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "AWAITINGAPPROVAL";
                IncomeAccountMISOverrideTEMPStatus obj = new IncomeAccountMISOverrideTEMPStatus();
                var ddb = obj.IncomeAccountMISOverrideTEMP(approvalstatus);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        [HttpGet]
        [Route("incomeaccountMISoverrideusingparamsAWAITING/{search}")]
        public HttpResponseMessage IncomeAccountMISOverrideUsingParamsAwaiting(HttpRequestMessage request, string search)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "AWAITINGAPPROVAL";
                IncomeAccountMISOverrideTEMPStatus obj = new IncomeAccountMISOverrideTEMPStatus();
                var ddb = obj.IncomeAccountMISOverrideTEMPusingparams(approvalstatus, search);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        //======================== awaiting ends ================================================================================

        //======================== approved starts ================================================================================

        [HttpGet]
        [Route("incomeaccountMISoverrideAPPROVED")]
        public HttpResponseMessage IncomeAccountMISOverrideApproved(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "APPROVED";
                IncomeAccountMISOverrideTEMPStatus obj = new IncomeAccountMISOverrideTEMPStatus();
                var ddb = obj.IncomeAccountMISOverrideTEMP(approvalstatus);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        [HttpGet]
        [Route("incomeaccountMISoverrideusingparamsAPPROVED/{search}")]
        public HttpResponseMessage TeamUsingParamsApproved(HttpRequestMessage request, string search)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "APPROVED";
                IncomeAccountMISOverrideTEMPStatus obj = new IncomeAccountMISOverrideTEMPStatus();
                var ddb = obj.IncomeAccountMISOverrideTEMPusingparams(approvalstatus, search);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        //======================== approved ends ================================================================================

        //======================== declined starts ================================================================================

        [HttpGet]
        [Route("incomeaccountMISoverrideDECLINED")]
        public HttpResponseMessage IncomeAccountMISOverrideDECLINED(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "DECLINED";
                IncomeAccountMISOverrideTEMPStatus obj = new IncomeAccountMISOverrideTEMPStatus();
                var ddb = obj.IncomeAccountMISOverrideTEMP(approvalstatus);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        [HttpGet]
        [Route("incomeaccountMISoverrideusingparamsDECLINED/{search}")]
        public HttpResponseMessage IncomeOtherBreakdownTEMPusingparamsDECLINED(HttpRequestMessage request, string search)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "DECLINED";
                IncomeAccountMISOverrideTEMPStatus obj = new IncomeAccountMISOverrideTEMPStatus();
                var ddb = obj.IncomeAccountMISOverrideTEMPusingparams(approvalstatus, search);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        //======================== declined ends ================================================================================

        [HttpPost]
        //[Route("incomeaccountMISoverrideapproval/{selectedIds}")]
        [Route("incomeaccountMISoverrideapproval")]
        public HttpResponseMessage EditIncomeAccountMISOverrideApproval(HttpRequestMessage request, [FromBody]string[] selectedIds2)
        {
            return GetHttpResponse(request, () =>
            {
                //IncomeAccountMISOverrideTEMPStatus Obj = new IncomeAccountMISOverrideTEMPStatus();
                //string selectedIds = String.Join(",", selectedIds2);
                //Obj.EditIncomeAccountMISOverrideApproval(selectedIds);

                //int counter = 1;

                IncomeAccountMISOverrideTEMPStatus Obj = new IncomeAccountMISOverrideTEMPStatus();
                foreach (var batch in selectedIds2.Batch(2000))
                {
                    //foreach (var eachId in batch)
                    //{
                    //Console.WriteLine("Batch: {0}, Id: {1}", counter, eachId);
                    //}
                    // counter++;

                    string selectedIds = String.Join(",", batch);
                    Obj.EditIncomeAccountMISOverrideApproval(selectedIds);
                }


                return request.CreateResponse(HttpStatusCode.OK);
            });
        }

        [HttpPost]
        //[Route("incomeaccountMISoverridedecline/{selectedIds}")]
        [Route("incomeaccountMISoverridedecline")]
        public HttpResponseMessage EditIncomeAccountMISOverrideDecline(HttpRequestMessage request, [FromBody]string[] selectedIds2)
        {
            return GetHttpResponse(request, () =>
            {
                //IncomeAccountMISOverrideTEMPStatus tObj = new IncomeAccountMISOverrideTEMPStatus();
                //string selectedIds = String.Join(",", selectedIds2);
                //tObj.EditIncomeAccountMISOverrideDecline(selectedIds);

                IncomeAccountMISOverrideTEMPStatus Obj = new IncomeAccountMISOverrideTEMPStatus();
                foreach (var batch in selectedIds2.Batch(2000))
                {
                    string selectedIds = String.Join(",", batch);
                    Obj.EditIncomeAccountMISOverrideDecline(selectedIds);
                }

                return request.CreateResponse(HttpStatusCode.OK);
            });
        }


        //[HttpGet]
        //[Route("incomeaccountMISoverrideAWAITING")]
        //public HttpResponseMessage GetAvailableIncomeAccountMISOverrideAwaiting(HttpRequestMessage request)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        //List<Models.OnBoardingUserModel> obuList = new List<Models.OnBoardingUserModel>();
        //        List<IncomeAccountMISOverrideTEMP> obuList = new List<IncomeAccountMISOverrideTEMP>();

        //        using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
        //        {
        //            var cmd = new System.Data.SqlClient.SqlCommand("", con);

        //            //cmd.CommandText = "select * from Names where Id=@Id";
        //            //cmd.Parameters.AddWithValue("@Id", id);

        //            con.Open();
        //            cmd.CommandText = "select top 500 Id, accountnumber, mis, AccountOfficer_Code, ApprovalStatus from Income_accountMIS_Override_TEMP where ApprovalStatus='AWAITINGAPPROVAL'";
        //            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                var obu = new IncomeAccountMISOverrideTEMP();

        //                obu.Id = reader["Id"] != DBNull.Value ? int.Parse(reader["Id"].ToString()) : 0;

        //                obu.accountnumber = reader["accountnumber"] != DBNull.Value ? reader["accountnumber"].ToString() : "";
        //                obu.mis = reader["mis"] != DBNull.Value ? reader["mis"].ToString() : "";
        //                obu.AccountOfficer_Code = reader["AccountOfficer_Code"] != DBNull.Value ? reader["AccountOfficer_Code"].ToString() : "";
        //                obu.ApprovalStatus = reader["ApprovalStatus"] != DBNull.Value ? reader["ApprovalStatus"].ToString() : "";                      

        //                obuList.Add(obu);
        //            }
        //            con.Close();

        //        }

        //        return request.CreateResponse(HttpStatusCode.OK, obuList);
        //    });
        //}

        //[HttpGet]
        //[Route("incomeaccountMISoverrideusingparamsAWAITING/{search}")]
        //public HttpResponseMessage GetAvailableIncomeAccountMISOverrideUsingParamsAwaiting(HttpRequestMessage request, string search)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        //List<Models.OnBoardingUserModel> obuList = new List<Models.OnBoardingUserModel>();
        //        List<IncomeAccountMISOverrideTEMP> obuList = new List<IncomeAccountMISOverrideTEMP>();

        //        using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
        //        {
        //            var cmd = new System.Data.SqlClient.SqlCommand("", con);

        //            //cmd.CommandText = "select * from Names where Id=@Id";
        //            //cmd.Parameters.AddWithValue("@Id", id);

        //            con.Open();
        //            cmd.CommandText = "select Id, accountnumber, mis, AccountOfficer_Code, ApprovalStatus from Income_accountMIS_Override_TEMP " +
        //            "where ApprovalStatus='AWAITINGAPPROVAL' and (accountnumber like @searchval OR mis like @searchval OR AccountOfficer_Code like @searchval)";
        //            cmd.Parameters.AddWithValue("@searchval", search);
        //            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                var obu = new IncomeAccountMISOverrideTEMP();

        //                obu.Id = reader["Id"] != DBNull.Value ? int.Parse(reader["Id"].ToString()) : 0;

        //                obu.accountnumber = reader["accountnumber"] != DBNull.Value ? reader["accountnumber"].ToString() : "";
        //                obu.mis = reader["mis"] != DBNull.Value ? reader["mis"].ToString() : "";
        //                obu.AccountOfficer_Code = reader["AccountOfficer_Code"] != DBNull.Value ? reader["AccountOfficer_Code"].ToString() : "";
        //                obu.ApprovalStatus = reader["ApprovalStatus"] != DBNull.Value ? reader["ApprovalStatus"].ToString() : "";

        //                obuList.Add(obu);
        //            }
        //            con.Close();

        //        }

        //        return request.CreateResponse(HttpStatusCode.OK, obuList);
        //    });
        //}

        //[HttpGet]
        //[Route("incomeaccountMISoverrideAPPROVED")]
        //public HttpResponseMessage GetAvailableIncomeAccountMISOverrideTEMPApproved(HttpRequestMessage request)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        List<IncomeAccountMISOverrideTEMP> obuList = new List<IncomeAccountMISOverrideTEMP>();

        //        using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
        //        {
        //            var cmd = new System.Data.SqlClient.SqlCommand("", con);

        //            //cmd.CommandText = "select * from Names where Id=@Id";
        //            //cmd.Parameters.AddWithValue("@Id", id);

        //            con.Open();
        //            cmd.CommandText = "select top 500 Id, accountnumber, mis, AccountOfficer_Code, ApprovalStatus from Income_accountMIS_Override_TEMP where ApprovalStatus='APPROVED'";
        //            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                var obu = new IncomeAccountMISOverrideTEMP();

        //                obu.Id = reader["Id"] != DBNull.Value ? int.Parse(reader["Id"].ToString()) : 0;

        //                obu.accountnumber = reader["accountnumber"] != DBNull.Value ? reader["accountnumber"].ToString() : "";
        //                obu.mis = reader["mis"] != DBNull.Value ? reader["mis"].ToString() : "";
        //                obu.AccountOfficer_Code = reader["AccountOfficer_Code"] != DBNull.Value ? reader["AccountOfficer_Code"].ToString() : "";
        //                obu.ApprovalStatus = reader["ApprovalStatus"] != DBNull.Value ? reader["ApprovalStatus"].ToString() : "";

        //                obuList.Add(obu);
        //            }
        //            con.Close();

        //        }

        //        return request.CreateResponse(HttpStatusCode.OK, obuList);
        //    });
        //}

        //[HttpGet]
        //[Route("incomeaccountMISoverrideusingparamsAPPROVED/{search}")]
        //public HttpResponseMessage GetAvailableIncomeAccountMISOverrideUsingParamsApproved(HttpRequestMessage request, string search)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        List<IncomeAccountMISOverrideTEMP> obuList = new List<IncomeAccountMISOverrideTEMP>();

        //        using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
        //        {
        //            var cmd = new System.Data.SqlClient.SqlCommand("", con);

        //            //cmd.CommandText = "select * from Names where Id=@Id";
        //            //cmd.Parameters.AddWithValue("@Id", id);

        //            con.Open();
        //            //cmd.CommandText = "select Id, accountnumber, mis, AccountOfficer_Code, ApprovalStatus from Income_accountMIS_Override_TEMP where ApprovalStatus='APPROVED'";
        //            cmd.CommandText = "select Id, accountnumber, mis, AccountOfficer_Code, ApprovalStatus from Income_accountMIS_Override_TEMP " +
        //           "where ApprovalStatus='APPROVED' and (accountnumber like @searchval OR mis like @searchval OR AccountOfficer_Code like @searchval)";
        //            cmd.Parameters.AddWithValue("@searchval", search);
        //            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                var obu = new IncomeAccountMISOverrideTEMP();

        //                obu.Id = reader["Id"] != DBNull.Value ? int.Parse(reader["Id"].ToString()) : 0;

        //                obu.accountnumber = reader["accountnumber"] != DBNull.Value ? reader["accountnumber"].ToString() : "";
        //                obu.mis = reader["mis"] != DBNull.Value ? reader["mis"].ToString() : "";
        //                obu.AccountOfficer_Code = reader["AccountOfficer_Code"] != DBNull.Value ? reader["AccountOfficer_Code"].ToString() : "";
        //                obu.ApprovalStatus = reader["ApprovalStatus"] != DBNull.Value ? reader["ApprovalStatus"].ToString() : "";

        //                obuList.Add(obu);
        //            }
        //            con.Close();

        //        }

        //        return request.CreateResponse(HttpStatusCode.OK, obuList);
        //    });
        //}



        //[HttpGet]
        //[Route("incomeaccountMISoverrideDECLINED")]
        //public HttpResponseMessage GetAvailableIncomeAccountMISOverrideTEMPDeclined(HttpRequestMessage request)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        List<IncomeAccountMISOverrideTEMP> obuList = new List<IncomeAccountMISOverrideTEMP>();

        //        using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
        //        {
        //            var cmd = new System.Data.SqlClient.SqlCommand("", con);

        //            //cmd.CommandText = "select * from Names where Id=@Id";
        //            //cmd.Parameters.AddWithValue("@Id", id);

        //            con.Open();
        //            cmd.CommandText = "select top 500 Id, accountnumber, mis, AccountOfficer_Code, ApprovalStatus from Income_accountMIS_Override_TEMP where ApprovalStatus='DECLINED'";
        //            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                var obu = new IncomeAccountMISOverrideTEMP();

        //                obu.Id = reader["Id"] != DBNull.Value ? int.Parse(reader["Id"].ToString()) : 0;

        //                obu.accountnumber = reader["accountnumber"] != DBNull.Value ? reader["accountnumber"].ToString() : "";
        //                obu.mis = reader["mis"] != DBNull.Value ? reader["mis"].ToString() : "";
        //                obu.AccountOfficer_Code = reader["AccountOfficer_Code"] != DBNull.Value ? reader["AccountOfficer_Code"].ToString() : "";
        //                obu.ApprovalStatus = reader["ApprovalStatus"] != DBNull.Value ? reader["ApprovalStatus"].ToString() : "";

        //                obuList.Add(obu);
        //            }
        //            con.Close();

        //        }

        //        return request.CreateResponse(HttpStatusCode.OK, obuList);
        //    });
        //}

        //[HttpGet]
        //[Route("incomeaccountMISoverrideusingparamsDECLINED/{search}")]
        //public HttpResponseMessage GetAvailableIncomeAccountMISOverrideUsingParamsDeclined(HttpRequestMessage request, string search)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        List<IncomeAccountMISOverrideTEMP> obuList = new List<IncomeAccountMISOverrideTEMP>();

        //        using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
        //        {
        //            var cmd = new System.Data.SqlClient.SqlCommand("", con);

        //            //cmd.CommandText = "select * from Names where Id=@Id";
        //            //cmd.Parameters.AddWithValue("@Id", id);

        //            con.Open();
        //            cmd.CommandText = "select Id, accountnumber, mis, AccountOfficer_Code, ApprovalStatus from Income_accountMIS_Override_TEMP " +
        //           "where ApprovalStatus='DECLINED' and (accountnumber like @searchval OR mis like @searchval OR AccountOfficer_Code like @searchval%)";
        //            cmd.Parameters.AddWithValue("@searchval", search);
        //            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                var obu = new IncomeAccountMISOverrideTEMP();

        //                obu.Id = reader["Id"] != DBNull.Value ? int.Parse(reader["Id"].ToString()) : 0;

        //                obu.accountnumber = reader["accountnumber"] != DBNull.Value ? reader["accountnumber"].ToString() : "";
        //                obu.mis = reader["mis"] != DBNull.Value ? reader["mis"].ToString() : "";
        //                obu.AccountOfficer_Code = reader["AccountOfficer_Code"] != DBNull.Value ? reader["AccountOfficer_Code"].ToString() : "";
        //                obu.ApprovalStatus = reader["ApprovalStatus"] != DBNull.Value ? reader["ApprovalStatus"].ToString() : "";

        //                obuList.Add(obu);
        //            }
        //            con.Close();

        //        }

        //        return request.CreateResponse(HttpStatusCode.OK, obuList);
        //    });
        //}


        //[HttpPost]
        //[Route("incomeaccountMISoverrideapproval/{selectedIds}")]
        ////public HttpResponseMessage EditOnboardingUsersApprovalAndDecline(HttpRequestMessage request, [FromBody]int[] model)
        //public HttpResponseMessage EditIncomeAccountMISOverrideApproval(HttpRequestMessage request, string selectedIds)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage res = null;

        //        using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
        //        {                    
        //            using (var cmd = new System.Data.SqlClient.SqlCommand("", con))
        //            {
        //                var userInput = selectedIds;
        //                var values = userInput.Split(',');

        //                con.Open();
        //                var sql = "update Income_accountMIS_Override_TEMP set ApprovalStatus='APPROVED' where id IN(";
        //                for (int i = 0; i < values.Length; i++)
        //                {
        //                    sql = $"{sql} @{i},";
        //                    cmd.Parameters.Add($"@{i}", System.Data.Odbc.OdbcType.Int).Value = values[i];
        //                }
        //                cmd.CommandText = sql.TrimEnd(',') + ");";
        //                cmd.ExecuteNonQuery();
        //                con.Close();
        //            }
        //        }

        //        return request.CreateResponse(HttpStatusCode.OK, res);
        //    });
        //}

        //[HttpPost]
        //[Route("incomeaccountMISoverridedecline/{selectedIds}")]
        ////public HttpResponseMessage EditOnboardingUsersApprovalAndDecline(HttpRequestMessage request, [FromBody]int[] model)
        //public HttpResponseMessage EditIncomeAccountMISOverrideDecline(HttpRequestMessage request, string selectedIds)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage res = null;

        //        using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
        //        {
        //            using (var cmd = new System.Data.SqlClient.SqlCommand("", con))
        //            {
        //                var userInput = selectedIds;
        //                var values = userInput.Split(',');

        //                con.Open();
        //                var sql = "update Income_accountMIS_Override_TEMP set ApprovalStatus='DECLINED' where id IN(";
        //                for (int i = 0; i < values.Length; i++)
        //                {
        //                    sql = $"{sql} @{i},";
        //                    cmd.Parameters.Add($"@{i}", System.Data.Odbc.OdbcType.Int).Value = values[i];
        //                }
        //                cmd.CommandText = sql.TrimEnd(',') + ");";
        //                cmd.ExecuteNonQuery();
        //                con.Close();
        //            }
        //        }

        //        return request.CreateResponse(HttpStatusCode.OK, res);
        //    });
        //}


    }
}
