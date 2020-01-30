using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Presentation.WebClient.Models;

namespace Fintrak.Presentation.WebClient.Additionalmethods.ApprovalMtd
{
    public class IncomeAccountsTreeMisCodesTEMPStatus
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;


        //================ methods to call starts IncomeNewDetails ==========================================
        public IEnumerable<IncomeAccountsTreeMisCodesTEMP> IncomeAccountsTreeMisCodesTEMP(string status)
        {
            List<IncomeAccountsTreeMisCodesTEMP> obuList = new List<IncomeAccountsTreeMisCodesTEMP>();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("", con);

                //cmd.CommandText = "select * from Names where Id=@Id";
                //cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                cmd.CommandText = "select top 1000 * from Income_AccountsTree_MISCodes_TEMP where ApprovalStatus=@STATUS";
                //cmd.CommandText = "select top 500 Id, accountnumber, mis, AccountOfficer_Code, ApprovalStatus from Income_accountMIS_Override_TEMP where ApprovalStatus=@STATUS";
                cmd.Parameters.AddWithValue("@STATUS", status);
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var obu = new IncomeAccountsTreeMisCodesTEMP();

                    obu.ID = reader["Id"] != DBNull.Value ? int.Parse(reader["Id"].ToString()) : 0;
                    obu.AccountNumber = reader["AccountNumber"] != DBNull.Value ? reader["AccountNumber"].ToString() : "";
                    obu.AccountOfficer_Code = reader["AccountOfficer_Code"] != DBNull.Value ? reader["AccountOfficer_Code"].ToString() : "";
                    obu.AccountOfficerName = reader["AccountOfficerName"] != DBNull.Value ? reader["AccountOfficerName"].ToString() : "";
                    obu.ShareRatio = reader["ShareRatio"] != DBNull.Value ? decimal.Parse(reader["ShareRatio"].ToString()) : 0;
                    obu.Team_Code = reader["Team_Code"] != DBNull.Value ? reader["Team_Code"].ToString() : "";
                    obu.ApprovalStatus = reader["ApprovalStatus"] != DBNull.Value ? reader["ApprovalStatus"].ToString() : "";

                    obuList.Add(obu);
                }
                con.Close();
            }
            return obuList;
        }

        public IEnumerable<IncomeAccountsTreeMisCodesTEMP> IncomeAccountsTreeMisCodesTEMPusingparams(string status, string search)
        {
            List<IncomeAccountsTreeMisCodesTEMP> obuList = new List<IncomeAccountsTreeMisCodesTEMP>();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("", con);

                //cmd.CommandText = "select * from Names where Id=@Id";
                //cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                //cmd.CommandText = "select top 500 Id, accountnumber, mis, AccountOfficer_Code, ApprovalStatus from Income_accountMIS_Override_TEMP " +
                //   "where ApprovalStatus=@STATUS and (accountnumber like @searchval OR mis like @searchval OR AccountOfficer_Code like @searchval)";
                cmd.CommandText = "select top 1000 * from Income_AccountsTree_MISCodes_TEMP " +
                  "where ApprovalStatus=@STATUS and (accountnumber like @searchval OR AccountOfficerName like @searchval OR AccountOfficer_Code like @searchval OR Team_Code like @searchval)";
                cmd.Parameters.AddWithValue("@STATUS", status);
                cmd.Parameters.AddWithValue("@searchval", "%" + search + "%");
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var obu = new IncomeAccountsTreeMisCodesTEMP();

                    obu.ID = reader["Id"] != DBNull.Value ? int.Parse(reader["Id"].ToString()) : 0;
                    obu.AccountNumber = reader["AccountNumber"] != DBNull.Value ? reader["AccountNumber"].ToString() : "";
                    obu.AccountOfficer_Code = reader["AccountOfficer_Code"] != DBNull.Value ? reader["AccountOfficer_Code"].ToString() : "";
                    obu.AccountOfficerName = reader["AccountOfficerName"] != DBNull.Value ? reader["AccountOfficerName"].ToString() : "";
                    obu.ShareRatio = reader["ShareRatio"] != DBNull.Value ? decimal.Parse(reader["ShareRatio"].ToString()) : 0;
                    obu.Team_Code = reader["Team_Code"] != DBNull.Value ? reader["Team_Code"].ToString() : "";
                    obu.ApprovalStatus = reader["ApprovalStatus"] != DBNull.Value ? reader["ApprovalStatus"].ToString() : "";

                    obuList.Add(obu);
                }
                con.Close();
            }
            return obuList;
        }
        //================ methods to call ends ============================================

        public void EditIncomeAccountsTreeMisCodesApproval(string selectedIds)
        {
            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                using (var cmd = new System.Data.SqlClient.SqlCommand("", con))
                {
                    var userInput = selectedIds;
                    var values = userInput.Split(',');
                    string currentuser = Convert.ToString(HttpContext.Current.User.Identity.Name);

                    con.Open();
                    var sql = "update Income_AccountsTree_MISCodes_TEMP set ApprovalStatus='APPROVED', ApprovalUser=@CURRENTUSER where Id IN(";
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

        public void EditIncomeAccountsTreeMisCodesDecline(string selectedIds)
        {
            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                using (var cmd = new System.Data.SqlClient.SqlCommand("", con))
                {
                    var userInput = selectedIds;
                    var values = userInput.Split(',');
                    string currentuser = Convert.ToString(HttpContext.Current.User.Identity.Name);

                    con.Open();
                    var sql = "update Income_AccountsTree_MISCodes_TEMP set ApprovalStatus='DECLINED', ApprovalUser=@CURRENTUSER where Id IN(";
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