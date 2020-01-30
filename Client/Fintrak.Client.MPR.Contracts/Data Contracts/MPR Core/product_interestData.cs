
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Client.MPR.Contracts
{
    [DataContract]
    public class product_interestData : DataContractBase
    {

        //int _product_interestId;
        //string _ProductCode;
        //string _Category;
        //Double _InterestRate;

        ////ModuleOwnerType _ModuleOwnerType;

        //public int product_interestId
        //{
        //    get { return _product_interestId; }
        //    set
        //    {
        //        if (_product_interestId != value)
        //        {
        //            _product_interestId = value;
        //            // OnPropertyChanged(() => crb_Data_Id);
        //        }
        //    }
        //}

        //public string ProductCode
        //{
        //    get { return _ProductCode; }
        //    set
        //    {
        //        if (_ProductCode != value)
        //        {
        //            _ProductCode = value;
        //            // OnPropertyChanged(() => Code);
        //        }
        //    }
        //}

        //public string Category
        //{
        //    get { return _Category; }
        //    set
        //    {
        //        if (_Category != value)
        //        {
        //            _Category = value;
        //            // OnPropertyChanged(() => Name);
        //        }
        //    }
        //}

        //public Double InterestRate
        //{
        //    get { return _InterestRate; }
        //    set
        //    {
        //        if (_InterestRate != value)
        //        {
        //            _InterestRate = value;
        //            // OnPropertyChanged(() => ParentCode);
        //        }
        //    }
        //}


        [DataMember]
        //[Browsable(false)]
        public Int32 product_interestId { get; set; }

        [DataMember]
        // [Required]
        public string ProductCode { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        //[Required]
        public Shared.Core.Framework.AccountTypeEnum Category { get; set; }

        [DataMember]
        public string CategoryName { get; set; }

        [DataMember]
        // [Required]
        public Double InterestRate { get; set; }
    }
}
