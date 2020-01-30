using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Client.Core.Contracts;
using Fintrak.Client.Core.Entities;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.MPR.Entities;
using Fintrak.Client.MPR.Contracts;
using Fintrak.Presentation.WebClient.Additionalmethods;
using Fintrak.Presentation.WebClient.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using ClosedXML.Excel;
using System.IO;
using System.Net.Http.Headers;
//using System.Web;
//using System.Web.Mvc;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/filedownload")]
    [UsesDisposableService]
    public class FileDownLoadApiController : ApiControllerBase
        
    {
        [ImportingConstructor]
        public FileDownLoadApiController(IMPROPEXService mprOPEXService)
        {
            _MPROPEXService = mprOPEXService;
        }

        IMPROPEXService _MPROPEXService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPROPEXService);
        }

        string constr = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

        //[HttpGet]
        //[Route("excelfiledownload")]
        //public HttpResponseMessage ExcelFileDownLoad(HttpRequestMessage request)
        //{
        //    HttpResponseMessage res = null;

        //    using (SqlConnection con = new SqlConnection(constr))
        //    {
        //        string sqlquery = "SELECT * FROM Raw_ExpensesFixed";

        //        using (SqlCommand cmd = new SqlCommand(sqlquery))
        //        {
        //            using (SqlDataAdapter sda = new SqlDataAdapter())
        //            {
        //                cmd.Connection = con;
        //                sda.SelectCommand = cmd;
        //                using (DataTable dt = new DataTable())
        //                {
        //                    MemoryStream MyMemoryStream = new MemoryStream();
        //                    sda.Fill(dt);
        //                    using (XLWorkbook wb = new XLWorkbook())
        //                    {
        //                        wb.Worksheets.Add(dt, "Raw_ExpensesFixed");

        //                        wb.SaveAs(MyMemoryStream);

        //                        MyMemoryStream.Seek(0, SeekOrigin.Begin);

        //                        res = new HttpResponseMessage(HttpStatusCode.OK);
        //                        res.Content = new StreamContent(MyMemoryStream);
        //                        res.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
        //                        res.Content.Headers.ContentDisposition.FileName = "fintrak01.xlsx";
        //                        res.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        //                        res.Content.Headers.ContentLength = MyMemoryStream.Length;
        //                        MyMemoryStream.Seek(0, SeekOrigin.Begin);

        //                        //string AppLocation = "";
        //                        //AppLocation = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
        //                        //AppLocation = AppLocation.Replace("file:\\", "");
        //                        //string date = DateTime.Now.ToShortDateString();
        //                        //date = date.Replace("/", "_");
        //                        //string filepath = AppLocation + "\\ExcelFiles\\" + "RECEIPTS_COMPARISON_3" + date + ".xlsx";

        //                        //wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        //                        //wb.Style.Font.Bold = true;
        //                        //wb.SaveAs(filepath);

        //                        //Response.Clear();
        //                        //Response.Buffer = true;
        //                        //Response.Charset = "";
        //                        //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //                        //Response.AddHeader("content-disposition", "attachment;filename=SqlExport.xlsx");

        //                        //using (MemoryStream MyMemoryStream = new MemoryStream())
        //                        //{
        //                        //    wb.SaveAs(MyMemoryStream);
        //                        //    MyMemoryStream.WriteTo(Response.OutputStream);
        //                        //    Response.Flush();
        //                        //    Response.End();
        //                        //}
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    res = request.CreateResponse(HttpStatusCode.OK);
        //    return res;
        //}

        [HttpGet]
        [Route("excelfiledownload")]
        public HttpResponseMessage ExcelFileDownLoad(HttpRequestMessage request)
        {
            FileDownLoadController o = new FileDownLoadController();
            o.ExcelFileDownLoadOnClient();

            return request.CreateResponse(HttpStatusCode.OK);
        }

        //[HttpGet]
        //[Route("excelfiledownload}")]
        //public System.Threading.Tasks.Task<HttpResponseMessage> ExecuteAsync()
        //{
        //    HttpResponseMessage response = null;

        //    var ms = new MemoryStream();
        //    using (var wb = new XLWorkbook())
        //    {
        //        var ws = wb.AddWorksheet("Sheet1");
        //        ws.FirstCell().Value = this.FileId;

        //        wb.SaveAs(ms);

        //        ms.Seek(0, SeekOrigin.Begin);

        //        response = new HttpResponseMessage(HttpStatusCode.OK);
        //        response.Content = new StreamContent(ms);
        //        response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
        //        response.Content.Headers.ContentDisposition.FileName = "test.xlsx";
        //        response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        //        response.Content.Headers.

        //        response.Content.Headers.ContentLength = ms.Length;
        //        ms.Seek(0, SeekOrigin.Begin);
        //    }

        //    return Task.FromResult(response);
        //}


    }
}
