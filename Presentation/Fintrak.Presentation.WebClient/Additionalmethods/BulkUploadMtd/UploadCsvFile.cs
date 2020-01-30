using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Presentation.WebClient.Models;
using System.IO;

namespace Fintrak.Presentation.WebClient.Additionalmethods.ApprovalMtd
{
    public class UploadCsvFile
    {
        // string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;
        string connectionString = string.Empty;
        public UploadCsvFile()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;
        }

        public string UploadcsvMtd(string tablename)
        {
            string uploaduser = Convert.ToString(System.Web.HttpContext.Current.User.Identity.Name);   //current user
                                                                                                       //DateTime datetimeuploaded = DateTime.Now;
            string res = "NA";
            int newId = 0;
            //string tablename = string.Empty;
            string filePath = string.Empty;

            UpLoadCSVFileModel ulf = new UpLoadCSVFileModel();

            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                // Get the uploaded image from the Files collection
                var httpPostedFile = System.Web.HttpContext.Current.Request.Files["csvfile"];
                filePath = System.Configuration.ConfigurationManager.AppSettings["UploadedFilePath"].ToString();

                //get other form data
                //var username = System.Web.HttpContext.Current.Request.Form["Username"];

                if (httpPostedFile != null)
                {
                    // Validate the uploaded file(optional)

                    //ulf.Filename = uploaduser + "_" + httpPostedFile.FileName + "_01";      //is fine
                    //ulf.Filename = httpPostedFile.FileName.Remove(httpPostedFile.FileName.LastIndexOf(".")); //file extension also removes
                    //ulf.Filename = httpPostedFile.FileName.Remove(httpPostedFile.FileName.LastIndexOf(".")); //file extension also removes
                    ulf.Filename = httpPostedFile.FileName;
                    //ulf.Filename = Path.GetFileName(httpPostedFile.FileName);
                    //ulf.Filename = Path.GetFileName(httpPostedFile.FileName); //get file name without extension
                    //Path.GetExtension(httpPostedFile.FileName); //file extension also removes

                    using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(connectionString))
                    {
                        using (var cmd = new System.Data.SqlClient.SqlCommand("", con))
                        {
                            var sql = "Insert into MPRUpLoadedFileStatus (Filename, Status, Treated, UploadUser, DateTimeUploaded, ReadStart, ReadEnd, Tablename) " +
                            "values(@FILENAME, 'awaiting', '0', @UPLOADUSER, @DATETIMEUPLOADED, @READSTART, @READEND, @TABLENAME); SELECT SCOPE_IDENTITY();";
                            //var sql = "Update MPRUpLoadedFileStatus set Filename=@FILENAME, UploadUser=@UPLOADUSER, DateTimeUploaded=@DATETIMEUPLOADED";

                            cmd.CommandText = sql;
                            cmd.Parameters.AddWithValue("@FILENAME", ulf.Filename);
                            cmd.Parameters.AddWithValue("@UPLOADUSER", uploaduser);
                            cmd.Parameters.AddWithValue("@DATETIMEUPLOADED", DateTime.Now);
                            cmd.Parameters.AddWithValue("@READSTART", DateTime.Now);
                            cmd.Parameters.AddWithValue("@READEND", DateTime.Now);
                            cmd.Parameters.AddWithValue("@TABLENAME", tablename);

                            con.Open();
                            //cmd.ExecuteNonQuery();
                            newId = Convert.ToInt32(cmd.ExecuteScalar());
                            con.Close();
                        }
                    }

                    //You could modify the following code and get the postedfile inputstream, then insert them into database.
                    // Get the complete file path
                    //var fileSavePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Upload"), httpPostedFile.FileName);
                    //var fileSavePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Upload"), newId + "_" + httpPostedFile.FileName);
                    var fileSavePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(filePath), newId + "_" + httpPostedFile.FileName);

                    // Save the uploaded file to "Upload" folder
                    httpPostedFile.SaveAs(fileSavePath);
                    res = "success";
                }
            }
            return res;
        }

    }
}