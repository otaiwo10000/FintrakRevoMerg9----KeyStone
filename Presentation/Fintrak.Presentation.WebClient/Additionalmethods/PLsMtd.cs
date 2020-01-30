using Fintrak.Presentation.WebClient.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Fintrak.Presentation.WebClient.Additionalmethods
{
    public class IncomeIncomeNewDetailsMtd
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;      

        public IEnumerable<Models.IncomeIncomeNewDetailsModel> GetIncomeIncomeNeDetails()
        {
            List<IncomeIncomeNewDetailsModel> obList = new List<IncomeIncomeNewDetailsModel>();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("", con);

                con.Open();
                //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "SELECT top 10 * FROM Income_IncomeNewDetails";
                //cmd.CommandTimeout = 0;

                //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

               
                //cmd.ExecuteNonQuery();
                //cmd2.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var ob = new IncomeIncomeNewDetailsModel();

                    //iob.ID = reader["ID"] != DBNull.Value ? int.Parse(reader["ID"].ToString()) : 0;
                    ob.MIS_Code = reader["MIS_Code"] != DBNull.Value ? reader["MIS_Code"].ToString() : "default";
                    ob.Caption = reader["Caption"] != DBNull.Value ? reader["Caption"].ToString() : "default";
                    ob.Accountnumber = reader["Accountnumber"] != DBNull.Value ? reader["Accountnumber"].ToString() : "default";
                    ob.CustomerName = reader["CustomerName"] != DBNull.Value ? reader["CustomerName"].ToString() : "default";
                    ob.AccountOfficer_Code = reader["AccountOfficer_Code"] != DBNull.Value ? reader["AccountOfficer_Code"].ToString() : "default";
                    ob.Period = reader["Period"] != DBNull.Value ? Int64.Parse(reader["Period"].ToString()) : 0;
                    ob.Year = reader["Year"] != DBNull.Value ? Int64.Parse(reader["Year"].ToString()) : 0;
                    ob.Narrative = reader["Narrative"] != DBNull.Value ? reader["Narrative"].ToString() : "default";
                    ob.EntryStatus = reader["EntryStatus"] != DBNull.Value ? reader["EntryStatus"].ToString() : "default";
                    ob.GLName = reader["GLName"] != DBNull.Value ? reader["GLName"].ToString() : "default";
                    ob.Amount = reader["Amount"] != DBNull.Value ? float.Parse(reader["Amount"].ToString()) : 0;
                    ob.DateEntered = reader["DateEntered"] != DBNull.Value ? DateTime.Parse(reader["DateEntered"].ToString()) : DateTime.Parse("1000-01-01");
                    ob.ProductCode = reader["ProductCode"] != DBNull.Value ? reader["ProductCode"].ToString() : "default";
                    ob.Tran_ID = reader["Tran_ID"] != DBNull.Value ? reader["Tran_ID"].ToString() : "default";
                    ob.Tran_Date = reader["Tran_Date"] != DBNull.Value ? DateTime.Parse(reader["Tran_Date"].ToString()) : DateTime.Parse("1000-01-01");
                    ob.RunDate = reader["RunDate"] != DBNull.Value ? DateTime.Parse(reader["RunDate"].ToString()) : DateTime.Parse("1000-01-01");
                    ob.StaffID = reader["StaffID"] != DBNull.Value ? reader["StaffID"].ToString() : "default";                    
                    ob.Sbucode = reader["Sbucode"] != DBNull.Value ? reader["Sbucode"].ToString() : "default";
                    obList.Add(ob);
                }
                con.Close();
            }
            return obList;
        } //========== end of the mtd

        public IEnumerable<Models.IncomeIncomeNewDetailsModel> GetIncomeIncomeNewDetailsUsingParams(string search, int period)
        {
            List<IncomeIncomeNewDetailsModel> obList = new List<IncomeIncomeNewDetailsModel>();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("", con);

                con.Open();
                //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "SELECT top 10 * FROM Income_IncomeNewDetails "
                    + " where Accountnumber like @searchval and Period = @period";
                cmd.Parameters.AddWithValue("@searchval", "%" + search + "%");
                cmd.Parameters.AddWithValue("@period", period);
                //cmd.CommandTimeout = 0;

                //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;


                //cmd.ExecuteNonQuery();
                //cmd2.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var ob = new IncomeIncomeNewDetailsModel();

                    //iob.ID = reader["ID"] != DBNull.Value ? int.Parse(reader["ID"].ToString()) : 0;
                    ob.MIS_Code = reader["MIS_Code"] != DBNull.Value ? reader["MIS_Code"].ToString() : "default";
                    ob.Caption = reader["Caption"] != DBNull.Value ? reader["Caption"].ToString() : "default";
                    ob.Accountnumber = reader["Accountnumber"] != DBNull.Value ? reader["Accountnumber"].ToString() : "default";
                    ob.CustomerName = reader["CustomerName"] != DBNull.Value ? reader["CustomerName"].ToString() : "default";
                    ob.AccountOfficer_Code = reader["AccountOfficer_Code"] != DBNull.Value ? reader["AccountOfficer_Code"].ToString() : "default";
                    ob.Period = reader["Period"] != DBNull.Value ? Int64.Parse(reader["Period"].ToString()) : 0;
                    ob.Year = reader["Year"] != DBNull.Value ? Int64.Parse(reader["Year"].ToString()) : 0;
                    ob.Narrative = reader["Narrative"] != DBNull.Value ? reader["Narrative"].ToString() : "default";
                    ob.EntryStatus = reader["EntryStatus"] != DBNull.Value ? reader["EntryStatus"].ToString() : "default";
                    ob.GLName = reader["GLName"] != DBNull.Value ? reader["GLName"].ToString() : "default";
                    ob.Amount = reader["Amount"] != DBNull.Value ? float.Parse(reader["Amount"].ToString()) : 0;
                    ob.DateEntered = reader["DateEntered"] != DBNull.Value ? DateTime.Parse(reader["DateEntered"].ToString()) : DateTime.Parse("1000-01-01");
                    ob.ProductCode = reader["ProductCode"] != DBNull.Value ? reader["ProductCode"].ToString() : "default";
                    ob.Tran_ID = reader["Tran_ID"] != DBNull.Value ? reader["Tran_ID"].ToString() : "default";
                    ob.Tran_Date = reader["Tran_Date"] != DBNull.Value ? DateTime.Parse(reader["Tran_Date"].ToString()) : DateTime.Parse("1000-01-01");
                    ob.RunDate = reader["RunDate"] != DBNull.Value ? DateTime.Parse(reader["RunDate"].ToString()) : DateTime.Parse("1000-01-01");
                    ob.StaffID = reader["StaffID"] != DBNull.Value ? reader["StaffID"].ToString() : "default";
                    ob.Sbucode = reader["Sbucode"] != DBNull.Value ? reader["Sbucode"].ToString() : "default";
                    obList.Add(ob);
                }
                con.Close();
            }
            return obList;
        } //========== end of the mtd


    }

    public class IncomeIncomeOtherBreakdownMtd
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

        public IEnumerable<Models.IncomeIncomeOtherBreakdownModel> GetIncomeIncomeOtherBreakdown()
        {
            List<IncomeIncomeOtherBreakdownModel> obList = new List<IncomeIncomeOtherBreakdownModel>();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("", con);

                con.Open();
                //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "SELECT top 10 * FROM Income_Income_OtherBreakdown";
                //cmd.CommandTimeout = 0;

                //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;


                //cmd.ExecuteNonQuery();
                //cmd2.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var ob = new IncomeIncomeOtherBreakdownModel();

                    //iob.ID = reader["ID"] != DBNull.Value ? int.Parse(reader["ID"].ToString()) : 0;
                    ob.MIS_Code = reader["MIS_Code"] != DBNull.Value ? reader["MIS_Code"].ToString() : "default";
                    ob.Caption = reader["Caption"] != DBNull.Value ? reader["Caption"].ToString() : "default";
                    ob.Accountnumber = reader["Accountnumber"] != DBNull.Value ? reader["Accountnumber"].ToString() : "default";
                    ob.Narrative = reader["Narrative"] != DBNull.Value ? reader["Narrative"].ToString() : "default";
                    ob.CustomerName = reader["CustomerName"] != DBNull.Value ? reader["CustomerName"].ToString() : "default";
                    ob.Period = reader["Period"] != DBNull.Value ? int.Parse(reader["Period"].ToString()) : 0;
                    ob.Year = reader["Year"] != DBNull.Value ? int.Parse(reader["Year"].ToString()) : 0;
                    ob.Amount = reader["Amount"] != DBNull.Value ? decimal.Parse(reader["Amount"].ToString()) : 0;
                    ob.AccountOfficer_Code = reader["AccountOfficer_Code"] != DBNull.Value ? reader["AccountOfficer_Code"].ToString() : "default";
                    ob.Volume = reader["Volume"] != DBNull.Value ? Decimal.Parse(reader["Volume"].ToString()) : 0;
                    ob.Indicator = reader["Indicator"] != DBNull.Value ? reader["Indicator"].ToString() : "default";
                    ob.EntryStatus = reader["EntryStatus"] != DBNull.Value ? reader["EntryStatus"].ToString() : "default";
                    ob.DateEntered = reader["DateEntered"] != DBNull.Value ? DateTime.Parse(reader["DateEntered"].ToString()) : DateTime.Parse("1000-01-01");
                    ob.ProductCode = reader["ProductCode"] != DBNull.Value ? reader["ProductCode"].ToString() : "default";
                    ob.RunDate = reader["RunDate"] != DBNull.Value ? DateTime.Parse(reader["RunDate"].ToString()) : DateTime.Parse("1000-01-01");
                    ob.EntryDate = reader["EntryDate"] != DBNull.Value ? DateTime.Parse(reader["EntryDate"].ToString()) : DateTime.Parse("1000-01-01");
                    obList.Add(ob);
                }
                con.Close();
            }
            return obList;
        } //========== end of the mtd

        public IEnumerable<Models.IncomeIncomeOtherBreakdownModel> GetIncomeIncomeOtherBreakdownUsingParams(string search, int period)
        {
            List<IncomeIncomeOtherBreakdownModel> obList = new List<IncomeIncomeOtherBreakdownModel>();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("", con);

                con.Open();
                //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "SELECT top 10 * FROM Income_Income_OtherBreakdown "
                    + " where Accountnumber like @searchval and Period = @period";
                cmd.Parameters.AddWithValue("@searchval", "%" + search + "%");
                cmd.Parameters.AddWithValue("@period", period);
                //cmd.CommandTimeout = 0;

                //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;


                //cmd.ExecuteNonQuery();
                //cmd2.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var ob = new IncomeIncomeOtherBreakdownModel();

                    //iob.ID = reader["ID"] != DBNull.Value ? int.Parse(reader["ID"].ToString()) : 0;
                    ob.MIS_Code = reader["MIS_Code"] != DBNull.Value ? reader["MIS_Code"].ToString() : "default";
                    ob.Caption = reader["Caption"] != DBNull.Value ? reader["Caption"].ToString() : "default";
                    ob.Accountnumber = reader["Accountnumber"] != DBNull.Value ? reader["Accountnumber"].ToString() : "default";
                    ob.Narrative = reader["Narrative"] != DBNull.Value ? reader["Narrative"].ToString() : "default";
                    ob.CustomerName = reader["CustomerName"] != DBNull.Value ? reader["CustomerName"].ToString() : "default";
                    ob.Period = reader["Period"] != DBNull.Value ? int.Parse(reader["Period"].ToString()) : 0;
                    ob.Year = reader["Year"] != DBNull.Value ? int.Parse(reader["Year"].ToString()) : 0;
                    ob.Amount = reader["Amount"] != DBNull.Value ? decimal.Parse(reader["Amount"].ToString()) : 0;
                    ob.AccountOfficer_Code = reader["AccountOfficer_Code"] != DBNull.Value ? reader["AccountOfficer_Code"].ToString() : "default";
                    ob.Volume = reader["Volume"] != DBNull.Value ? Decimal.Parse(reader["Volume"].ToString()) : 0;
                    ob.Indicator = reader["Indicator"] != DBNull.Value ? reader["Indicator"].ToString() : "default";
                    ob.EntryStatus = reader["EntryStatus"] != DBNull.Value ? reader["EntryStatus"].ToString() : "default";
                    ob.DateEntered = reader["DateEntered"] != DBNull.Value ? DateTime.Parse(reader["DateEntered"].ToString()) : DateTime.Parse("1000-01-01");
                    ob.ProductCode = reader["ProductCode"] != DBNull.Value ? reader["ProductCode"].ToString() : "default";
                    ob.RunDate = reader["RunDate"] != DBNull.Value ? DateTime.Parse(reader["RunDate"].ToString()) : DateTime.Parse("1000-01-01");
                    ob.EntryDate = reader["EntryDate"] != DBNull.Value ? DateTime.Parse(reader["EntryDate"].ToString()) : DateTime.Parse("1000-01-01");
                    obList.Add(ob);
                }
                con.Close();
            }
            return obList;
        } //========== end of the mtd


    }
}
