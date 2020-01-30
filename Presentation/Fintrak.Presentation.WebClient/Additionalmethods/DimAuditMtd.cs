using Fintrak.Presentation.WebClient.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Fintrak.Presentation.WebClient.Additionalmethods
{
    public class DimAuditMtd
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDB2ndConnection"].ConnectionString;      

        public IEnumerable<Models.DimAuditModel> GetDimAudit()
        {
            List<DimAuditModel> obList = new List<DimAuditModel>();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("", con);

                con.Open();
                //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.CommandText = "SELECT top 3000 TableName, PkgName, ExecStartDT, ExecStopDT, ExtractRowCnt, InsertRowCnt, UpdateRowCnt, ErrorRowCnt, TableInitialRowCnt, TableFinalRowCnt, SuccessfulProcessingInd FROM [10.1.7.117\fintraksql].fintrakdaily.dbo.dimaudit";
                cmd.CommandText = "SELECT top 3000 TableName, PkgName, ExecStartDT, ExecStopDT, ExtractRowCnt, InsertRowCnt, UpdateRowCnt, ErrorRowCnt, TableInitialRowCnt, TableFinalRowCnt, SuccessfulProcessingInd FROM dimaudit";

                //cmd.CommandTimeout = 0;

                //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;


                //cmd.ExecuteNonQuery();
                //cmd2.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var ob = new DimAuditModel();

                    //iob.ID = reader["ID"] != DBNull.Value ? int.Parse(reader["ID"].ToString()) : 0;
                    ob.TableName = reader["TableName"] != DBNull.Value ? reader["TableName"].ToString() : "default";
                    ob.PkgName = reader["PkgName"] != DBNull.Value ? reader["PkgName"].ToString() : "default";
                    ob.ExecStartDT = reader["ExecStartDT"] != DBNull.Value ? DateTime.Parse(reader["ExecStartDT"].ToString()) : DateTime.Parse("1000-01-01");
                    ob.ExecStopDT = reader["ExecStopDT"] != DBNull.Value ? DateTime.Parse(reader["ExecStopDT"].ToString()) : DateTime.Parse("1000-01-01");
                    ob.ExtractRowCnt = reader["ExtractRowCnt"] != DBNull.Value ? Int64.Parse(reader["ExtractRowCnt"].ToString()) : 0;
                    ob.InsertRowCnt = reader["InsertRowCnt"] != DBNull.Value ? Int64.Parse(reader["InsertRowCnt"].ToString()) : 0;
                    ob.UpdateRowCnt = reader["UpdateRowCnt"] != DBNull.Value ? Int64.Parse(reader["UpdateRowCnt"].ToString()) : 0;
                    ob.ErrorRowCnt = reader["ErrorRowCnt"] != DBNull.Value ? Int64.Parse(reader["ErrorRowCnt"].ToString()) : 0;
                    ob.TableInitialRowCnt = reader["TableInitialRowCnt"] != DBNull.Value ? Int64.Parse(reader["TableInitialRowCnt"].ToString()) : 0;
                    ob.TableFinalRowCnt = reader["TableFinalRowCnt"] != DBNull.Value ? Int64.Parse(reader["TableFinalRowCnt"].ToString()) : 0;
                    ob.SuccessfulProcessingInd = reader["SuccessfulProcessingInd"] != DBNull.Value ? reader["SuccessfulProcessingInd"].ToString() : "default";
                    obList.Add(ob);
                }
                con.Close();
            }
            return obList;
        } //========== end of the mtd

        public IEnumerable<Models.DimAuditModel> GetDimAuditUsingParams(string search)
        {
            List<DimAuditModel> obList = new List<DimAuditModel>();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("", con);

                con.Open();
                //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "SELECT top 3000 TableName, PkgName, ExecStartDT, ExecStopDT, ExtractRowCnt, InsertRowCnt, UpdateRowCnt, ErrorRowCnt, TableInitialRowCnt, TableFinalRowCnt, SuccessfulProcessingInd FROM dimaudit "
                    + " where TableName like @searchval";
                cmd.Parameters.AddWithValue("@searchval", "%" + search + "%");
                //cmd.CommandTimeout = 0;

                //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;


                //cmd.ExecuteNonQuery();
                //cmd2.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var ob = new DimAuditModel();

                    //iob.ID = reader["ID"] != DBNull.Value ? int.Parse(reader["ID"].ToString()) : 0;
                    ob.TableName = reader["TableName"] != DBNull.Value ? reader["TableName"].ToString() : "default";
                    ob.PkgName = reader["PkgName"] != DBNull.Value ? reader["PkgName"].ToString() : "default";
                    ob.ExecStartDT = reader["ExecStartDT"] != DBNull.Value ? DateTime.Parse(reader["ExecStartDT"].ToString()) : DateTime.Parse("1000-01-01");
                    ob.ExecStopDT = reader["ExecStopDT"] != DBNull.Value ? DateTime.Parse(reader["ExecStopDT"].ToString()) : DateTime.Parse("1000-01-01");
                    ob.ExtractRowCnt = reader["ExtractRowCnt"] != DBNull.Value ? Int64.Parse(reader["ExtractRowCnt"].ToString()) : 0;
                    ob.InsertRowCnt = reader["InsertRowCnt"] != DBNull.Value ? Int64.Parse(reader["InsertRowCnt"].ToString()) : 0;
                    ob.UpdateRowCnt = reader["UpdateRowCnt"] != DBNull.Value ? Int64.Parse(reader["UpdateRowCnt"].ToString()) : 0;
                    ob.ErrorRowCnt = reader["ErrorRowCnt"] != DBNull.Value ? Int64.Parse(reader["ErrorRowCnt"].ToString()) : 0;
                    ob.TableInitialRowCnt = reader["TableInitialRowCnt"] != DBNull.Value ? Int64.Parse(reader["TableInitialRowCnt"].ToString()) : 0;
                    ob.TableFinalRowCnt = reader["TableFinalRowCnt"] != DBNull.Value ? Int64.Parse(reader["TableFinalRowCnt"].ToString()) : 0;
                    ob.SuccessfulProcessingInd = reader["SuccessfulProcessingInd"] != DBNull.Value ? reader["SuccessfulProcessingInd"].ToString() : "default";
                    obList.Add(ob);
                }
                con.Close();
            }
            return obList;
        } //========== end of the mtd

        //public IEnumerable<Models.IncomeAdjustmentSummaryModel> IncomeAdjustmentGetSBUSummary(int pr, int yr, string cp)
        //{
        //    cp = cp.Replace("FORWARDSLASHXTER", "/");
        //    cp = cp.Replace("DOTXTER", ".");

        //    List<IncomeAdjustmentSummaryModel> obList = new List<IncomeAdjustmentSummaryModel>();

        //    using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
        //    {
        //        var cmd = new System.Data.SqlClient.SqlCommand("Income_Adjustment_GetSBUSummary", con);
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.CommandTimeout = 0;

        //        //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

        //        cmd.Parameters.Add(new SqlParameter
        //        {
        //            ParameterName = "Year",
        //            Value = yr,
        //        });

        //        cmd.Parameters.Add(new SqlParameter
        //        {
        //            ParameterName = "Period",
        //            Value = pr,
        //        });

        //        cmd.Parameters.Add(new SqlParameter
        //        {
        //            ParameterName = "Caption",
        //            Value = cp,
        //        });

        //        con.Open();
        //        //cmd.ExecuteNonQuery();
        //        //cmd2.ExecuteNonQuery();

        //        SqlDataReader reader = cmd.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            var ob = new IncomeAdjustmentSummaryModel();

        //            //iob.ID = reader["ID"] != DBNull.Value ? int.Parse(reader["ID"].ToString()) : 0;
        //            ob.sbu_Code = reader["sbu_Code"] != DBNull.Value ? reader["sbu_Code"].ToString() : "default";
        //            ob.SBUName = reader["SBUName"] != DBNull.Value ? reader["SBUName"].ToString() : "default";
        //            ob.ActualBalance = reader["Actual"] != DBNull.Value ? decimal.Parse(reader["Actual"].ToString()) : 0;
        //            ob.AverageBalance = reader["Average"] != DBNull.Value ? decimal.Parse(reader["Average"].ToString()) : 0;
        //            ob.Rev_Exp = reader["RevExp"] != DBNull.Value ? decimal.Parse(reader["RevExp"].ToString()) : 0;

        //            obList.Add(ob);
        //        }
        //        con.Close();
        //    }
        //    return obList;
        //} //========== end of the mtd

        //public IEnumerable<Models.IncomeAdjustmentSummaryModel> IncomeAdjustmentVolCaptionSummary(int pr, int yr, string cp)
        //{
        //    cp = cp.Replace("FORWARDSLASHXTER", "/");
        //    cp = cp.Replace("DOTXTER", ".");

        //    List<IncomeAdjustmentSummaryModel> obList = new List<IncomeAdjustmentSummaryModel>();

        //    using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
        //    {
        //        var cmd = new System.Data.SqlClient.SqlCommand("Income_Adjustment_VolCaptionSummary", con);
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.CommandTimeout = 0;

        //        //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

        //        cmd.Parameters.Add(new SqlParameter
        //        {
        //            ParameterName = "Year",
        //            Value = yr,
        //        });

        //        cmd.Parameters.Add(new SqlParameter
        //        {
        //            ParameterName = "Period",
        //            Value = pr,
        //        });

        //        cmd.Parameters.Add(new SqlParameter
        //        {
        //            ParameterName = "Caption",
        //            Value = cp,
        //        });

        //        con.Open();
        //        //cmd.ExecuteNonQuery();
        //        //cmd2.ExecuteNonQuery();

        //        SqlDataReader reader = cmd.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            var ob = new IncomeAdjustmentSummaryModel();

        //            //iob.ID = reader["ID"] != DBNull.Value ? int.Parse(reader["ID"].ToString()) : 0;
        //            ob.MIS_Code = reader["MIS_Code"] != DBNull.Value ? reader["MIS_Code"].ToString() : "default";
        //            ob.ActualBalance = reader["Actual Balance"] != DBNull.Value ? decimal.Parse(reader["Actual Balance"].ToString()) : 0;
        //            ob.AverageBalance = reader["Average Balance"] != DBNull.Value ? decimal.Parse(reader["Average Balance"].ToString()) : 0;
        //            ob.Rev_Exp = reader["Rev/Exp"] != DBNull.Value ? decimal.Parse(reader["Rev/Exp"].ToString()) : 0;

        //            obList.Add(ob);
        //        }
        //        con.Close();
        //    }
        //    return obList;
        //} //========== end of the mtd

        //public IEnumerable<Models.IncomeAdjustmentSummaryModel> IncomeAdjustmentCommFeesSBUCaptionSummary(int pr, int yr, string cp)
        //{
        //    cp = cp.Replace("FORWARDSLASHXTER", "/");
        //    cp = cp.Replace("DOTXTER", ".");

        //    List<IncomeAdjustmentSummaryModel> obList = new List<IncomeAdjustmentSummaryModel>();

        //    using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
        //    {
        //        var cmd = new System.Data.SqlClient.SqlCommand("Income_Adjustment_CommFeesSBUCaptionSummary", con);
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.CommandTimeout = 0;

        //        //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

        //        cmd.Parameters.Add(new SqlParameter
        //        {
        //            ParameterName = "Year",
        //            Value = yr,
        //        });

        //        cmd.Parameters.Add(new SqlParameter
        //        {
        //            ParameterName = "Period",
        //            Value = pr,
        //        });

        //        cmd.Parameters.Add(new SqlParameter
        //        {
        //            ParameterName = "Caption",
        //            Value = cp,
        //        });

        //        con.Open();
        //        //cmd.ExecuteNonQuery();
        //        //cmd2.ExecuteNonQuery();

        //        SqlDataReader reader = cmd.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            var ob = new IncomeAdjustmentSummaryModel();

        //            //iob.ID = reader["ID"] != DBNull.Value ? int.Parse(reader["ID"].ToString()) : 0;
        //            ob.sbu_Code = reader["sbu_Code"] != DBNull.Value ? reader["sbu_Code"].ToString() : "default";
        //            ob.SBUName = reader["SBUName"] != DBNull.Value ? reader["SBUName"].ToString() : "default";
        //            ob.Amount = reader["Comm & Fees Income"] != DBNull.Value ? decimal.Parse(reader["Comm & Fees Income"].ToString()) : 0;

        //            obList.Add(ob);
        //        }
        //        con.Close();
        //    }
        //    return obList;
        //} //========== end of the mtd

        //public IEnumerable<Models.IncomeAdjustmentSummaryModel> IncomeAdjustmentCommFeesCaptionSummary(int pr, int yr, string cp)
        //{
        //    cp = cp.Replace("FORWARDSLASHXTER", "/");
        //    cp = cp.Replace("DOTXTER", ".");

        //    List<IncomeAdjustmentSummaryModel> obList = new List<IncomeAdjustmentSummaryModel>();

        //    using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
        //    {
        //        var cmd = new System.Data.SqlClient.SqlCommand("Income_Adjustment_CommFeesCaptionSummary", con);
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.CommandTimeout = 0;

        //        //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

        //        cmd.Parameters.Add(new SqlParameter
        //        {
        //            ParameterName = "Year",
        //            Value = yr,
        //        });

        //        cmd.Parameters.Add(new SqlParameter
        //        {
        //            ParameterName = "Period",
        //            Value = pr,
        //        });

        //        cmd.Parameters.Add(new SqlParameter
        //        {
        //            ParameterName = "Caption",
        //            Value = cp,
        //        });

        //        con.Open();
        //        //cmd.ExecuteNonQuery();
        //        //cmd2.ExecuteNonQuery();

        //        SqlDataReader reader = cmd.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            var ob = new IncomeAdjustmentSummaryModel();

        //            //iob.ID = reader["ID"] != DBNull.Value ? int.Parse(reader["ID"].ToString()) : 0;
        //            ob.MIS_Code = reader["MIS_Code"] != DBNull.Value ? reader["MIS_Code"].ToString() : "default";
        //            ob.Amount = reader["Comm & Fees Income"] != DBNull.Value ? decimal.Parse(reader["Comm & Fees Income"].ToString()) : 0;

        //            obList.Add(ob);
        //        }
        //        con.Close();
        //    }
        //    return obList;
        //} //========== end of the mtd

        //public IEnumerable<Models.IncomeAdjustmentSummaryModel> GetIncomeAdjustmentVolProdSummary(int pr, int yr)
        //{
        //    List<IncomeAdjustmentSummaryModel> obList = new List<IncomeAdjustmentSummaryModel>();

        //    using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
        //    {
        //        var cmd = new System.Data.SqlClient.SqlCommand("Income_Adjustment_VolProdSummary", con);
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.CommandTimeout = 0;

        //        //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

        //        cmd.Parameters.Add(new SqlParameter
        //        {
        //            ParameterName = "Year",
        //            Value = yr,
        //        });

        //        cmd.Parameters.Add(new SqlParameter
        //        {
        //            ParameterName = "Period",
        //            Value = pr,
        //        });

        //        con.Open();
        //        //cmd.ExecuteNonQuery();
        //        //cmd2.ExecuteNonQuery();

        //        SqlDataReader reader = cmd.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            var ob = new IncomeAdjustmentSummaryModel();

        //            //iob.ID = reader["ID"] != DBNull.Value ? int.Parse(reader["ID"].ToString()) : 0;
        //            ob.ProductCode = reader["ProductCode"] != DBNull.Value ? reader["ProductCode"].ToString() : "default";
        //            ob.Caption = reader["Caption"] != DBNull.Value ? reader["Caption"].ToString() : "default";
        //            ob.ActualBalance = reader["Actual"] != DBNull.Value ? decimal.Parse(reader["Actual"].ToString()) : 0;
        //            ob.AverageBalance = reader["Average"] != DBNull.Value ? decimal.Parse(reader["Average"].ToString()) : 0;
        //            ob.Rev_Exp = reader["RevExp"] != DBNull.Value ? decimal.Parse(reader["RevExp"].ToString()) : 0;
        //            ob.Category = reader["Category"] != DBNull.Value ? reader["Category"].ToString() : "default";
        //            ob.CurrencyType = reader["Currency_Type"] != DBNull.Value ? reader["Currency_Type"].ToString() : "default";
        //            ob.Period = reader["Period"] != DBNull.Value ? int.Parse(reader["Period"].ToString()) : 0;
        //            ob.Year = reader["Year"] != DBNull.Value ? int.Parse(reader["Year"].ToString()) : 0;

        //            obList.Add(ob);
        //        }
        //        con.Close();
        //    }
        //    return obList;
        //} //========== end of the mtd

    }
}