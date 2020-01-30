using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Presentation.WebClient.Models;

namespace Fintrak.Presentation.WebClient.Additionalmethods.ApprovalMtd
{
    public class IncomeCustomerRatingOverrideTEMPStatus
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;
        int taketop = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["TakeTop"].Trim());

        //================ methods to call starts IncomeNewDetails ==========================================
        public IEnumerable<IncomeCustomerRatingOverrideTEMP> IncomeCustomerRatingOverrideTEMP(string status)
        {
            List<IncomeCustomerRatingOverrideTEMP> obuList = new List<IncomeCustomerRatingOverrideTEMP>();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("", con);

                //cmd.CommandText = "select * from Names where Id=@Id";
                //cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                cmd.CommandText = "select top (@TAKETOP) * from Income_CustomerRating_Override_TEMP where ApprovalStatus=@STATUS";
                //cmd.CommandText = "select top 500 Id, Cust_ID, Ref_No, Settlement_Account, Customer_Name, Limit, " +
                //                   "PrincipalOutstandingBal, Value_Date, Maturity_Date, Risk_Rating, ApprovalStatus " +
                //                   "from Income_CustomerRating_Override_TEMP where ApprovalStatus=@STATUS";
                cmd.Parameters.AddWithValue("@STATUS", status);
                cmd.Parameters.AddWithValue("@TAKETOP", taketop);
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var obu = new IncomeCustomerRatingOverrideTEMP();

                    obu.Id = reader["Id"] != DBNull.Value ? int.Parse(reader["Id"].ToString()) : 0;

                    obu.Cust_ID = reader["Cust_ID"] != DBNull.Value ? reader["Cust_ID"].ToString() : "";
                    obu.Ref_No = reader["Ref_No"] != DBNull.Value ? reader["Ref_No"].ToString() : "";
                    obu.Settlement_Account = reader["Settlement_Account"] != DBNull.Value ? reader["Settlement_Account"].ToString() : "";
                    obu.Customer_Name = reader["Customer_Name"] != DBNull.Value ? reader["Customer_Name"].ToString() : "";
                    obu.Limit = reader["Limit"] != DBNull.Value ? Convert.ToDecimal(reader["Limit"].ToString()) : 0;

                    obu.PrincipalOutstandingBal = reader["PrincipalOutstandingBal"] != DBNull.Value ? Convert.ToDecimal(reader["PrincipalOutstandingBal"].ToString()) : 0;
                    //ddb.P_Date = reader["P_Date"] != DBNull.Value ? DateTime.Parse(reader["P_Date"].ToString()) : DateTime.Parse("1000-01-01");
                    //obu.Value_Date = reader["Value_Date"] != DBNull.Value ? DateTime.Parse(reader["Value_Date"].ToString()) : DateTime.Now;
                    obu.Value_Date = reader["Value_Date"] != DBNull.Value ? DateTime.Parse(reader["Value_Date"].ToString()) : DateTime.Parse("1000-01-01");
                    obu.Maturity_Date = reader["Maturity_Date"] != DBNull.Value ? DateTime.Parse(reader["Maturity_Date"].ToString()) : DateTime.Parse("1000-01-01");
                    obu.Risk_Rating = reader["Risk_Rating"] != DBNull.Value ? reader["Risk_Rating"].ToString() : "";
                    obu.ApprovalStatus = reader["ApprovalStatus"] != DBNull.Value ? reader["ApprovalStatus"].ToString() : "";

                    obuList.Add(obu);
                }
                con.Close();
            }
            return obuList;
        }

        public IEnumerable<IncomeCustomerRatingOverrideTEMP> IncomeCustomerRatingOverrideTEMPusingparams(string status, string search)
        {
            List<IncomeCustomerRatingOverrideTEMP> obuList = new List<IncomeCustomerRatingOverrideTEMP>();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("", con);

                con.Open();
                cmd.CommandText = "select top 500 * from Income_CustomerRating_Override_TEMP where ApprovalStatus=@STATUS and " +
                 "(Ref_No like @searchval OR Customer_Name like @searchval)";
                //cmd.CommandText = "select top 500 Id, Cust_ID, Ref_No, Settlement_Account, Customer_Name, Limit, " +
                //  "PrincipalOutstandingBal, Value_Date, Maturity_Date, Risk_Rating, ApprovalStatus " +
                //  "from Income_CustomerRating_Override_TEMP where ApprovalStatus='AWAITINGAPPROVAL' and " +
                //  "(Ref_No like @searchval OR Customer_Name like @searchval)";
                cmd.Parameters.AddWithValue("@STATUS", status);
                cmd.Parameters.AddWithValue("@searchval", "%" + search + "%");
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var obu = new IncomeCustomerRatingOverrideTEMP();

                    obu.Id = reader["Id"] != DBNull.Value ? int.Parse(reader["Id"].ToString()) : 0;

                    obu.Cust_ID = reader["Cust_ID"] != DBNull.Value ? reader["Cust_ID"].ToString() : "";
                    obu.Ref_No = reader["Ref_No"] != DBNull.Value ? reader["Ref_No"].ToString() : "";
                    obu.Settlement_Account = reader["Settlement_Account"] != DBNull.Value ? reader["Settlement_Account"].ToString() : "";
                    obu.Customer_Name = reader["Customer_Name"] != DBNull.Value ? reader["Customer_Name"].ToString() : "";
                    obu.Limit = reader["Limit"] != DBNull.Value ? Convert.ToDecimal(reader["Limit"].ToString()) : 0;

                    obu.PrincipalOutstandingBal = reader["PrincipalOutstandingBal"] != DBNull.Value ? Convert.ToDecimal(reader["PrincipalOutstandingBal"].ToString()) : 0;
                    //ddb.P_Date = reader["P_Date"] != DBNull.Value ? DateTime.Parse(reader["P_Date"].ToString()) : DateTime.Parse("1000-01-01");
                    //obu.Value_Date = reader["Value_Date"] != DBNull.Value ? DateTime.Parse(reader["Value_Date"].ToString()) : DateTime.Now;
                    obu.Value_Date = reader["Value_Date"] != DBNull.Value ? DateTime.Parse(reader["Value_Date"].ToString()) : DateTime.Parse("1000-01-01");
                    obu.Maturity_Date = reader["Maturity_Date"] != DBNull.Value ? DateTime.Parse(reader["Maturity_Date"].ToString()) : DateTime.Parse("1000-01-01");
                    obu.Risk_Rating = reader["Risk_Rating"] != DBNull.Value ? reader["Risk_Rating"].ToString() : "";
                    obu.ApprovalStatus = reader["ApprovalStatus"] != DBNull.Value ? reader["ApprovalStatus"].ToString() : "";

                    obuList.Add(obu);
                }
                con.Close();
            }
            return obuList;
        }
        //================ methods to call ends ============================================

        public void EditIncomeCustomerRatingOverrideTEMPApproval(string selectedIds)
        {
            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                using (var cmd = new System.Data.SqlClient.SqlCommand("", con))
                {
                    var userInput = selectedIds;
                    var values = userInput.Split(',');
                    string currentuser = Convert.ToString(HttpContext.Current.User.Identity.Name);

                    con.Open();
                    var sql = "update Income_CustomerRating_Override_TEMP set ApprovalStatus='APPROVED', ApprovalUser=@CURRENTUSER where Id IN(";
                    cmd.Parameters.AddWithValue("@CURRENTUSER", currentuser);
                    for (int i = 0; i < values.Length; i++)
                    {
                        sql = $"{sql} @{i},";
                        cmd.Parameters.Add($"@{i}", System.Data.Odbc.OdbcType.Int).Value = values[i];
                    }
                    cmd.CommandText = sql.TrimEnd(',') + ");";
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        public void EditIncomeCustomerRatingOverrideTEMPDecline(string selectedIds)
        {
            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                using (var cmd = new System.Data.SqlClient.SqlCommand("", con))
                {
                    var userInput = selectedIds;
                    var values = userInput.Split(',');
                    string currentuser = Convert.ToString(HttpContext.Current.User.Identity.Name);

                    con.Open();
                    var sql = "update Income_CustomerRating_Override_TEMP set ApprovalStatus='DECLINED', ApprovalUser=@CURRENTUSER where Id IN(";
                    cmd.Parameters.AddWithValue("@CURRENTUSER", currentuser);
                    for (int i = 0; i < values.Length; i++)
                    {
                        sql = $"{sql} @{i},";
                        cmd.Parameters.Add($"@{i}", System.Data.Odbc.OdbcType.Int).Value = values[i];
                    }
                    cmd.CommandText = sql.TrimEnd(',') + ");";
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }


    }
}