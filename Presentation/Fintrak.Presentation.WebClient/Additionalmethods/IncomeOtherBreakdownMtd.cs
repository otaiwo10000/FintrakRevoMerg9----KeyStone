using Fintrak.Presentation.WebClient.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Fintrak.Presentation.WebClient.Additionalmethods
{
    public class IncomeOtherBreakdownMtd
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;      

        public IEnumerable<Models.IncomeOtherBreakdownModel> GetIncomeOtherBreakdown()
        {
            List<IncomeOtherBreakdownModel> iobList = new List<IncomeOtherBreakdownModel>();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("spp_getincomeotherbreakdownforlatestyearmonth", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

                con.Open();
                //cmd.ExecuteNonQuery();
                //cmd2.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var iob = new IncomeOtherBreakdownModel();

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
                    iob.Volume = reader["Volume"] != DBNull.Value ? decimal.Parse(reader["Volume"].ToString()) : 0;
                    iob.Indicator = reader["Indicator"] != DBNull.Value ? reader["Indicator"].ToString() : "default";
                    iob.EntryStatus = reader["EntryStatus"] != DBNull.Value ? reader["EntryStatus"].ToString() : "default";
                    iob.DateEntered = reader["DateEntered"] != DBNull.Value ? DateTime.Parse(reader["DateEntered"].ToString()) : DateTime.Parse("1000-01-01");
                    iob.ProductCode = reader["ProductCode"] != DBNull.Value ? reader["ProductCode"].ToString() : "default";
                    iob.RunDate = reader["DateEntered"] != DBNull.Value ? DateTime.Parse(reader["DateEntered"].ToString()) : DateTime.Parse("1000-01-01");

                    iobList.Add(iob);
                }
                con.Close();
            }
            return iobList;
        } //========== end of the mtd

        public IEnumerable<Models.IncomeOtherBreakdownModel> GetIncomeOtherBreakdownUsingYearPeriod(int yr, int pr, string search)
        {
            search = search.Replace("FORWARDSLASHXTER", "/");
            search = search.Replace("DOTXTER", ".");

            List<IncomeOtherBreakdownModel> iobList = new List<IncomeOtherBreakdownModel>();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("spp_getincomeotherbreakdownusingyearmonth", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Year",
                    Value = yr,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Period",
                    Value = pr,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Search",
                    Value = search,
                });

                con.Open();
                //cmd.ExecuteNonQuery();
                //cmd2.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var iob = new IncomeOtherBreakdownModel();

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
                    iob.Volume = reader["Volume"] != DBNull.Value ? decimal.Parse(reader["Volume"].ToString()) : 0;
                    iob.Indicator = reader["Indicator"] != DBNull.Value ? reader["Indicator"].ToString() : "default";
                    iob.EntryStatus = reader["EntryStatus"] != DBNull.Value ? reader["EntryStatus"].ToString() : "default";
                    iob.DateEntered = reader["DateEntered"] != DBNull.Value ? DateTime.Parse(reader["DateEntered"].ToString()) : DateTime.Parse("1000-01-01");
                    iob.ProductCode = reader["ProductCode"] != DBNull.Value ? reader["ProductCode"].ToString() : "default";
                    iob.RunDate = reader["DateEntered"] != DBNull.Value ? DateTime.Parse(reader["DateEntered"].ToString()) : DateTime.Parse("1000-01-01");

                    iobList.Add(iob);
                }
                con.Close();
            }
            return iobList;
        } //========== end of the mtd



       


    }
}