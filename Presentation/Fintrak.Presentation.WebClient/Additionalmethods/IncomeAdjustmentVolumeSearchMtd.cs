using Fintrak.Presentation.WebClient.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Fintrak.Presentation.WebClient.Additionalmethods
{
    public class IncomeAdjustmentVolumeSearchMtd
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

        public IEnumerable<Models.IncomeAdjustmentVolumeSearchModel> GetIncomeAdjustmentVolumeSearchUsingYearPeriod(int yr, int pr, string param)
        {
            param = param.Replace("FORWARDSLASHXTER", "/");
            param = param.Replace("DOTXTER", ".");

            List<IncomeAdjustmentVolumeSearchModel> ddbList = new List<IncomeAdjustmentVolumeSearchModel>();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("Income_adjustment_VolumeSearch", con);
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
                    var ddb = new IncomeAdjustmentVolumeSearchModel();

                    ddb.ID = reader["S/N"] != DBNull.Value ? int.Parse(reader["S/N"].ToString()) : 0;
                    ddb.Caption = reader["Caption"] != DBNull.Value ? reader["Caption"].ToString() : "default";
                    ddb.AccountNumber = reader["AccountNumber"] != DBNull.Value ? reader["AccountNumber"].ToString() : "default";
                    ddb.Refno = reader["Refno"] != DBNull.Value ? reader["Refno"].ToString() : "default";
                    ddb.customername = reader["customername"] != DBNull.Value ? reader["customername"].ToString() : "default";
                    ddb.EntryStatus = reader["EntryStatus"] != DBNull.Value ? reader["EntryStatus"].ToString() : "default";
                    ddb.sbuCode = reader["SBU Code"] != DBNull.Value ? reader["SBU Code"].ToString() : "default";
                    ddb.MIS_Code = reader["MIS Code"] != DBNull.Value ? reader["MIS Code"].ToString() : "default";
                    ddb.accountofficercode = reader["accountofficercode"] != DBNull.Value ? reader["accountofficercode"].ToString() : "default";
                    //ddb.accountofficer = reader["accountofficer"] != DBNull.Value ? reader["accountofficer"].ToString() : "default";
                    ddb.ActualBalance = reader["ActualBalance"] != DBNull.Value ? decimal.Parse(reader["ActualBalance"].ToString()) : 0;
                    ddb.AverageBalance = reader["AverageBalance"] != DBNull.Value ? decimal.Parse(reader["AverageBalance"].ToString()) : 0;
                    ddb.RevExp = reader["RevExp"] != DBNull.Value ? decimal.Parse(reader["RevExp"].ToString()) : 0;
                    ddb.interestRate = reader["interestRate"] != DBNull.Value ? decimal.Parse(reader["interestRate"].ToString()) : 0;
                    ddb.PoolRate = reader["PoolRate"] != DBNull.Value ? decimal.Parse(reader["PoolRate"].ToString()) : 0;
                    ddb.ProductCode = reader["ProductCode"] != DBNull.Value ? reader["ProductCode"].ToString() : "default";
                    ddb.Category = reader["Category"] != DBNull.Value ? reader["Category"].ToString() : "default";
                    ddb.Currency_Type = reader["Currency_Type"] != DBNull.Value ? reader["Currency_Type"].ToString() : "default";
                    ddb.GL_Sub = reader["GL_Sub"] != DBNull.Value ? reader["GL_Sub"].ToString() : "default";
                    ddb.postedDate = reader["postedDate"] != DBNull.Value ? DateTime.Parse(reader["postedDate"].ToString()) : DateTime.Parse("1000-01-01");
                    //ddb.Period = reader["Period"] != DBNull.Value ? int.Parse(reader["Period"].ToString()) : 0;
                    //ddb.Year = reader["Year"] != DBNull.Value ? int.Parse(reader["Year"].ToString()) : 0;
                    ddb.Indicator = reader["Indicator"] != DBNull.Value ? reader["Indicator"].ToString() : "default";

                    ddbList.Add(ddb);
                }
                con.Close();
            }
            return ddbList;
        } //========== end of the mtd

        public Models.IncomeAdjustmentVolumeSearchModel GetIncomeAdjustmentById(int Id)
        {
            var ddb = new IncomeAdjustmentVolumeSearchModel();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("Income_adjustment_VolumeSearch_Id", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "ID",
                    Value = Id,
                });

              
                con.Open();
                //cmd.ExecuteNonQuery();
                //cmd2.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ddb.ID = reader["S/N"] != DBNull.Value ? int.Parse(reader["S/N"].ToString()) : 0;
                    ddb.Caption = reader["Caption"] != DBNull.Value ? reader["Caption"].ToString() : "default";
                    ddb.AccountNumber = reader["AccountNumber"] != DBNull.Value ? reader["AccountNumber"].ToString() : "default";
                    ddb.Refno = reader["Refno"] != DBNull.Value ? reader["Refno"].ToString() : "default";
                    ddb.customername = reader["customername"] != DBNull.Value ? reader["customername"].ToString() : "default";
                    ddb.EntryStatus = reader["EntryStatus"] != DBNull.Value ? reader["EntryStatus"].ToString() : "default";
                    ddb.sbuCode = reader["SBU Code"] != DBNull.Value ? reader["SBU Code"].ToString() : "default";
                    ddb.MIS_Code = reader["MIS Code"] != DBNull.Value ? reader["MIS Code"].ToString() : "default";
                    ddb.accountofficercode = reader["accountofficercode"] != DBNull.Value ? reader["accountofficercode"].ToString() : "default";
                    //ddb.accountofficer = reader["accountofficer"] != DBNull.Value ? reader["accountofficer"].ToString() : "default";
                    ddb.ActualBalance = reader["ActualBalance"] != DBNull.Value ? decimal.Parse(reader["ActualBalance"].ToString()) : 0;
                    ddb.AverageBalance = reader["AverageBalance"] != DBNull.Value ? decimal.Parse(reader["AverageBalance"].ToString()) : 0;
                    ddb.RevExp = reader["RevExp"] != DBNull.Value ? decimal.Parse(reader["RevExp"].ToString()) : 0;
                    ddb.interestRate = reader["interestRate"] != DBNull.Value ? decimal.Parse(reader["interestRate"].ToString()) : 0;
                    ddb.PoolRate = reader["PoolRate"] != DBNull.Value ? decimal.Parse(reader["PoolRate"].ToString()) : 0;
                    ddb.ProductCode = reader["ProductCode"] != DBNull.Value ? reader["ProductCode"].ToString() : "default";
                    ddb.Category = reader["Category"] != DBNull.Value ? reader["Category"].ToString() : "default";
                    ddb.Currency_Type = reader["Currency_Type"] != DBNull.Value ? reader["Currency_Type"].ToString() : "default";
                    ddb.GL_Sub = reader["GL_Sub"] != DBNull.Value ? reader["GL_Sub"].ToString() : "default";
                    ddb.postedDate = reader["postedDate"] != DBNull.Value ? DateTime.Parse(reader["postedDate"].ToString()) : DateTime.Parse("1000-01-01");
                    ddb.Period = reader["Period"] != DBNull.Value ? int.Parse(reader["Period"].ToString()) : 0;
                    ddb.Year = reader["Year"] != DBNull.Value ? int.Parse(reader["Year"].ToString()) : 0;
                    ddb.Indicator = reader["Indicator"] != DBNull.Value ? reader["Indicator"].ToString() : "default";
                }
                con.Close();
            }
            return ddb;
        } //========== end of the mtd

        //public void AddIncomeAdjustmentVolumeSearch(string MISCODE, string ACCTCODE, string ACCOUNTNUMBER, string CUSTNAME, double BALANCE, double AVERAGE, double INTEREST, string PRODUCTCODE, string CATEGORY, string CURRENCY)
        public void AddIncomeAdjustmentVolumeSearch(AddIncomeAdjustmentVolumeSearchModel addv)
        {
            //param = param.Replace("FORWARDSLASHXTER", "/");
            //param = param.Replace("DOTXTER", ".");

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("Income_Adjustment_VolumeAdd", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "miscode",
                    Value = addv.MISCODE,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Acctcode",
                    Value = addv.ACCTCODE,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "AccountNumber",
                    Value = addv.ACCOUNTNUMBER,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Custname",
                    Value = addv.CUSTNAME,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Balance",
                    Value = addv.BALANCE,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Average",
                    Value = addv.AVERAGE,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Intrest",
                    Value = addv.INTEREST,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "ProductCode",
                    Value = addv.PRODUCTCODE,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Category",
                    Value = addv.CATEGORY,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Currency",
                    Value = addv.CURRENCY,
                });

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //public void UpdateIncomeAdjustmentVolumeSearch(Int64 ID, string MISCODE, string ACCTCODE, int PERIOD, int YEAR, string ACCOUNTNUMBER, string PRODUCTCODE, string CATEGORY, string CURRENCY, string CUSTNAME, string CAPTION, string ACCOUNTNUMBER1)
        public void UpdateIncomeAdjustmentVolumeSearch(UpdateIncomeAdjustmentVolumeSearchModel updatev)
        {
            //param = param.Replace("FORWARDSLASHXTER", "/");
            //param = param.Replace("DOTXTER", ".");

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("income_adjustment_volumeupdate", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "id",
                    Value = updatev.ID,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "miscode",
                    Value = updatev.MISCODE,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Acctcode",
                    Value = updatev.ACCTCODE,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "period",
                    Value = updatev.PERIOD,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "YEAR",
                    Value = updatev.YEAR,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "accountno",
                    Value = updatev.ACCOUNTNUMBER,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "prodcode",
                    Value = updatev.PRODUCTCODE,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "category",
                    Value = updatev.CATEGORY,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Currency",
                    Value = updatev.CURRENCY,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Custname",
                    Value = updatev.CUSTNAME,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Caption",
                    Value = updatev.CAPTION,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "accountno1",
                    Value = updatev.ACCOUNTNUMBER1,
                });

              

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void DeleteIncomeAdjustmentVolumeSearch(int Id)
        {          
            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("income_adjustment_volumedelete", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "ID",
                    Value = Id,
                });              

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

    }
}