using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web.Mvc;
using Fintrak.Client.SystemCore.Contracts;
using Fintrak.Presentation.WebClient.Models;
using System.Data;
using System.Reflection;
using Fintrak.Client.MPR.Entities;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Configuration;
using System.Data.SqlClient;
using ClosedXML.Excel;

namespace Fintrak.Presentation.WebClient
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Authorize]
    [RoutePrefix("filedownload")]
    public class FileDownLoadController : Controller
    {
        //[ImportingConstructor]
        //public FileDownLoadController()
        //{

        //}

        //[ImportingConstructor]
        //public FileDownLoadController(ICoreService coreService)
        //{
        //    _CoreService = coreService;
        //}

        //ICoreService _CoreService;
        //List<MenuData> _AllMenu;




        string constr = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;
        public ActionResult ExcelFileDownLoadOnClient()
        //public void ExcelFileDownLoadOnClient()
        {
            ////string constr = ConfigurationManager.ConnectionStrings["dataConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sqlquery = "SELECT * FROM Raw_ExpensesFixed";

                using (SqlCommand cmd = new SqlCommand(sqlquery))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            using (XLWorkbook wb = new XLWorkbook())
                            {
                                wb.Worksheets.Add(dt, "Raw_ExpensesFixed");

                                //string AppLocation = "";
                                //AppLocation = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
                                //AppLocation = AppLocation.Replace("file:\\", "");
                                //string date = DateTime.Now.ToShortDateString();
                                //date = date.Replace("/", "_");
                                //string filepath = AppLocation + "\\ExcelFiles\\" + "RECEIPTS_COMPARISON_3" + date + ".xlsx";

                                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                wb.Style.Font.Bold = true;
                                //wb.SaveAs(filepath);

                                Response.Clear();
                                Response.Buffer = true;
                                Response.Charset = "";
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                Response.AddHeader("content-disposition", "attachment;filename=SqlExport.xlsx");

                                using (MemoryStream MyMemoryStream = new MemoryStream())
                                {
                                    wb.SaveAs(MyMemoryStream);
                                    MyMemoryStream.WriteTo(Response.OutputStream);
                                    Response.Flush();
                                    Response.End();
                                }
                            }
                        }
                    }
                }
            }

            return View();
        }


    }
}
