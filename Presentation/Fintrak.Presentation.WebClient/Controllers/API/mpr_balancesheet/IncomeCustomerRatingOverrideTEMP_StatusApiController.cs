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
    [RoutePrefix("api/incomecustomerratingoverrideTEMPstatus")]
    [UsesDisposableService]
    public class IncomeCustomerRatingOverrideTEMPStatusApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomeCustomerRatingOverrideTEMPStatusApiController(IMPRBSService mprBSService)
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
        [Route("incomecustomerratingoverrideAWAITING")]
        public HttpResponseMessage GetAvailableIncomeRetailProductOverrideAwaiting(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "AWAITINGAPPROVAL";
                IncomeCustomerRatingOverrideTEMPStatus obj = new IncomeCustomerRatingOverrideTEMPStatus();
                var ddb = obj.IncomeCustomerRatingOverrideTEMP(approvalstatus);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        [HttpGet]
        [Route("incomecustomerratingoverrideusingparamsAWAITING/{search}")]
        public HttpResponseMessage GetAvailableIncomeRetailProductOverrideUsingParamsAwaiting(HttpRequestMessage request, string search)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "AWAITINGAPPROVAL";
                IncomeCustomerRatingOverrideTEMPStatus obj = new IncomeCustomerRatingOverrideTEMPStatus();
                var ddb = obj.IncomeCustomerRatingOverrideTEMPusingparams(approvalstatus, search);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        //======================== awaiting ends ================================================================================

        //======================== approved starts ================================================================================

        [HttpGet]
        [Route("incomecustomerratingoverrideAPPROVED")]
        public HttpResponseMessage IncomeAccountMISOverrideApproved(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "APPROVED";
                IncomeCustomerRatingOverrideTEMPStatus obj = new IncomeCustomerRatingOverrideTEMPStatus();
                var ddb = obj.IncomeCustomerRatingOverrideTEMP(approvalstatus);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        [HttpGet]
        [Route("incomecustomerratingoverrideusingparamsAPPROVED/{search}")]
        public HttpResponseMessage TeamUsingParamsApproved(HttpRequestMessage request, string search)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "APPROVED";
                IncomeCustomerRatingOverrideTEMPStatus obj = new IncomeCustomerRatingOverrideTEMPStatus();
                var ddb = obj.IncomeCustomerRatingOverrideTEMPusingparams(approvalstatus, search);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        //======================== approved ends ================================================================================

        //======================== declined starts ================================================================================

        [HttpGet]
        [Route("incomecustomerratingoverrideDECLINED")]
        public HttpResponseMessage IncomeAccountMISOverrideDECLINED(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "DECLINED";
                IncomeCustomerRatingOverrideTEMPStatus obj = new IncomeCustomerRatingOverrideTEMPStatus();
                var ddb = obj.IncomeCustomerRatingOverrideTEMP(approvalstatus);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        [HttpGet]
        [Route("incomecustomerratingoverrideusingparamsDECLINED/{search}")]
        public HttpResponseMessage IncomeOtherBreakdownTEMPusingparamsDECLINED(HttpRequestMessage request, string search)
        {
            return GetHttpResponse(request, () =>
            {
                string approvalstatus = "DECLINED";
                IncomeCustomerRatingOverrideTEMPStatus obj = new IncomeCustomerRatingOverrideTEMPStatus();
                var ddb = obj.IncomeCustomerRatingOverrideTEMPusingparams(approvalstatus, search);

                return request.CreateResponse(HttpStatusCode.OK, ddb);
            });
        }

        //======================== declined ends ================================================================================

        [HttpPost]
        //[Route("incomecustomerratingoverrideapproval/{selectedIds}")]
        [Route("incomecustomerratingoverrideapproval")]
        public HttpResponseMessage EditIncomeAccountMISOverrideApproval(HttpRequestMessage request, [FromBody]string[] selectedIds2)
        {
            return GetHttpResponse(request, () =>
            {
                //IncomeCustomerRatingOverrideTEMPStatus Obj = new IncomeCustomerRatingOverrideTEMPStatus();
                //string selectedIds = String.Join(",", selectedIds2);
                //Obj.EditIncomeCustomerRatingOverrideTEMPApproval(selectedIds);

                IncomeCustomerRatingOverrideTEMPStatus Obj = new IncomeCustomerRatingOverrideTEMPStatus();
                foreach (var batch in selectedIds2.Batch(2000))
                {
                    string selectedIds = String.Join(",", batch);
                    Obj.EditIncomeCustomerRatingOverrideTEMPApproval(selectedIds);
                }

                return request.CreateResponse(HttpStatusCode.OK);
            });
        }

        [HttpPost]
        //[Route("incomecustomerratingoverridedecline/{selectedIds}")]
        [Route("incomecustomerratingoverridedecline")]
        public HttpResponseMessage EditIncomeAccountMISOverrideDecline(HttpRequestMessage request, [FromBody]string[] selectedIds2)
        {
            return GetHttpResponse(request, () =>
            {
                //IncomeCustomerRatingOverrideTEMPStatus tObj = new IncomeCustomerRatingOverrideTEMPStatus();
                //string selectedIds = String.Join(",", selectedIds2);
                //tObj.EditIncomeCustomerRatingOverrideTEMPDecline(selectedIds);

                IncomeCustomerRatingOverrideTEMPStatus Obj = new IncomeCustomerRatingOverrideTEMPStatus();
                foreach (var batch in selectedIds2.Batch(2000))
                {
                    string selectedIds = String.Join(",", batch);
                    Obj.EditIncomeCustomerRatingOverrideTEMPDecline(selectedIds);
                }

                return request.CreateResponse(HttpStatusCode.OK);
            });
        }


        //[HttpGet]
        //[Route("incomecustomerratingoverrideAWAITING")]
        //public HttpResponseMessage GetAvailableIncomeCustomerRatingOverrideAwaiting(HttpRequestMessage request)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        List<IncomeCustomerRatingOverrideTEMP> obuList = new List<IncomeCustomerRatingOverrideTEMP>();

        //        using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
        //        {
        //            var cmd = new System.Data.SqlClient.SqlCommand("", con);

        //            //cmd.CommandText = "select * from Names where Id=@Id";
        //            //cmd.Parameters.AddWithValue("@Id", id);

        //            con.Open();
        //            cmd.CommandText = "select top 500 Id, Cust_ID, Ref_No, Settlement_Account, Customer_Name, Limit, " +
        //            "PrincipalOutstandingBal, Value_Date, Maturity_Date, Risk_Rating, ApprovalStatus " +
        //            "from Income_CustomerRating_Override_TEMP where ApprovalStatus='AWAITINGAPPROVAL'";

        //            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                var obu = new IncomeCustomerRatingOverrideTEMP();

        //                obu.Id = reader["Id"] != DBNull.Value ? int.Parse(reader["Id"].ToString()) : 0;

        //                obu.Cust_ID = reader["Cust_ID"] != DBNull.Value ? reader["Cust_ID"].ToString() : "";
        //                obu.Ref_No = reader["Ref_No"] != DBNull.Value ? reader["Ref_No"].ToString() : "";
        //                obu.Settlement_Account = reader["Settlement_Account"] != DBNull.Value ? reader["Settlement_Account"].ToString() : "";
        //                obu.Customer_Name = reader["Customer_Name"] != DBNull.Value ? reader["Customer_Name"].ToString() : "";
        //                obu.Limit = reader["Limit"] != DBNull.Value ? Convert.ToDecimal(reader["Limit"].ToString()) : 0;

        //                obu.PrincipalOutstandingBal = reader["PrincipalOutstandingBal"] != DBNull.Value ? Convert.ToDecimal(reader["PrincipalOutstandingBal"].ToString()) : 0;
        //                //ddb.P_Date = reader["P_Date"] != DBNull.Value ? DateTime.Parse(reader["P_Date"].ToString()) : DateTime.Parse("1000-01-01");
        //                //obu.Value_Date = reader["Value_Date"] != DBNull.Value ? DateTime.Parse(reader["Value_Date"].ToString()) : DateTime.Now;
        //                obu.Value_Date = reader["Value_Date"] != DBNull.Value ? DateTime.Parse(reader["Value_Date"].ToString()) : DateTime.Parse("1000-01-01");
        //                obu.Maturity_Date = reader["Maturity_Date"] != DBNull.Value ? DateTime.Parse(reader["Maturity_Date"].ToString()) : DateTime.Parse("1000-01-01");
        //                obu.Risk_Rating = reader["Risk_Rating"] != DBNull.Value ? reader["Risk_Rating"].ToString() : "";
        //                obu.ApprovalStatus = reader["ApprovalStatus"] != DBNull.Value ? reader["ApprovalStatus"].ToString() : "";

        //                obuList.Add(obu);
        //            }
        //            con.Close();
        //        }

        //        return request.CreateResponse(HttpStatusCode.OK, obuList);
        //    });
        //}

        //[HttpGet]
        //[Route("incomecustomerratingoverrideusingparamsAWAITING/{search}")]
        //public HttpResponseMessage GetAvailableIncomeCustomerRatingOverrideUsingParamsAwaiting(HttpRequestMessage request, string search)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        List<IncomeCustomerRatingOverrideTEMP> obuList = new List<IncomeCustomerRatingOverrideTEMP>();

        //        using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
        //        {
        //            var cmd = new System.Data.SqlClient.SqlCommand("", con);

        //            //cmd.CommandText = "select * from Names where Id=@Id";
        //            //cmd.Parameters.AddWithValue("@Id", id);

        //            con.Open();                   
        //            cmd.CommandText = "select top 500 Id, Cust_ID, Ref_No, Settlement_Account, Customer_Name, Limit, " +
        //          "PrincipalOutstandingBal, Value_Date, Maturity_Date, Risk_Rating, ApprovalStatus " +
        //          "from Income_CustomerRating_Override_TEMP where ApprovalStatus='AWAITINGAPPROVAL' and " +
        //          "(Ref_No like @searchval OR Customer_Name like @searchval)";
        //            cmd.Parameters.AddWithValue("@searchval", search);
        //            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                var obu = new IncomeCustomerRatingOverrideTEMP();

        //                obu.Id = reader["Id"] != DBNull.Value ? int.Parse(reader["Id"].ToString()) : 0;

        //                obu.Cust_ID = reader["Cust_ID"] != DBNull.Value ? reader["Cust_ID"].ToString() : "";
        //                obu.Ref_No = reader["Ref_No"] != DBNull.Value ? reader["Ref_No"].ToString() : "";
        //                obu.Settlement_Account = reader["Settlement_Account"] != DBNull.Value ? reader["Settlement_Account"].ToString() : "";
        //                obu.Customer_Name = reader["Customer_Name"] != DBNull.Value ? reader["Customer_Name"].ToString() : "";
        //                obu.Limit = reader["Limit"] != DBNull.Value ? Convert.ToDecimal(reader["Limit"].ToString()) : 0;

        //                obu.PrincipalOutstandingBal = reader["PrincipalOutstandingBal"] != DBNull.Value ? Convert.ToDecimal(reader["PrincipalOutstandingBal"].ToString()) : 0;
        //                //ddb.P_Date = reader["P_Date"] != DBNull.Value ? DateTime.Parse(reader["P_Date"].ToString()) : DateTime.Parse("1000-01-01");
        //                //obu.Value_Date = reader["Value_Date"] != DBNull.Value ? DateTime.Parse(reader["Value_Date"].ToString()) : DateTime.Now;
        //                obu.Value_Date = reader["Value_Date"] != DBNull.Value ? DateTime.Parse(reader["Value_Date"].ToString()) : DateTime.Parse("1000-01-01");
        //                obu.Maturity_Date = reader["Maturity_Date"] != DBNull.Value ? DateTime.Parse(reader["Maturity_Date"].ToString()) : DateTime.Parse("1000-01-01");
        //                obu.Risk_Rating = reader["Risk_Rating"] != DBNull.Value ? reader["Risk_Rating"].ToString() : "";
        //                obu.ApprovalStatus = reader["ApprovalStatus"] != DBNull.Value ? reader["ApprovalStatus"].ToString() : "";

        //                obuList.Add(obu);
        //            }
        //            con.Close();

        //        }

        //        return request.CreateResponse(HttpStatusCode.OK, obuList);
        //    });
        //}

        //[HttpGet]
        //[Route("incomecustomerratingoverrideAPPROVED")]
        //public HttpResponseMessage GetAvailableIncomeAccountMISOverrideTEMPApproved(HttpRequestMessage request)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        List<IncomeCustomerRatingOverrideTEMP> obuList = new List<IncomeCustomerRatingOverrideTEMP>();

        //        using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
        //        {
        //            var cmd = new System.Data.SqlClient.SqlCommand("", con);

        //            //cmd.CommandText = "select * from Names where Id=@Id";
        //            //cmd.Parameters.AddWithValue("@Id", id);

        //            con.Open();
        //            cmd.CommandText = "select top 500 Id, Cust_ID, Ref_No, Settlement_Account, Customer_Name, Limit, " +
        //           "PrincipalOutstandingBal, Value_Date, Maturity_Date, Risk_Rating, ApprovalStatus " +
        //           "from Income_CustomerRating_Override_TEMP where ApprovalStatus='APPROVED'";

        //            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                var obu = new IncomeCustomerRatingOverrideTEMP();

        //                obu.Id = reader["Id"] != DBNull.Value ? int.Parse(reader["Id"].ToString()) : 0;

        //                obu.Cust_ID = reader["Cust_ID"] != DBNull.Value ? reader["Cust_ID"].ToString() : "";
        //                obu.Ref_No = reader["Ref_No"] != DBNull.Value ? reader["Ref_No"].ToString() : "";
        //                obu.Settlement_Account = reader["Settlement_Account"] != DBNull.Value ? reader["Settlement_Account"].ToString() : "";
        //                obu.Customer_Name = reader["Customer_Name"] != DBNull.Value ? reader["Customer_Name"].ToString() : "";
        //                obu.Limit = reader["Limit"] != DBNull.Value ? Convert.ToDecimal(reader["Limit"].ToString()) : 0;

        //                obu.PrincipalOutstandingBal = reader["PrincipalOutstandingBal"] != DBNull.Value ? Convert.ToDecimal(reader["PrincipalOutstandingBal"].ToString()) : 0;
        //                //ddb.P_Date = reader["P_Date"] != DBNull.Value ? DateTime.Parse(reader["P_Date"].ToString()) : DateTime.Parse("1000-01-01");
        //                //obu.Value_Date = reader["Value_Date"] != DBNull.Value ? DateTime.Parse(reader["Value_Date"].ToString()) : DateTime.Now;
        //                obu.Value_Date = reader["Value_Date"] != DBNull.Value ? DateTime.Parse(reader["Value_Date"].ToString()) : DateTime.Parse("1000-01-01");
        //                obu.Maturity_Date = reader["Maturity_Date"] != DBNull.Value ? DateTime.Parse(reader["Maturity_Date"].ToString()) : DateTime.Parse("1000-01-01");
        //                obu.Risk_Rating = reader["Risk_Rating"] != DBNull.Value ? reader["Risk_Rating"].ToString() : "";
        //                obu.ApprovalStatus = reader["ApprovalStatus"] != DBNull.Value ? reader["ApprovalStatus"].ToString() : "";

        //                obuList.Add(obu);
        //            }
        //            con.Close();

        //        }

        //        return request.CreateResponse(HttpStatusCode.OK, obuList);
        //    });
        //}

        //[HttpGet]
        //[Route("incomecustomerratingoverrideusingparamsAPPROVED/{search}")]
        //public HttpResponseMessage GetAvailableIncomeAccountMISOverrideUsingParamsApproved(HttpRequestMessage request, string search)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        List<IncomeCustomerRatingOverrideTEMP> obuList = new List<IncomeCustomerRatingOverrideTEMP>();

        //        using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
        //        {
        //            var cmd = new System.Data.SqlClient.SqlCommand("", con);

        //            //cmd.CommandText = "select * from Names where Id=@Id";
        //            //cmd.Parameters.AddWithValue("@Id", id);

        //            con.Open();
        //            cmd.CommandText = "select top 500 Id, Cust_ID, Ref_No, Settlement_Account, Customer_Name, Limit, " +
        //          "PrincipalOutstandingBal, Value_Date, Maturity_Date, Risk_Rating, ApprovalStatus " +
        //          "from Income_CustomerRating_Override_TEMP where ApprovalStatus='APPROVED' and " +
        //          "(Ref_No like @searchval OR Customer_Name like @searchval)";
        //            cmd.Parameters.AddWithValue("@searchval", search);
        //            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                var obu = new IncomeCustomerRatingOverrideTEMP();

        //                obu.Id = reader["Id"] != DBNull.Value ? int.Parse(reader["Id"].ToString()) : 0;

        //                obu.Cust_ID = reader["Cust_ID"] != DBNull.Value ? reader["Cust_ID"].ToString() : "";
        //                obu.Ref_No = reader["Ref_No"] != DBNull.Value ? reader["Ref_No"].ToString() : "";
        //                obu.Settlement_Account = reader["Settlement_Account"] != DBNull.Value ? reader["Settlement_Account"].ToString() : "";
        //                obu.Customer_Name = reader["Customer_Name"] != DBNull.Value ? reader["Customer_Name"].ToString() : "";
        //                obu.Limit = reader["Limit"] != DBNull.Value ? Convert.ToDecimal(reader["Limit"].ToString()) : 0;

        //                obu.PrincipalOutstandingBal = reader["PrincipalOutstandingBal"] != DBNull.Value ? Convert.ToDecimal(reader["PrincipalOutstandingBal"].ToString()) : 0;
        //                //ddb.P_Date = reader["P_Date"] != DBNull.Value ? DateTime.Parse(reader["P_Date"].ToString()) : DateTime.Parse("1000-01-01");
        //                //obu.Value_Date = reader["Value_Date"] != DBNull.Value ? DateTime.Parse(reader["Value_Date"].ToString()) : DateTime.Now;
        //                obu.Value_Date = reader["Value_Date"] != DBNull.Value ? DateTime.Parse(reader["Value_Date"].ToString()) : DateTime.Parse("1000-01-01");
        //                obu.Maturity_Date = reader["Maturity_Date"] != DBNull.Value ? DateTime.Parse(reader["Maturity_Date"].ToString()) : DateTime.Parse("1000-01-01");
        //                obu.Risk_Rating = reader["Risk_Rating"] != DBNull.Value ? reader["Risk_Rating"].ToString() : "";
        //                obu.ApprovalStatus = reader["ApprovalStatus"] != DBNull.Value ? reader["ApprovalStatus"].ToString() : "";

        //                obuList.Add(obu);
        //            }
        //            con.Close();

        //        }

        //        return request.CreateResponse(HttpStatusCode.OK, obuList);
        //    });
        //}



        //[HttpGet]
        //[Route("incomecustomerratingoverrideDECLINED")]
        //public HttpResponseMessage GetAvailableIncomeAccountMISOverrideTEMPDeclined(HttpRequestMessage request)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        List<IncomeCustomerRatingOverrideTEMP> obuList = new List<IncomeCustomerRatingOverrideTEMP>();

        //        using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
        //        {
        //            var cmd = new System.Data.SqlClient.SqlCommand("", con);

        //            //cmd.CommandText = "select * from Names where Id=@Id";
        //            //cmd.Parameters.AddWithValue("@Id", id);

        //            con.Open();
        //            cmd.CommandText = "select top 500 Id, Cust_ID, Ref_No, Settlement_Account, Customer_Name, Limit, " +
        //          "PrincipalOutstandingBal, Value_Date, Maturity_Date, Risk_Rating, ApprovalStatus " +
        //          "from Income_CustomerRating_Override_TEMP where ApprovalStatus='DECLINED'";
        //            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                var obu = new IncomeCustomerRatingOverrideTEMP();

        //                obu.Id = reader["Id"] != DBNull.Value ? int.Parse(reader["Id"].ToString()) : 0;

        //                obu.Cust_ID = reader["Cust_ID"] != DBNull.Value ? reader["Cust_ID"].ToString() : "";
        //                obu.Ref_No = reader["Ref_No"] != DBNull.Value ? reader["Ref_No"].ToString() : "";
        //                obu.Settlement_Account = reader["Settlement_Account"] != DBNull.Value ? reader["Settlement_Account"].ToString() : "";
        //                obu.Customer_Name = reader["Customer_Name"] != DBNull.Value ? reader["Customer_Name"].ToString() : "";
        //                obu.Limit = reader["Limit"] != DBNull.Value ? Convert.ToDecimal(reader["Limit"].ToString()) : 0;

        //                obu.PrincipalOutstandingBal = reader["PrincipalOutstandingBal"] != DBNull.Value ? Convert.ToDecimal(reader["PrincipalOutstandingBal"].ToString()) : 0;
        //                //ddb.P_Date = reader["P_Date"] != DBNull.Value ? DateTime.Parse(reader["P_Date"].ToString()) : DateTime.Parse("1000-01-01");
        //                //obu.Value_Date = reader["Value_Date"] != DBNull.Value ? DateTime.Parse(reader["Value_Date"].ToString()) : DateTime.Now;
        //                obu.Value_Date = reader["Value_Date"] != DBNull.Value ? DateTime.Parse(reader["Value_Date"].ToString()) : DateTime.Parse("1000-01-01");
        //                obu.Maturity_Date = reader["Maturity_Date"] != DBNull.Value ? DateTime.Parse(reader["Maturity_Date"].ToString()) : DateTime.Parse("1000-01-01");
        //                obu.Risk_Rating = reader["Risk_Rating"] != DBNull.Value ? reader["Risk_Rating"].ToString() : "";
        //                obu.ApprovalStatus = reader["ApprovalStatus"] != DBNull.Value ? reader["ApprovalStatus"].ToString() : "";

        //                obuList.Add(obu);
        //            }
        //            con.Close();

        //        }

        //        return request.CreateResponse(HttpStatusCode.OK, obuList);
        //    });
        //}

        //[HttpGet]
        //[Route("incomecustomerratingoverrideusingparamsDECLINED/{search}")]
        //public HttpResponseMessage GetAvailableIncomeAccountMISOverrideUsingParamsDeclined(HttpRequestMessage request, string search)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        List<IncomeCustomerRatingOverrideTEMP> obuList = new List<IncomeCustomerRatingOverrideTEMP>();

        //        using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
        //        {
        //            var cmd = new System.Data.SqlClient.SqlCommand("", con);

        //            //cmd.CommandText = "select * from Names where Id=@Id";
        //            //cmd.Parameters.AddWithValue("@Id", id);

        //            con.Open();                  
        //            cmd.CommandText = "select top 500 Id, Cust_ID, Ref_No, Settlement_Account, Customer_Name, Limit, " +
        //         "PrincipalOutstandingBal, Value_Date, Maturity_Date, Risk_Rating, ApprovalStatus " +
        //         "from Income_CustomerRating_Override_TEMP where ApprovalStatus='DECLINED' and " +
        //         "(Ref_No like @searchval OR Customer_Name like @searchval)";
        //            cmd.Parameters.AddWithValue("@searchval", search);
        //            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                var obu = new IncomeCustomerRatingOverrideTEMP();

        //                obu.Id = reader["Id"] != DBNull.Value ? int.Parse(reader["Id"].ToString()) : 0;

        //                obu.Cust_ID = reader["Cust_ID"] != DBNull.Value ? reader["Cust_ID"].ToString() : "";
        //                obu.Ref_No = reader["Ref_No"] != DBNull.Value ? reader["Ref_No"].ToString() : "";
        //                obu.Settlement_Account = reader["Settlement_Account"] != DBNull.Value ? reader["Settlement_Account"].ToString() : "";
        //                obu.Customer_Name = reader["Customer_Name"] != DBNull.Value ? reader["Customer_Name"].ToString() : "";
        //                obu.Limit = reader["Limit"] != DBNull.Value ? Convert.ToDecimal(reader["Limit"].ToString()) : 0;

        //                obu.PrincipalOutstandingBal = reader["PrincipalOutstandingBal"] != DBNull.Value ? Convert.ToDecimal(reader["PrincipalOutstandingBal"].ToString()) : 0;
        //                //ddb.P_Date = reader["P_Date"] != DBNull.Value ? DateTime.Parse(reader["P_Date"].ToString()) : DateTime.Parse("1000-01-01");
        //                //obu.Value_Date = reader["Value_Date"] != DBNull.Value ? DateTime.Parse(reader["Value_Date"].ToString()) : DateTime.Now;
        //                obu.Value_Date = reader["Value_Date"] != DBNull.Value ? DateTime.Parse(reader["Value_Date"].ToString()) : DateTime.Parse("1000-01-01");
        //                obu.Maturity_Date = reader["Maturity_Date"] != DBNull.Value ? DateTime.Parse(reader["Maturity_Date"].ToString()) : DateTime.Parse("1000-01-01");
        //                obu.Risk_Rating = reader["Risk_Rating"] != DBNull.Value ? reader["Risk_Rating"].ToString() : "";
        //                obu.ApprovalStatus = reader["ApprovalStatus"] != DBNull.Value ? reader["ApprovalStatus"].ToString() : "";

        //                obuList.Add(obu);
        //            }
        //            con.Close();

        //        }

        //        return request.CreateResponse(HttpStatusCode.OK, obuList);
        //    });
        //}


        //[HttpPost]
        //[Route("incomecustomerratingoverrideapproval/{selectedIds}")]
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
        //                var sql = "update Income_CustomerRating_Override_TEMP set ApprovalStatus='APPROVED' where id IN(";
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
        //[Route("incomecustomerratingoverridedecline/{selectedIds}")]
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
        //                var sql = "update Income_CustomerRating_Override_TEMP set ApprovalStatus='DECLINED' where id IN(";
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
