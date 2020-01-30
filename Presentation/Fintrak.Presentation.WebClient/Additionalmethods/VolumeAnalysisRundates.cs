using Fintrak.Presentation.WebClient.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Fintrak.Presentation.WebClient.Additionalmethods
{
    public class VolumeAnalysisRundatesMtd
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDB2ndConnection"].ConnectionString;

        // string linksev = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["linkserver"].Trim());
        //string linksev = @"[127.0.0.1\dell].fintrak.dbo.volume_analysis_rundates";

        public List<VolumeAnalysisRundatesModel> GetAllVolumeAnalysisRundates()
        {
            List<VolumeAnalysisRundatesModel> obuList = new List<VolumeAnalysisRundatesModel>();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("", con);

                //string ls = linksev + ".volume_analysis_rundates";

                con.Open();
                //cmd.CommandText = "SELECT * from [10.1.14.49\fintrack].fintrakreport.dbo.volume_analysis_rundates";
                //cmd.CommandText = " SELECT * from @LINKSERVER ";
                //var sqlString = string.Format("insert into {0} (time, date, pin) values (@time, @date, @pin)", linksev);

                //var sqlString = string.Format("SELECT * from {0} ", linksev);
                //cmd.CommandText = sqlString;

                // cmd.Parameters.AddWithValue("@LINKSERVER", linksev + ".dbo.volume_analysis_rundates");

                cmd.CommandText = "SELECT * from volume_analysis_rundates";

                 System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var ob = new VolumeAnalysisRundatesModel();

                    ob.Id = reader["Id"] != DBNull.Value ? Int32.Parse(reader["Id"].ToString()) : 0;
                    ob.rundate = reader["rundate"] != DBNull.Value ? Convert.ToDateTime(reader["rundate"].ToString()).Date : Convert.ToDateTime("2001-01-01").Date;
                    ob.visible = reader["visible"] != DBNull.Value ? Convert.ToString(reader["visible"]) : "";

                    obuList.Add(ob);
                }
                con.Close();
            }
            return obuList;
        }

        public VolumeAnalysisRundatesModel GetVolumeAnalysisRundates(int Id)
        {
            VolumeAnalysisRundatesModel ob = new VolumeAnalysisRundatesModel();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("", con);

                con.Open();
                cmd.CommandText = "SELECT * from volume_analysis_rundates where Id=@ID";
                cmd.Parameters.AddWithValue("@ID", Id);
                //cmd.Parameters.AddWithValue("@TAKETOP", taketop);
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ob.Id = reader["Id"] != DBNull.Value ? Int32.Parse(reader["Id"].ToString()) : 0;
                    ob.rundate = reader["rundate"] != DBNull.Value ? Convert.ToDateTime(reader["rundate"].ToString()).Date : Convert.ToDateTime("2001-01-01").Date;
                    ob.visible = reader["visible"] != DBNull.Value ? Convert.ToString(reader["visible"]) : "";
                }
                con.Close();
            }
            return ob;
        }


        public void InsertVolumeAnalysisRundates(VolumeAnalysisRundatesModel vrd)
        {
            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("", con);

                con.Open();
                cmd.CommandText = "Insert into volume_analysis_rundates (rundate, visible) values(@RUNDATE, @VISIBLE)";
                cmd.Parameters.AddWithValue("@RUNDATE", vrd.rundate);
                cmd.Parameters.AddWithValue("@VISIBLE", vrd.visible);

                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void UpdateVolumeAnalysisRundates(VolumeAnalysisRundatesModel vrd)
        {
            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("", con);

                con.Open();
                cmd.CommandText = "Update volume_analysis_rundates set visible = @VISIBLE where Id=@ID";
                cmd.Parameters.AddWithValue("@ID", vrd.Id);
                cmd.Parameters.AddWithValue("@VISIBLE", vrd.visible);

                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void DeleteVolumeAnalysisRundates(int Id)
        {
            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("", con);

                con.Open();
                cmd.CommandText = "delete volume_analysis_rundates where Id=@ID";
                cmd.Parameters.AddWithValue("@ID", Id);

                cmd.ExecuteNonQuery();
                con.Close();
            }
        }


    }
     
}





//var sqlString = string.Format("insert into {0} (time, date, pin) values (@time, @date, @pin)", tblname)
//SqlCommand com = new SqlCommand(sqlString);


//    string tblname = "; DROP TABLE users;";
//var sqlString = string.Format("insert into {0} (time, date, pin) values (@time, @date, @pin)", tblname)