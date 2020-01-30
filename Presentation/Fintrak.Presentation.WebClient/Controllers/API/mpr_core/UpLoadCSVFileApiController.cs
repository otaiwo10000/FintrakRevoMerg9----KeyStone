using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.MPR.Contracts;
using Fintrak.Client.MPR.Entities;
using Fintrak.Presentation.WebClient.Models;
using System.IO;
using Fintrak.Presentation.WebClient.Additionalmethods.ApprovalMtd;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/uploadcsvfile")]
    [UsesDisposableService]
    public class UpLoadCSVFileApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public UpLoadCSVFileApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }

        [HttpPost]
        [Route("incomeaccountmisoverride")]
        public HttpResponseMessage IncomeAccountMISOverride(HttpRequestMessage request)
        {

            return GetHttpResponse(request, () =>
            {
                string returnval = "NA";
                string tablenameparam = "Income_AccountMIS_Override_TEMP";

                UploadCsvFile uf = new UploadCsvFile();
                returnval = uf.UploadcsvMtd(tablenameparam);

                return request.CreateResponse(HttpStatusCode.OK, returnval);

            });
        }

        [HttpPost]
        [Route("incomeaccountslisting")]
        public HttpResponseMessage IncomeAccountsListing(HttpRequestMessage request)
        {

            return GetHttpResponse(request, () =>
            {
                string returnval = "NA";
                string tablenameparam = "Income_AccountsListing_TEMP";

                UploadCsvFile uf = new UploadCsvFile();
                returnval = uf.UploadcsvMtd(tablenameparam);

                return request.CreateResponse(HttpStatusCode.OK, returnval);
            });
        }





        // string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

        //[HttpPost]
        //[Route("uploadingcsvfile")]
        //public HttpResponseMessage UploadingCSVFile(HttpRequestMessage request)
        //{

        //    return GetHttpResponse(request, () =>
        //    {
        //        //string filePath = ConfigurationManager.AppSettings["UploadedFilePath"].ToString().ToUpper();
        //        string uploaduser = string.Empty;
        //        //DateTime datetimeuploaded = DateTime.Now;
        //        int res = 0;
        //        int newId = 0;
        //        string tablename = Income_AccountMIS_Override_TEMP;
        //        string filePath = string.Empty;
        //        uploaduser = Convert.ToString(System.Web.HttpContext.Current.User.Identity.Name);   //current user

        //        UpLoadCSVFileModel ulf = new UpLoadCSVFileModel();

        //        if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
        //        {
        //            // Get the uploaded image from the Files collection
        //            var httpPostedFile = System.Web.HttpContext.Current.Request.Files["csvfile"];
        //            filePath = System.Configuration.ConfigurationManager.AppSettings["UploadedFilePath"].ToString().ToUpper();

        //            //get other form data
        //            //var username = System.Web.HttpContext.Current.Request.Form["Username"];

        //            if (httpPostedFile != null)
        //            {
        //                // Validate the uploaded file(optional)

        //                //ulf.Filename = uploaduser + "_" + httpPostedFile.FileName + "_01";      //is fine
        //                ulf.Filename = httpPostedFile.FileName.Remove(httpPostedFile.FileName.LastIndexOf(".")); //file extension also removes

        //                using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(connectionString))
        //                {
        //                    using (var cmd = new System.Data.SqlClient.SqlCommand("", con))
        //                    {
        //                        var sql = "Insert into MPRUpLoadedFileStatus (Filename, Status, Treated, UploadUser, DateTimeUploaded, ReadStart, ReadEnd, Tablename) " +
        //                        "values(@FILENAME, 'awaiting', '0', @UPLOADUSER, @DATETIMEUPLOADED, @READSTART, @READEND, @TABLENAME); SELECT SCOPE_IDENTITY();";
        //                        //var sql = "Update MPRUpLoadedFileStatus set Filename=@FILENAME, UploadUser=@UPLOADUSER, DateTimeUploaded=@DATETIMEUPLOADED";

        //                        cmd.CommandText = sql;
        //                        cmd.Parameters.AddWithValue("@FILENAME", ulf.Filename);
        //                        cmd.Parameters.AddWithValue("@UPLOADUSER", uploaduser);
        //                        cmd.Parameters.AddWithValue("@DATETIMEUPLOADED", DateTime.Now);
        //                        cmd.Parameters.AddWithValue("@READSTART", DateTime.Now);
        //                        cmd.Parameters.AddWithValue("@READEND", DateTime.Now);
        //                        cmd.Parameters.AddWithValue("@TABLENAME", tablename);

        //                        con.Open();
        //                        //cmd.ExecuteNonQuery();
        //                        newId = Convert.ToInt32(cmd.ExecuteScalar());
        //                        con.Close();
        //                        res = 1;
        //                    }
        //                }

        //                //You could modify the following code and get the postedfile inputstream, then insert them into database.
        //                // Get the complete file path
        //                //var fileSavePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Upload"), httpPostedFile.FileName);
        //                //var fileSavePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Upload"), newId + "_" + httpPostedFile.FileName);
        //                var fileSavePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(filePath), newId + "_" + httpPostedFile.FileName);

        //                // Save the uploaded file to "Upload" folder
        //                httpPostedFile.SaveAs(fileSavePath);


        //            }
        //        }

        //       return request.CreateResponse(HttpStatusCode.OK, res);
        //    });
        //}


    }
}
