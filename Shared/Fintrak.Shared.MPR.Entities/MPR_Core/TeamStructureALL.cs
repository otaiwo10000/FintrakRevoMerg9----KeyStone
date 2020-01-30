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
    public partial class TeamStructureALL : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int Team_StructureId { get; set; }

        [DataMember]
        //[Required]
        public string Accountofficer_Code { get; set; }

        [DataMember]
        public string AccountofficerName { get; set; }

        [DataMember]
        //[Required]
        public string Team_Code { get; set; }

        [DataMember]
        public string TeamName { get; set; }

        [DataMember]
        //[Required]
        public string Branch_Code { get; set; }

        [DataMember]
        public string BranchName { get; set; }

        [DataMember]
        //[Required]
        public string Group_Code { get; set; }

        [DataMember]
        public string GroupName { get; set; }

        [DataMember]
       // [Required]
        public string Region_Code { get; set; }

        [DataMember]
        public string RegionName { get; set; }

        [DataMember]
        //[Required]
        public string Division_Code { get; set; }

        [DataMember]
        public string DivisionName { get; set; }

        [DataMember]
        //[Required]
        public string DIRECTORATECODE { get; set; }

        [DataMember]
        public string DIRECTORATENAME { get; set; }

        [DataMember]
        public string PPRCategory { get; set; }

        [DataMember]
        [Required]
        public int Year { get; set; }

        [DataMember]
        public string staff_id { get; set; }

        [DataMember]
        //[Required]
        public string unit_code { get; set; }

        [DataMember]       
        public string unitname { get; set; }

        [DataMember]
        //[Required]
        public string Zone_Code { get; set; }

        [DataMember]
        public string ZoneName { get; set; }

        [DataMember]
        //[Required]
        public string Segment_Code { get; set; }

        [DataMember]
        public string SegmentName { get; set; }

        [DataMember]
        //[Required]
        public string SuperSegment_Code { get; set; }

        [DataMember]
        public string SuperSegmentName { get; set; }

        [DataMember]
        [Required]
        public int Period { get; set; }

        //[DataMember]
        //public string ApprovalStatus { get; set; }

        //[DataMember]
        //public bool Migrated { get; set; }

        public int EntityId
        {
            get
            {
                return Team_StructureId;
            }
        }
    }
}
