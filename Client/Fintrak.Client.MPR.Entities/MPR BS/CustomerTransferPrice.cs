using System;
using System.Linq;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.Core.Framework;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class CustomerTransferPrice : ObjectBase
    {
        int _customertransferpriceId;
        string _CustNo;
        //BalanceSheetCategory _BalanceSheetCategory;
        BalanceSheetCategory _Category;

        // BalanceSheetType _BalanceSheetType;
        double _Rate;
        int _Year;
        int _Period;
        int _SolutionId;
        string _CompanyCode;
        bool _Active;

        public int customertransferpriceId
        {
            get { return _customertransferpriceId; }
            set
            {
                if (_customertransferpriceId != value)
                {
                    _customertransferpriceId = value;
                    OnPropertyChanged(() => customertransferpriceId);
                }
            }
        }

        public string CustNo
        {
            get { return _CustNo; }
            set
            {
                if (_CustNo != value)
                {
                    _CustNo = value;
                    OnPropertyChanged(() => CustNo);
                }
            }
        }

        public BalanceSheetCategory Category
        {
            get { return _Category; }
            set
            {
                if (_Category != value)
                {
                    _Category = value;
                    OnPropertyChanged(() => Category);
                }
            }
        }
        public double Rate
        {
            get { return _Rate; }
            set
            {
                if (_Rate != value)
                {
                    _Rate = value;
                    OnPropertyChanged(() => Rate);
                }
            }
        }

        public int Year
        {
            get { return _Year; }
            set
            {
                if (_Year != value)
                {
                    _Year = value;
                    OnPropertyChanged(() => Year);
                }
            }
        }

        public int Period
        {
            get { return _Period; }
            set
            {
                if (_Period != value)
                {
                    _Period = value;
                    OnPropertyChanged(() => Period);
                }
            }
        }       

        public string CompanyCode
        {
            get { return _CompanyCode; }
            set
            {
                if (_CompanyCode != value)
                {
                    _CompanyCode = value;
                    OnPropertyChanged(() => CompanyCode);
                }
            }
        }

        public int SolutionId
        {
            get { return _SolutionId; }
            set
            {
                if (_SolutionId != value)
                {
                    _SolutionId = value;
                    OnPropertyChanged(() => SolutionId);
                }
            }
        }

        public bool Active
        {
            get { return _Active; }
            set
            {
                if (_Active != value)
                {
                    _Active = value;
                    OnPropertyChanged(() => Active);
                }
            }
        }

        
        class CustomerTransferPriceValidator : AbstractValidator<CustomerTransferPrice>
        {
            public CustomerTransferPriceValidator()
            {
                RuleFor(obj => obj.CustNo).NotEmpty().WithMessage("Customer Number is required.");
                RuleFor(obj => obj.Category).NotEmpty().WithMessage("BalanceSheet Category is required.");
                RuleFor(obj => obj.Year).NotEmpty().WithMessage("Year is required.");
                RuleFor(obj => obj.Period).NotEmpty().WithMessage("Period is required.");
             }
        }

        protected override IValidator GetValidator()
        {
            return new CustomerTransferPriceValidator();
        }
    }
}
