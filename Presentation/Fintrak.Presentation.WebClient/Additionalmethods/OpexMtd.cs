using Fintrak.Presentation.WebClient.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Fintrak.Presentation.WebClient.Additionalmethods
{
    public class OpexMtd
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

        public IEnumerable<Models.OpexStaffExpDiffModel> GetTopOpexStaffExpDiffMtd()
        {
            List<OpexStaffExpDiffModel> opexstaffexpdiffList = new List<OpexStaffExpDiffModel>();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("spp_proc_ExpenseGLR", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
   
                con.Open();
               
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var op = new OpexStaffExpDiffModel();

                    //op.OPEX_STAFFEXP_DIFF_Id = reader["OPEX_STAFFEXP_DIFF_Id"] != DBNull.Value ? int.Parse(reader["OPEX_STAFFEXP_DIFF_Id"].ToString()) : 0;
                    //op.Salary_BranchCode = reader["Salary_BranchCode"] != DBNull.Value ? reader["Salary_BranchCode"].ToString() : "default";
                    //op.RawExpense_BranchCode = reader["RawExpense_BranchCode"] != DBNull.Value ? reader["RawExpense_BranchCode"].ToString() : "default";
                    //op.SalaryAMount = reader["SalaryAMount"] != DBNull.Value ? double.Parse(reader["SalaryAMount"].ToString()) : 0;
                    //op.RawExpenseAmount = reader["RawExpenseAmount"] != DBNull.Value ? double.Parse(reader["RawExpenseAmount"].ToString()) : 0;
                    //op.Differences = reader["Differences"] != DBNull.Value ? double.Parse(reader["Differences"].ToString()) : 0;

                    op.ID = reader["ID"] != DBNull.Value ? int.Parse(reader["ID"].ToString()) : 0;
                    op.GL_Code = reader["GL_Code"] != DBNull.Value ? reader["GL_Code"].ToString() : "null";
                    op.GL_Description = reader["GL_Description"] != DBNull.Value ? reader["GL_Description"].ToString() : "null";
                    op.FinstatAmount = reader["FinstatAmount"] != DBNull.Value ? double.Parse(reader["FinstatAmount"].ToString()) : 0;
                    op.OpexAmount = reader["OpexAmount"] != DBNull.Value ? double.Parse(reader["OpexAmount"].ToString()) : 0;
                    op.Difference = reader["Difference"] != DBNull.Value ? double.Parse(reader["Difference"].ToString()) : 0;


                    opexstaffexpdiffList.Add(op);
                }
                con.Close();
                System.Web.HttpContext.Current.Session["opexstaffexpdiff"] = opexstaffexpdiffList;
            }
            return opexstaffexpdiffList;
        } //========== end of the mtd


        public IEnumerable<Models.RawExpensesFixedModel> GetAllRawExpensesFixedModelMtd()
        {
            List<RawExpensesFixedModel> opexrawexpensesfixedList = new List<RawExpensesFixedModel>();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("spp_RawExpensesFixed", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var op = new RawExpensesFixedModel();
                    
                    op.FINID = reader["FINID"] != DBNull.Value ? int.Parse(reader["FINID"].ToString()) : 0;
                    op.GL_Code = reader["GL_Code"] != DBNull.Value ? reader["GL_Code"].ToString() : "null";
                    op.GL_Name = reader["GL_Name"] != DBNull.Value ? reader["GL_Name"].ToString() : "null";
                    op.Post_Date = reader["Post_Date"] != DBNull.Value ? reader["Post_Date"].ToString() : "null";
                    op.Amount = reader["Amount"] != DBNull.Value ? double.Parse(reader["Amount"].ToString()) : 0;
                    op.Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : "null";
                    op.ChkMIS1 = reader["ChkMIS1"] != DBNull.Value ? reader["ChkMIS1"].ToString() : "null";
                    op.ChkMIS2 = reader["ChkMIS2"] != DBNull.Value ? reader["ChkMIS2"].ToString() : "null";

                    op.MIS_Code = reader["MIS_Code"] != DBNull.Value ? reader["MIS_Code"].ToString() : "null";
                    op.BranchCode = reader["BranchCode"] != DBNull.Value ? reader["BranchCode"].ToString() : "null";
                    op.TRANSID = reader["TRANSID"] != DBNull.Value ? reader["TRANSID"].ToString() : "null";
                    op.SUBGL = reader["SUBGL"] != DBNull.Value ? reader["SUBGL"].ToString() : "null";

                    op.SBUCODE = reader["SBUCODE"] != DBNull.Value ? reader["SBUCODE"].ToString() : "null";
                    op.Account = reader["Account"] != DBNull.Value ? reader["Account"].ToString() : "null";
                    op.Product = reader["Product"] != DBNull.Value ? reader["Product"].ToString() : "null";

                    opexrawexpensesfixedList.Add(op);
                }
                con.Close();
                //System.Web.HttpContext.Current.Session["opexstaffexpdiff"] = opexstaffexpdiffList;
            }
            return opexrawexpensesfixedList;
        } //========== end of the mtd


        public IEnumerable<Models.RawExpensesFixedModel> GetAllRawExpensesFixedModelMtd_2(string svalue)
        {
            svalue = svalue.Replace("FORWARDSLASHXTER", "/");
            svalue = svalue.Replace("DOTXTER", ".");

            List<RawExpensesFixedModel> opexrawexpensesfixedList = new List<RawExpensesFixedModel>();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("spp_RawExpensesFixed_2", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "SearchValue",
                    Value = svalue,
                });

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var op = new RawExpensesFixedModel();

                    op.FINID = reader["FINID"] != DBNull.Value ? int.Parse(reader["FINID"].ToString()) : 0;
                    op.GL_Code = reader["GL_Code"] != DBNull.Value ? reader["GL_Code"].ToString() : "null";
                    op.GL_Name = reader["GL_Name"] != DBNull.Value ? reader["GL_Name"].ToString() : "null";
                    op.Post_Date = reader["Post_Date"] != DBNull.Value ? reader["Post_Date"].ToString() : "null";
                    op.Amount = reader["Amount"] != DBNull.Value ? double.Parse(reader["Amount"].ToString()) : 0;
                    op.Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : "null";
                    op.ChkMIS1 = reader["ChkMIS1"] != DBNull.Value ? reader["ChkMIS1"].ToString() : "null";
                    op.ChkMIS2 = reader["ChkMIS2"] != DBNull.Value ? reader["ChkMIS2"].ToString() : "null";

                    op.MIS_Code = reader["MIS_Code"] != DBNull.Value ? reader["MIS_Code"].ToString() : "null";
                    op.BranchCode = reader["BranchCode"] != DBNull.Value ? reader["BranchCode"].ToString() : "null";
                    op.TRANSID = reader["TRANSID"] != DBNull.Value ? reader["TRANSID"].ToString() : "null";
                    op.SUBGL = reader["SUBGL"] != DBNull.Value ? reader["SUBGL"].ToString() : "null";

                    opexrawexpensesfixedList.Add(op);
                }
                con.Close();
                //System.Web.HttpContext.Current.Session["opexstaffexpdiff"] = opexstaffexpdiffList;
            }
            return opexrawexpensesfixedList;
        } //========== end of the mtd

        public Models.RawExpensesFixedModel GetRawExpensesFixedById(int Id)
        {
            var op = new RawExpensesFixedModel();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("spp_RawExpensesFixed_Id", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Id",
                    Value = Id,
                });


                con.Open();
                //cmd.ExecuteNonQuery();
                //cmd2.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    op.FINID = reader["FINID"] != DBNull.Value ? int.Parse(reader["FINID"].ToString()) : 0;
                    op.GL_Code = reader["GL_Code"] != DBNull.Value ? reader["GL_Code"].ToString() : "null";
                    op.GL_Name = reader["GL_Name"] != DBNull.Value ? reader["GL_Name"].ToString() : "null";
                    op.Post_Date = reader["Post_Date"] != DBNull.Value ? reader["Post_Date"].ToString() : "null";
                    op.Amount = reader["Amount"] != DBNull.Value ? double.Parse(reader["Amount"].ToString()) : 0;
                    op.Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : "null";
                    op.ChkMIS1 = reader["ChkMIS1"] != DBNull.Value ? reader["ChkMIS1"].ToString() : "null";
                    op.ChkMIS2 = reader["ChkMIS2"] != DBNull.Value ? reader["ChkMIS2"].ToString() : "null";

                    op.MIS_Code = reader["MIS_Code"] != DBNull.Value ? reader["MIS_Code"].ToString() : "null";
                    op.BranchCode = reader["BranchCode"] != DBNull.Value ? reader["BranchCode"].ToString() : "null";
                    op.TRANSID = reader["TRANSID"] != DBNull.Value ? reader["TRANSID"].ToString() : "null";
                    op.SUBGL = reader["SUBGL"] != DBNull.Value ? reader["SUBGL"].ToString() : "null";

                    op.SBUCODE = reader["SBUCODE"] != DBNull.Value ? reader["SBUCODE"].ToString() : "null";
                    op.Account = reader["Account"] != DBNull.Value ? reader["Account"].ToString() : "null";
                    op.Product = reader["Product"] != DBNull.Value ? reader["Product"].ToString() : "null";
                }
                con.Close();
            }
            return op;
        } //========== end of the mtd

        public void UpdateRawExpensesFixed(RawExpensesFixedModel updateopex)
        {           
            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("spp_RawExpensesFixed_update", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "id",
                    Value = updateopex.FINID,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "glcode",
                    Value = updateopex.GL_Code,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "glname",
                    Value = updateopex.GL_Name,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "mis_code",
                    Value = updateopex.MIS_Code,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "branchcode",
                    Value = updateopex.BranchCode,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "subgl",
                    Value = updateopex.SUBGL,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "sbucode",
                    Value = updateopex.SBUCODE,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "product",
                    Value = updateopex.Product,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "account",
                    Value = updateopex.Account,
                });

               

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        ////public void insertfromOpexStaffExpDiffINTORawExpensesFixed(string branchcode, string glcode, string glname, string diff, string description, string miscode)
        ////public void insertfromOpexStaffExpDiffINTORawExpensesFixed(OpexStaffExpDiffModel opexstaffexpdiff, string glname, string description, string glcode, string miscode)
        public void insertfromOpexStaffExpDiffINTORawExpensesFixed(string glcode, string diff, string miscode)
        {
            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("spp_ExpenseGLR_Insert", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
             
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "GL_Code",
                    Value = glcode,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Difference",
                    Value = diff,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "MisCode",
                    Value = miscode,
                });

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void insertfromOpexStaffExpDiffINTORawExpensesFixed_2(OpexStaffExpDiffModel_2 opexstaffexpdiff)
        {
            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("spp_ExpenseGLR_Insert", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "GL_Code",
                    Value = opexstaffexpdiff.GL_Code,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Difference",
                    Value = opexstaffexpdiff.Difference,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "MisCode",
                    Value = opexstaffexpdiff.miscode,
                });

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public Models.OpexStaffExpDiffModel GetOpexStaffExpDiffMtd(int id)
        {
           // List<int> IDs_of_currentSubCaptions = (List<int>)System.Web.HttpContext.Current.Session["IDs_of_currentSubCaptionsOtherInfo"];
            //List<int> IDs_of_currentSubCaptions = (List<int>)System.Web.HttpContext.Current.Session["IDs_of_currentSubCaptionsOtherInfo"];
            List<OpexStaffExpDiffModel> op = (List<OpexStaffExpDiffModel>)System.Web.HttpContext.Current.Session["opexstaffexpdiff"];

            OpexStaffExpDiffModel opObj = op.Where(x => x.ID == id).FirstOrDefault();

            return opObj;
        }

        //public void DeleteOpexStaffExpDiffMtd(int Id)
        //{
        //    using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
        //    {
        //        var cmd = new System.Data.SqlClient.SqlCommand("spp_deleteOpexStaffExpDiff", con);
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.CommandTimeout = 0;

        //        cmd.Parameters.Add(new SqlParameter
        //        {
        //            ParameterName = "id",
        //            Value = Id,
        //        });

        //        con.Open();
        //        cmd.ExecuteNonQuery();
        //        con.Close();
        //    }
        //} 

        public IEnumerable<Models.OPEX_DETAIL_SHARE_BUD> GetAllOPEXDetailShareBUDMtd()
        {
            List<OPEX_DETAIL_SHARE_BUD> oDetailList = new List<OPEX_DETAIL_SHARE_BUD>();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("spp_proc_sharedcost", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var op = new OPEX_DETAIL_SHARE_BUD();

                    op.TEAM_CODE = reader["TEAM_CODE"] != DBNull.Value ? reader["TEAM_CODE"].ToString() : "null";
                    op.BranchName = reader["BranchName"] != DBNull.Value ? reader["BranchName"].ToString() : "null";
                    op.RegionName = reader["RegionName"] != DBNull.Value ? reader["RegionName"].ToString() : "null";
                    op.DIRECTORATENAME = reader["DIRECTORATENAME"] != DBNull.Value ? reader["DIRECTORATENAME"].ToString() : "null";
                    op.CAPTION = reader["CAPTION"] != DBNull.Value ? reader["CAPTION"].ToString() : "null";
                    op.AMOUNT = reader["AMOUNT"] != DBNull.Value ? double.Parse(reader["AMOUNT"].ToString()) : 0;

                    oDetailList.Add(op);
                }
                con.Close();
                //System.Web.HttpContext.Current.Session["opexstaffexpdiff"] = opexstaffexpdiffList;
            }
            return oDetailList;
        } //========== end of the mtd

        public IEnumerable<Models.OPEX_DETAIL_SHARE_BUD> GetAllOPEXDetailShareBUDMtd_2(string svalue)
        {
            List<OPEX_DETAIL_SHARE_BUD> oDetailList = new List<OPEX_DETAIL_SHARE_BUD>();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("spp_proc_sharedcost_2", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "SearchValue",
                    Value = svalue,
                });

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var op = new OPEX_DETAIL_SHARE_BUD();

                    op.TEAM_CODE = reader["TEAM_CODE"] != DBNull.Value ? reader["TEAM_CODE"].ToString() : "null";
                    op.BranchName = reader["BranchName"] != DBNull.Value ? reader["BranchName"].ToString() : "null";
                    op.RegionName = reader["RegionName"] != DBNull.Value ? reader["RegionName"].ToString() : "null";
                    op.DIRECTORATENAME = reader["DIRECTORATENAME"] != DBNull.Value ? reader["DIRECTORATENAME"].ToString() : "null";
                    op.CAPTION = reader["CAPTION"] != DBNull.Value ? reader["CAPTION"].ToString() : "null";
                    op.AMOUNT = reader["AMOUNT"] != DBNull.Value ? double.Parse(reader["AMOUNT"].ToString()) : 0;

                    oDetailList.Add(op);
                }
                con.Close();
                //System.Web.HttpContext.Current.Session["opexstaffexpdiff"] = opexstaffexpdiffList;
            }
            return oDetailList;
        } //========== end of the mtd

        public IEnumerable<Models.sharedCostCC> GetAllsharedCostCCMtd()
        {
            List<sharedCostCC> oCCList = new List<sharedCostCC>();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("spp_sharedCostCC", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var op = new sharedCostCC();

                    op.MIS_CODE = reader["MIS_CODE"] != DBNull.Value ? reader["MIS_CODE"].ToString() : "null";
                    op.AMOUNT = reader["AMOUNT"] != DBNull.Value ? double.Parse(reader["AMOUNT"].ToString()) : 0;
                    //op.TEAM_CODE = reader["TEAM_CODE"] != DBNull.Value ? reader["TEAM_CODE"].ToString() : "null";

                    oCCList.Add(op);
                }
                con.Close();
                //System.Web.HttpContext.Current.Session["opexstaffexpdiff"] = opexstaffexpdiffList;
            }
            return oCCList;
        } //========== end of the mtd


        public List<string> GetCaptions()
        {
            List<string> ls = new List<string>();
            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {              
                var cmd = new System.Data.SqlClient.SqlCommand("spp_groupedCaptions", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //var op = new OpexStaffExpDiffModel();

                   // op.ID = reader["ID"] != DBNull.Value ? int.Parse(reader["ID"].ToString()) : 0;
                    string a = reader["Caption"] != DBNull.Value ? reader["Caption"].ToString() : "null";
                    
                    ls.Add(a);
                }
                con.Close();
                //System.Web.HttpContext.Current.Session["opexstaffexpdiff"] = opexstaffexpdiffList;
            }
            return ls;
        } //========== end of the mtd

        public List<string> GetCaptionItem(string caption)
        {
            List<string> ls = new List<string>();
            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("spp_captionItems", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "caption",
                    Value = caption,
                });


                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //var op = new OpexStaffExpDiffModel();

                    // op.ID = reader["ID"] != DBNull.Value ? int.Parse(reader["ID"].ToString()) : 0;
                    string a = reader["CaptionName"] != DBNull.Value ? reader["CaptionName"].ToString() : "null";

                    ls.Add(a);
                }
                con.Close();
                //System.Web.HttpContext.Current.Session["opexstaffexpdiff"] = opexstaffexpdiffList;
            }
            return ls;
        } //========== end of the mtd

    }
}