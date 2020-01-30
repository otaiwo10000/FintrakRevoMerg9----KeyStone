using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Presentation.WebClient.Models;
using Fintrak.Client.MPR.Entities;

namespace Fintrak.Presentation.WebClient.Additionalmethods.ApprovalMtd
{
    public class IncomeAccountsListingTEMPStatus
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;
        int taketop = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["TakeTop"].Trim());

        //================ methods to call starts IncomeNewDetails ==========================================
        public IEnumerable<IncomeAccountsListingTEMP> IncomeAccountsListingTEMP(string status)
        {
            List<IncomeAccountsListingTEMP> obuList = new List<IncomeAccountsListingTEMP>();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("", con);

                //cmd.CommandText = "select * from Names where Id=@Id";
                //cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                //cmd.CommandText = "select top 500 * from Income_accountMIS_Override_TEMP where ApprovalStatus=@STATUS";
                cmd.CommandText = "select top (@TAKETOP) * from Income_AccountsListing_TEMP where ApprovalStatus=@STATUS";
                cmd.Parameters.AddWithValue("@STATUS", status);
                cmd.Parameters.AddWithValue("@TAKETOP", taketop);
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var obu = new IncomeAccountsListingTEMP();

                    obu.Id = reader["Id"] != DBNull.Value ? int.Parse(reader["Id"].ToString()) : 0;

                    obu.accountnumber = reader["accountnumber"] != DBNull.Value ? reader["accountnumber"].ToString() : "";
                    obu.CustomerName = reader["CustomerName"] != DBNull.Value ? reader["CustomerName"].ToString() : "";
                    obu.MIS_Code = reader["MIS_Code"] != DBNull.Value ? reader["MIS_Code"].ToString() : "";
                    obu.BranchCode = reader["BranchCode"] != DBNull.Value ? reader["BranchCode"].ToString() : "";
                    obu.AccountOfficer_Code = reader["AccountOfficer_Code"] != DBNull.Value ? reader["AccountOfficer_Code"].ToString() : "";
                    obu.Team_branch = reader["Team_branch"] != DBNull.Value ? reader["Team_branch"].ToString() : "";
                    obu.Date_Open = reader["Date_Open"] != DBNull.Value ? Convert.ToDateTime(reader["Date_Open"]) : Convert.ToDateTime("");
                    obu.ApprovalStatus = reader["ApprovalStatus"] != DBNull.Value ? reader["ApprovalStatus"].ToString() : "";

                    obuList.Add(obu);
                }
                con.Close();
            }
            return obuList;
        }

        public IEnumerable<IncomeAccountsListingTEMP> IncomeAccountsListingTEMPusingparams(string status, string search)
        {
            List<IncomeAccountsListingTEMP> obuList = new List<IncomeAccountsListingTEMP>();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("", con);

                //cmd.CommandText = "select * from Names where Id=@Id";
                //cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                cmd.CommandText = "select top 500 Id, accountnumber, CustomerName, MIS_Code, BranchCode, AccountOfficer_Code, Team_branch, Date_Open, ApprovalStatus from Income_AccountsListing_TEMP " +
                   "where ApprovalStatus=@STATUS and (accountnumber like @searchval OR MIS_Code like @searchval OR AccountOfficer_Code like @searchval)";
                cmd.Parameters.AddWithValue("@STATUS", status);
                cmd.Parameters.AddWithValue("@searchval", "%" + search + "%");
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var obu = new IncomeAccountsListingTEMP();

                    obu.Id = reader["Id"] != DBNull.Value ? int.Parse(reader["Id"].ToString()) : 0;

                    obu.accountnumber = reader["accountnumber"] != DBNull.Value ? reader["accountnumber"].ToString() : "";
                    obu.CustomerName = reader["CustomerName"] != DBNull.Value ? reader["CustomerName"].ToString() : "";
                    obu.MIS_Code = reader["MIS_Code"] != DBNull.Value ? reader["MIS_Code"].ToString() : "";
                    obu.BranchCode = reader["BranchCode"] != DBNull.Value ? reader["BranchCode"].ToString() : "";
                    obu.AccountOfficer_Code = reader["AccountOfficer_Code"] != DBNull.Value ? reader["AccountOfficer_Code"].ToString() : "";
                    obu.Team_branch = reader["Team_branch"] != DBNull.Value ? reader["Team_branch"].ToString() : "";
                    obu.Date_Open = reader["Date_Open"] != DBNull.Value ? Convert.ToDateTime(reader["Date_Open"]) : Convert.ToDateTime("");
                    obu.ApprovalStatus = reader["ApprovalStatus"] != DBNull.Value ? reader["ApprovalStatus"].ToString() : "";

                    obuList.Add(obu);
                }
                con.Close();
            }
            return obuList;
        }
        //================ methods to call ends ============================================

        public void EditIncomeAccountsListingApproval(string selectedIds)
        {
            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                using (var cmd = new System.Data.SqlClient.SqlCommand("", con))
                {
                    var userInput = selectedIds;
                    var values = userInput.Split(',');
                    string currentuser = Convert.ToString(HttpContext.Current.User.Identity.Name);
                    //var v = HttpContext.Current.User;
                    //string currentuser = v.Identity.Name;

                    con.Open();
                    var sql = "update Income_AccountsListing_TEMP set ApprovalStatus='APPROVED', ApprovalUser=@CURRENTUSER  where Id IN(";
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

        public void EditIncomeAccountsListingDecline(string selectedIds)
        {
            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                using (var cmd = new System.Data.SqlClient.SqlCommand("", con))
                {
                    var userInput = selectedIds;
                    var values = userInput.Split(',');
                    string currentuser = Convert.ToString(HttpContext.Current.User.Identity.Name);

                    con.Open();
                    var sql = "update Income_AccountsListing_TEMP set ApprovalStatus='DECLINED', ApprovalUser=@CURRENTUSER where Id IN(";
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