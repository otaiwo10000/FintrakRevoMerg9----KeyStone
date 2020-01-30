using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Presentation.WebClient.Models;

namespace Fintrak.Presentation.WebClient.Additionalmethods.ApprovalMtd
{
    public class IncomeAccountsTreeAccountTEMPStatus
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;


        //================ methods to call starts IncomeNewDetails ==========================================
        public IEnumerable<IncomeAccountsTreeAccountTEMP> IncomeAccountsTreeAccountTEMP(string status)
        {
            List<IncomeAccountsTreeAccountTEMP> obuList = new List<IncomeAccountsTreeAccountTEMP>();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("", con);

                //cmd.CommandText = "select * from Names where Id=@Id";
                //cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                cmd.CommandText = "select top 1000 * from Income_AccountsTree_Accounts_TEMP where ApprovalStatus=@STATUS";
                //cmd.CommandText = "select top 500 Id, accountnumber, mis, AccountOfficer_Code, ApprovalStatus from Income_accountMIS_Override_TEMP where ApprovalStatus=@STATUS";
                cmd.Parameters.AddWithValue("@STATUS", status);
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var obu = new IncomeAccountsTreeAccountTEMP();

                    obu.ID = reader["Id"] != DBNull.Value ? int.Parse(reader["Id"].ToString()) : 0;
                    obu.AccountNumber = reader["AccountNumber"] != DBNull.Value ? reader["AccountNumber"].ToString() : "";
                    obu.ShareReason = reader["ShareReason"] != DBNull.Value ? reader["ShareReason"].ToString() : "";
                    obu.ExpirationPeriod = reader["ExpirationPeriod"] != DBNull.Value ? Int32.Parse(reader["ExpirationPeriod"].ToString()) : 0;
                    obu.ExpirationYear = reader["ExpirationYear"] != DBNull.Value ? Convert.ToInt32(reader["ExpirationYear"].ToString()) : 0;

                    obuList.Add(obu);
                }
                con.Close();
            }
            return obuList;
        }

        public IEnumerable<IncomeAccountsTreeAccountTEMP> IncomeAccountsTreeAccountTEMPusingparams(string status, string search)
        {
            List<IncomeAccountsTreeAccountTEMP> obuList = new List<IncomeAccountsTreeAccountTEMP>();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("", con);

                //cmd.CommandText = "select * from Names where Id=@Id";
                //cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                //cmd.CommandText = "select top 500 Id, accountnumber, mis, AccountOfficer_Code, ApprovalStatus from Income_accountMIS_Override_TEMP " +
                //   "where ApprovalStatus=@STATUS and (accountnumber like @searchval OR mis like @searchval OR AccountOfficer_Code like @searchval)";
                //cmd.Parameters.AddWithValue("@STATUS", status);
                cmd.CommandText = "select top 1000 * from Income_accountMIS_Override_TEMP " +
                   "where ApprovalStatus=@STATUS and (accountnumber like @searchval OR ExpirationPeriod = @searchval OR ExpirationYear = @searchval)";

                cmd.Parameters.AddWithValue("@searchval", "%" + search + "%");
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var obu = new IncomeAccountsTreeAccountTEMP();

                    obu.ID = reader["Id"] != DBNull.Value ? int.Parse(reader["Id"].ToString()) : 0;
                    obu.AccountNumber = reader["AccountNumber"] != DBNull.Value ? reader["AccountNumber"].ToString() : "";
                    obu.ShareReason = reader["ShareReason"] != DBNull.Value ? reader["ShareReason"].ToString() : "";
                    obu.ExpirationPeriod = reader["ExpirationPeriod"] != DBNull.Value ? Int32.Parse(reader["ExpirationPeriod"].ToString()) : 0;
                    obu.ExpirationYear = reader["ExpirationYear"] != DBNull.Value ? Convert.ToInt32(reader["ExpirationYear"].ToString()) : 0;

                    obuList.Add(obu);
                }
                con.Close();
            }
            return obuList;
        }
        //================ methods to call ends ============================================

        public void EditIncomeAccountsTreeAccountApproval(string selectedIds)
        {
            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                using (var cmd = new System.Data.SqlClient.SqlCommand("", con))
                {
                    var userInput = selectedIds;
                    var values = userInput.Split(',');
                    string currentuser = Convert.ToString(HttpContext.Current.User.Identity.Name);

                    con.Open();
                    var sql = "update Income_AccountsTree_Accounts_TEMP set ApprovalStatus='APPROVED', ApprovalUser=@CURRENTUSER where Id IN(";
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

        public void EditIncomeAccountsTreeAccountDecline(string selectedIds)
        {
            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                using (var cmd = new System.Data.SqlClient.SqlCommand("", con))
                {
                    var userInput = selectedIds;
                    var values = userInput.Split(',');
                    string currentuser = Convert.ToString(HttpContext.Current.User.Identity.Name);

                    con.Open();
                    var sql = "update Income_AccountsTree_Accounts_TEMP set ApprovalStatus='DECLINED', ApprovalUser=@CURRENTUSER where Id IN(";
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