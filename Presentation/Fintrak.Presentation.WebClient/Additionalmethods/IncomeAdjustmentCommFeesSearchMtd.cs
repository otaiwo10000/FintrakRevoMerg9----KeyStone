using Fintrak.Presentation.WebClient.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Fintrak.Presentation.WebClient.Additionalmethods
{
    public class IncomeAdjustmentCommFeesSearchMtd
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;      

        public IEnumerable<Models.IncomeAdjustmentCommFeesSearchModel> GetCommFees(int yr, int pr, string search)
        {
            search = search.Replace("FORWARDSLASHXTER", "/");
            search = search.Replace("DOTXTER", ".");

            List<IncomeAdjustmentCommFeesSearchModel> ddbList = new List<IncomeAdjustmentCommFeesSearchModel>();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("Income_Adjustment_CommFeesSearch", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Year",
                    Value = yr,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Period",
                    Value = pr,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Search",
                    Value = search,
                });

                con.Open();
                //cmd.ExecuteNonQuery();
                //cmd2.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var ddb = new IncomeAdjustmentCommFeesSearchModel();

                    ddb.ID = reader["ID"] != DBNull.Value ? int.Parse(reader["ID"].ToString()) : 0;

                    ddb.MIS_Code = reader["MIS_Code"] != DBNull.Value ? reader["MIS_Code"].ToString() : "default";
                    ddb.BranchCode = reader["BranchCode"] != DBNull.Value ? reader["BranchCode"].ToString() : "default";
                    ddb.Inc_Exp = reader["Inc_Exp"] != DBNull.Value ? reader["Inc_Exp"].ToString() : "default";
                    ddb.Amount = reader["Amount"] != DBNull.Value ? decimal.Parse(reader["Amount"].ToString()) : 0;
                    ddb.CurrencyType = reader["CurrencyType"] != DBNull.Value ? reader["CurrencyType"].ToString() : "default";
                    ddb.GL_Code = reader["GL_Code"] != DBNull.Value ? reader["GL_Code"].ToString() : "default";
                    ddb.RelatedAccount = reader["RelatedAccount"] != DBNull.Value ? reader["RelatedAccount"].ToString() : "default";
                    ddb.Narrative = reader["Narrative"] != DBNull.Value ? reader["Narrative"].ToString() : "default";
                    ddb.Period = reader["Period"] != DBNull.Value ? int.Parse(reader["Period"].ToString()) : 0;
                    ddb.Year = reader["Year"] != DBNull.Value ? int.Parse(reader["Year"].ToString()) : 0;
                    //ddb.CustomerCode = reader["CustomerCode"] != DBNull.Value ? reader["CustomerCode"].ToString() : "default";
                    ddb.AccountOfficer_Code = reader["AccountOfficer_Code"] != DBNull.Value ? reader["AccountOfficer_Code"].ToString() : "default";
                    ddb.CustomerName = reader["CustomerName"] != DBNull.Value ? reader["CustomerName"].ToString() : "default";
                    ddb.P_Date = reader["P_Date"] != DBNull.Value ? DateTime.Parse(reader["P_Date"].ToString()) : DateTime.Parse("1000-01-01");
                    ddb.Caption = reader["Caption"] != DBNull.Value ? reader["Caption"].ToString() : "default";
                    ddb.Tran_ID = reader["Tran_ID"] != DBNull.Value ? reader["Tran_ID"].ToString() : "default";
                    //ddb.Tran_Date = reader["Tran_Date"] != DBNull.Value ? DateTime.Parse(reader["Tran_Date"].ToString()) : DateTime.Parse("1000-01-01");
                    ddb.GLName = reader["GLName"] != DBNull.Value ? reader["GLName"].ToString() : "default";
                    ddb.EntryStatus = reader["EntryStatus"] != DBNull.Value ? reader["EntryStatus"].ToString() : "default";
                   // ddb.GroupCaption = reader["GroupCaption"] != DBNull.Value ? reader["GroupCaption"].ToString() : "default";
                    ddb.Rate = reader["Rate"] != DBNull.Value ? decimal.Parse(reader["Rate"].ToString()) : 0;
                    ddb.Raw_Amt = reader["Raw_Amt"] != DBNull.Value ? decimal.Parse(reader["Raw_Amt"].ToString()) : 0;

                    ddb.Sub_Head_GL_Code = reader["Sub_Head_GL_Code"] != DBNull.Value ? reader["Sub_Head_GL_Code"].ToString() : "default";
                    ddb.ProductCode = reader["ProductCode"] != DBNull.Value ? reader["ProductCode"].ToString() : "default";
                    //ddb.Trans_Code = reader["Trans_Code"] != DBNull.Value ? reader["Trans_Code"].ToString() : "default";
                    //ddb.Ref_Num = reader["Ref_Num"] != DBNull.Value ? reader["Ref_Num"].ToString() : "default";
                    //ddb.rcre_user_id = reader["rcre_user_id"] != DBNull.Value ? reader["rcre_user_id"].ToString() : "default";
                    //ddb.entry_user_id = reader["entry_user_id"] != DBNull.Value ? reader["entry_user_id"].ToString() : "default";
                    //ddb.Co_Dode = reader["Co_Dode"] != DBNull.Value ? reader["Co_Dode"].ToString() : "default";
                    //ddb.Co_AO = reader["Co_AO"] != DBNull.Value ? reader["Co_AO"].ToString() : "default";
                    //ddb.TranIDLEN = reader["TranIDLEN"] != DBNull.Value ? int.Parse(reader["TranIDLEN"].ToString()) : 0;
                    //ddb.T24Key = reader["T24Key"] != DBNull.Value ? reader["T24Key"].ToString() : "default";
                   
                    ddbList.Add(ddb);
                }
                con.Close();
            }
            return ddbList;
        } //========== end of the mtd


        public Models.IncomeAdjustmentCommFeesSearchModel GetCommFeeById(int Id)
        {
            var comfee = new IncomeAdjustmentCommFeesSearchModel();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("Income_adjustment_commfeesSearch_Id", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "ID",
                    Value = Id,
                });


                con.Open();
                //cmd.ExecuteNonQuery();
                //cmd2.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    comfee.ID = reader["ID"] != DBNull.Value ? int.Parse(reader["ID"].ToString()) : 0;

                    comfee.MIS_Code = reader["MIS_Code"] != DBNull.Value ? reader["MIS_Code"].ToString() : "default";
                    comfee.BranchCode = reader["BranchCode"] != DBNull.Value ? reader["BranchCode"].ToString() : "default";
                    comfee.Inc_Exp = reader["Inc_Exp"] != DBNull.Value ? reader["Inc_Exp"].ToString() : "default";
                    comfee.Amount = reader["Amount"] != DBNull.Value ? decimal.Parse(reader["Amount"].ToString()) : 0;
                    comfee.CurrencyType = reader["CurrencyType"] != DBNull.Value ? reader["CurrencyType"].ToString() : "default";
                    comfee.GL_Code = reader["GL_Code"] != DBNull.Value ? reader["GL_Code"].ToString() : "default";
                    comfee.RelatedAccount = reader["RelatedAccount"] != DBNull.Value ? reader["RelatedAccount"].ToString() : "default";
                    comfee.Narrative = reader["Narrative"] != DBNull.Value ? reader["Narrative"].ToString() : "default";
                    comfee.Period = reader["Period"] != DBNull.Value ? int.Parse(reader["Period"].ToString()) : 0;
                    comfee.Year = reader["Year"] != DBNull.Value ? int.Parse(reader["Year"].ToString()) : 0;
                    comfee.AccountOfficer_Code = reader["AccountOfficer_Code"] != DBNull.Value ? reader["AccountOfficer_Code"].ToString() : "default";
                    comfee.CustomerName = reader["CustomerName"] != DBNull.Value ? reader["CustomerName"].ToString() : "default";
                    comfee.P_Date = reader["P_Date"] != DBNull.Value ? DateTime.Parse(reader["P_Date"].ToString()) : DateTime.Parse("1000-01-01");
                    comfee.Caption = reader["Caption"] != DBNull.Value ? reader["Caption"].ToString() : "default";
                    comfee.Tran_ID = reader["Tran_ID"] != DBNull.Value ? reader["Tran_ID"].ToString() : "default";
                    comfee.GLName = reader["GLName"] != DBNull.Value ? reader["GLName"].ToString() : "default";
                    comfee.EntryStatus = reader["EntryStatus"] != DBNull.Value ? reader["EntryStatus"].ToString() : "default";
                    comfee.Rate = reader["Rate"] != DBNull.Value ? decimal.Parse(reader["Rate"].ToString()) : 0;
                    comfee.Raw_Amt = reader["Raw_Amt"] != DBNull.Value ? decimal.Parse(reader["Raw_Amt"].ToString()) : 0;

                    comfee.Sub_Head_GL_Code = reader["Sub_Head_GL_Code"] != DBNull.Value ? reader["Sub_Head_GL_Code"].ToString() : "default";
                    comfee.ProductCode = reader["ProductCode"] != DBNull.Value ? reader["ProductCode"].ToString() : "default";
                }
                con.Close();
            }
            return comfee;
        } //========== end of the mtd

        public void AddIncomeAdjustmentCommFeesSearch(IncomeAdjustmentCommFeesSearchModel addv)
        {
            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("proc_Income_CommFeesAdd", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "MIS_Code",
                    Value = addv.MIS_Code,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "BranchCode",
                    Value = addv.BranchCode,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Inc_Exp",
                    Value = addv.Inc_Exp,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Amount",
                    Value = addv.Amount,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "CurrencyType",
                    Value = addv.CurrencyType,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "GL_Code",
                    Value = addv.GL_Code,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "sub_head",
                    Value = addv.Sub_Head_GL_Code,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "RelatedAccount",
                    Value = addv.RelatedAccount,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Narrative",
                    Value = addv.Narrative,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "CustomerName",
                    Value = addv.CustomerName,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Accountofficer",
                    Value = addv.AccountOfficer_Code,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Caption",
                    Value = addv.Caption,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Username",
                    Value = addv.username,
                });


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void UpdateIncomeAdjustmentCommFeesSearch(IncomeAdjustmentCommFeesSearchModel updatev)
        {           
            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("Income_Adjustment_CommFeesUpdate", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "id",
                    Value = updatev.ID,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "miscode",
                    Value = updatev.MIS_Code,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "accountoff",
                    Value = updatev.AccountOfficer_Code,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Narration",
                    Value = updatev.Narrative,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "AcctNo",
                    Value = updatev.RelatedAccount,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Amount",
                    Value = updatev.Amount,
                });


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void UpdateIncomeAdjustmentCommFeesSearchACCESS(IncomeAdjustmentCommFeesSearchModel updatev)
        {
            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("Income_Adjustment_CommFeesUpdate", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "id",
                    Value = updatev.ID,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "miscode",
                    Value = updatev.MIS_Code,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "accountoff",
                    Value = updatev.AccountOfficer_Code,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Narration",
                    Value = updatev.Narrative,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "AcctNo",
                    Value = updatev.RelatedAccount,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "CustomerName",
                    Value = updatev.CustomerName,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Amount",
                    Value = updatev.Amount,
                });


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void DeleteIncomeAdjustmentCommFeesSearch(int Id)
        {
            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("income_adjustment_commfeesdelete", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "ID",
                    Value = Id,
                });

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }


    }
}