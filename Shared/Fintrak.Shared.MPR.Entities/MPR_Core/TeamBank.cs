using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.MPR.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Fintrak.Shared.MPR.Entities
{
    public partial class TeamBank : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int TeamBankId { get; set; }

        [DataMember]
        [Required]
        public string Team_Code { get; set; }

        [DataMember]
        public string TeamName { get; set; }

        [DataMember]
        [Required]
        public string Branch_Code { get; set; }

        [DataMember]
        public string BranchName { get; set; }       

        [DataMember]
        [Required]
        public string Zone_Code { get; set; }

        [DataMember]
        public string ZoneName { get; set; }

        [DataMember]
        [Required]
        public string Segment_Code { get; set; }

        [DataMember]
        public string SegmentName { get; set; }

        [DataMember]
        [Required]
        public string Group_Code { get; set; }

        [DataMember]
        public string GroupName { get; set; }

        [DataMember]
        [Required]
        public string Region_Code { get; set; }

        [DataMember]
        public string RegionName { get; set; }

        [DataMember]
        [Required]
        public string Division_Code { get; set; }

        [DataMember]
        public string DivisionName { get; set; }

        [DataMember]
        [Required]
        public string SBU_Code { get; set; }

        [DataMember]
        public string SBUName { get; set; }

        [DataMember]
        [Required]
        public string AccountOfficer_Code { get; set; }

        [DataMember]
        public string AccountOfficer_Name { get; set; }       

        //[DataMember]
        //[Required]
        //public string DIRECTORATECODE { get; set; }

        //[DataMember]
        //public string DIRECTORATENAME { get; set; }

        //[DataMember]
        //public string PPRCategory { get; set; }

        [DataMember]
        [Required]
        public int Year { get; set; }

        [DataMember]
        public string StaffID { get; set; }

        //[DataMember]
        //[Required]
        //public string unit_code { get; set; }

        //[DataMember]       
        //public string unitname { get; set; }

        //[DataMember]
        //[Required]
        //public int Period { get; set; }

        public int EntityId
        {
            get
            {
                return TeamBankId;
            }
        }
    }
}
