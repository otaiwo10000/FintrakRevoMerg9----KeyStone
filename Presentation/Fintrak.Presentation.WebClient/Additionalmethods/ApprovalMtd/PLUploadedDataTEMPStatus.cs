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
    public class PLUploadedDataTEMPStatus
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;
        int taketop = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["TakeTop"].Trim());

        //================ methods to call starts IncomeNewDetails ==========================================
        public IEnumerable<PLUploadedDataTEMP> PLUploadedDataTEMP(string status)
        {
            List<PLUploadedDataTEMP> obuList = new List<PLUploadedDataTEMP>();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("", con);

                //cmd.CommandText = "select * from Names where Id=@Id";
                //cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                //cmd.CommandText = "select top 500 * from Income_accountMIS_Override_TEMP where ApprovalStatus=@STATUS";
                cmd.CommandText = "select top (@TAKETOP) * from PLUploadedData_TEMP where ApprovalStatus=@STATUS";
                cmd.Parameters.AddWithValue("@STATUS", status);
                cmd.Parameters.AddWithValue("@TAKETOP", taketop);
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var obu = new PLUploadedDataTEMP();

                    obu.ID = reader["ID"] != DBNull.Value ? int.Parse(reader["ID"].ToString()) : 0;

                    obu.MIS_Code = reader["MIS_Code"] != DBNull.Value ? reader["MIS_Code"].ToString() : "";
                    obu.Maincaption = reader["Maincaption"] != DBNull.Value ? reader["Maincaption"].ToString() : "";
                    obu.Accountnumber = reader["Accountnumber"] != DBNull.Value ? reader["Accountnumber"].ToString() : "";
                    obu.Narrative = reader["Narrative"] != DBNull.Value ? reader["Narrative"].ToString() : "";
                    obu.Period = reader["Period"] != DBNull.Value ? int.Parse(reader["Period"].ToString()) : 0;
                    obu.Year = reader["Year"] != DBNull.Value ? int.Parse(reader["Year"].ToString()) : 0;
                    obu.Amount = reader["Amount"] != DBNull.Value ? decimal.Parse(reader["Amount"].ToString()) : 0;
                    obu.AcctOfficer = reader["AcctOfficer"] != DBNull.Value ? reader["AcctOfficer"].ToString() : "";
                    obu.Volume = reader["Volume"] != DBNull.Value ? decimal.Parse(reader["Volume"].ToString()) : 0;
                    obu.ApprovalStatus = reader["ApprovalStatus"] != DBNull.Value ? reader["ApprovalStatus"].ToString() : "";

                    obuList.Add(obu);
                }
                con.Close();
            }
            return obuList;
        }

        public IEnumerable<PLUploadedDataTEMP> PLUploadedDataTEMPusingparams(string status, string search)
        {
            List<PLUploadedDataTEMP> obuList = new List<PLUploadedDataTEMP>();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("", con);

                //cmd.CommandText = "select * from Names where Id=@Id";
                //cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                cmd.CommandText = "select top 500 * from PLUploadedData_TEMP " +
                   "where ApprovalStatus=@STATUS and (Accountnumber like @searchval OR MIS_Code like @searchval OR AcctOfficer like @searchval)";
                cmd.Parameters.AddWithValue("@STATUS", status);
                cmd.Parameters.AddWithValue("@searchval", "%" + search + "%");
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var obu = new PLUploadedDataTEMP();

                    obu.ID = reader["ID"] != DBNull.Value ? int.Parse(reader["ID"].ToString()) : 0;

                    obu.MIS_Code = reader["MIS_Code"] != DBNull.Value ? reader["MIS_Code"].ToString() : "";
                    obu.Maincaption = reader["Maincaption"] != DBNull.Value ? reader["Maincaption"].ToString() : "";
                    obu.Accountnumber = reader["Accountnumber"] != DBNull.Value ? reader["Accountnumber"].ToString() : "";
                    obu.Narrative = reader["Narrative"] != DBNull.Value ? reader["Narrative"].ToString() : "";
                    obu.Period = reader["Period"] != DBNull.Value ? int.Parse(reader["Period"].ToString()) : 0;
                    obu.Year = reader["Year"] != DBNull.Value ? int.Parse(reader["Year"].ToString()) : 0;
                    obu.Amount = reader["Amount"] != DBNull.Value ? decimal.Parse(reader["Amount"].ToString()) : 0;
                    obu.AcctOfficer = reader["AcctOfficer"] != DBNull.Value ? reader["AcctOfficer"].ToString() : "";
                    obu.Volume = reader["Volume"] != DBNull.Value ? decimal.Parse(reader["Volume"].ToString()) : 0;
                    obu.ApprovalStatus = reader["ApprovalStatus"] != DBNull.Value ? reader["ApprovalStatus"].ToString() : "";

                    obuList.Add(obu);
                }
                con.Close();
            }
            return obuList;
        }
        //================ methods to call ends ============================================

        public void EditPLUploadedDataApproval(string selectedIds)
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
                    var sql = "update PLUploadedData_TEMP set ApprovalStatus='APPROVED', CreatedBy=@CURRENTUSER  where Id IN(";
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

        public void EditPLUploadedDataDecline(string selectedIds)
        {
            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                using (var cmd = new System.Data.SqlClient.SqlCommand("", con))
                {
                    var userInput = selectedIds;
                    var values = userInput.Split(',');
                    string currentuser = Convert.ToString(HttpContext.Current.User.Identity.Name);

                    con.Open();
                    var sql = "update PLUploadedData_TEMP set ApprovalStatus='DECLINED', CreatedBy=@CURRENTUSER where Id IN(";
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