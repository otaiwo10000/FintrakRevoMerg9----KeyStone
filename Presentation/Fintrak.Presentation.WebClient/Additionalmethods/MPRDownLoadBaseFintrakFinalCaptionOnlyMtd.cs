using Fintrak.Presentation.WebClient.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Fintrak.Presentation.WebClient.Additionalmethods
{
    public class MPRDownLoadBaseFintrakFinalCaptionOnlyMtd
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;      

        public IEnumerable<Models.MPRDownLoadBaseFintrakFinalCaptionOnlyModel> GetDDBFintrakManual()
        {
            List<MPRDownLoadBaseFintrakFinalCaptionOnlyModel> iobList = new List<MPRDownLoadBaseFintrakFinalCaptionOnlyModel>();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("spp_downloadBasefintrakfinal_captionONLY", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

                con.Open();
                //cmd.ExecuteNonQuery();
                //cmd2.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var iob = new MPRDownLoadBaseFintrakFinalCaptionOnlyModel();
                    
                    iob.Caption = reader["Caption"] != DBNull.Value ? reader["Caption"].ToString() : "default";
                    
                    iobList.Add(iob);
                }
                con.Close();
            }
            return iobList;
        } //========== end of the mtd

        public IEnumerable<Models.MPRDownLoadBaseFintrakFinalCaptionOnlyModel> GetIncomeLineCaption()
        {
            List<MPRDownLoadBaseFintrakFinalCaptionOnlyModel> iobList = new List<MPRDownLoadBaseFintrakFinalCaptionOnlyModel>();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("spp_commfeelinecaptions", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

                con.Open();
                //cmd.ExecuteNonQuery();
                //cmd2.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var iob = new MPRDownLoadBaseFintrakFinalCaptionOnlyModel();

                    iob.Caption = reader["Caption"] != DBNull.Value ? reader["Caption"].ToString() : "default";

                    iobList.Add(iob);
                }
                con.Close();
            }
            return iobList;
        } //========== end of the mtd

    }
}