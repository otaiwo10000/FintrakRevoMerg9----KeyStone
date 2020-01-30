using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Presentation.WebClient.Models;
using System.Configuration;

namespace Fintrak.Presentation.WebClient.Additionalmethods.ApprovalMtd
{
    public class MPRDownloadBaseFintrakFinalManualTEMPStatus
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;


        //================ methods to call starts IncomeNewDetails ==========================================
        public IEnumerable<MPRDownLoadBaseFintrakFinalMemoManualModel> MPRDownloadBaseFintrakFinalManualTEMP(string status)
        {
            List<MPRDownLoadBaseFintrakFinalMemoManualModel> obuList = new List<MPRDownLoadBaseFintrakFinalMemoManualModel>();
            var taketop = ConfigurationManager.AppSettings["TakeTop"].ToString().ToUpper();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("", con);

                //cmd.CommandText = "select * from Names where Id=@Id";
                //cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                //cmd.CommandText = "select top @TAKETOP * from mpr_downloadBase_FintrakFinalManual where ApprovalStatus=@STATUS";
                cmd.CommandText = "select top (@TAKETOP) * from mpr_downloadBase_FintrakFinalManual_TEMP where ApprovalStatus=@STATUS";
                cmd.Parameters.AddWithValue("@STATUS", status);
                cmd.Parameters.AddWithValue("@TAKETOP", taketop);
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var ddb = new MPRDownLoadBaseFintrakFinalMemoManualModel();

                    ddb.AccountNumber = reader["AccountNumber"] != DBNull.Value ? reader["AccountNumber"].ToString() : "default";
                    ddb.customername = reader["customername"] != DBNull.Value ? reader["customername"].ToString() : "default";
                    ddb.MIS_Code = reader["MIS_Code"] != DBNull.Value ? reader["MIS_Code"].ToString() : "default";
                    ddb.accountofficercode = reader["accountofficercode"] != DBNull.Value ? reader["accountofficercode"].ToString() : "default";
                    ddb.accountofficer = reader["accountofficer"] != DBNull.Value ? reader["accountofficer"].ToString() : "default";
                    ddb.ActualBalance = reader["ActualBalance"] != DBNull.Value ? decimal.Parse(reader["ActualBalance"].ToString()) : 0;
                    ddb.AverageBalance = reader["AverageBalance"] != DBNull.Value ? decimal.Parse(reader["AverageBalance"].ToString()) : 0;
                    //ddb.RevExp = reader["RevExp"] != DBNull.Value ? decimal.Parse(reader["RevExp"].ToString()) : 0;
                    ddb.ProductCode = reader["ProductCode"] != DBNull.Value ? reader["ProductCode"].ToString() : "default";
                    ddb.Category = reader["Category"] != DBNull.Value ? reader["Category"].ToString() : "default";
                    ddb.Currency_Type = reader["Currency_Type"] != DBNull.Value ? reader["Currency_Type"].ToString() : "default";
                    ddb.Period = reader["Period"] != DBNull.Value ? int.Parse(reader["Period"].ToString()) : 0;
                    ddb.Year = reader["Year"] != DBNull.Value ? int.Parse(reader["Year"].ToString()) : 0;

                    //ddb.GL_Sub = reader["GL_Sub"] != DBNull.Value ? reader["GL_Sub"].ToString() : "default";
                    ddb.Refno = reader["Refno"] != DBNull.Value ? reader["Refno"].ToString() : "default";
                    ddb.PoolRate = reader["PoolRate"] != DBNull.Value ? decimal.Parse(reader["PoolRate"].ToString()) : 0;
                  
                    ddb.Caption = reader["Caption"] != DBNull.Value ? reader["Caption"].ToString() : "default";
                    //ddb.Category_Filter = reader["Category_Filter"] != DBNull.Value ? reader["Category_Filter"].ToString() : "default";
                    ddb.Currency_Code = reader["Currency_Code"] != DBNull.Value ? reader["Currency_Code"].ToString() : "default";

                    obuList.Add(ddb);
                }
                con.Close();
            }
            return obuList;
        }

        public IEnumerable<IncomeOtherBreakdownTEMPModel> MPRDownloadBaseFintrakFinalManualTEMPusingparams(string status, string search)
        {
            List<IncomeOtherBreakdownTEMPModel> obuList = new List<IncomeOtherBreakdownTEMPModel>();
            var taketop = ConfigurationManager.AppSettings["TakeTop"].ToString().ToUpper();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("", con);

                //cmd.CommandText = "select * from Names where Id=@Id";
                //cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                cmd.CommandText = "select top @TAKETOP * from mpr_downloadBase_FintrakFinalManual_TEMP where ApprovalStatus=@STATUS and " +
                    "(MIS_Code like @searchval or Caption like @searchval or Accountnumber like @searchval or Caption like @searchval or " +
                    "CustomerName like @searchval or AccountOfficerCode like @searchval or ProductCode like @searchval)";
                cmd.Parameters.AddWithValue("@STATUS", status);
                cmd.Parameters.AddWithValue("@searchval", "%" + search + "%");
                cmd.Parameters.AddWithValue("@TAKETOP", taketop);
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var iob = new IncomeOtherBreakdownTEMPModel();

                    iob.ID = reader["ID"] != DBNull.Value ? int.Parse(reader["ID"].ToString()) : 0;
                    iob.MIS_Code = reader["MIS_Code"] != DBNull.Value ? reader["MIS_Code"].ToString() : "default";
                    iob.Caption = reader["Caption"] != DBNull.Value ? reader["Caption"].ToString() : "default";
                    iob.Accountnumber = reader["Accountnumber"] != DBNull.Value ? reader["Accountnumber"].ToString() : "default";
                    iob.Narrative = reader["Narrative"] != DBNull.Value ? reader["Narrative"].ToString() : "default";
                    iob.CustomerName = reader["CustomerName"] != DBNull.Value ? reader["CustomerName"].ToString() : "default";
                    iob.Period = reader["Period"] != DBNull.Value ? int.Parse(reader["Period"].ToString()) : 0;
                    iob.Year = reader["Year"] != DBNull.Value ? int.Parse(reader["Year"].ToString()) : 0;
                    iob.Amount = reader["Amount"] != DBNull.Value ? decimal.Parse(reader["Amount"].ToString()) : 0;
                    iob.AccountOfficer_Code = reader["AccountOfficer_Code"] != DBNull.Value ? reader["AccountOfficer_Code"].ToString() : "default";
                    //iob.Volume = reader["Volume"] != DBNull.Value ? decimal.Parse(reader["Volume"].ToString()) : 0;
                    //iob.Indicator = reader["Indicator"] != DBNull.Value ? reader["Indicator"].ToString() : "default";
                    iob.EntryStatus = reader["EntryStatus"] != DBNull.Value ? reader["EntryStatus"].ToString() : "default";
                    iob.DateEntered = reader["DateEntered"] != DBNull.Value ? DateTime.Parse(reader["DateEntered"].ToString()) : DateTime.Parse("1000-01-01");
                    iob.ProductCode = reader["ProductCode"] != DBNull.Value ? reader["ProductCode"].ToString() : "default";
                    iob.RunDate = reader["DateEntered"] != DBNull.Value ? DateTime.Parse(reader["DateEntered"].ToString()) : DateTime.Parse("1000-01-01");
                    iob.GLName = reader["GLName"] != DBNull.Value ? reader["GLName"].ToString() : "";
                    iob.Tran_ID = reader["Tran_ID"] != DBNull.Value ? reader["Tran_ID"].ToString() : "";
                    iob.Tran_Date = reader["Tran_Date"] != DBNull.Value ? DateTime.Parse(reader["Tran_Date"].ToString()) : DateTime.Parse("1000-01-01");

                    obuList.Add(iob);
                }
                con.Close();
            }
            return obuList;
        }
        //================ methods to call ends ============================================

        public void EditDownloadbasefintrakfinalmanualApproval(string selectedIds)
        {
            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                using (var cmd = new System.Data.SqlClient.SqlCommand("", con))
                {
                    var userInput = selectedIds;
                    var values = userInput.Split(',');
                    string currentuser = Convert.ToString(HttpContext.Current.User.Identity.Name);

                    con.Open();
                    var sql = "update mpr_downloadBase_FintrakFinalManual_TEMP set ApprovalStatus='APPROVED', ApprovalUser=@CURRENTUSER where Id IN(";
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

        public void EditDownloadbasefintrakfinalmanualDecline(string selectedIds)
        {
            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                using (var cmd = new System.Data.SqlClient.SqlCommand("", con))
                {
                    var userInput = selectedIds;
                    var values = userInput.Split(',');
                    string currentuser = Convert.ToString(HttpContext.Current.User.Identity.Name);

                    con.Open();
                    var sql = "update mpr_downloadBase_FintrakFinalManual_TEMP set ApprovalStatus='DECLINED', ApprovalUser=@CURRENTUSER where Id IN(";
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