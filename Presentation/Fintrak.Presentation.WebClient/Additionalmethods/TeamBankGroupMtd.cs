using Fintrak.Presentation.WebClient.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Fintrak.Presentation.WebClient.Additionalmethods
{
    public class TeamBankGroupMtd
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

        public IEnumerable<Models.teamBankGroupDropDownModel> GetTeamstructureByDefinitionCode(string code)
        {
            List<teamBankGroupDropDownModel> teambankgroupList = new List<teamBankGroupDropDownModel>();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("spp_getteambankgroupnamecode", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Code",
                    Value = code,
                });


                con.Open();
                //cmd.ExecuteNonQuery();
                //cmd2.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var tb = new teamBankGroupDropDownModel();

                    if (code == "DIR")
                    {
                        tb.Directorate_Code = reader["Division_Code"] != DBNull.Value ? reader["Division_Code"].ToString() : "default";
                        tb.DirectorateName = reader["DivisionName"] != DBNull.Value ? reader["DivisionName"].ToString() : "default";
                    }
                    //else if (code == "ACCT")
                    //{
                    //    tb.AccountOfficer_Code = reader["AccountOfficer_Code"] != DBNull.Value ? reader["AccountOfficer_Code"].ToString() : "default";
                    //    tb.AccountOfficer_Name = reader["AccountOfficer_Name"] != DBNull.Value ? reader["AccountOfficer_Name"].ToString() : "default";
                    //}
                    else if (code == "BRH")
                    {
                        tb.Branch_Code = reader["Team_Code"] != DBNull.Value ? reader["Team_Code"].ToString() : "default";
                        tb.BranchName = reader["TeamName"] != DBNull.Value ? reader["TeamName"].ToString() : "default";
                    }
                    else if (code == "DIV")
                    {
                        tb.Division_Code = reader["Branch_Code"] != DBNull.Value ? reader["Branch_Code"].ToString() : "default";
                        tb.DivisionName = reader["BranchName"] != DBNull.Value ? reader["BranchName"].ToString() : "default";
                    }
                    else if (code == "REG")
                    {
                        tb.Region_Code = reader["Region_Code"] != DBNull.Value ? reader["Region_Code"].ToString() : "default";
                        tb.RegionName = reader["RegionName"] != DBNull.Value ? reader["RegionName"].ToString() : "default";
                    }

                    teambankgroupList.Add(tb);
                }
                con.Close();
            }
            return teambankgroupList;
        } //========== end of the mtd

        public IEnumerable<Models.AccountOfficer> GetTeamstructureByAcct(string code, string search)
        {
            List<AccountOfficer> acctOfficerList = new List<AccountOfficer>();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("spp_getteambankacctofficer", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

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
                    var tb = new AccountOfficer();

                    if (code == "ACCT")
                    {
                        tb.AccountOfficer_Code = reader["AccountOfficer_Code"] != DBNull.Value ? reader["AccountOfficer_Code"].ToString() : "default";
                        tb.AccountOfficer_Name = reader["AccountOfficer_Name"] != DBNull.Value ? reader["AccountOfficer_Name"].ToString() : "default";
                    }
                    acctOfficerList.Add(tb);
                }
                con.Close();
            }
            return acctOfficerList;
        } //========== end of the mtd





    }
}