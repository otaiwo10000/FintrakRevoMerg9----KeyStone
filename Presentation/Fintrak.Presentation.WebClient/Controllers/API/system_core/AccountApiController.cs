using System.Web.Security;
using Fintrak.Client.SystemCore.Contracts;
using Fintrak.Client.SystemCore.Entities;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Presentation.WebClient.Models;
using Fintrak.Shared.SystemCore.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Security;
using WebMatrix.WebData;

using FinTrakLicense;
using static FinTrakLicense.FinLicense;
using log4net;

namespace Fintrak.Presentation.WebClient.Controllers.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/account")]
    public class AccountApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public AccountApiController(ISecurityAdapter securityAdapter, ICoreService coreService)
        {
            _SecurityAdapter = securityAdapter;
            _CoreService = coreService;
        }

        ISecurityAdapter _SecurityAdapter;
        ICoreService _CoreService;


        [HttpPost]
        [Route("login")]
        public HttpResponseMessage Login(HttpRequestMessage request, [FromBody]AccountLoginModel accountModel)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                bool success = _SecurityAdapter.Login(accountModel.LoginID, accountModel.Password, accountModel.CompanyCode, accountModel.RememberMe);
                if (success)
                    response = request.CreateResponse(HttpStatusCode.OK);
                else
                    response = request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Unauthorized login.");

                return response;
            });
        }


        //[HttpPost]
        //[Route("login")]
        //public HttpResponseMessage Login(HttpRequestMessage request, [FromBody]AccountLoginModel accountModel)
        //{
        //    string res = "";
        //    return GetHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;
        //        string _displaymsg = "";
        //        FinLicense rst = new FinLicense();
        //        LicenseFileDetail rst2 = new LicenseFileDetail();
        //        LicenseStatus _status = rst2.Status;
        //        //LicenseFileDetail examine = rst.ExamineLicense("ERMS"); //Product name eg: MPR
        //        LicenseFileDetail examine = rst.ExamineLicense("HRMS"); //Product name eg: MPR, ERMS
        //                                                                //string sts = examine.Status.ToString();
        //        int sts = Convert.ToInt32(examine.Status);
        //        //rst.ExamineLicense.Status.ToString();  // need to be known

        //        //DateTime rdate = GetAppRunDate().Date;
        //        DateTime rdate = Convert.ToDateTime("09-07-2018");

        //        if (sts == 0)
        //        {
        //            string Licensee = examine.Licensee;
        //            DateTime LicenseDate = examine.LicenseDate;
        //            DateTime ExpireDate = examine.ExpireDate;
        //            string SerialNumber = examine.SerialNumber;
        //            string MachineUniqueIdentityKey = examine.MachineUniqueIdentityKey;
        //            string MUIK = ConfigurationManager.AppSettings["MachineUniqueIdentityKey"];

        //            string ClientName = ConfigurationManager.AppSettings["Licensee"];

        //            if (MachineUniqueIdentityKey != MUIK)
        //            {
        //                res = "Application not running from the desired Server";
        //            }

        //            else if (ClientName != Licensee)
        //            {
        //                res = "Application not running for the intended Licensee";
        //            }

        //            else if (!(rdate > LicenseDate && rdate < ExpireDate))
        //            {
        //                res = "License Expiry Date Altered";
        //            }

        //            else if (rdate > LicenseDate && rdate < ExpireDate)
        //            {

        //                //================= Original starts ============================================================================

        //                // HttpResponseMessage response = null;

        //                bool success = _SecurityAdapter.Login(accountModel.LoginID, accountModel.Password, accountModel.CompanyCode, accountModel.RememberMe);
        //                if (success)
        //                    response = request.CreateResponse(HttpStatusCode.OK);
        //                else
        //                    response = request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Unauthorized login.");

        //                //return response;
        //                //============================= Original end ===============================================================
        //            }
        //        }
        //        else
        //        {
        //            res = FriendlyMessage(sts);
        //            response = request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, res);
        //        }

        //        return response;
        //    });
        //}


        [HttpPost]
        [Route("changepw")]
        [Authorize]
        public HttpResponseMessage ChangePassword(HttpRequestMessage request, [FromBody]AccountChangePasswordModel passwordModel)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var loginName = User.Identity.Name;
                passwordModel.LoginID = loginName;
                ValidateAuthorizedUser(passwordModel.LoginID);
                _SecurityAdapter.Initialize();
                //_SecurityAdapter.LogOut();

                bool success = _SecurityAdapter.ChangePassword(passwordModel.LoginID, passwordModel.OldPassword, passwordModel.NewPassword);
                //bool success = _SecurityAdapter.ChangePassword(loginName, passwordModel.OldPassword, passwordModel.NewPassword);
                if (success)
                    response = request.CreateResponse(HttpStatusCode.OK);
                else
                    response = request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Unable to change password.");
                //_SecurityAdapter.LogOut();
                return response;
               
            });
        }

        [HttpGet]
        [Route("getaccount/{accountId}")]
        [Authorize]
        public HttpResponseMessage GetAccountInfo(HttpRequestMessage request, int accountId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                UserSetup account = _CoreService.GetUserSetup(accountId);
                // notice no need to create a seperate model object since Account entity will do just fine

                response = request.CreateResponse<UserSetup>(HttpStatusCode.OK, account);

                return response;
            });
        }

        [HttpGet]
        [Route("getaccountdetail/{accountId}")]
        [Authorize]
        public HttpResponseMessage GetAccountDetailInfo(HttpRequestMessage request, int accountId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var userModel = new UserModel();

                userModel.UserSetup = _CoreService.GetUserSetup(accountId);
            
                var solutions = _CoreService.GetAllSolutions();
                var accountGroups = _CoreService.GetUserRoleByLoginID(userModel.UserSetup.LoginID);

                var userGroupModels = new List<UserGroupModel>();

                foreach (var solution in solutions)
                {
                    var roles = _CoreService.GetAllRoles().Where(c => c.SolutionId == solution.SolutionId && c.Type == RoleType.Application).ToArray();

                    var roleIds = roles.Select(c=>c.RoleId).Distinct();

                    var accountGroup = accountGroups.Where(c => c.SolutionId == solution.SolutionId && c.UserSetupId == userModel.UserSetup.UserSetupId && roleIds.Contains(c.RoleId)).FirstOrDefault();

                    userGroupModels.Add(new UserGroupModel()
                    {
                        UserSetupId = userModel.UserSetup.UserSetupId,
                        LoginID = userModel.UserSetup.LoginID,
                        RoleId = accountGroup == null ? 0 : accountGroup.RoleId,
                        RoleName = accountGroup == null ? "" : accountGroup.RoleName,
                        SolutionId = solution.SolutionId,
                        SolutionName = solution.Alias,
                        Roles = roles
                    });

                }

                userModel.Roles = userGroupModels.ToArray();

                var userGroupReportModels = new List<UserGroupModel>();
                foreach (var solution in solutions)
                {
                    var roles = _CoreService.GetAllRoles().Where(c => c.SolutionId == solution.SolutionId && c.Type == RoleType.Report).ToArray();

                    var roleIds = roles.Select(c => c.RoleId).Distinct();

                    var accountGroup = accountGroups.Where(c => c.SolutionId == solution.SolutionId && c.UserSetupId == userModel.UserSetup.UserSetupId && roleIds.Contains(c.RoleId)).FirstOrDefault();

                    userGroupReportModels.Add(new UserGroupModel()
                    {
                        UserSetupId = userModel.UserSetup.UserSetupId,
                        LoginID = userModel.UserSetup.LoginID,
                        RoleId = accountGroup == null ? 0 : accountGroup.RoleId,
                        RoleName = accountGroup == null ? "" : accountGroup.RoleName,
                        SolutionId = solution.SolutionId,
                        SolutionName = solution.Alias,
                        Roles = roles
                    });

                }

                userModel.ReportRoles = userGroupReportModels.ToArray();

                //Companies
                var companies = _CoreService.GetAllCompanies();
                var userCompanies = _CoreService.GetCompanyUserByLogin(userModel.UserSetup.LoginID);

                var userCompanyModels = new List<UserCompanyModel>();

                foreach (var company in companies)
                {
                    var accountCompany = userCompanies.Where(c => c.CompanyCode == company.Code && c.UserId == userModel.UserSetup.UserSetupId).FirstOrDefault();

                    userCompanyModels.Add(new UserCompanyModel()
                    {
                        UserSetupId = userModel.UserSetup.UserSetupId,
                        LoginID = userModel.UserSetup.LoginID,
                        CompanyCode = company.Code,
                        CompanyName = company.Name ,
                        IsChecked = accountCompany != null ? true : false
                    });

                }

                userModel.UserCompanies = userCompanyModels.ToArray();

                response = request.CreateResponse<UserModel>(HttpStatusCode.OK, userModel);

                return response;
            });
        }

        [HttpGet]
        [Route("getallaccount")]
        [Authorize]
        public HttpResponseMessage GetAllAccountInfo(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                UserSetup[] accounts = _CoreService.GetAllUserSetups();
                // notice no need to create a seperate model object since Account entity will do just fine

                response = request.CreateResponse<UserSetup[]>(HttpStatusCode.OK, accounts);

                return response;
            });
        }

        [HttpPost]
        [Route("updateaccount")]
        public HttpResponseMessage UpdateAccountDetail(HttpRequestMessage request, [FromBody]UserSetup accountModel)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                UserSetup account = null;

                var securityMode = ConfigurationManager.AppSettings["SecurityMode"].ToString();

                // revalidate all steps to ensure this operation is secure against hacks
                if (accountModel.UserSetupId <= 0)
                {
                    if (securityMode == "UP")
                    {
                        _SecurityAdapter.Initialize();
                        _SecurityAdapter.Register(accountModel.LoginID, "@password",
                            propertyValues: new
                            {
                                Name = accountModel.Name,
                                Email = accountModel.Email,
                                MultiCompanyAccess = accountModel.MultiCompanyAccess,
                                LatestConnection = DateTime.Now,
                                Deleted = false,
                                Active = true,
                                CreatedBy = User.Identity.Name,
                                CreatedOn = DateTime.Now,
                                UpdatedBy = User.Identity.Name,
                                UpdatedOn = DateTime.Now,
                            });

                        account = _CoreService.GetUserSetupByLoginID(accountModel.LoginID);
                    }
                    else
                    {
                        accountModel.LatestConnection = DateTime.Now;
                        accountModel.Active = true;
                        accountModel.Deleted = false;
                        accountModel.CreatedBy = User.Identity.Name;
                        accountModel.CreatedOn = DateTime.Now ;
                        accountModel.UpdatedBy = User.Identity.Name;
                        accountModel.UpdatedOn = DateTime.Now;

                        account = _CoreService.UpdateUserSetup(accountModel);
                    }
                }
                else
                {
                    account = _CoreService.UpdateUserSetup(accountModel);
                }

                response = request.CreateResponse<UserSetup>(HttpStatusCode.OK, account);

                return response;
            });
        }

        [HttpPost]
        [Route("updateaccountdetail")]
        public HttpResponseMessage UpdateAccount(HttpRequestMessage request, [FromBody]UserModel accountModel)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                UserSetup account = null;

                var securityMode = ConfigurationManager.AppSettings["SecurityMode"].ToString();
                var clientcode = Convert.ToString(ConfigurationManager.AppSettings["ClientCode"]);

                // revalidate all steps to ensure this operation is secure against hacks
                if (accountModel.UserSetup.UserSetupId <= 0)
                {
                    if (securityMode == "UP")
                    {
                        _SecurityAdapter.Initialize();
                        _SecurityAdapter.Register(accountModel.UserSetup.LoginID, "@password",
                            propertyValues: new
                            {
                                Name = accountModel.UserSetup.Name,
                                Email = accountModel.UserSetup.Email,
                                MultiCompanyAccess = accountModel.UserSetup.MultiCompanyAccess,
                                LatestConnection = DateTime.Now,
                                Deleted = false,
                                Active = true,
                                CreatedBy = User.Identity.Name,
                                CreatedOn = DateTime.Now,
                                UpdatedBy = User.Identity.Name,
                                UpdatedOn = DateTime.Now,

                                Mis_Code = accountModel.UserSetup.Mis_Code,
                                Grade = accountModel.UserSetup.Grade,
                                ManagerID = accountModel.UserSetup.ManagerID,
                                Segment = accountModel.UserSetup.Segment,
                                DateEmployed = accountModel.UserSetup.DateEmployed,

                            });


                        account = _CoreService.GetUserSetupByLoginID(accountModel.UserSetup.LoginID);
                    }
                    else
                    {
                        ////=========== another sample ==================
                        //DirectoryEntry de = new DirectoryEntry(ConfigurationManager.AppSettings.Get("ADPath"));
                        //de.Username = ConfigurationManager.AppSettings.Get("ADServiceAccount");
                        //de.Password = ConfigurationManager.AppSettings.Get("ADServiceAccountPassword");
                        //de.AuthenticationType = AuthenticationTypes.FastBind;
                        //DirectorySearcher dssearch = new DirectorySearcher(de);
                        //dssearch.Filter = "(CN=" + Session["username"].ToString() + ")";
                        //SearchResult sresult = dssearch.FindOne();
                        //DirectoryEntry dsresult = sresult.GetDirectoryEntry();
                        //lblfname.Text = dsresult.Properties["displayName"][0].ToString();
                        //lbltitle.Text = dsresult.Properties["title"][0].ToString();
                        //lbllname.Text = dsresult.Properties["telephonenumber"][0].ToString();
                        //lblemail.Text = dsresult.Properties["mobile"][0].ToString();
                        ////=========== another sample ends =============

                        //string connection = ConfigurationManager.ConnectionStrings["ADConnectionString"].ToString();
                        //appLog.InfoFormat("declaring connection with connection name: ADConnectionString");

                        //System.DirectoryServices.DirectorySearcher dssearch = new System.DirectoryServices.DirectorySearcher(connection);
                        //appLog.InfoFormat("calling DirectorySearcher(x) method to pass the AD connection to the declared DirectorySearcher property: dssearch");
                        ////dssearch.Filter = username;
                        ////dssearch.Filter = "fintrakbusiness";
                        ////dssearch.Filter = "(CN=" + Session["username"].ToString() + ")";
                        ////dssearch.Filter = "(CN=MyName)";
                        ////dssearch.Filter = "(sAMAccountName=" + txtusername.Text + ")";
                        //dssearch.Filter = "(sAMAccountName=" + "fintrack" + ")";
                        //appLog.InfoFormat("passing sAMAccountName fintrack to dssearch.Filter.");

                        ////dssearch.Filter = "(CN=" + "fintrack" + ")";
                        //System.DirectoryServices.SearchResult sresult = dssearch.FindOne();
                        //appLog.InfoFormat("calling FindOne()");
                        //System.DirectoryServices.DirectoryEntry dsresult = sresult.GetDirectoryEntry();
                        //appLog.InfoFormat("calling GetDirectoryEntry()");

                        //string firstname = dsresult.Properties["givenName"][0].ToString();
                        //string lastname = dsresult.Properties["sn"][0].ToString();  //sn means surname
                        ////accountModel.UserSetup.Name = firstname + " " + lastname;

                        ////accountModel.UserSetup.Email = dsresult.Properties["mail"][0].ToString();
                        //////string initials = dsresult.Properties["initials"][0].ToString();
                        //////string displayName = dsresult.Properties["displayName"][0].ToString();
                        //////string mobile = dsresult.Properties["mobile"][0].ToString();
                        ////accountModel.UserSetup.LoginID = dsresult.Properties["sAMAccountName"][0].ToString();

                        //string empid = dsresult.Properties["employeeID"][0].ToString();
                        //string empno = dsresult.Properties["employeeNumber"][0].ToString();
                        ////accountModel.UserSetup.StaffID = empid + "" + empno;


                            accountModel.UserSetup.LatestConnection = DateTime.Now;
                            accountModel.UserSetup.Active = true;
                            accountModel.UserSetup.Deleted = false;
                            accountModel.UserSetup.CreatedBy = User.Identity.Name;
                            accountModel.UserSetup.CreatedOn = DateTime.Now;
                            accountModel.UserSetup.UpdatedBy = User.Identity.Name;
                            accountModel.UserSetup.UpdatedOn = DateTime.Now;
                       
                        account = _CoreService.UpdateUserSetup(accountModel.UserSetup);
                    }  

                    //create default role
                    _CoreService.AssignDefaultRole(account);
                }
                else
                {
                    account = _CoreService.UpdateUserSetup(accountModel.UserSetup);
                }

                var existingUserRoles = _CoreService.GetUserRoleByLoginID(account.LoginID);

                foreach (var userRole in existingUserRoles)
                {
                    _CoreService.DeleteUserRole(userRole.UserRoleId);
                }

                foreach (var userRole in accountModel.Roles)
                {
                    if (userRole.RoleId  > 0)
                    {
                        var newUserRole = new UserRole()
                        {
                            UserSetupId = account.UserSetupId,
                            RoleId = userRole.RoleId
                        };

                        _CoreService.UpdateUserRole(newUserRole);

                    }
                }

                foreach (var userRole in accountModel.ReportRoles)
                {
                    if (userRole.RoleId > 0)
                    {
                        var newUserRole = new UserRole()
                        {
                            UserSetupId = account.UserSetupId,
                            RoleId = userRole.RoleId
                        };

                        _CoreService.UpdateUserRole(newUserRole);
                    }
                }

                //Companies
                var existingUserCompanies = _CoreService.GetCompanyUserByLogin(account.LoginID);

                foreach (var userCompany in accountModel.UserCompanies)
                {
                    var existingUserCompany = existingUserCompanies.Where(c => c.CompanyCode == userCompany.CompanyCode).FirstOrDefault();

                    if (existingUserCompany == null)
                    {
                        var newUserCompany = new CompanyUser()
                        {
                            UserId = account.UserSetupId,
                            CompanyCode = userCompany.CompanyCode,
                            Active = true
                        };

                        if (userCompany.IsChecked)
                            _CoreService.UpdateCompanyUser(newUserCompany);
                    }
                    else
                    {
                        if (!userCompany.IsChecked)
                            _CoreService.DeleteCompanyUser(existingUserCompany.CompanyUserId);
                    }
                }

                response = request.CreateResponse<UserSetup>(HttpStatusCode.OK, account);

                return response;
            });
        }

        [HttpGet]
        [Route("getactivedirectoryuserdetail/{loginid}")]
        [Authorize]
        public HttpResponseMessage getActiveDirectoryUserDetail(HttpRequestMessage request, string loginid)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                loginid = loginid.Replace("FORWARDSLASHXTER", "/").Trim();
                loginid = loginid.Replace("DOTXTER", ".").Trim();

                string connection = ConfigurationManager.ConnectionStrings["ADConnectionString"].ToString();

                System.DirectoryServices.DirectorySearcher dssearch = new System.DirectoryServices.DirectorySearcher(connection);
                dssearch.Filter = "(sAMAccountName=" + loginid + ")";
                System.DirectoryServices.SearchResult sresult = dssearch.FindOne();
                System.DirectoryServices.DirectoryEntry dsresult = sresult.GetDirectoryEntry();

                string firstname = Convert.ToString(dsresult.Properties["givenName"].Value);
                string lastname = Convert.ToString(dsresult.Properties["sn"].Value);  //sn means surname
                //string empid = Convert.ToString(dsresult.Properties["employeeID"].Value);
                string empid = Convert.ToString(dsresult.Properties["company"].Value);
                //string empno = Convert.ToString(dsresult.Properties["employeeNumber"].Value);
                string mail = Convert.ToString(dsresult.Properties["mail"].Value);


                var ADuserdetail = new UserSetup()
                {
                    //LoginID = loginid,
                    //Name = "Taiwo",
                    //Email = "t@gmail.com",
                    //StaffID = "empid"

                    LoginID = loginid,
                    Name = firstname + " " + lastname,
                    Email = mail,
                    StaffID = empid
                };

                response = request.CreateResponse<UserSetup>(HttpStatusCode.OK, ADuserdetail);

                return response;
            });
        }


        [HttpGet]
        [Route("getuserprofile")]
        [Authorize]
        public HttpResponseMessage updateUserProfile(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                UserSetup account = _CoreService.GetUserSetupByLoginID(User.Identity.Name);
                // notice no need to create a seperate model object since Account entity will do just fine

                response = request.CreateResponse<UserSetup>(HttpStatusCode.OK, account);

                return response;
            });
        }

        [HttpPost]
        [Route("updateusersetupprofile")]
        public HttpResponseMessage UpdateUserSetupProfile(HttpRequestMessage request, [FromBody]UserSetup userSetup)
        {
            return GetHttpResponse(request, () =>
            {
                userSetup = _CoreService.UpdateUserSetupProfile(userSetup);

                return request.CreateResponse<UserSetup>(HttpStatusCode.OK, userSetup);
            });
        }

        [HttpPost]
        [Route("passwordreset/{loginId}")]
        [Authorize]
        public HttpResponseMessage ResetPassword(HttpRequestMessage request,string loginId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                _SecurityAdapter.Initialize();
                var token = WebSecurity.GeneratePasswordResetToken(loginId);
                WebSecurity.ResetPassword(token, "@password");
                              response = request.CreateResponse(HttpStatusCode.OK);

                return response;
            });
        }

       // [HttpGet]
       // [Route("firstlogon/{loginId}")]
       //// [Authorize]
       // public HttpResponseMessage ConfirmFirstLogon(HttpRequestMessage request, string loginId)
       // {
       //     return GetHttpResponse(request, () =>
       //     {
       //         HttpResponseMessage response = null;

       //         bool isFirstLogon = _CoreService.IsFirstLogon(loginId);
       //         // notice no need to create a seperate model object since Account entity will do just fine

       //         response = request.CreateResponse<bool>(HttpStatusCode.OK, isFirstLogon);

       //         return response;
       //     });
       // }

        [HttpGet]
        [Route("userexist/{loginId}")]
        public HttpResponseMessage UserExists(HttpRequestMessage request, string loginId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                //var resluts;
                bool success = _SecurityAdapter.UserExists(loginId);
                if (success)
                    //resluts='1';
                    response = request.CreateResponse(HttpStatusCode.OK);
                else
                    response = request.CreateErrorResponse(HttpStatusCode.Unauthorized, "User does not exist.");

                return response;
            });
        }



        public string FriendlyMessage(int Status)
        {
            string LicenseStatusMsg = "";
            try
            {
                if (Status == 1) { LicenseStatusMsg = "License File is Missing from the default directory"; }

                else if (Status == 2) { LicenseStatusMsg = "License File is Corrupt"; }

                else if (Status == 3) { LicenseStatusMsg = "Invalid License File"; }

                else if (Status == 4) { LicenseStatusMsg = "Application Not Yet Licensed"; }

                else if (Status == 5) { LicenseStatusMsg = "The License has Expired"; }

                else if (Status == 6) { LicenseStatusMsg = "The License is not for this Application Version"; }
            }

            catch (Exception ex)
            {
                //return false;
                //Return False;
            }

            return LicenseStatusMsg;
        }


    }
}

