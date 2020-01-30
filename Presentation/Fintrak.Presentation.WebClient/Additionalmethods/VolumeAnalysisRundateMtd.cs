using Fintrak.Presentation.WebClient.Models;
using Fintrak.Presentation.WebClient.Models.MPR;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Fintrak.Presentation.WebClient.Additionalmethods
{
    public class VolumeAnalysisRundateMtd
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

        public IEnumerable<VolumeAnalysisRundateRundateModel> GetAllAnalysisRundate()
        {
            //param = param.Replace("FORWARDSLASHXTER", "/");
            //param = param.Replace("DOTXTER", ".");

            List<VolumeAnalysisRundateRundateModel> ddbList = new List<VolumeAnalysisRundateRundateModel>();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("", con);

                //cmd.CommandText = "select * from Names where Id=@Id";
                //cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                cmd.CommandText = "select * from volume_analysis_rundate";
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var obu = new VolumeAnalysisRundateRundateModel();

                    obu.Id = reader["Id"] != DBNull.Value ? int.Parse(reader["Id"].ToString()) : 0;
                    obu.RunDate = reader["rundate"] != DBNull.Value ? DateTime.Parse(reader["rundate"].ToString()) : DateTime.Parse("1000-01-01");
                    obu.visible = reader["visible"] != DBNull.Value ? reader["visible"].ToString() : "";

                    ddbList.Add(obu);
                }
                con.Close();
            }
            return ddbList;
        } //========== end of the mtd

        public VolumeAnalysisRundateRundateModel GetVolumeAnalysisRundateById(int Id)
        {
            VolumeAnalysisRundateRundateModel ddbObj = new VolumeAnalysisRundateRundateModel();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("", con);

                //cmd.CommandText = "select * from Names where Id=@Id";
                //cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                cmd.CommandText = "select * from volume_analysis_rundate where Id = @ID";
                cmd.Parameters.AddWithValue("@ID", Id);
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //var obu = new VolumeAnalysisRundateRundateModel();

                    ddbObj.Id = reader["Id"] != DBNull.Value ? int.Parse(reader["Id"].ToString()) : 0;
                    ddbObj.RunDate = reader["rundate"] != DBNull.Value ? DateTime.Parse(reader["rundate"].ToString()) : DateTime.Parse("1000-01-01");
                    ddbObj.visible = reader["visible"] != DBNull.Value ? reader["visible"].ToString() : "";
                }
                con.Close();
            }
            return ddbObj;
            } //========== end of the mtd

       
        public void UpdateVolumeAnalysisRundate(VolumeAnalysisRundateRundateModel updatev)
        {
            //param = param.Replace("FORWARDSLASHXTER", "/");
            //param = param.Replace("DOTXTER", ".");

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                using (var cmd = new System.Data.SqlClient.SqlCommand("", con))
                {
                    var query = "update volume_analysis_rundate set visible=@VISIBLE where Id = @ID";

                    con.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@VISIBLE", updatev.visible);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        //public void DeleteIncomeAdjustmentVolumeSearch(int Id)
        //{          
        //    using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
        //    {
        //        var cmd = new System.Data.SqlClient.SqlCommand("income_adjustment_volumedelete", con);
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.CommandTimeout = 0;

        //        //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

        //        cmd.Parameters.Add(new SqlParameter
        //        {
        //            ParameterName = "ID",
        //            Value = Id,
        //        });              

        //        con.Open();
        //        cmd.ExecuteNonQuery();
        //        con.Close();
        //    }
        //}

    }
}