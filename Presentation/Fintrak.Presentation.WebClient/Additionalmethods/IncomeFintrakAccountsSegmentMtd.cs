using Fintrak.Presentation.WebClient.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Fintrak.Presentation.WebClient.Additionalmethods
{
    public class IncomeFintrakAccountsSegmentMtd
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;      

        public string LoadIncomeFintrakAccountsSegment()
        {
            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("income_sbu_update", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                ////cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

                //cmd.Parameters.Add(new SqlParameter
                //{
                //    ParameterName = "ID",
                //    Value = Id,
                //});

                con.Open();
                cmd.ExecuteNonQuery();
                string status = "success";
                con.Close();

                return status;
            }
        }


    }
}