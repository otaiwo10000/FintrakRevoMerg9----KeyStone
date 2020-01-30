using Fintrak.Presentation.WebClient.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Fintrak.Presentation.WebClient.Additionalmethods
{
    public class MPRDownLoadBaseFintrakFinalMemoManualMtd
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;      

        public IEnumerable<Models.MPRDownLoadBaseFintrakFinalMemoManualModel> GetMPRDownloadBaseFintrakFinalManual()
        {
            List<MPRDownLoadBaseFintrakFinalMemoManualModel> ddbList = new List<MPRDownLoadBaseFintrakFinalMemoManualModel>();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("spp_mprdownloadbasefintrakfinalmanualforlatestyearmonth", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

                con.Open();
                //cmd.ExecuteNonQuery();
                //cmd2.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var ddb = new MPRDownLoadBaseFintrakFinalMemoManualModel();

                    ddb.ID = reader["ID"] != DBNull.Value ? int.Parse(reader["ID"].ToString()) : 0;

                    ddb.AccountNumber = reader["AccountNumber"] != DBNull.Value ? reader["AccountNumber"].ToString() : "default";
                    ddb.customername = reader["customername"] != DBNull.Value ? reader["customername"].ToString() : "default";
                    //ddb.sbuCode = reader["sbuCode"] != DBNull.Value ? reader["sbuCode"].ToString() : "default";
                    ddb.MIS_Code = reader["MIS_Code"] != DBNull.Value ? reader["MIS_Code"].ToString() : "default";
                    ddb.accountofficercode = reader["accountofficercode"] != DBNull.Value ? reader["accountofficercode"].ToString() : "default";
                    ddb.accountofficer = reader["accountofficer"] != DBNull.Value ? reader["accountofficer"].ToString() : "default";
                    ddb.ActualBalance = reader["ActualBalance"] != DBNull.Value ? decimal.Parse(reader["ActualBalance"].ToString()) : 0;
                    ddb.AverageBalance = reader["AverageBalance"] != DBNull.Value ? decimal.Parse(reader["AverageBalance"].ToString()) : 0;
                    ddb.RevExp = reader["RevExp"] != DBNull.Value ? decimal.Parse(reader["RevExp"].ToString()) : 0;
                    //ddb.interestRate = reader["interestRate"] != DBNull.Value ? decimal.Parse(reader["interestRate"].ToString()) : 0;
                    ddb.ProductCode = reader["ProductCode"] != DBNull.Value ? reader["ProductCode"].ToString() : "default";
                    ddb.Category = reader["Category"] != DBNull.Value ? reader["Category"].ToString() : "default";
                    ddb.Currency_Type = reader["Currency_Type"] != DBNull.Value ? reader["Currency_Type"].ToString() : "default";
                    //ddb.postedDate = reader["postedDate"] != DBNull.Value ? DateTime.Parse(reader["postedDate"].ToString()) : DateTime.Parse("1000-01-01");
                    ddb.Period = reader["Period"] != DBNull.Value ? int.Parse(reader["Period"].ToString()) : 0;
                    ddb.Year = reader["Year"] != DBNull.Value ? int.Parse(reader["Year"].ToString()) : 0;

                    //ddb.EntryStatus = reader["EntryStatus"] != DBNull.Value ? reader["EntryStatus"].ToString() : "default";
                    ddb.GL_Sub = reader["GL_Sub"] != DBNull.Value ? reader["GL_Sub"].ToString() : "default";
                    ddb.Refno = reader["Refno"] != DBNull.Value ? reader["Refno"].ToString() : "default";
                    ddb.PoolRate = reader["PoolRate"] != DBNull.Value ? decimal.Parse(reader["PoolRate"].ToString()) : 0;
                    //ddb.BankMaxRate = reader["BankMaxRate"] != DBNull.Value ? decimal.Parse(reader["BankMaxRate"].ToString()) : 0;
                    //ddb.CustomerRating = reader["CustomerRating"] != DBNull.Value ? reader["CustomerRating"].ToString() : "default";

                    ddb.EffYield = reader["EffYield"] != DBNull.Value ? decimal.Parse(reader["EffYield"].ToString()) : 0;
                    ddb.ExpRev = reader["ExpRev"] != DBNull.Value ? decimal.Parse(reader["ExpRev"].ToString()) : 0;

                    ddb.Caption = reader["Caption"] != DBNull.Value ? reader["Caption"].ToString() : "default";
                    ddb.Category_Filter = reader["Category_Filter"] != DBNull.Value ? reader["Category_Filter"].ToString() : "default";
                    ddb.Currency_Code = reader["Currency_Code"] != DBNull.Value ? reader["Currency_Code"].ToString() : "default";
                    //ddb.Unit = reader["Unit"] != DBNull.Value ? reader["Unit"].ToString() : "default";
                    ddb.EntryDate = reader["EntryDate"] != DBNull.Value ? DateTime.Parse(reader["EntryDate"].ToString()) : DateTime.Parse("1000-01-01");
                   
                    ddbList.Add(ddb);
                }
                con.Close();
            }
            return ddbList;
        } //========== end of the mtd

        public IEnumerable<Models.MPRDownLoadBaseFintrakFinalMemoManualModel> GetMPRDownloadBaseFintrakFinalManualUsingYearPeriod(int yr, int pr, string param)
        {
            param = param.Replace("FORWARDSLASHXTER", "/");
            param = param.Replace("DOTXTER", ".");

            List<MPRDownLoadBaseFintrakFinalMemoManualModel> ddbList = new List<MPRDownLoadBaseFintrakFinalMemoManualModel>();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("spp_mprdownloadbasefintrakfinalmanualusingparams", con);
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
                    ParameterName = "search",
                    Value = param,
                });

                con.Open();
                //cmd.ExecuteNonQuery();
                //cmd2.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var ddb = new MPRDownLoadBaseFintrakFinalMemoManualModel();

                    ddb.ID = reader["ID"] != DBNull.Value ? int.Parse(reader["ID"].ToString()) : 0;

                    ddb.AccountNumber = reader["AccountNumber"] != DBNull.Value ? reader["AccountNumber"].ToString() : "default";
                    ddb.customername = reader["customername"] != DBNull.Value ? reader["customername"].ToString() : "default";

                    //ddb.sbuCode = reader["sbuCode"] != DBNull.Value ? reader["sbuCode"].ToString() : "default";

                    ddb.MIS_Code = reader["MIS_Code"] != DBNull.Value ? reader["MIS_Code"].ToString() : "default";
                    ddb.accountofficercode = reader["accountofficercode"] != DBNull.Value ? reader["accountofficercode"].ToString() : "default";
                    ddb.accountofficer = reader["accountofficer"] != DBNull.Value ? reader["accountofficer"].ToString() : "default";
                    ddb.ActualBalance = reader["ActualBalance"] != DBNull.Value ? decimal.Parse(reader["ActualBalance"].ToString()) : 0;
                    ddb.AverageBalance = reader["AverageBalance"] != DBNull.Value ? decimal.Parse(reader["AverageBalance"].ToString()) : 0;
                    ddb.RevExp = reader["RevExp"] != DBNull.Value ? decimal.Parse(reader["RevExp"].ToString()) : 0;
                    //ddb.interestRate = reader["interestRate"] != DBNull.Value ? decimal.Parse(reader["interestRate"].ToString()) : 0;
                    ddb.ProductCode = reader["ProductCode"] != DBNull.Value ? reader["ProductCode"].ToString() : "default";
                    ddb.Category = reader["Category"] != DBNull.Value ? reader["Category"].ToString() : "default";
                    ddb.Currency_Type = reader["Currency_Type"] != DBNull.Value ? reader["Currency_Type"].ToString() : "default";
                    //ddb.postedDate = reader["postedDate"] != DBNull.Value ? DateTime.Parse(reader["postedDate"].ToString()) : DateTime.Parse("1000-01-01");
                    ddb.Period = reader["Period"] != DBNull.Value ? int.Parse(reader["Period"].ToString()) : 0;
                    ddb.Year = reader["Year"] != DBNull.Value ? int.Parse(reader["Year"].ToString()) : 0;

                    //ddb.EntryStatus = reader["EntryStatus"] != DBNull.Value ? reader["EntryStatus"].ToString() : "default";
                    ddb.GL_Sub = reader["GL_Sub"] != DBNull.Value ? reader["GL_Sub"].ToString() : "default";
                    ddb.Refno = reader["Refno"] != DBNull.Value ? reader["Refno"].ToString() : "default";
                    ddb.PoolRate = reader["PoolRate"] != DBNull.Value ? decimal.Parse(reader["PoolRate"].ToString()) : 0;
                    //ddb.BankMaxRate = reader["BankMaxRate"] != DBNull.Value ? decimal.Parse(reader["BankMaxRate"].ToString()) : 0;
                    //ddb.CustomerRating = reader["CustomerRating"] != DBNull.Value ? reader["CustomerRating"].ToString() : "default";

                    ddb.EffYield = reader["EffYield"] != DBNull.Value ? decimal.Parse(reader["EffYield"].ToString()) : 0;
                    ddb.ExpRev = reader["ExpRev"] != DBNull.Value ? decimal.Parse(reader["ExpRev"].ToString()) : 0;

                    ddb.Caption = reader["Caption"] != DBNull.Value ? reader["Caption"].ToString() : "default";
                    ddb.Category_Filter = reader["Category_Filter"] != DBNull.Value ? reader["Category_Filter"].ToString() : "default";
                    ddb.Currency_Code = reader["Currency_Code"] != DBNull.Value ? reader["Currency_Code"].ToString() : "default";
                    //ddb.Unit = reader["Unit"] != DBNull.Value ? reader["Unit"].ToString() : "default";
                    ddb.EntryDate = reader["Entry_Date"] != DBNull.Value ? DateTime.Parse(reader["Entry_Date"].ToString()) : DateTime.Parse("1000-01-01");

                    ddbList.Add(ddb);
                }
                con.Close();
            }
            return ddbList;
        } //========== end of the mtd

    }
}