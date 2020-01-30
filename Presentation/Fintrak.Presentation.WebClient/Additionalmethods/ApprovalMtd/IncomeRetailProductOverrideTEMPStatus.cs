using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Presentation.WebClient.Models;

namespace Fintrak.Presentation.WebClient.Additionalmethods.ApprovalMtd
{
    public class IncomeRetailProductOverrideTEMPStatus
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;
        int taketop = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["TakeTop"].Trim());

        //================ methods to call starts IncomeNewDetails ==========================================
        public IEnumerable<IncomeRetailProductOverrideTEMP> IncomeRetailProductOverrideTEMP(string status)
        {
            List<IncomeRetailProductOverrideTEMP> obuList = new List<IncomeRetailProductOverrideTEMP>();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("", con);

                //cmd.CommandText = "select * from Names where Id=@Id";
                //cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                //cmd.CommandText = "select top 500 * from Income_RetailProduct_Override_TEMP where ApprovalStatus=@STATUS";
                cmd.CommandText = "select  top (@TAKETOP) * from Income_RetailProduct_Override_TEMP where ApprovalStatus=@STATUS";
                cmd.Parameters.AddWithValue("@STATUS", status);
                cmd.Parameters.AddWithValue("@TAKETOP", taketop);
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var obu = new IncomeRetailProductOverrideTEMP();

                    obu.Id = reader["Id"] != DBNull.Value ? int.Parse(reader["Id"].ToString()) : 0;

                    obu.Customerid = reader["Customerid"] != DBNull.Value ? int.Parse(reader["Customerid"].ToString()) : 0;
                    obu.Bank = reader["Bank"] != DBNull.Value ? reader["Bank"].ToString() : "";
                    obu.Mis_code = reader["Mis_code"] != DBNull.Value ? reader["Mis_code"].ToString() : "";
                    obu.AccountOfficer_Code = reader["AccountOfficer_Code"] != DBNull.Value ? reader["AccountOfficer_Code"].ToString() : "";
                    obu.ApprovalStatus = reader["ApprovalStatus"] != DBNull.Value ? reader["ApprovalStatus"].ToString() : "";

                    obuList.Add(obu);
                }
                con.Close();
            }
            return obuList;
        }

        public IEnumerable<IncomeRetailProductOverrideTEMP> IncomeRetailProductOverrideTEMPusingparams(string status, string search)
        {
            List<IncomeRetailProductOverrideTEMP> obuList = new List<IncomeRetailProductOverrideTEMP>();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("", con);

                //cmd.CommandText = "select * from Names where Id=@Id";
                //cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                cmd.CommandText = "select * from Income_RetailProduct_Override_TEMP " +
                   "where ApprovalStatus=@STATUS and (Customerid like @searchval OR Mis_code like @searchval OR AccountOfficer_Code like @searchval)";
                //cmd.CommandText = "select Id, Customerid, Bank, Mis_code, AccountOfficer_Code, ApprovalStatus from Income_RetailProduct_Override_TEMP " +
                //    "where ApprovalStatus=@STATUS and (Customerid like @searchval OR Mis_code like @searchval OR AccountOfficer_Code like @searchval)";
                cmd.Parameters.AddWithValue("@STATUS", status);
                cmd.Parameters.AddWithValue("@searchval", "%" + search + "%");
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var obu = new IncomeRetailProductOverrideTEMP();

                    obu.Id = reader["Id"] != DBNull.Value ? int.Parse(reader["Id"].ToString()) : 0;

                    obu.Customerid = reader["Customerid"] != DBNull.Value ? int.Parse(reader["Customerid"].ToString()) : 0;
                    obu.Bank = reader["Bank"] != DBNull.Value ? reader["Bank"].ToString() : "";
                    obu.Mis_code = reader["Mis_code"] != DBNull.Value ? reader["Mis_code"].ToString() : "";
                    obu.AccountOfficer_Code = reader["AccountOfficer_Code"] != DBNull.Value ? reader["AccountOfficer_Code"].ToString() : "";
                    obu.ApprovalStatus = reader["ApprovalStatus"] != DBNull.Value ? reader["ApprovalStatus"].ToString() : "";

                    obuList.Add(obu);
                }
                con.Close();
            }
            return obuList;
        }
        //================ methods to call ends ============================================

        public void EditIncomeRetailProductOverrideApproval(string selectedIds)
        {
            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                using (var cmd = new System.Data.SqlClient.SqlCommand("", con))
                {
                    var userInput = selectedIds;
                    var values = userInput.Split(',');
                    string currentuser = Convert.ToString(HttpContext.Current.User.Identity.Name);

                    con.Open();
                    var sql = "update Income_RetailProduct_Override_TEMP set ApprovalStatus='APPROVED', ApprovalUser=@CURRENTUSER where Id IN(";
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

        public void EditIncomeRetailProductOverrideDecline(string selectedIds)
        {
            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                using (var cmd = new System.Data.SqlClient.SqlCommand("", con))
                {
                    var userInput = selectedIds;
                    var values = userInput.Split(',');
                    string currentuser = Convert.ToString(HttpContext.Current.User.Identity.Name);

                    con.Open();
                    var sql = "update Income_RetailProduct_Override_TEMP set ApprovalStatus='DECLINED', ApprovalUser=@CURRENTUSER where Id IN(";
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