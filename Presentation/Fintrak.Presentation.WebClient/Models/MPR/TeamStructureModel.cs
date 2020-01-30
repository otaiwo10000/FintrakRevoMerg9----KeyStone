using Fintrak.Client.Core.Contracts;
using Fintrak.Client.MPR.Entities;
using Fintrak.Client.SystemCore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Fintrak.Presentation.WebClient.Models
{
    public class TeamStructureModel
    {
        //======== backup onn 04-08-2019 starts ====================

        //public int TeamBankId { get; set; }
        //public int TeamGroupId { get; set; }
        //public string Team_Code { get; set; }
        //public string TeamName { get; set; }
        //public string Branch_Code { get; set; }
        //public string BranchName { get; set; }
        //public string Region_Code { get; set; }
        //public string RegionName { get; set; }
        //public string Division_Code { get; set; }
        //public string DivisionName { get; set; }
        //public string AccountOfficer_Code { get; set; }
        //public string AccountOfficer_Name { get; set; }
        //public string StaffID { get; set; }
        //public int Year { get; set; }

        //======== backup onn 04-08-2019 ends ====================

        public int TeamBankId { get; set; }
        public int TeamGroupId { get; set; }
        public string Team_Code { get; set; }
        public string TeamName { get; set; }
        public string Branch_Code { get; set; }
        public string BranchName { get; set; }
        public string Region_Code { get; set; }
        public string RegionName { get; set; }
        public string Division_Code { get; set; }
        public string DivisionName { get; set; }
        public string AccountOfficer_Code { get; set; }
        public string AccountOfficer_Name { get; set; }
        public string StaffID { get; set; }
        public int Year { get; set; }
        public string DIRECTORATECODE { get; set; }
        public string DIRECTORATENAME { get; set; }
        public string Zone_Code { get; set; }
        public string ZoneName { get; set; }
        public int Period { get; set; }
        public int Team_StructureId { get; set; }
        public string Accountofficer_Code { get; set; }
        public string AccountofficerName { get; set; }
        public string staff_id { get; set; }

    }

    public class TeamGroupModel
    {
        public int TeamGroupId { get; set; }
        public string Branch_Code { get; set; }
        public string BranchName { get; set; }
        public string Region_Code { get; set; }
        public string RegionName { get; set; }
        public string Division_Code { get; set; }
        public string DivisionName { get; set; }

        public int Year { get; set; }
    }

    public class TeamBankModel
    {
        public int TeamBankId { get; set; }
        public string Team_Code { get; set; }
        public string TeamName { get; set; }
        public string Branch_Code { get; set; }
        public string BranchName { get; set; }
        public string Region_Code { get; set; }
        public string RegionName { get; set; }
        public string Division_Code { get; set; }
        public string DivisionName { get; set; }
        public string AccountOfficer_Code { get; set; }
        public string AccountOfficer_Name { get; set; }
        public string StaffID { get; set; }
        public int Year { get; set; }
    }

     public class TeamBankGroup
     {
        public TeamBankModel TeamBankModelObj { get; set; }
        public TeamGroupModel TeamGroupModelObj { get; set; }
    }

    public class teamBankGroupNameCodeModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class teamBankGroupDropDownModel
    {

        public string Directorate_Code { get; set; }
        public string DirectorateName { get; set; }
        public string Branch_Code { get; set; }
        public string BranchName { get; set; }
        public string Region_Code { get; set; }
        public string RegionName { get; set; }
        public string Division_Code { get; set; }
        public string DivisionName { get; set; }
        public string AccountOfficer_Code { get; set; }
        public string AccountOfficer_Name { get; set; }
    }

        public class AccountOfficer
        {
            public string AccountOfficer_Name { get; set; }
            public string AccountOfficer_Code { get; set; }
        }

    public class TeamSTructureCodeNameModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class YearModel
    {
        public int value { get; set; }
        public int name { get; set; }
    }

    public class TeamStructureMappingModel
    {
        public string lowerlevelcode { get; set; }
        public string lowerlevelmiscode { get; set; }
        public string higherlevelcode { get; set; }
        public string higherlevelmiscode { get; set; }
        public string higherlevelmisname { get; set; }
        public int year { get; set; }
    }

    public class TeamStructureMapping2Model
    {    
        public string Team_Code { get; set; }
        public string TeamName { get; set; }
        public string Branch_Code { get; set; }
        public string BranchName { get; set; }
        public string Branch_Code2 { get; set; }
        public string Region_Code { get; set; }
        public string RegionName { get; set; }
        public string Region_Code2 { get; set; }
        public string Division_Code { get; set; }
        public string DivisionName { get; set; }      
        public int Year { get; set; }
    }

    public class TeamStructureALLModel
    {
        public int Team_StructureId { get; set; }

        public string Accountofficer_Code { get; set; }
        public string AccountofficerName { get; set; }
        public string Team_Code { get; set; }
        public string TeamName { get; set; }
        public string Zone_Code { get; set; }
        public string ZoneName { get; set; }
        public string Branch_Code { get; set; }
        public string BranchName { get; set; }
        public string Group_Code { get; set; }
        public string GroupName { get; set; }
        public string Region_Code { get; set; }
        public string RegionName { get; set; }
        public string Division_Code { get; set; }
        public string DivisionName { get; set; }
        public string DIRECTORATECODE { get; set; }
        public string DIRECTORATENAME { get; set; }
        public string Segment_Code { get; set; }
        public string SegmentName { get; set; }
        public string SuperSegment_Code { get; set; }
        public string SuperSegmentName { get; set; }
        public int Year { get; set; }
        public string staff_id { get; set; }
        [Required]
        public string ApprovalStatus { get; set; }
    }


}