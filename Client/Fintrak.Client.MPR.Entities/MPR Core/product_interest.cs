
using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.Core.Framework;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class product_interest : ObjectBase
    {

        int _product_interestId;
        string _ProductCode;
        AccountTypeEnum _Category;
        Double _InterestRate;

        //ModuleOwnerType _ModuleOwnerType;

        public int product_interestId
        {
            get { return _product_interestId; }
            set
            {
                if (_product_interestId != value)
                {
                    _product_interestId = value;
                    // OnPropertyChanged(() => crb_Data_Id);
                }
            }
        }

        public string ProductCode
        {
            get { return _ProductCode; }
            set
            {
                if (_ProductCode != value)
                {
                    _ProductCode = value;
                    // OnPropertyChanged(() => Code);
                }
            }
        }

        public AccountTypeEnum Category
        {
            get { return _Category; }
            set
            {
                if (_Category != value)
                {
                    _Category = value;
                    // OnPropertyChanged(() => Name);
                }
            }
        }

        public Double InterestRate
        {
            get { return _InterestRate; }
            set
            {
                if (_InterestRate != value)
                {
                    _InterestRate = value;
                    // OnPropertyChanged(() => ParentCode);
                }
            }
        }
    }
}
