

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Business.MPR.Contracts
{
    [DataContract]
    public class TeamStructureData : DataContractBase
    {
        [DataMember]
        public int Team_StructureId { get; set; }

        [DataMember]
        public string Accountofficer_Code { get; set; }

        [DataMember]
        public string AccountofficerName { get; set; }

        [DataMember]
        public string Team_Code { get; set; }

        [DataMember]
        public string TeamName { get; set; }

        [DataMember]
        public string Branch_Code { get; set; }

        [DataMember]
        public string BranchName { get; set; }

        [DataMember]
        public string Group_Code { get; set; }

        [DataMember]
        public string GroupName { get; set; }

        [DataMember]
        public string Region_Code { get; set; }

        [DataMember]
        public string RegionName { get; set; }

        [DataMember]
        public string Division_Code { get; set; }

        [DataMember]
        public string DivisionName { get; set; }

        [DataMember]
        public string DIRECTORATECODE { get; set; }

        [DataMember]
        public string DIRECTORATENAME { get; set; }

        [DataMember]
        public string PPRCategory { get; set; }

        [DataMember]
        public string Year { get; set; }

        [DataMember]
        public string staff_id { get; set; }

        [DataMember]
        public string unit_code { get; set; }

        [DataMember]
        public string unitname { get; set; }

        [DataMember]
        public int Period { get; set; }
    }
}
