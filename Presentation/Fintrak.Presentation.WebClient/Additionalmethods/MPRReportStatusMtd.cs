using Fintrak.Client.MPR.Entities;
using Fintrak.Presentation.WebClient.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Fintrak.Presentation.WebClient.Additionalmethods
{
    public class MPRReportStatusMtd
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDB2ndConnection"].ConnectionString;

        public List<MPRReportStatus> GetAllMPRReportStatus()
        {
            List<MPRReportStatus> obuList = new List<MPRReportStatus>();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("", con);

                con.Open();
               
                cmd.CommandText = "SELECT * from MPRReportStatus";

                 System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var ob = new MPRReportStatus();

                    ob.MPRReportStatusId = reader["MPRReportStatusId"] != DBNull.Value ? Int32.Parse(reader["MPRReportStatusId"].ToString()) : 0;
                    ob.Year = reader["Year"] != DBNull.Value ? Int32.Parse(reader["Year"].ToString()) : 0;
                    ob.Period = reader["Period"] != DBNull.Value ? Convert.ToString(reader["Period"]) : "";
                    ob.Status = reader["Status"] != DBNull.Value ? Convert.ToString(reader["Status"]) : "";

                    obuList.Add(ob);
                }
                con.Close();
            }
            return obuList;
        }

        public MPRReportStatus GetMPRReportStatus(int Id)
        {
            MPRReportStatus ob = new MPRReportStatus();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("", con);

                con.Open();
                cmd.CommandText = "SELECT * from MPRReportStatus where MPRReportStatusId=@ID";
                cmd.Parameters.AddWithValue("@ID", Id);
                //cmd.Parameters.AddWithValue("@TAKETOP", taketop);
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ob.MPRReportStatusId = reader["MPRReportStatusId"] != DBNull.Value ? Int32.Parse(reader["MPRReportStatusId"].ToString()) : 0;
                    ob.Year = reader["Year"] != DBNull.Value ? Int32.Parse(reader["Year"].ToString()) : 0;
                    ob.Period = reader["Period"] != DBNull.Value ? Convert.ToString(reader["Period"]) : "";
                    ob.Status = reader["Status"] != DBNull.Value ? Convert.ToString(reader["Status"]) : "";
                }
                con.Close();
            }
            return ob;
        }


        //public void InsertVolumeAnalysisRundates(VolumeAnalysisRundatesModel vrd)
        //{
        //    using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
        //    {
        //        var cmd = new System.Data.SqlClient.SqlCommand("", con);

        //        con.Open();
        //        cmd.CommandText = "Insert into volume_analysis_rundates (date, visible) values(@DATE, @VISIBLE)";
        //        cmd.Parameters.AddWithValue("@DATE", vrd.date);
        //        cmd.Parameters.AddWithValue("@VISIBLE", vrd.visible);

        //        cmd.ExecuteNonQuery();
        //        con.Close();
        //    }
        //}

        public void UpdateMPRReportStatus(MPRReportStatus vrd)
        {
            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("", con);

                con.Open();
                cmd.CommandText = "Update MPRReportStatus set Period=@PERIOD, Status = @STATUS  where MPRReportStatusId=@ID";
                cmd.Parameters.AddWithValue("@ID", vrd.MPRReportStatusId);
                cmd.Parameters.AddWithValue("@PERIOD", vrd.Period);
                cmd.Parameters.AddWithValue("@STATUS", vrd.Status);

                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //public void DeleteVolumeAnalysisRundates(int Id)
        //{
        //    using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
        //    {
        //        var cmd = new System.Data.SqlClient.SqlCommand("", con);

        //        con.Open();
        //        cmd.CommandText = "delete volume_analysis_rundates where Id=@ID";
        //        cmd.Parameters.AddWithValue("@ID", Id);

        //        cmd.ExecuteNonQuery();
        //        con.Close();
        //    }
        //}


    }
     
}





//var sqlString = string.Format("insert into {0} (time, date, pin) values (@time, @date, @pin)", tblname)
//SqlCommand com = new SqlCommand(sqlString);


//    string tblname = "; DROP TABLE users;";
//var sqlString = string.Format("insert into {0} (time, date, pin) values (@time, @date, @pin)", tblname)