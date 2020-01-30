using Fintrak.Presentation.WebClient.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Fintrak.Presentation.WebClient.Additionalmethods
{
    public class YearPeriodMtd
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;
       


        public int Year()
        {
            int res = 0;
            string query = "SELECT max(year) from income_setup_daily";
            using (System.Data.SqlClient.SqlConnection myConnection = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                myConnection.Open();
                using (System.Data.SqlClient.SqlCommand myCommand = new System.Data.SqlClient.SqlCommand(query, myConnection))
                {
                     res = (int)myCommand.ExecuteScalar(); // returns the first column of the first row
                }
                return res;
            }
        }

        public int Period()
        {
            int res = 0;
            string query = "SELECT max(CurrentPeriod) from income_setup_daily";
            using (System.Data.SqlClient.SqlConnection myConnection = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                myConnection.Open();
                using (System.Data.SqlClient.SqlCommand myCommand = new System.Data.SqlClient.SqlCommand(query, myConnection))
                {
                    res = (int)myCommand.ExecuteScalar(); // returns the first column of the first row
                }
                return res;
            }
        } //========== end of the mtd
    }

    public class YearPeriod
    {
        public int Year { get; set; }
        public int Period { get; set; }
    }

}