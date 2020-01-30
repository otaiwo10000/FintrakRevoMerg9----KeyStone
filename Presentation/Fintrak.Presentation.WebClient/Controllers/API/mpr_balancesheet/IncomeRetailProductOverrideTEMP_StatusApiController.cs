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
    [RoutePrefix("api/incomeretailproductoverrideTEMPstatus")]
    [UsesDisposableService]
    public class IncomeRetailProductOverrideTEMPStatusApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomeRetailProductOverrideTEMPStatusApiController(IMPRBSService mprBSService)
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
        [Route("incomeretailproductoverrideAWAITING")]
        public HttpResponseMessage GetAvailableIncomeRetailProductOverrideAwaiting(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "AWAITINGAPPROVAL";
                IncomeRetailProductOverrideTEMPStatus obj = new IncomeRetailProductOverrideTEMPStatus();
                var ddb = obj.IncomeRetailProductOverrideTEMP(approvalstatus);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        [HttpGet]
        [Route("incomeretailproductoverrideusingparamsAWAITING/{search}")]
        public HttpResponseMessage GetAvailableIncomeRetailProductOverrideUsingParamsAwaiting(HttpRequestMessage request, string search)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "AWAITINGAPPROVAL";
                IncomeRetailProductOverrideTEMPStatus obj = new IncomeRetailProductOverrideTEMPStatus();
                var ddb = obj.IncomeRetailProductOverrideTEMPusingparams(approvalstatus, search);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        //======================== awaiting ends ================================================================================

        //======================== approved starts ================================================================================

        [HttpGet]
        [Route("incomeretailproductoverrideAPPROVED")]
        public HttpResponseMessage IncomeAccountMISOverrideApproved(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "APPROVED";
                IncomeRetailProductOverrideTEMPStatus obj = new IncomeRetailProductOverrideTEMPStatus();
                var ddb = obj.IncomeRetailProductOverrideTEMP(approvalstatus);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        [HttpGet]
        [Route("incomeretailproductoverrideusingparamsAPPROVED/{search}")]
        public HttpResponseMessage TeamUsingParamsApproved(HttpRequestMessage request, string search)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "APPROVED";
                IncomeRetailProductOverrideTEMPStatus obj = new IncomeRetailProductOverrideTEMPStatus();
                var ddb = obj.IncomeRetailProductOverrideTEMPusingparams(approvalstatus, search);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        //======================== approved ends ================================================================================

        //======================== declined starts ================================================================================

        [HttpGet]
        [Route("incomeretailproductoverrideDECLINED")]
        public HttpResponseMessage IncomeAccountMISOverrideDECLINED(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "DECLINED";
                IncomeRetailProductOverrideTEMPStatus obj = new IncomeRetailProductOverrideTEMPStatus();
                var ddb = obj.IncomeRetailProductOverrideTEMP(approvalstatus);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        [HttpGet]
        [Route("incomeretailproductoverrideusingparamsDECLINED/{search}")]
        public HttpResponseMessage IncomeOtherBreakdownTEMPusingparamsDECLINED(HttpRequestMessage request, string search)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "DECLINED";
                IncomeRetailProductOverrideTEMPStatus obj = new IncomeRetailProductOverrideTEMPStatus();
                var ddb = obj.IncomeRetailProductOverrideTEMPusingparams(approvalstatus, search);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        //======================== declined ends ================================================================================

        [HttpPost]
        //[Route("incomeretailproductoverrideapproval/{selectedIds}")]
        [Route("incomeretailproductoverrideapproval")]
        public HttpResponseMessage EditIncomeAccountMISOverrideApproval(HttpRequestMessage request, [FromBody]string[] selectedIds2)
        {
            return GetHttpResponse(request, () =>
            {
                //IncomeRetailProductOverrideTEMPStatus Obj = new IncomeRetailProductOverrideTEMPStatus();
                //string selectedIds = String.Join(",", selectedIds2);
                //Obj.EditIncomeRetailProductOverrideApproval(selectedIds);

                IncomeRetailProductOverrideTEMPStatus Obj = new IncomeRetailProductOverrideTEMPStatus();
                foreach (var batch in selectedIds2.Batch(2000))
                {
                    string selectedIds = String.Join(",", batch);
                    Obj.EditIncomeRetailProductOverrideApproval(selectedIds);
                }

                return request.CreateResponse(HttpStatusCode.OK);
            });
        }

        [HttpPost]
        //[Route("incomeretailproductoverridedecline/{selectedIds}")]
        [Route("incomeretailproductoverridedecline")]
        public HttpResponseMessage EditIncomeAccountMISOverrideDecline(HttpRequestMessage request, [FromBody]string[] selectedIds2)
        {
            return GetHttpResponse(request, () =>
            {
                //IncomeRetailProductOverrideTEMPStatus tObj = new IncomeRetailProductOverrideTEMPStatus();
                //string selectedIds = String.Join(",", selectedIds2);
                //tObj.EditIncomeRetailProductOverrideDecline(selectedIds);

                IncomeRetailProductOverrideTEMPStatus Obj = new IncomeRetailProductOverrideTEMPStatus();
                foreach (var batch in selectedIds2.Batch(2000))
                {
                    string selectedIds = String.Join(",", batch);
                    Obj.EditIncomeRetailProductOverrideDecline(selectedIds);
                }

                return request.CreateResponse(HttpStatusCode.OK);
            });
        }






        //[HttpGet]
        //[Route("incomeretailproductoverrideAWAITING")]
        //public HttpResponseMessage GetAvailableIncomeRetailProductOverrideAwaiting(HttpRequestMessage request)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        //List<Models.OnBoardingUserModel> obuList = new List<Models.OnBoardingUserModel>();
        //        List<IncomeRetailProductOverrideTEMP> obuList = new List<IncomeRetailProductOverrideTEMP>();
                
        //        using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
        //        {
        //            var cmd = new System.Data.SqlClient.SqlCommand("", con);

        //            //cmd.CommandText = "select * from Names where Id=@Id";
        //            //cmd.Parameters.AddWithValue("@Id", id);

        //            con.Open();
        //            cmd.CommandText = "select top 500 Id, Customerid, Bank, Mis_code, AccountOfficer_Code, ApprovalStatus from Income_RetailProduct_Override_TEMP where ApprovalStatus='AWAITINGAPPROVAL'";
        //            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                var obu = new IncomeRetailProductOverrideTEMP();

        //                obu.Id = reader["Id"] != DBNull.Value ? int.Parse(reader["Id"].ToString()) : 0;

        //                obu.Customerid = reader["Customerid"] != DBNull.Value ? int.Parse(reader["Customerid"].ToString()) : 0;
        //                obu.Bank = reader["Bank"] != DBNull.Value ? reader["Bank"].ToString() : "";
        //                obu.Mis_code = reader["Mis_code"] != DBNull.Value ? reader["Mis_code"].ToString() : "";
        //                obu.AccountOfficer_Code = reader["AccountOfficer_Code"] != DBNull.Value ? reader["AccountOfficer_Code"].ToString() : "";
        //                obu.ApprovalStatus = reader["ApprovalStatus"] != DBNull.Value ? reader["ApprovalStatus"].ToString() : "";                      

        //                obuList.Add(obu);
        //            }
        //            con.Close();

        //        }
        //        //Staff[] staffs = _CoreService.GetAllStaffs();

        //        return request.CreateResponse(HttpStatusCode.OK, obuList);
        //    });
        //}

        //[HttpGet]
        //[Route("incomeretailproductoverrideusingparamsAWAITING/{search}")]
        //public HttpResponseMessage GetAvailableIncomeRetailProductOverrideUsingParamsAwaiting(HttpRequestMessage request, string search)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        //List<Models.OnBoardingUserModel> obuList = new List<Models.OnBoardingUserModel>();
        //        List<IncomeRetailProductOverrideTEMP> obuList = new List<IncomeRetailProductOverrideTEMP>();

        //        using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
        //        {
        //            var cmd = new System.Data.SqlClient.SqlCommand("", con);

        //            //cmd.CommandText = "select * from Names where Id=@Id";
        //            //cmd.Parameters.AddWithValue("@Id", id);

        //            con.Open();
        //            //cmd.CommandText = "select Id, Customerid, Bank, Mis_code, AccountOfficer_Code, ApprovalStatus from Income_RetailProduct_Override_TEMP where ApprovalStatus='AWAITINGAPPROVAL'";
        //            cmd.CommandText = "select Id, Customerid, Bank, Mis_code, AccountOfficer_Code, ApprovalStatus from Income_RetailProduct_Override_TEMP " +
        //            "where ApprovalStatus='AWAITINGAPPROVAL' and (Customerid like @searchval OR Mis_code like @searchval OR AccountOfficer_Code like @searchval)";
        //            cmd.Parameters.AddWithValue("@searchval", search);
        //            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                var obu = new IncomeRetailProductOverrideTEMP();

        //                obu.Id = reader["Id"] != DBNull.Value ? int.Parse(reader["Id"].ToString()) : 0;

        //                obu.Customerid = reader["Customerid"] != DBNull.Value ? int.Parse(reader["Customerid"].ToString()) : 0;
        //                obu.Bank = reader["Bank"] != DBNull.Value ? reader["Bank"].ToString() : "";
        //                obu.Mis_code = reader["Mis_code"] != DBNull.Value ? reader["Mis_code"].ToString() : "";
        //                obu.AccountOfficer_Code = reader["AccountOfficer_Code"] != DBNull.Value ? reader["AccountOfficer_Code"].ToString() : "";
        //                obu.ApprovalStatus = reader["ApprovalStatus"] != DBNull.Value ? reader["ApprovalStatus"].ToString() : "";

        //                obuList.Add(obu);
        //            }
        //            con.Close();

        //        }
        //        //Staff[] staffs = _CoreService.GetAllStaffs();

        //        return request.CreateResponse(HttpStatusCode.OK, obuList);
        //    });
        //}


        //[HttpGet]
        //[Route("incomeretailproductoverrideAPPROVED")]
        //public HttpResponseMessage GetAvailableIncomeRetailProductOverrideApproved(HttpRequestMessage request)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        //List<Models.OnBoardingUserModel> obuList = new List<Models.OnBoardingUserModel>();
        //        List<IncomeRetailProductOverrideTEMP> obuList = new List<IncomeRetailProductOverrideTEMP>();

        //        using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
        //        {
        //            var cmd = new System.Data.SqlClient.SqlCommand("", con);

        //            //cmd.CommandText = "select * from Names where Id=@Id";
        //            //cmd.Parameters.AddWithValue("@Id", id);

        //            con.Open();
        //            cmd.CommandText = "select top 500 Id, Customerid, Bank, Mis_code, AccountOfficer_Code, ApprovalStatus from Income_RetailProduct_Override_TEMP where ApprovalStatus='APPROVED'";
        //            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                var obu = new IncomeRetailProductOverrideTEMP();

        //                obu.Id = reader["Id"] != DBNull.Value ? int.Parse(reader["Id"].ToString()) : 0;

        //                obu.Customerid = reader["Customerid"] != DBNull.Value ? int.Parse(reader["Customerid"].ToString()) : 0;
        //                obu.Bank = reader["Bank"] != DBNull.Value ? reader["Bank"].ToString() : "";
        //                obu.Mis_code = reader["Mis_code"] != DBNull.Value ? reader["Mis_code"].ToString() : "";
        //                obu.AccountOfficer_Code = reader["AccountOfficer_Code"] != DBNull.Value ? reader["AccountOfficer_Code"].ToString() : "";
        //                obu.ApprovalStatus = reader["ApprovalStatus"] != DBNull.Value ? reader["ApprovalStatus"].ToString() : "";

        //                obuList.Add(obu);
        //            }
        //            con.Close();

        //        }
        //        //Staff[] staffs = _CoreService.GetAllStaffs();

        //        return request.CreateResponse(HttpStatusCode.OK, obuList);
        //    });
        //}

        //[HttpGet]
        //[Route("incomeretailproductoverrideusingparamsAPPROVED/{search}")]
        //public HttpResponseMessage GetAvailableIncomeRetailProductOverrideUsingParamApproved(HttpRequestMessage request, string search)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        //List<Models.OnBoardingUserModel> obuList = new List<Models.OnBoardingUserModel>();
        //        List<IncomeRetailProductOverrideTEMP> obuList = new List<IncomeRetailProductOverrideTEMP>();

        //        using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
        //        {
        //            var cmd = new System.Data.SqlClient.SqlCommand("", con);

        //            //cmd.CommandText = "select * from Names where Id=@Id";
        //            //cmd.Parameters.AddWithValue("@Id", id);

        //            con.Open();
        //            //cmd.CommandText = "select Id, Customerid, Bank, Mis_code, AccountOfficer_Code, ApprovalStatus from Income_RetailProduct_Override_TEMP where ApprovalStatus='APPROVED'";
        //            cmd.CommandText = "select Id, Customerid, Bank, Mis_code, AccountOfficer_Code, ApprovalStatus from Income_RetailProduct_Override_TEMP " +
        //          "where ApprovalStatus='APPROVED' and (Customerid like @searchval OR Mis_code like @searchval OR AccountOfficer_Code like @searchval)";
        //            cmd.Parameters.AddWithValue("@searchval", search);
        //            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                var obu = new IncomeRetailProductOverrideTEMP();

        //                obu.Id = reader["Id"] != DBNull.Value ? int.Parse(reader["Id"].ToString()) : 0;

        //                obu.Customerid = reader["Customerid"] != DBNull.Value ? int.Parse(reader["Customerid"].ToString()) : 0;
        //                obu.Bank = reader["Bank"] != DBNull.Value ? reader["Bank"].ToString() : "";
        //                obu.Mis_code = reader["Mis_code"] != DBNull.Value ? reader["Mis_code"].ToString() : "";
        //                obu.AccountOfficer_Code = reader["AccountOfficer_Code"] != DBNull.Value ? reader["AccountOfficer_Code"].ToString() : "";
        //                obu.ApprovalStatus = reader["ApprovalStatus"] != DBNull.Value ? reader["ApprovalStatus"].ToString() : "";

        //                obuList.Add(obu);
        //            }
        //            con.Close();

        //        }
        //        //Staff[] staffs = _CoreService.GetAllStaffs();

        //        return request.CreateResponse(HttpStatusCode.OK, obuList);
        //    });
        //}

        //[HttpGet]
        //[Route("incomeretailproductoverrideDECLINED")]
        //public HttpResponseMessage GetAvailableIncomeRetailProductOverrideDeclined(HttpRequestMessage request)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        //List<Models.OnBoardingUserModel> obuList = new List<Models.OnBoardingUserModel>();
        //        List<IncomeRetailProductOverrideTEMP> obuList = new List<IncomeRetailProductOverrideTEMP>();

        //        using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
        //        {
        //            var cmd = new System.Data.SqlClient.SqlCommand("", con);

        //            //cmd.CommandText = "select * from Names where Id=@Id";
        //            //cmd.Parameters.AddWithValue("@Id", id);

        //            con.Open();
        //            cmd.CommandText = "select top 500 Id, Customerid, Bank, Mis_code, AccountOfficer_Code, ApprovalStatus from Income_RetailProduct_Override_TEMP where ApprovalStatus='DECLINED'";
        //            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                var obu = new IncomeRetailProductOverrideTEMP();

        //                obu.Id = reader["Id"] != DBNull.Value ? int.Parse(reader["Id"].ToString()) : 0;

        //                obu.Customerid = reader["Customerid"] != DBNull.Value ? int.Parse(reader["Customerid"].ToString()) : 0;
        //                obu.Bank = reader["Bank"] != DBNull.Value ? reader["Bank"].ToString() : "";
        //                obu.Mis_code = reader["Mis_code"] != DBNull.Value ? reader["Mis_code"].ToString() : "";
        //                obu.AccountOfficer_Code = reader["AccountOfficer_Code"] != DBNull.Value ? reader["AccountOfficer_Code"].ToString() : "";
        //                obu.ApprovalStatus = reader["ApprovalStatus"] != DBNull.Value ? reader["ApprovalStatus"].ToString() : "";

        //                obuList.Add(obu);
        //            }
        //            con.Close();

        //        }
        //        //Staff[] staffs = _CoreService.GetAllStaffs();

        //        return request.CreateResponse(HttpStatusCode.OK, obuList);
        //    });
        //}

        //[HttpGet]
        //[Route("incomeretailproductoverrideusingparamSDECLINED/{search}")]
        //public HttpResponseMessage GetAvailableIncomeRetailProductOverrideDeclined(HttpRequestMessage request, string search)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        //List<Models.OnBoardingUserModel> obuList = new List<Models.OnBoardingUserModel>();
        //        List<IncomeRetailProductOverrideTEMP> obuList = new List<IncomeRetailProductOverrideTEMP>();

        //        using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
        //        {
        //            var cmd = new System.Data.SqlClient.SqlCommand("", con);

        //            //cmd.CommandText = "select * from Names where Id=@Id";
        //            //cmd.Parameters.AddWithValue("@Id", id);

        //            con.Open();
        //            //cmd.CommandText = "select Id, Customerid, Bank, Mis_code, AccountOfficer_Code, ApprovalStatus from Income_RetailProduct_Override_TEMP where ApprovalStatus='DECLINED'";
        //            cmd.CommandText = "select Id, Customerid, Bank, Mis_code, AccountOfficer_Code, ApprovalStatus from Income_RetailProduct_Override_TEMP " +
        //         "where ApprovalStatus='DECLINED' and (Customerid like @searchval OR Mis_code like @searchval OR AccountOfficer_Code like @searchval)";
        //            cmd.Parameters.AddWithValue("@searchval", search);
        //            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                var obu = new IncomeRetailProductOverrideTEMP();

        //                obu.Id = reader["Id"] != DBNull.Value ? int.Parse(reader["Id"].ToString()) : 0;

        //                obu.Customerid = reader["Customerid"] != DBNull.Value ? int.Parse(reader["Customerid"].ToString()) : 0;
        //                obu.Bank = reader["Bank"] != DBNull.Value ? reader["Bank"].ToString() : "";
        //                obu.Mis_code = reader["Mis_code"] != DBNull.Value ? reader["Mis_code"].ToString() : "";
        //                obu.AccountOfficer_Code = reader["AccountOfficer_Code"] != DBNull.Value ? reader["AccountOfficer_Code"].ToString() : "";
        //                obu.ApprovalStatus = reader["ApprovalStatus"] != DBNull.Value ? reader["ApprovalStatus"].ToString() : "";

        //                obuList.Add(obu);
        //            }
        //            con.Close();

        //        }
        //        //Staff[] staffs = _CoreService.GetAllStaffs();

        //        return request.CreateResponse(HttpStatusCode.OK, obuList);
        //    });
        //}

        //[HttpPost]
        //[Route("incomeretailproductoverrideapproval/{selectedIds}")]
        ////public HttpResponseMessage EditOnboardingUsersApprovalAndDecline(HttpRequestMessage request, [FromBody]int[] model)
        //public HttpResponseMessage EditOnboardingUsersApproval(HttpRequestMessage request, string selectedIds)
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
        //                var sql = "update Income_RetailProduct_Override_TEMP set ApprovalStatus='APPROVED' where id IN(";
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
        //[Route("incomeretailproductoverridedecline/{selectedIds}")]
        ////public HttpResponseMessage EditOnboardingUsersApprovalAndDecline(HttpRequestMessage request, [FromBody]int[] model)
        //public HttpResponseMessage EditOnboardingUsersDecline(HttpRequestMessage request, string selectedIds)
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
        //                var sql = "update Income_RetailProduct_Override_TEMP set ApprovalStatus='DECLINED' where id IN(";
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
