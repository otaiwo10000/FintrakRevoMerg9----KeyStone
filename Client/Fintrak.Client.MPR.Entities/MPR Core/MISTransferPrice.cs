
using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.MPR.Framework;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class MISTransferPrice : ObjectBase
    {
        int _mistransferpriceId;      
        string _DefinitionCode;       
        string _MisCode;
        BalanceSheetCategory _BalanceSheetCategory;
        CurrencyType _CurrencyType;
        double _Rate;
        int _Period;
        int _Year;
        int _SolutionId;
        string _CompanyCode;
         

        public int mistransferpriceId
        {
            get { return _mistransferpriceId; }
            set
            {
                if (_mistransferpriceId != value)
                {
                    _mistransferpriceId = value;
                     OnPropertyChanged(() => mistransferpriceId);
                }
            }
        }

        public string DefinitionCode
        {
            get { return _DefinitionCode; }
            set
            {
                if (_DefinitionCode != value)
                {
                    _DefinitionCode = value;
                    OnPropertyChanged(() => DefinitionCode);
                }
            }
        }

        public string MisCode
        {
            get { return _MisCode; }
            set
            {
                if (_MisCode != value)
                {
                    _MisCode = value;
                    OnPropertyChanged(() => MisCode);
                }
            }
        }

        public BalanceSheetCategory BalanceSheetCategory
        {
            get { return _BalanceSheetCategory; }
            set
            {
                if (_BalanceSheetCategory != value)
                {
                    _BalanceSheetCategory = value;
                    OnPropertyChanged(() => BalanceSheetCategory);
                }
            }
        }

        public CurrencyType CurrencyType
        {
            get { return _CurrencyType; }
            set
            {
                if (_CurrencyType != value)
                {
                    _CurrencyType = value;
                    OnPropertyChanged(() => CurrencyType);
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

        class MISTransferPriceValidator : AbstractValidator<MISTransferPrice>
        {
            public MISTransferPriceValidator()
            {
                RuleFor(obj => obj.DefinitionCode).NotEmpty().WithMessage("Definition Code is required.");
                RuleFor(obj => obj.MisCode).NotEmpty().WithMessage("MIS Code is required.");
                RuleFor(obj => obj.CurrencyType).NotEmpty().WithMessage("Currency is required.");
                RuleFor(obj => obj.Rate).NotEmpty().WithMessage("Rate is required.");
                RuleFor(obj => obj.Period).NotEmpty().WithMessage("Period is required.");
                RuleFor(obj => obj.Year).NotEmpty().WithMessage("Year is required.");

            }
        }

        protected override IValidator GetValidator()
        {
            return new MISTransferPriceValidator();
        }
    }
}
