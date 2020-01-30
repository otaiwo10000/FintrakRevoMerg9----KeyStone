using Fintrak.Presentation.WebClient.Core;
using Fintrak.Presentation.WebClient.Models;
using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web.Mvc;
using System.Configuration;
using WebMatrix.WebData;
//using System.Web.Configuration;
using FinTrakLicense;
using static FinTrakLicense.FinLicense;
//using FinTrakLicense.LicenseStatus;

namespace Fintrak.Presentation.WebClient.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("account")]
    public class AccountController : ViewControllerBase
    {
       
        [ImportingConstructor]
        public AccountController(ISecurityAdapter securityAdapter)
        {

            _SecurityAdapter = securityAdapter;
        }

        ISecurityAdapter _SecurityAdapter;

        //public string _status = WebConfigurationManager.AppSettings["Status"];


        [HttpGet]
        // the route is defined in RouteConfig
        public ActionResult Register()
        {
            _SecurityAdapter.Initialize();

            return View();
        }

       
        [HttpGet]
        [Route("login")]
        public ActionResult Login(string returnUrl)
        {
            _SecurityAdapter.Initialize();

            string configReferrer = ConfigurationManager.AppSettings["expectedreferal"];
            string defaultReferrer = ConfigurationManager.AppSettings["defaultReferrer"];
            string actualreferrer = string.Empty;

            if (HttpContext.Request.UrlReferrer != null)
            {
                actualreferrer = HttpContext.Request.UrlReferrer.ToString();
            }
            else
            {
                actualreferrer = "";
            }

            if (actualreferrer == "" || actualreferrer == "null")
            {
                actualreferrer = "null";
            }
            if (actualreferrer == configReferrer)
            {
                return View(new AccountLoginModel() { ReturnUrl = returnUrl });
            }
            else
            {
                //Response.Redirect(defaultReferrer);
                //return null;

                //_SecurityAdapter.Initialize();
                // _SecurityAdapter.LogOut();

                //return RedirectToAction("Index", "Home");
                //return RedirectToAction("login", "account");
                return View(new AccountLoginModel() { ReturnUrl = returnUrl });
            }
        }


        [HttpGet]
        [Route("logout")]
        public ActionResult Logout()
        {

            //_SecurityAdapter.Initialize();
            //WebSecurity.Logout();
            _SecurityAdapter.LogOut();

            //return RedirectToAction("Index", "Home");
            return RedirectToAction("login", "account");
        }

        [HttpGet]
        [Route("changepassword")]
        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpGet]
        [Route("forgotpassword")]
        [Authorize]
        public ActionResult ForgotPassword()
        {
            _SecurityAdapter.Initialize();
            return View();
        }



        //================================ some methods ========================================

        //public DateTime GetAppRunDate()
        //{
        //    DateTime runDate = Convert.ToDateTime("01-01-0001");
        //    var connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

        //    try
        //    {
        //        using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
        //        {
        //            var cmd = new System.Data.SqlClient.SqlCommand("GetSolutionRunDate", con);
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            cmd.CommandTimeout = 999999;

        //            var sqldt = new System.Data.SqlClient.SqlDataAdapter(cmd);
        //            sqldt.Fill(dts);
        //            runDate = dts.Tables(0).Rows(0).Item(0).ToString();
        //        }

        //    }

        //    catch (Exception ex)
        //    {
        //        //return ex;
        //    }

        //    return runDate;
        //}


        //public string FriendlyMessage(int Status)
        //{
        //    string LicenseStatusMsg = "";
        //    try
        //    {
        //        if (Status == 1) { LicenseStatusMsg = "License File is Missing from the default directory"; }

        //        else if (Status == 2) { LicenseStatusMsg = "License File is Corrupt"; }

        //        else if (Status == 3) { LicenseStatusMsg = "Invalid License File"; }

        //        else if (Status == 4) { LicenseStatusMsg = "Application Not Yet Licensed"; }

        //        else if (Status == 5) { LicenseStatusMsg = "The License has Expired"; }

        //        else if (Status == 6) { LicenseStatusMsg = "The License is not for this Application Version"; }
        //    }

        //    catch (Exception ex)
        //    {
        //        //return false;
        //        //Return False;
        //    }

        //    return LicenseStatusMsg;
        //}


    }
}
