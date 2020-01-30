using Fintrak.Presentation.WebClient.Models;
using Fintrak.Shared.MPR.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Fintrak.Presentation.WebClient.Additionalmethods
{
    public class OtherInfo
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

        public List<string> GetMainCAptionFromOtherInfo()
        {
            List<string> maincaptionList = new List<string>();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("spp_OtherInfo", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

                con.Open();
                //cmd.ExecuteNonQuery();
                //cmd2.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                   string maincaption = reader["maincaption"] != DBNull.Value ? reader["maincaption"].ToString() : "";

                    maincaptionList.Add(maincaption);
                }
                con.Close();
            }
            return maincaptionList;
        } //========== end of the mtd

        public ExtractionProgressModel GetExtractionProgressInfo()
        {
            List<ExtractionProgressTableModel> progressInfo = new List<ExtractionProgressTableModel>();
            
            ExtractionProgressModel exProg = new ExtractionProgressModel();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("spp_extractionprogressinfo", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

                con.Open();
                //cmd.ExecuteNonQuery();
                //cmd2.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ExtractionProgressTableModel pObj = new ExtractionProgressTableModel();

                    // progressInfo.TotalCount = reader["teambankid"] != DBNull.Value ? int.Parse(reader["teambankid"].ToString()) : 0;
                    pObj.PackageName  = reader["processname"] != DBNull.Value ? reader["processname"].ToString() : "";
                    pObj.EstimatedTime = reader["EstimatedTime"] != DBNull.Value ? Convert.ToDateTime(reader["EstimatedTime"].ToString()) : Convert.ToDateTime("00:00:00.000");
                    pObj.Status = reader["Status"] != DBNull.Value ? reader["Status"].ToString() : "";
                    //pObj.Status = pObj.Status.ToUpper().Trim();

                    progressInfo.Add(pObj);
                }
                con.Close();

                string status = "DONE";
                exProg.TotalCount = progressInfo.Count();
                exProg.CompletedPkgCount = progressInfo.Where(x => x.Status.ToUpper().Trim() == status).Count();
                exProg.PackageName = progressInfo.Select(x => x.PackageName).FirstOrDefault();
                exProg.EstimatedTimeString = progressInfo.Select(x => x.EstimatedTime).FirstOrDefault().ToString("HH:mm:ss");
                //exProg.EstimatedTimeString = progressInfo.Select(x => x.EstimatedTime).FirstOrDefault().ToString("yyyy-MM-dd");
                //string dt = DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss tt");

                if (exProg.TotalCount == 0)
                {
                    exProg.CompletedPkgCount = exProg.TotalCount;
                }
            }
            return exProg;
        } //========== end of the mtd

        public ProcessProgressModel GetProcessProgressInfo()
        {
            List<ProcessProgressTableModel> progressInfo = new List<ProcessProgressTableModel>();

            ProcessProgressModel exProg = new ProcessProgressModel();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("spp_processprogressinfo", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

                con.Open();
                //cmd.ExecuteNonQuery();
                //cmd2.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ProcessProgressTableModel pObj = new ProcessProgressTableModel();

                    // progressInfo.TotalCount = reader["teambankid"] != DBNull.Value ? int.Parse(reader["teambankid"].ToString()) : 0;
                    pObj.PackageName = reader["processname"] != DBNull.Value ? reader["processname"].ToString() : "";
                    pObj.EstimatedTime = reader["EstimatedTime"] != DBNull.Value ? Convert.ToDateTime(reader["EstimatedTime"].ToString()) : Convert.ToDateTime("00:00:00.000");
                    pObj.Status = reader["Status"] != DBNull.Value ? reader["Status"].ToString() : "";
                    //pObj.Status = pObj.Status.ToUpper().Trim();

                    progressInfo.Add(pObj);
                }
                con.Close();

                string status = "DONE";
                exProg.TotalCount = progressInfo.Count();
                exProg.CompletedPkgCount = progressInfo.Where(x => x.Status.ToUpper().Trim() == status).Count();
                exProg.PackageName = progressInfo.Select(x => x.PackageName).FirstOrDefault();
                exProg.EstimatedTimeString = progressInfo.Select(x => x.EstimatedTime).FirstOrDefault().ToString("HH:mm:ss");
                //exProg.EstimatedTimeString = progressInfo.Select(x => x.EstimatedTime).FirstOrDefault().ToString("yyyy-MM-dd");
                //string dt = DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss tt");
            }
            return exProg;
        } //========== end of the mtd

        public List<AccountCustomerModel> GetAccountCustomerInfo(string SearchValue)
        {
            SearchValue = SearchValue.Replace("FORWARDSLASHXTER", "/");
            SearchValue = SearchValue.Replace("DOTXTER", ".");

            List<AccountCustomerModel> accountcustomerInfo = new List<AccountCustomerModel>();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("spp_acountnumbers_customernames", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "searchvalue",
                    Value = SearchValue,
                });

                con.Open();
                //cmd.ExecuteNonQuery();
                //cmd2.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    AccountCustomerModel pObj = new AccountCustomerModel();

                    // progressInfo.TotalCount = reader["teambankid"] != DBNull.Value ? int.Parse(reader["teambankid"].ToString()) : 0;
                    pObj.AccountNumber = reader["AcccountNumber"] != DBNull.Value ? reader["AcccountNumber"].ToString() : "";
                    pObj.CustomerName = reader["CustomerName"] != DBNull.Value ? reader["CustomerName"].ToString() : "";
                    //pObj.Status = reader["Status"] != DBNull.Value ? reader["Status"].ToString() : "";
                    //pObj.Status = pObj.Status.ToUpper().Trim();

                    accountcustomerInfo.Add(pObj);
                }
                con.Close();
            }
            return accountcustomerInfo;
        } //========== end of the mtd

        public IncomeSetup GetLatestIncomeSetUp()
        {
            IncomeSetup incomesetup = new IncomeSetup();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("latestincomesetup", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;


                //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

                //cmd.Parameters.Add(new SqlParameter
                //{
                //    ParameterName = "searchvalue",
                //    Value = SearchValue,
                //});

                con.Open();
                //cmd.ExecuteNonQuery();
                //cmd2.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //AccountCustomerModel pObj = new AccountCustomerModel();

                    // progressInfo.TotalCount = reader["teambankid"] != DBNull.Value ? int.Parse(reader["teambankid"].ToString()) : 0;
                    incomesetup.Year = reader["Year"] != DBNull.Value ? int.Parse(reader["Year"].ToString()) : 0;
                    incomesetup.CurrentPeriod = reader["CurrentPeriod"] != DBNull.Value ? int.Parse(reader["CurrentPeriod"].ToString()) : 0;
                }
                con.Close();
            }
            return incomesetup;
        } //========== end of the mtd

    }
}