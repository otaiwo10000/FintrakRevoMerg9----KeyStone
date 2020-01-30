using Fintrak.Client.MPR.Contracts;
using Fintrak.Client.MPR.Entities;
using Fintrak.Client.Core.Contracts;
using Fintrak.Client.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Presentation.WebClient.Models
{
    public class TeamAccessReadModel
    {
        public int TeamId { get; set; }
        public string Accountofficer_Code1 { get; set; }
        public string AccountofficerName1 { get; set; }
        public string Team_Code { get; set; }
        public string TeamName { get; set; }
        public string Branch_Code { get; set; }
        public string BranchName { get; set; }
        public string Group_Code { get; set; }
        public string GroupName { get; set; }
        public string Region_Code { get; set; }
        public string RegionName { get; set; }
        public string Division_Code { get; set; }
        public string DivisionName { get; set; }
        public string PPRCategory { get; set; }
        public int Year { get; set; }
        public string StaffID { get; set; }
        public string SegmentName { get; set; }
        public string Segment_Code { get; set; }
        public string SectorName { get; set; }
        public string Sector_Code { get; set; }
        public bool IsRelationshipManager { get; set; }
        public string Zone_Code { get; set; }
        public string ZoneName { get; set; }
        public string Strategy_Code { get; set; }
        public string StrategyName { get; set; }
        public string branchcode { get; set; }
        public string SuperSegment_Code { get; set; }
        public string SuperSegmentName { get; set; }


        public string username { get; set; }

   
    }

   
}