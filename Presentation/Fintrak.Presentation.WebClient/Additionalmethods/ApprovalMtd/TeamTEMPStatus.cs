using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Fintrak.Shared.MPR.Entities;

namespace Fintrak.Presentation.WebClient.Additionalmethods.ApprovalMtd
{
    public class TeamTEMPStatus
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;
        int taketop = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["TakeTop"].Trim());

        public IEnumerable<TeamStructureALLTEMP> TeamStructureTEMP(string status)
        {
            List<TeamStructureALLTEMP> obuList = new List<TeamStructureALLTEMP>();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("", con);

                //cmd.CommandText = "select * from Names where Id=@Id";
                //cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                cmd.CommandText = "select top (@TAKETOP) TeamId, Accountofficer_Code1, AccountofficerName1, Team_Code, TeamName," +
                    "Branch_Code, BranchName, Group_Code, GroupName, Region_Code, RegionName, Division_Code, DivisionName," +
                    "PPRCategory, Year, StaffID, SegmentName, Segment_Code, SectorName, Sector_Code, Zone_Code, ZoneName," +
                    "SuperSegment_Code, SuperSegmentName, ApprovalStatus from Team_TEMP where ApprovalStatus=@STATUS";
                cmd.Parameters.AddWithValue("@STATUS", status);
                cmd.Parameters.AddWithValue("@TAKETOP", taketop);
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var obu = new TeamStructureALLTEMP();

                    obu.Team_StructureId = reader["TeamId"] != DBNull.Value ? int.Parse(reader["TeamId"].ToString()) : 0;

                    obu.AccountofficerName = reader["Accountofficer_Code1"] != DBNull.Value ? reader["Accountofficer_Code1"].ToString() : "";
                    obu.Accountofficer_Code = reader["AccountofficerName1"] != DBNull.Value ? reader["AccountofficerName1"].ToString() : "";
                    obu.Team_Code = reader["Team_Code"] != DBNull.Value ? reader["Team_Code"].ToString() : "";
                    obu.TeamName = reader["TeamName"] != DBNull.Value ? reader["TeamName"].ToString() : "";

                    obu.Branch_Code = reader["Branch_Code"] != DBNull.Value ? reader["Branch_Code"].ToString() : "";
                    obu.BranchName = reader["BranchName"] != DBNull.Value ? reader["BranchName"].ToString() : "";
                    obu.Group_Code = reader["Group_Code"] != DBNull.Value ? reader["Group_Code"].ToString() : "";
                    obu.GroupName = reader["GroupName"] != DBNull.Value ? reader["GroupName"].ToString() : "";

                    obu.Region_Code = reader["Region_Code"] != DBNull.Value ? reader["Region_Code"].ToString() : "";
                    obu.RegionName = reader["RegionName"] != DBNull.Value ? reader["RegionName"].ToString() : "";
                    obu.Division_Code = reader["Division_Code"] != DBNull.Value ? reader["Division_Code"].ToString() : "";
                    obu.DivisionName = reader["DivisionName"] != DBNull.Value ? reader["DivisionName"].ToString() : "";

                    obu.PPRCategory = reader["PPRCategory"] != DBNull.Value ? reader["PPRCategory"].ToString() : "";
                    obu.Year = reader["Year"] != DBNull.Value ? int.Parse(reader["Year"].ToString()) : 0;
                    obu.staff_id = reader["StaffID"] != DBNull.Value ? reader["StaffID"].ToString() : "";
                    obu.SegmentName = reader["SegmentName"] != DBNull.Value ? reader["SegmentName"].ToString() : "";
                    obu.Segment_Code = reader["Segment_Code"] != DBNull.Value ? reader["Segment_Code"].ToString() : "";

                    obu.Zone_Code = reader["Zone_Code"] != DBNull.Value ? reader["Zone_Code"].ToString() : "";
                    obu.ZoneName = reader["ZoneName"] != DBNull.Value ? reader["ZoneName"].ToString() : "";
                    obu.SuperSegment_Code = reader["SuperSegment_Code"] != DBNull.Value ? reader["SuperSegment_Code"].ToString() : "";
                    obu.SuperSegmentName = reader["SuperSegmentName"] != DBNull.Value ? reader["SuperSegmentName"].ToString() : "";
                    obu.ApprovalStatus = reader["ApprovalStatus"] != DBNull.Value ? reader["ApprovalStatus"].ToString() : "";

                    obuList.Add(obu);
                }
                con.Close();
            }
            return obuList;
        }

        public IEnumerable<TeamStructureALLTEMP> TeamStructureusingparamsTEMP(string status, string search)
        {
            List<TeamStructureALLTEMP> obuList = new List<TeamStructureALLTEMP>();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("", con);

                //cmd.CommandText = "select * from Names where Id=@Id";
                //cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                cmd.CommandText = "select top 500 TeamId, Accountofficer_Code1, AccountofficerName1, Team_Code, TeamName," +
                   "Branch_Code, BranchName, Group_Code, GroupName, Region_Code, RegionName, Division_Code, DivisionName," +
                   "PPRCategory, Year, StaffID, SegmentName, Segment_Code, SectorName, Sector_Code, Zone_Code, ZoneName," +
                   "SuperSegment_Code, SuperSegmentName, ApprovalStatus from Team_TEMP where ApprovalStatus=@STATUS and" +
                   "(Team_Code like @miscode or Branch_Code like @miscode or Group_Code like @miscode or Region_Code like @miscode or " +
                    "Division_Code like @miscode or Segment_Code like @miscode or SuperSegment_Code like @miscode or SuperSegment_Code like @miscode)";
               cmd.Parameters.AddWithValue("@STATUS", status);
                cmd.Parameters.AddWithValue("@miscode", "%" + search + "%");
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var obu = new TeamStructureALLTEMP();

                    obu.Team_StructureId = reader["TeamId"] != DBNull.Value ? int.Parse(reader["TeamId"].ToString()) : 0;

                    obu.AccountofficerName = reader["Accountofficer_Code1"] != DBNull.Value ? reader["Accountofficer_Code1"].ToString() : "";
                    obu.Accountofficer_Code = reader["AccountofficerName1"] != DBNull.Value ? reader["AccountofficerName1"].ToString() : "";
                    obu.Team_Code = reader["Team_Code"] != DBNull.Value ? reader["Team_Code"].ToString() : "";
                    obu.TeamName = reader["TeamName"] != DBNull.Value ? reader["TeamName"].ToString() : "";

                    obu.Branch_Code = reader["Branch_Code"] != DBNull.Value ? reader["Branch_Code"].ToString() : "";
                    obu.BranchName = reader["BranchName"] != DBNull.Value ? reader["BranchName"].ToString() : "";
                    obu.Group_Code = reader["Group_Code"] != DBNull.Value ? reader["Group_Code"].ToString() : "";
                    obu.GroupName = reader["GroupName"] != DBNull.Value ? reader["GroupName"].ToString() : "";

                    obu.Region_Code = reader["Region_Code"] != DBNull.Value ? reader["Region_Code"].ToString() : "";
                    obu.RegionName = reader["RegionName"] != DBNull.Value ? reader["RegionName"].ToString() : "";
                    obu.Division_Code = reader["Division_Code"] != DBNull.Value ? reader["Division_Code"].ToString() : "";
                    obu.DivisionName = reader["DivisionName"] != DBNull.Value ? reader["DivisionName"].ToString() : "";

                    obu.PPRCategory = reader["PPRCategory"] != DBNull.Value ? reader["PPRCategory"].ToString() : "";
                    obu.Year = reader["Year"] != DBNull.Value ? int.Parse(reader["Year"].ToString()) : 0;
                    obu.staff_id = reader["StaffID"] != DBNull.Value ? reader["StaffID"].ToString() : "";
                    obu.SegmentName = reader["SegmentName"] != DBNull.Value ? reader["SegmentName"].ToString() : "";
                    obu.Segment_Code = reader["Segment_Code"] != DBNull.Value ? reader["Segment_Code"].ToString() : "";

                    obu.Zone_Code = reader["Zone_Code"] != DBNull.Value ? reader["Zone_Code"].ToString() : "";
                    obu.ZoneName = reader["ZoneName"] != DBNull.Value ? reader["ZoneName"].ToString() : "";
                    obu.SuperSegment_Code = reader["SuperSegment_Code"] != DBNull.Value ? reader["SuperSegment_Code"].ToString() : "";
                    obu.SuperSegmentName = reader["SuperSegmentName"] != DBNull.Value ? reader["SuperSegmentName"].ToString() : "";
                    obu.ApprovalStatus = reader["ApprovalStatus"] != DBNull.Value ? reader["ApprovalStatus"].ToString() : "";

                    obuList.Add(obu);
                }
                con.Close();
            }
            return obuList;
        }


        //======================== awaiting starts ================================================================================
        public IEnumerable<TeamStructureALLTEMP> TeamStructureAWAITING()
        {
            List<TeamStructureALLTEMP> obuList = new List<TeamStructureALLTEMP>();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("", con);

                //cmd.CommandText = "select * from Names where Id=@Id";
                //cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                cmd.CommandText = "select top 500 TeamId, Accountofficer_Code1, AccountofficerName1, Team_Code, TeamName," +
                    "Branch_Code, BranchName, Group_Code, GroupName, Region_Code, RegionName, Division_Code, DivisionName," +
                    "PPRCategory, Year, StaffID, SegmentName, Segment_Code, SectorName, Sector_Code, Zone_Code, ZoneName," +
                    "SuperSegment_Code, SuperSegmentName, ApprovalStatus from Team_TEMP where ApprovalStatus='AWAITINGAPPROVAL'";
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var obu = new TeamStructureALLTEMP();

                    obu.Team_StructureId = reader["TeamId"] != DBNull.Value ? int.Parse(reader["TeamId"].ToString()) : 0;

                    obu.AccountofficerName = reader["Accountofficer_Code1"] != DBNull.Value ? reader["Accountofficer_Code1"].ToString() : "";
                    obu.Accountofficer_Code = reader["AccountofficerName1"] != DBNull.Value ? reader["AccountofficerName1"].ToString() : "";
                    obu.Team_Code = reader["Team_Code"] != DBNull.Value ? reader["Team_Code"].ToString() : "";
                    obu.TeamName = reader["TeamName"] != DBNull.Value ? reader["TeamName"].ToString() : "";

                    obu.Branch_Code = reader["Branch_Code"] != DBNull.Value ? reader["Branch_Code"].ToString() : "";
                    obu.BranchName = reader["BranchName"] != DBNull.Value ? reader["BranchName"].ToString() : "";
                    obu.Group_Code = reader["Group_Code"] != DBNull.Value ? reader["Group_Code"].ToString() : "";
                    obu.GroupName = reader["GroupName"] != DBNull.Value ? reader["GroupName"].ToString() : "";

                    obu.Region_Code = reader["Region_Code"] != DBNull.Value ? reader["Region_Code"].ToString() : "";
                    obu.RegionName = reader["RegionName"] != DBNull.Value ? reader["RegionName"].ToString() : "";
                    obu.Division_Code = reader["Division_Code"] != DBNull.Value ? reader["Division_Code"].ToString() : "";
                    obu.DivisionName = reader["DivisionName"] != DBNull.Value ? reader["DivisionName"].ToString() : "";

                    obu.PPRCategory = reader["PPRCategory"] != DBNull.Value ? reader["PPRCategory"].ToString() : "";
                    obu.Year =  reader["Year"] != DBNull.Value ? int.Parse(reader["Year"].ToString()) : 0;
                    obu.staff_id = reader["StaffID"] != DBNull.Value ? reader["StaffID"].ToString() : "";
                    obu.SegmentName = reader["SegmentName"] != DBNull.Value ? reader["SegmentName"].ToString() : "";
                    obu.Segment_Code = reader["Segment_Code"] != DBNull.Value ? reader["Segment_Code"].ToString() : "";

                    obu.Zone_Code = reader["Zone_Code"] != DBNull.Value ? reader["Zone_Code"].ToString() : "";
                    obu.ZoneName = reader["ZoneName"] != DBNull.Value ? reader["ZoneName"].ToString() : "";
                    obu.SuperSegment_Code = reader["SuperSegment_Code"] != DBNull.Value ? reader["SuperSegment_Code"].ToString() : "";
                    obu.SuperSegmentName = reader["SuperSegmentName"] != DBNull.Value ? reader["SuperSegmentName"].ToString() : "";
                    obu.ApprovalStatus = reader["ApprovalStatus"] != DBNull.Value ? reader["ApprovalStatus"].ToString() : "";

                    obuList.Add(obu);
                }
                con.Close();
            }
            return obuList;
        }

        public IEnumerable<TeamStructureALLTEMP> TeamStructureusingparamsAWAITING(string search)
        {
            List<TeamStructureALLTEMP> obuList = new List<TeamStructureALLTEMP>();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("", con);

                //cmd.CommandText = "select * from Names where Id=@Id";
                //cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                cmd.CommandText = "select top 500 TeamId, Accountofficer_Code1, AccountofficerName1, Team_Code, TeamName," +
                    "Branch_Code, BranchName, Group_Code, GroupName, Region_Code, RegionName, Division_Code, DivisionName," +
                    "PPRCategory, Year, StaffID, SegmentName, Segment_Code, SectorName, Sector_Code, Zone_Code, ZoneName," +
                    "SuperSegment_Code, SuperSegmentName, ApprovalStatus from Team_TEMP where ApprovalStatus='AWAITINGAPPROVAL' " +
                    "and (Team_Code=@miscode or Branch_Code=@miscode or Group_Code=@miscode or Region_Code=@miscode or " +
                    "Division_Code=@miscode or Segment_Code=@miscode or SuperSegment_Code=@miscode or SuperSegment_Code=@miscode)";
                cmd.Parameters.AddWithValue("@miscode", search);
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var obu = new TeamStructureALLTEMP();

                    obu.Team_StructureId = reader["TeamId"] != DBNull.Value ? int.Parse(reader["TeamId"].ToString()) : 0;

                    obu.AccountofficerName = reader["Accountofficer_Code1"] != DBNull.Value ? reader["Accountofficer_Code1"].ToString() : "";
                    obu.Accountofficer_Code = reader["AccountofficerName1"] != DBNull.Value ? reader["AccountofficerName1"].ToString() : "";
                    obu.Team_Code = reader["Team_Code"] != DBNull.Value ? reader["Team_Code"].ToString() : "";
                    obu.TeamName = reader["TeamName"] != DBNull.Value ? reader["TeamName"].ToString() : "";

                    obu.Branch_Code = reader["Branch_Code"] != DBNull.Value ? reader["Branch_Code"].ToString() : "";
                    obu.BranchName = reader["BranchName"] != DBNull.Value ? reader["BranchName"].ToString() : "";
                    obu.Group_Code = reader["Group_Code"] != DBNull.Value ? reader["Group_Code"].ToString() : "";
                    obu.GroupName = reader["GroupName"] != DBNull.Value ? reader["GroupName"].ToString() : "";

                    obu.Region_Code = reader["Region_Code"] != DBNull.Value ? reader["Region_Code"].ToString() : "";
                    obu.RegionName = reader["RegionName"] != DBNull.Value ? reader["RegionName"].ToString() : "";
                    obu.Division_Code = reader["Division_Code"] != DBNull.Value ? reader["Division_Code"].ToString() : "";
                    obu.DivisionName = reader["DivisionName"] != DBNull.Value ? reader["DivisionName"].ToString() : "";

                    obu.PPRCategory = reader["PPRCategory"] != DBNull.Value ? reader["PPRCategory"].ToString() : "";
                    obu.Year = reader["Year"] != DBNull.Value ? int.Parse(reader["Year"].ToString()) : 0;
                    obu.staff_id = reader["StaffID"] != DBNull.Value ? reader["StaffID"].ToString() : "";
                    obu.SegmentName = reader["SegmentName"] != DBNull.Value ? reader["SegmentName"].ToString() : "";
                    obu.Segment_Code = reader["Segment_Code"] != DBNull.Value ? reader["Segment_Code"].ToString() : "";

                    obu.Zone_Code = reader["Zone_Code"] != DBNull.Value ? reader["Zone_Code"].ToString() : "";
                    obu.ZoneName = reader["ZoneName"] != DBNull.Value ? reader["ZoneName"].ToString() : "";
                    obu.SuperSegment_Code = reader["SuperSegment_Code"] != DBNull.Value ? reader["SuperSegment_Code"].ToString() : "";
                    obu.SuperSegmentName = reader["SuperSegmentName"] != DBNull.Value ? reader["SuperSegmentName"].ToString() : "";
                    obu.ApprovalStatus = reader["ApprovalStatus"] != DBNull.Value ? reader["ApprovalStatus"].ToString() : "";

                    obuList.Add(obu);
                }
                con.Close();
            }
            return obuList;
        }
        //======================== awaiting ends ================================================================================

        ////======================== approved starts ================================================================================
        //public IEnumerable<TeamStructureALL> TeamStructureAPPROVED()
        //{
        //    List<TeamStructureALL> obuList = new List<TeamStructureALL>();

        //    using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
        //    {
        //        var cmd = new System.Data.SqlClient.SqlCommand("", con);

        //        //cmd.CommandText = "select * from Names where Id=@Id";
        //        //cmd.Parameters.AddWithValue("@Id", id);

        //        con.Open();
        //        cmd.CommandText = "select top 500 TeamId, Accountofficer_Code1, AccountofficerName1, Team_Code, TeamName," +
        //            "Branch_Code, BranchName, Group_Code, GroupName, Region_Code, RegionName, Division_Code, DivisionName," +
        //            "PPRCategory, Year, StaffID, SegmentName, Segment_Code, SectorName, Sector_Code, Zone_Code, ZoneName," +
        //            "SuperSegment_Code, SuperSegmentName, ApprovalStatus from Team_TEMP where ApprovalStatus='APPROVED'";
        //        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            var obu = new TeamStructureALL();

        //            obu.Team_StructureId = reader["TeamId"] != DBNull.Value ? int.Parse(reader["TeamId"].ToString()) : 0;

        //            obu.AccountofficerName = reader["Accountofficer_Code1"] != DBNull.Value ? reader["Accountofficer_Code1"].ToString() : "";
        //            obu.Accountofficer_Code = reader["AccountofficerName1"] != DBNull.Value ? reader["AccountofficerName1"].ToString() : "";
        //            obu.Team_Code = reader["Team_Code"] != DBNull.Value ? reader["Team_Code"].ToString() : "";
        //            obu.TeamName = reader["TeamName"] != DBNull.Value ? reader["TeamName"].ToString() : "";

        //            obu.Branch_Code = reader["Branch_Code"] != DBNull.Value ? reader["Branch_Code"].ToString() : "";
        //            obu.BranchName = reader["BranchName"] != DBNull.Value ? reader["BranchName"].ToString() : "";
        //            obu.Group_Code = reader["Group_Code"] != DBNull.Value ? reader["Group_Code"].ToString() : "";
        //            obu.GroupName = reader["GroupName"] != DBNull.Value ? reader["GroupName"].ToString() : "";

        //            obu.Region_Code = reader["Region_Code"] != DBNull.Value ? reader["Region_Code"].ToString() : "";
        //            obu.RegionName = reader["RegionName"] != DBNull.Value ? reader["RegionName"].ToString() : "";
        //            obu.Division_Code = reader["Division_Code"] != DBNull.Value ? reader["Division_Code"].ToString() : "";
        //            obu.DivisionName = reader["DivisionName"] != DBNull.Value ? reader["DivisionName"].ToString() : "";

        //            obu.PPRCategory = reader["PPRCategory"] != DBNull.Value ? reader["PPRCategory"].ToString() : "";
        //            obu.Year = reader["Year"] != DBNull.Value ? int.Parse(reader["Year"].ToString()) : 0;
        //            obu.staff_id = reader["StaffID"] != DBNull.Value ? reader["StaffID"].ToString() : "";
        //            obu.SegmentName = reader["SegmentName"] != DBNull.Value ? reader["SegmentName"].ToString() : "";
        //            obu.Segment_Code = reader["Segment_Code"] != DBNull.Value ? reader["Segment_Code"].ToString() : "";

        //            obu.Zone_Code = reader["Zone_Code"] != DBNull.Value ? reader["Zone_Code"].ToString() : "";
        //            obu.ZoneName = reader["ZoneName"] != DBNull.Value ? reader["ZoneName"].ToString() : "";
        //            obu.SuperSegment_Code = reader["SuperSegment_Code"] != DBNull.Value ? reader["SuperSegment_Code"].ToString() : "";
        //            obu.SuperSegmentName = reader["SuperSegmentName"] != DBNull.Value ? reader["SuperSegmentName"].ToString() : "";
        //            obu.ApprovalStatus = reader["ApprovalStatus"] != DBNull.Value ? reader["ApprovalStatus"].ToString() : "";

        //            obuList.Add(obu);
        //        }
        //        con.Close();
        //    }
        //    return obuList;
        //}

        //public IEnumerable<TeamStructureALL> TeamStructureusingparamsAPPROVED(string search)
        //{
        //    List<TeamStructureALL> obuList = new List<TeamStructureALL>();

        //    using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
        //    {
        //        var cmd = new System.Data.SqlClient.SqlCommand("", con);

        //        //cmd.CommandText = "select * from Names where Id=@Id";
        //        //cmd.Parameters.AddWithValue("@Id", id);

        //        con.Open();               
        //        cmd.CommandText = "select top 500 TeamId, Accountofficer_Code1, AccountofficerName1, Team_Code, TeamName," +
        //           "Branch_Code, BranchName, Group_Code, GroupName, Region_Code, RegionName, Division_Code, DivisionName," +
        //           "PPRCategory, Year, StaffID, SegmentName, Segment_Code, SectorName, Sector_Code, Zone_Code, ZoneName," +
        //           "SuperSegment_Code, SuperSegmentName, ApprovalStatus from Team_TEMP where ApprovalStatus='APPROVED' " +
        //           "and (Team_Code=@miscode or Branch_Code=@miscode or Group_Code=@miscode or Region_Code=@miscode or " +
        //           "Division_Code=@miscode or Segment_Code=@miscode or SuperSegment_Code=@miscode or SuperSegment_Code=@miscode)";
        //        cmd.Parameters.AddWithValue("@miscode", search);
        //        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            var obu = new TeamStructureALL();

        //            obu.Team_StructureId = reader["TeamId"] != DBNull.Value ? int.Parse(reader["TeamId"].ToString()) : 0;

        //            obu.AccountofficerName = reader["Accountofficer_Code1"] != DBNull.Value ? reader["Accountofficer_Code1"].ToString() : "";
        //            obu.Accountofficer_Code = reader["AccountofficerName1"] != DBNull.Value ? reader["AccountofficerName1"].ToString() : "";
        //            obu.Team_Code = reader["Team_Code"] != DBNull.Value ? reader["Team_Code"].ToString() : "";
        //            obu.TeamName = reader["TeamName"] != DBNull.Value ? reader["TeamName"].ToString() : "";

        //            obu.Branch_Code = reader["Branch_Code"] != DBNull.Value ? reader["Branch_Code"].ToString() : "";
        //            obu.BranchName = reader["BranchName"] != DBNull.Value ? reader["BranchName"].ToString() : "";
        //            obu.Group_Code = reader["Group_Code"] != DBNull.Value ? reader["Group_Code"].ToString() : "";
        //            obu.GroupName = reader["GroupName"] != DBNull.Value ? reader["GroupName"].ToString() : "";

        //            obu.Region_Code = reader["Region_Code"] != DBNull.Value ? reader["Region_Code"].ToString() : "";
        //            obu.RegionName = reader["RegionName"] != DBNull.Value ? reader["RegionName"].ToString() : "";
        //            obu.Division_Code = reader["Division_Code"] != DBNull.Value ? reader["Division_Code"].ToString() : "";
        //            obu.DivisionName = reader["DivisionName"] != DBNull.Value ? reader["DivisionName"].ToString() : "";

        //            obu.PPRCategory = reader["PPRCategory"] != DBNull.Value ? reader["PPRCategory"].ToString() : "";
        //            obu.Year = reader["Year"] != DBNull.Value ? int.Parse(reader["Year"].ToString()) : 0;
        //            obu.staff_id = reader["StaffID"] != DBNull.Value ? reader["StaffID"].ToString() : "";
        //            obu.SegmentName = reader["SegmentName"] != DBNull.Value ? reader["SegmentName"].ToString() : "";
        //            obu.Segment_Code = reader["Segment_Code"] != DBNull.Value ? reader["Segment_Code"].ToString() : "";

        //            obu.Zone_Code = reader["Zone_Code"] != DBNull.Value ? reader["Zone_Code"].ToString() : "";
        //            obu.ZoneName = reader["ZoneName"] != DBNull.Value ? reader["ZoneName"].ToString() : "";
        //            obu.SuperSegment_Code = reader["SuperSegment_Code"] != DBNull.Value ? reader["SuperSegment_Code"].ToString() : "";
        //            obu.SuperSegmentName = reader["SuperSegmentName"] != DBNull.Value ? reader["SuperSegmentName"].ToString() : "";
        //            obu.ApprovalStatus = reader["ApprovalStatus"] != DBNull.Value ? reader["ApprovalStatus"].ToString() : "";

        //            obuList.Add(obu);
        //        }
        //        con.Close();
        //    }
        //    return obuList;
        //}
        ////======================== approved ends ================================================================================

        ////======================== declined starts ================================================================================
        //public IEnumerable<TeamStructureALL> TeamStructureDECLINED()
        //{
        //    List<TeamStructureALL> obuList = new List<TeamStructureALL>();

        //    using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
        //    {
        //        var cmd = new System.Data.SqlClient.SqlCommand("", con);

        //        //cmd.CommandText = "select * from Names where Id=@Id";
        //        //cmd.Parameters.AddWithValue("@Id", id);

        //        con.Open();
        //        cmd.CommandText = "select top 500 TeamId, Accountofficer_Code1, AccountofficerName1, Team_Code, TeamName," +
        //            "Branch_Code, BranchName, Group_Code, GroupName, Region_Code, RegionName, Division_Code, DivisionName," +
        //            "PPRCategory, Year, StaffID, SegmentName, Segment_Code, SectorName, Sector_Code, Zone_Code, ZoneName," +
        //            "SuperSegment_Code, SuperSegmentName, ApprovalStatus from Team_TEMP where ApprovalStatus='DECLINED'";
        //        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            var obu = new TeamStructureALL();

        //            obu.Team_StructureId = reader["TeamId"] != DBNull.Value ? int.Parse(reader["TeamId"].ToString()) : 0;

        //            obu.AccountofficerName = reader["Accountofficer_Code1"] != DBNull.Value ? reader["Accountofficer_Code1"].ToString() : "";
        //            obu.Accountofficer_Code = reader["AccountofficerName1"] != DBNull.Value ? reader["AccountofficerName1"].ToString() : "";
        //            obu.Team_Code = reader["Team_Code"] != DBNull.Value ? reader["Team_Code"].ToString() : "";
        //            obu.TeamName = reader["TeamName"] != DBNull.Value ? reader["TeamName"].ToString() : "";

        //            obu.Branch_Code = reader["Branch_Code"] != DBNull.Value ? reader["Branch_Code"].ToString() : "";
        //            obu.BranchName = reader["BranchName"] != DBNull.Value ? reader["BranchName"].ToString() : "";
        //            obu.Group_Code = reader["Group_Code"] != DBNull.Value ? reader["Group_Code"].ToString() : "";
        //            obu.GroupName = reader["GroupName"] != DBNull.Value ? reader["GroupName"].ToString() : "";

        //            obu.Region_Code = reader["Region_Code"] != DBNull.Value ? reader["Region_Code"].ToString() : "";
        //            obu.RegionName = reader["RegionName"] != DBNull.Value ? reader["RegionName"].ToString() : "";
        //            obu.Division_Code = reader["Division_Code"] != DBNull.Value ? reader["Division_Code"].ToString() : "";
        //            obu.DivisionName = reader["DivisionName"] != DBNull.Value ? reader["DivisionName"].ToString() : "";

        //            obu.PPRCategory = reader["PPRCategory"] != DBNull.Value ? reader["PPRCategory"].ToString() : "";
        //            obu.Year = reader["Year"] != DBNull.Value ? int.Parse(reader["Year"].ToString()) : 0;
        //            obu.staff_id = reader["StaffID"] != DBNull.Value ? reader["StaffID"].ToString() : "";
        //            obu.SegmentName = reader["SegmentName"] != DBNull.Value ? reader["SegmentName"].ToString() : "";
        //            obu.Segment_Code = reader["Segment_Code"] != DBNull.Value ? reader["Segment_Code"].ToString() : "";

        //            obu.Zone_Code = reader["Zone_Code"] != DBNull.Value ? reader["Zone_Code"].ToString() : "";
        //            obu.ZoneName = reader["ZoneName"] != DBNull.Value ? reader["ZoneName"].ToString() : "";
        //            obu.SuperSegment_Code = reader["SuperSegment_Code"] != DBNull.Value ? reader["SuperSegment_Code"].ToString() : "";
        //            obu.SuperSegmentName = reader["SuperSegmentName"] != DBNull.Value ? reader["SuperSegmentName"].ToString() : "";
        //            obu.ApprovalStatus = reader["ApprovalStatus"] != DBNull.Value ? reader["ApprovalStatus"].ToString() : "";

        //            obuList.Add(obu);
        //        }
        //        con.Close();
        //    }
        //    return obuList;
        //}

        //public IEnumerable<TeamStructureALL> TeamStructureusingparamsDECLINED(string search)
        //{
        //    List<TeamStructureALL> obuList = new List<TeamStructureALL>();

        //    using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
        //    {
        //        var cmd = new System.Data.SqlClient.SqlCommand("", con);

        //        //cmd.CommandText = "select * from Names where Id=@Id";
        //        //cmd.Parameters.AddWithValue("@Id", id);

        //        con.Open();              
        //        cmd.CommandText = "select top 500 TeamId, Accountofficer_Code1, AccountofficerName1, Team_Code, TeamName," +
        //         "Branch_Code, BranchName, Group_Code, GroupName, Region_Code, RegionName, Division_Code, DivisionName," +
        //         "PPRCategory, Year, StaffID, SegmentName, Segment_Code, SectorName, Sector_Code, Zone_Code, ZoneName," +
        //         "SuperSegment_Code, SuperSegmentName, ApprovalStatus from Team_TEMP where ApprovalStatus='DECLINED' " +
        //         "and (Team_Code=@miscode or Branch_Code=@miscode or Group_Code=@miscode or Region_Code=@miscode or " +
        //         "Division_Code=@miscode or Segment_Code=@miscode or SuperSegment_Code=@miscode or SuperSegment_Code=@miscode)";
        //        cmd.Parameters.AddWithValue("@miscode", search);
        //        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            var obu = new TeamStructureALL();

        //            obu.Team_StructureId = reader["TeamId"] != DBNull.Value ? int.Parse(reader["TeamId"].ToString()) : 0;

        //            obu.AccountofficerName = reader["Accountofficer_Code1"] != DBNull.Value ? reader["Accountofficer_Code1"].ToString() : "";
        //            obu.Accountofficer_Code = reader["AccountofficerName1"] != DBNull.Value ? reader["AccountofficerName1"].ToString() : "";
        //            obu.Team_Code = reader["Team_Code"] != DBNull.Value ? reader["Team_Code"].ToString() : "";
        //            obu.TeamName = reader["TeamName"] != DBNull.Value ? reader["TeamName"].ToString() : "";

        //            obu.Branch_Code = reader["Branch_Code"] != DBNull.Value ? reader["Branch_Code"].ToString() : "";
        //            obu.BranchName = reader["BranchName"] != DBNull.Value ? reader["BranchName"].ToString() : "";
        //            obu.Group_Code = reader["Group_Code"] != DBNull.Value ? reader["Group_Code"].ToString() : "";
        //            obu.GroupName = reader["GroupName"] != DBNull.Value ? reader["GroupName"].ToString() : "";

        //            obu.Region_Code = reader["Region_Code"] != DBNull.Value ? reader["Region_Code"].ToString() : "";
        //            obu.RegionName = reader["RegionName"] != DBNull.Value ? reader["RegionName"].ToString() : "";
        //            obu.Division_Code = reader["Division_Code"] != DBNull.Value ? reader["Division_Code"].ToString() : "";
        //            obu.DivisionName = reader["DivisionName"] != DBNull.Value ? reader["DivisionName"].ToString() : "";

        //            obu.PPRCategory = reader["PPRCategory"] != DBNull.Value ? reader["PPRCategory"].ToString() : "";
        //            obu.Year = reader["Year"] != DBNull.Value ? int.Parse(reader["Year"].ToString()) : 0;
        //            obu.staff_id = reader["StaffID"] != DBNull.Value ? reader["StaffID"].ToString() : "";
        //            obu.SegmentName = reader["SegmentName"] != DBNull.Value ? reader["SegmentName"].ToString() : "";
        //            obu.Segment_Code = reader["Segment_Code"] != DBNull.Value ? reader["Segment_Code"].ToString() : "";

        //            obu.Zone_Code = reader["Zone_Code"] != DBNull.Value ? reader["Zone_Code"].ToString() : "";
        //            obu.ZoneName = reader["ZoneName"] != DBNull.Value ? reader["ZoneName"].ToString() : "";
        //            obu.SuperSegment_Code = reader["SuperSegment_Code"] != DBNull.Value ? reader["SuperSegment_Code"].ToString() : "";
        //            obu.SuperSegmentName = reader["SuperSegmentName"] != DBNull.Value ? reader["SuperSegmentName"].ToString() : "";
        //            obu.ApprovalStatus = reader["ApprovalStatus"] != DBNull.Value ? reader["ApprovalStatus"].ToString() : "";

        //            obuList.Add(obu);
        //        }
        //        con.Close();
        //    }
        //    return obuList;
        //}
        ////======================== declined ends ================================================================================

        public void EditTeamApproval(string selectedIds)
        {
            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                using (var cmd = new System.Data.SqlClient.SqlCommand("", con))
                {
                    var userInput = selectedIds;
                    var values = userInput.Split(',');
                    string currentuser = Convert.ToString(HttpContext.Current.User.Identity.Name);

                    con.Open();
                    var sql = "update Team_TEMP set ApprovalStatus='APPROVED', ApprovalUser=@CURRENTUSER where TeamId IN(";
                    cmd.Parameters.AddWithValue("@CURRENTUSER", currentuser);
                    for (int i = 0; i < values.Length; i++)
                    {
                        sql = $"{sql} @{i},";
                        cmd.Parameters.Add($"@{i}", System.Data.Odbc.OdbcType.Int).Value = values[i];
                    }
                    cmd.CommandText = sql.TrimEnd(',') + ");";
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        public void EditTeamDecline(string selectedIds)
        {
            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                using (var cmd = new System.Data.SqlClient.SqlCommand("", con))
                {
                    var userInput = selectedIds;
                    var values = userInput.Split(',');
                    string currentuser = Convert.ToString(HttpContext.Current.User.Identity.Name);

                    con.Open();
                    var sql = "update Team_TEMP set ApprovalStatus='DECLINED', ApprovalUser=@CURRENTUSER where TeamId IN(";
                    cmd.Parameters.AddWithValue("@CURRENTUSER", currentuser);
                    for (int i = 0; i < values.Length; i++)
                    {
                        sql = $"{sql} @{i},";
                        cmd.Parameters.Add($"@{i}", System.Data.Odbc.OdbcType.Int).Value = values[i];
                    }
                    cmd.CommandText = sql.TrimEnd(',') + ");";
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }


    }
}