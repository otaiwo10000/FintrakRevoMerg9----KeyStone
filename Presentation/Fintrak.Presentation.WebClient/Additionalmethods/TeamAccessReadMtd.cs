using Fintrak.Presentation.WebClient.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Fintrak.Presentation.WebClient.Additionalmethods
{
    public class TeamAccessReadMtd
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;      

        public IEnumerable<Models.TeamAccessReadModel> GetTeamAccessRead()
        {
            List<TeamAccessReadModel> obList = new List<TeamAccessReadModel>();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("", con);

                con.Open();
                //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "SELECT top 1000 * FROM Team";
                //cmd.CommandTimeout = 0;

                //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

               
                //cmd.ExecuteNonQuery();
                //cmd2.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var ob = new TeamAccessReadModel();

                    //iob.ID = reader["ID"] != DBNull.Value ? int.Parse(reader["ID"].ToString()) : 0;
                    ob.Accountofficer_Code1 = reader["Accountofficer_Code1"] != DBNull.Value ? reader["Accountofficer_Code1"].ToString() : "default";
                    ob.AccountofficerName1 = reader["AccountofficerName1"] != DBNull.Value ? reader["AccountofficerName1"].ToString() : "default";
                    ob.Team_Code = reader["Team_Code"] != DBNull.Value ? reader["Team_Code"].ToString() : "default";
                    ob.TeamName = reader["TeamName"] != DBNull.Value ? reader["TeamName"].ToString() : "default";
                    ob.Branch_Code = reader["Branch_Code"] != DBNull.Value ? reader["Branch_Code"].ToString() : "default";
                    ob.BranchName = reader["BranchName"] != DBNull.Value ? reader["BranchName"].ToString() : "default";
                    ob.Group_Code = reader["Group_Code"] != DBNull.Value ? reader["Group_Code"].ToString() : "default";
                    ob.GroupName = reader["GroupName"] != DBNull.Value ? reader["GroupName"].ToString() : "default";
                    ob.Region_Code = reader["Region_Code"] != DBNull.Value ? reader["Region_Code"].ToString() : "default";
                    ob.RegionName = reader["RegionName"] != DBNull.Value ? reader["RegionName"].ToString() : "default";
                    ob.Division_Code = reader["Division_Code"] != DBNull.Value ? reader["Division_Code"].ToString() : "default";
                    ob.DivisionName = reader["DivisionName"] != DBNull.Value ? reader["DivisionName"].ToString() : "default";
                    ob.PPRCategory = reader["PPRCategory"] != DBNull.Value ? reader["PPRCategory"].ToString() : "default";
                    ob.Year = reader["Year"] != DBNull.Value ? int.Parse(reader["Year"].ToString()) : 0;
                    ob.StaffID = reader["StaffID"] != DBNull.Value ? reader["StaffID"].ToString() : "default";
                    ob.SegmentName = reader["SegmentName"] != DBNull.Value ? reader["SegmentName"].ToString() : "default";
                    ob.Segment_Code = reader["Segment_Code"] != DBNull.Value ? reader["Segment_Code"].ToString() : "default";
                    ob.SectorName = reader["SectorName"] != DBNull.Value ? reader["SectorName"].ToString() : "default";
                    ob.Sector_Code = reader["Sector_Code"] != DBNull.Value ? reader["Sector_Code"].ToString() : "default";
                    ob.IsRelationshipManager = reader["IsRelationshipManager"] != DBNull.Value ? bool.Parse(reader["IsRelationshipManager"].ToString()) : false;
                    ob.Zone_Code = reader["Zone_Code"] != DBNull.Value ? reader["Zone_Code"].ToString() : "default";
                    ob.ZoneName = reader["ZoneName"] != DBNull.Value ? reader["ZoneName"].ToString() : "default";
                    ob.Strategy_Code = reader["Strategy_Code"] != DBNull.Value ? reader["Strategy_Code"].ToString() : "default";
                    ob.StrategyName = reader["StrategyName"] != DBNull.Value ? reader["StrategyName"].ToString() : "default";
                    ob.branchcode = reader["branchcode"] != DBNull.Value ? reader["branchcode"].ToString() : "default";
                    ob.SuperSegment_Code = reader["SuperSegment_Code"] != DBNull.Value ? reader["SuperSegment_Code"].ToString() : "default";
                    ob.SuperSegmentName = reader["SuperSegmentName"] != DBNull.Value ? reader["SuperSegmentName"].ToString() : "default";
                    obList.Add(ob);
                }
                con.Close();
            }
            return obList;
        } //========== end of the mtd

        public IEnumerable<Models.TeamAccessReadModel> GetTeamAccessReadUsingParams(string search)
        {
            List<TeamAccessReadModel> obList = new List<TeamAccessReadModel>();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("", con);

                con.Open();
                //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "SELECT top 1000 * FROM Team "
                    + " where Accountofficer_Code1 like @searchval";
                cmd.Parameters.AddWithValue("@searchval", "%" + search + "%");
                //cmd.CommandTimeout = 0;

                //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;


                //cmd.ExecuteNonQuery();
                //cmd2.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var ob = new TeamAccessReadModel();

                    //iob.ID = reader["ID"] != DBNull.Value ? int.Parse(reader["ID"].ToString()) : 0;
                    ob.Accountofficer_Code1 = reader["Accountofficer_Code1"] != DBNull.Value ? reader["Accountofficer_Code1"].ToString() : "default";
                    ob.AccountofficerName1 = reader["AccountofficerName1"] != DBNull.Value ? reader["AccountofficerName1"].ToString() : "default";
                    ob.Team_Code = reader["Team_Code"] != DBNull.Value ? reader["Team_Code"].ToString() : "default";
                    ob.TeamName = reader["TeamName"] != DBNull.Value ? reader["TeamName"].ToString() : "default";
                    ob.Branch_Code = reader["Branch_Code"] != DBNull.Value ? reader["Branch_Code"].ToString() : "default";
                    ob.BranchName = reader["BranchName"] != DBNull.Value ? reader["BranchName"].ToString() : "default";
                    ob.Group_Code = reader["Group_Code"] != DBNull.Value ? reader["Group_Code"].ToString() : "default";
                    ob.GroupName = reader["GroupName"] != DBNull.Value ? reader["GroupName"].ToString() : "default";
                    ob.Region_Code = reader["Region_Code"] != DBNull.Value ? reader["Region_Code"].ToString() : "default";
                    ob.RegionName = reader["RegionName"] != DBNull.Value ? reader["RegionName"].ToString() : "default";
                    ob.Division_Code = reader["Division_Code"] != DBNull.Value ? reader["Division_Code"].ToString() : "default";
                    ob.DivisionName = reader["DivisionName"] != DBNull.Value ? reader["DivisionName"].ToString() : "default";
                    ob.PPRCategory = reader["PPRCategory"] != DBNull.Value ? reader["PPRCategory"].ToString() : "default";
                    ob.Year = reader["Year"] != DBNull.Value ? int.Parse(reader["Year"].ToString()) : 0;
                    ob.StaffID = reader["StaffID"] != DBNull.Value ? reader["StaffID"].ToString() : "default";
                    ob.SegmentName = reader["SegmentName"] != DBNull.Value ? reader["SegmentName"].ToString() : "default";
                    ob.Segment_Code = reader["Segment_Code"] != DBNull.Value ? reader["Segment_Code"].ToString() : "default";
                    ob.SectorName = reader["SectorName"] != DBNull.Value ? reader["SectorName"].ToString() : "default";
                    ob.Sector_Code = reader["Sector_Code"] != DBNull.Value ? reader["Sector_Code"].ToString() : "default";
                    ob.IsRelationshipManager = reader["IsRelationshipManager"] != DBNull.Value ? bool.Parse(reader["IsRelationshipManager"].ToString()) : false;
                    ob.Zone_Code = reader["Zone_Code"] != DBNull.Value ? reader["Zone_Code"].ToString() : "default";
                    ob.ZoneName = reader["ZoneName"] != DBNull.Value ? reader["ZoneName"].ToString() : "default";
                    ob.Strategy_Code = reader["Strategy_Code"] != DBNull.Value ? reader["Strategy_Code"].ToString() : "default";
                    ob.StrategyName = reader["StrategyName"] != DBNull.Value ? reader["StrategyName"].ToString() : "default";
                    ob.branchcode = reader["branchcode"] != DBNull.Value ? reader["branchcode"].ToString() : "default";
                    ob.SuperSegment_Code = reader["SuperSegment_Code"] != DBNull.Value ? reader["SuperSegment_Code"].ToString() : "default";
                    ob.SuperSegmentName = reader["SuperSegmentName"] != DBNull.Value ? reader["SuperSegmentName"].ToString() : "default";
                    obList.Add(ob);
                }
                con.Close();
            }
            return obList;
        } //========== end of the mtd

        //public IEnumerable<Models.IncomeAdjustmentSummaryModel> IncomeAdjustmentGetSBUSummary(int pr, int yr, string cp)
        //{
        //    cp = cp.Replace("FORWARDSLASHXTER", "/");
        //    cp = cp.Replace("DOTXTER", ".");

        //    List<IncomeAdjustmentSummaryModel> obList = new List<IncomeAdjustmentSummaryModel>();

        //    using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
        //    {
        //        var cmd = new System.Data.SqlClient.SqlCommand("Income_Adjustment_GetSBUSummary", con);
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.CommandTimeout = 0;

        //        //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

        //        cmd.Parameters.Add(new SqlParameter
        //        {
        //            ParameterName = "Year",
        //            Value = yr,
        //        });

        //        cmd.Parameters.Add(new SqlParameter
        //        {
        //            ParameterName = "Period",
        //            Value = pr,
        //        });

        //        cmd.Parameters.Add(new SqlParameter
        //        {
        //            ParameterName = "Caption",
        //            Value = cp,
        //        });

        //        con.Open();
        //        //cmd.ExecuteNonQuery();
        //        //cmd2.ExecuteNonQuery();

        //        SqlDataReader reader = cmd.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            var ob = new IncomeAdjustmentSummaryModel();

        //            //iob.ID = reader["ID"] != DBNull.Value ? int.Parse(reader["ID"].ToString()) : 0;
        //            ob.sbu_Code = reader["sbu_Code"] != DBNull.Value ? reader["sbu_Code"].ToString() : "default";
        //            ob.SBUName = reader["SBUName"] != DBNull.Value ? reader["SBUName"].ToString() : "default";
        //            ob.ActualBalance = reader["Actual"] != DBNull.Value ? decimal.Parse(reader["Actual"].ToString()) : 0;
        //            ob.AverageBalance = reader["Average"] != DBNull.Value ? decimal.Parse(reader["Average"].ToString()) : 0;
        //            ob.Rev_Exp = reader["RevExp"] != DBNull.Value ? decimal.Parse(reader["RevExp"].ToString()) : 0;

        //            obList.Add(ob);
        //        }
        //        con.Close();
        //    }
        //    return obList;
        //} //========== end of the mtd

        //public IEnumerable<Models.IncomeAdjustmentSummaryModel> IncomeAdjustmentVolCaptionSummary(int pr, int yr, string cp)
        //{
        //    cp = cp.Replace("FORWARDSLASHXTER", "/");
        //    cp = cp.Replace("DOTXTER", ".");

        //    List<IncomeAdjustmentSummaryModel> obList = new List<IncomeAdjustmentSummaryModel>();

        //    using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
        //    {
        //        var cmd = new System.Data.SqlClient.SqlCommand("Income_Adjustment_VolCaptionSummary", con);
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.CommandTimeout = 0;

        //        //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

        //        cmd.Parameters.Add(new SqlParameter
        //        {
        //            ParameterName = "Year",
        //            Value = yr,
        //        });

        //        cmd.Parameters.Add(new SqlParameter
        //        {
        //            ParameterName = "Period",
        //            Value = pr,
        //        });

        //        cmd.Parameters.Add(new SqlParameter
        //        {
        //            ParameterName = "Caption",
        //            Value = cp,
        //        });

        //        con.Open();
        //        //cmd.ExecuteNonQuery();
        //        //cmd2.ExecuteNonQuery();

        //        SqlDataReader reader = cmd.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            var ob = new IncomeAdjustmentSummaryModel();

        //            //iob.ID = reader["ID"] != DBNull.Value ? int.Parse(reader["ID"].ToString()) : 0;
        //            ob.MIS_Code = reader["MIS_Code"] != DBNull.Value ? reader["MIS_Code"].ToString() : "default";
        //            ob.ActualBalance = reader["Actual Balance"] != DBNull.Value ? decimal.Parse(reader["Actual Balance"].ToString()) : 0;
        //            ob.AverageBalance = reader["Average Balance"] != DBNull.Value ? decimal.Parse(reader["Average Balance"].ToString()) : 0;
        //            ob.Rev_Exp = reader["Rev/Exp"] != DBNull.Value ? decimal.Parse(reader["Rev/Exp"].ToString()) : 0;

        //            obList.Add(ob);
        //        }
        //        con.Close();
        //    }
        //    return obList;
        //} //========== end of the mtd

        //public IEnumerable<Models.IncomeAdjustmentSummaryModel> IncomeAdjustmentCommFeesSBUCaptionSummary(int pr, int yr, string cp)
        //{
        //    cp = cp.Replace("FORWARDSLASHXTER", "/");
        //    cp = cp.Replace("DOTXTER", ".");

        //    List<IncomeAdjustmentSummaryModel> obList = new List<IncomeAdjustmentSummaryModel>();

        //    using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
        //    {
        //        var cmd = new System.Data.SqlClient.SqlCommand("Income_Adjustment_CommFeesSBUCaptionSummary", con);
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.CommandTimeout = 0;

        //        //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

        //        cmd.Parameters.Add(new SqlParameter
        //        {
        //            ParameterName = "Year",
        //            Value = yr,
        //        });

        //        cmd.Parameters.Add(new SqlParameter
        //        {
        //            ParameterName = "Period",
        //            Value = pr,
        //        });

        //        cmd.Parameters.Add(new SqlParameter
        //        {
        //            ParameterName = "Caption",
        //            Value = cp,
        //        });

        //        con.Open();
        //        //cmd.ExecuteNonQuery();
        //        //cmd2.ExecuteNonQuery();

        //        SqlDataReader reader = cmd.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            var ob = new IncomeAdjustmentSummaryModel();

        //            //iob.ID = reader["ID"] != DBNull.Value ? int.Parse(reader["ID"].ToString()) : 0;
        //            ob.sbu_Code = reader["sbu_Code"] != DBNull.Value ? reader["sbu_Code"].ToString() : "default";
        //            ob.SBUName = reader["SBUName"] != DBNull.Value ? reader["SBUName"].ToString() : "default";
        //            ob.Amount = reader["Comm & Fees Income"] != DBNull.Value ? decimal.Parse(reader["Comm & Fees Income"].ToString()) : 0;

        //            obList.Add(ob);
        //        }
        //        con.Close();
        //    }
        //    return obList;
        //} //========== end of the mtd

        //public IEnumerable<Models.IncomeAdjustmentSummaryModel> IncomeAdjustmentCommFeesCaptionSummary(int pr, int yr, string cp)
        //{
        //    cp = cp.Replace("FORWARDSLASHXTER", "/");
        //    cp = cp.Replace("DOTXTER", ".");

        //    List<IncomeAdjustmentSummaryModel> obList = new List<IncomeAdjustmentSummaryModel>();

        //    using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
        //    {
        //        var cmd = new System.Data.SqlClient.SqlCommand("Income_Adjustment_CommFeesCaptionSummary", con);
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.CommandTimeout = 0;

        //        //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

        //        cmd.Parameters.Add(new SqlParameter
        //        {
        //            ParameterName = "Year",
        //            Value = yr,
        //        });

        //        cmd.Parameters.Add(new SqlParameter
        //        {
        //            ParameterName = "Period",
        //            Value = pr,
        //        });

        //        cmd.Parameters.Add(new SqlParameter
        //        {
        //            ParameterName = "Caption",
        //            Value = cp,
        //        });

        //        con.Open();
        //        //cmd.ExecuteNonQuery();
        //        //cmd2.ExecuteNonQuery();

        //        SqlDataReader reader = cmd.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            var ob = new IncomeAdjustmentSummaryModel();

        //            //iob.ID = reader["ID"] != DBNull.Value ? int.Parse(reader["ID"].ToString()) : 0;
        //            ob.MIS_Code = reader["MIS_Code"] != DBNull.Value ? reader["MIS_Code"].ToString() : "default";
        //            ob.Amount = reader["Comm & Fees Income"] != DBNull.Value ? decimal.Parse(reader["Comm & Fees Income"].ToString()) : 0;

        //            obList.Add(ob);
        //        }
        //        con.Close();
        //    }
        //    return obList;
        //} //========== end of the mtd

        //public IEnumerable<Models.IncomeAdjustmentSummaryModel> GetIncomeAdjustmentVolProdSummary(int pr, int yr)
        //{
        //    List<IncomeAdjustmentSummaryModel> obList = new List<IncomeAdjustmentSummaryModel>();

        //    using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
        //    {
        //        var cmd = new System.Data.SqlClient.SqlCommand("Income_Adjustment_VolProdSummary", con);
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.CommandTimeout = 0;

        //        //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

        //        cmd.Parameters.Add(new SqlParameter
        //        {
        //            ParameterName = "Year",
        //            Value = yr,
        //        });

        //        cmd.Parameters.Add(new SqlParameter
        //        {
        //            ParameterName = "Period",
        //            Value = pr,
        //        });

        //        con.Open();
        //        //cmd.ExecuteNonQuery();
        //        //cmd2.ExecuteNonQuery();

        //        SqlDataReader reader = cmd.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            var ob = new IncomeAdjustmentSummaryModel();

        //            //iob.ID = reader["ID"] != DBNull.Value ? int.Parse(reader["ID"].ToString()) : 0;
        //            ob.ProductCode = reader["ProductCode"] != DBNull.Value ? reader["ProductCode"].ToString() : "default";
        //            ob.Caption = reader["Caption"] != DBNull.Value ? reader["Caption"].ToString() : "default";
        //            ob.ActualBalance = reader["Actual"] != DBNull.Value ? decimal.Parse(reader["Actual"].ToString()) : 0;
        //            ob.AverageBalance = reader["Average"] != DBNull.Value ? decimal.Parse(reader["Average"].ToString()) : 0;
        //            ob.Rev_Exp = reader["RevExp"] != DBNull.Value ? decimal.Parse(reader["RevExp"].ToString()) : 0;
        //            ob.Category = reader["Category"] != DBNull.Value ? reader["Category"].ToString() : "default";
        //            ob.CurrencyType = reader["Currency_Type"] != DBNull.Value ? reader["Currency_Type"].ToString() : "default";
        //            ob.Period = reader["Period"] != DBNull.Value ? int.Parse(reader["Period"].ToString()) : 0;
        //            ob.Year = reader["Year"] != DBNull.Value ? int.Parse(reader["Year"].ToString()) : 0;

        //            obList.Add(ob);
        //        }
        //        con.Close();
        //    }
        //    return obList;
        //} //========== end of the mtd

    }
}