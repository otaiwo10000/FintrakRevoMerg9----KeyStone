
using Fintrak.Presentation.WebClient.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Fintrak.Presentation.WebClient.Additionalmethods
{
    public class IncomeAdjustmentSummaryMtd
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;      

        public IEnumerable<Models.IncomeAdjustmentSummaryModel> GetIncomeAdjustmentVolSummary(int pr, int yr)
        {
            List<IncomeAdjustmentSummaryModel> obList = new List<IncomeAdjustmentSummaryModel>();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("Income_Adjustment_VolSummary", con);
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

                con.Open();
                //cmd.ExecuteNonQuery();
                //cmd2.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var ob = new IncomeAdjustmentSummaryModel();

                    //iob.ID = reader["ID"] != DBNull.Value ? int.Parse(reader["ID"].ToString()) : 0;
                    ob.Caption = reader["Caption"] != DBNull.Value ? reader["Caption"].ToString() : "default";
                    ob.ActualBalance = reader["Actual Balance"] != DBNull.Value ? decimal.Parse(reader["Actual Balance"].ToString()) : 0;
                    ob.AverageBalance = reader["Average Balance"] != DBNull.Value ? decimal.Parse(reader["Average Balance"].ToString()) : 0;
                    ob.Rev_Exp = reader["Rev/Exp"] != DBNull.Value ? decimal.Parse(reader["Rev/Exp"].ToString()) : 0;
                    ob.Category = reader["Category"] != DBNull.Value ? reader["Category"].ToString() : "default";
                    ob.CurrencyType = reader["Currency Type"] != DBNull.Value ? reader["Currency Type"].ToString() : "default";
                    ob.RunDate = reader["Run Date"] != DBNull.Value ? DateTime.Parse(reader["Run Date"].ToString()) : DateTime.Parse("1000-01-01");
                    ob.Period = reader["Period"] != DBNull.Value ? int.Parse(reader["Period"].ToString()) : 0;
                    ob.Year = reader["Year"] != DBNull.Value ? int.Parse(reader["Year"].ToString()) : 0;
                  
                    obList.Add(ob);
                }
                con.Close();
            }
            return obList;
        } //========== end of the mtd

        public IEnumerable<Models.IncomeAdjustmentSummaryModel> IncomeAdjustmentGetSBUSummary(int pr, int yr, string cp)
        {
            cp = cp.Replace("FORWARDSLASHXTER", "/");
            cp = cp.Replace("DOTXTER", ".");

            List<IncomeAdjustmentSummaryModel> obList = new List<IncomeAdjustmentSummaryModel>();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("Income_Adjustment_GetSBUSummary", con);
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
                    ParameterName = "Caption",
                    Value = cp,
                });

                con.Open();
                //cmd.ExecuteNonQuery();
                //cmd2.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var ob = new IncomeAdjustmentSummaryModel();

                    //iob.ID = reader["ID"] != DBNull.Value ? int.Parse(reader["ID"].ToString()) : 0;
                    ob.sbu_Code = reader["sbu_Code"] != DBNull.Value ? reader["sbu_Code"].ToString() : "default";
                    ob.SBUName = reader["SBUName"] != DBNull.Value ? reader["SBUName"].ToString() : "default";
                    ob.ActualBalance = reader["Actual"] != DBNull.Value ? decimal.Parse(reader["Actual"].ToString()) : 0;
                    ob.AverageBalance = reader["Average"] != DBNull.Value ? decimal.Parse(reader["Average"].ToString()) : 0;
                    ob.Rev_Exp = reader["RevExp"] != DBNull.Value ? decimal.Parse(reader["RevExp"].ToString()) : 0;
                   
                    obList.Add(ob);
                }
                con.Close();
            }
            return obList;
        } //========== end of the mtd

        public IEnumerable<Models.IncomeAdjustmentSummaryModel> IncomeAdjustmentVolCaptionSummary(int pr, int yr, string cp)
        {
            cp = cp.Replace("FORWARDSLASHXTER", "/");
            cp = cp.Replace("DOTXTER", ".");

            List<IncomeAdjustmentSummaryModel> obList = new List<IncomeAdjustmentSummaryModel>();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("Income_Adjustment_VolCaptionSummary", con);
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
                    ParameterName = "Caption",
                    Value = cp,
                });

                con.Open();
                //cmd.ExecuteNonQuery();
                //cmd2.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var ob = new IncomeAdjustmentSummaryModel();

                    //iob.ID = reader["ID"] != DBNull.Value ? int.Parse(reader["ID"].ToString()) : 0;
                    ob.MIS_Code = reader["MIS_Code"] != DBNull.Value ? reader["MIS_Code"].ToString() : "default";
                    ob.ActualBalance = reader["Actual Balance"] != DBNull.Value ? decimal.Parse(reader["Actual Balance"].ToString()) : 0;
                    ob.AverageBalance = reader["Average Balance"] != DBNull.Value ? decimal.Parse(reader["Average Balance"].ToString()) : 0;
                    ob.Rev_Exp = reader["Rev/Exp"] != DBNull.Value ? decimal.Parse(reader["Rev/Exp"].ToString()) : 0;

                    obList.Add(ob);
                }
                con.Close();
            }
            return obList;
        } //========== end of the mtd

        public IEnumerable<Models.IncomeAdjustmentSummaryModel> IncomeAdjustmentCommFeesSBUCaptionSummary(int pr, int yr, string cp)
        {
            cp = cp.Replace("FORWARDSLASHXTER", "/");
            cp = cp.Replace("DOTXTER", ".");

            List<IncomeAdjustmentSummaryModel> obList = new List<IncomeAdjustmentSummaryModel>();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("Income_Adjustment_CommFeesSBUCaptionSummary", con);
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
                    ParameterName = "Caption",
                    Value = cp,
                });

                con.Open();
                //cmd.ExecuteNonQuery();
                //cmd2.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var ob = new IncomeAdjustmentSummaryModel();

                    //iob.ID = reader["ID"] != DBNull.Value ? int.Parse(reader["ID"].ToString()) : 0;
                    ob.sbu_Code = reader["sbu_Code"] != DBNull.Value ? reader["sbu_Code"].ToString() : "default";
                    ob.SBUName = reader["SBUName"] != DBNull.Value ? reader["SBUName"].ToString() : "default";
                    ob.Amount = reader["Comm & Fees Income"] != DBNull.Value ? decimal.Parse(reader["Comm & Fees Income"].ToString()) : 0;

                    obList.Add(ob);
                }
                con.Close();
            }
            return obList;
        } //========== end of the mtd

        public IEnumerable<Models.IncomeAdjustmentSummaryModel> IncomeAdjustmentCommFeesCaptionSummary(int pr, int yr, string cp)
        {
            cp = cp.Replace("FORWARDSLASHXTER", "/");
            cp = cp.Replace("DOTXTER", ".");

            List<IncomeAdjustmentSummaryModel> obList = new List<IncomeAdjustmentSummaryModel>();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("Income_Adjustment_CommFeesCaptionSummary", con);
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
                    ParameterName = "Caption",
                    Value = cp,
                });

                con.Open();
                //cmd.ExecuteNonQuery();
                //cmd2.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var ob = new IncomeAdjustmentSummaryModel();

                    //iob.ID = reader["ID"] != DBNull.Value ? int.Parse(reader["ID"].ToString()) : 0;
                    ob.MIS_Code = reader["MIS_Code"] != DBNull.Value ? reader["MIS_Code"].ToString() : "default";
                    ob.Amount = reader["Comm & Fees Income"] != DBNull.Value ? decimal.Parse(reader["Comm & Fees Income"].ToString()) : 0;

                    obList.Add(ob);
                }
                con.Close();
            }
            return obList;
        } //========== end of the mtd

        public IEnumerable<Models.IncomeAdjustmentSummaryModel> GetIncomeAdjustmentVolProdSummary(int pr, int yr)
        {
            List<IncomeAdjustmentSummaryModel> obList = new List<IncomeAdjustmentSummaryModel>();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("Income_Adjustment_VolProdSummary", con);
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

                con.Open();
                //cmd.ExecuteNonQuery();
                //cmd2.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var ob = new IncomeAdjustmentSummaryModel();

                    //iob.ID = reader["ID"] != DBNull.Value ? int.Parse(reader["ID"].ToString()) : 0;
                    ob.ProductCode = reader["ProductCode"] != DBNull.Value ? reader["ProductCode"].ToString() : "default";
                    ob.Caption = reader["Caption"] != DBNull.Value ? reader["Caption"].ToString() : "default";
                    ob.ActualBalance = reader["Actual"] != DBNull.Value ? decimal.Parse(reader["Actual"].ToString()) : 0;
                    ob.AverageBalance = reader["Average"] != DBNull.Value ? decimal.Parse(reader["Average"].ToString()) : 0;
                    ob.Rev_Exp = reader["RevExp"] != DBNull.Value ? decimal.Parse(reader["RevExp"].ToString()) : 0;
                    ob.Category = reader["Category"] != DBNull.Value ? reader["Category"].ToString() : "default";
                    ob.CurrencyType = reader["Currency_Type"] != DBNull.Value ? reader["Currency_Type"].ToString() : "default";
                    ob.Period = reader["Period"] != DBNull.Value ? int.Parse(reader["Period"].ToString()) : 0;
                    ob.Year = reader["Year"] != DBNull.Value ? int.Parse(reader["Year"].ToString()) : 0;

                    obList.Add(ob);
                }
                con.Close();
            }
            return obList;
        } //========== end of the mtd

    }
}