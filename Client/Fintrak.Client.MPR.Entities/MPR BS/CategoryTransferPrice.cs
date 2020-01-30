
using System;
using System.Linq;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.Core.Framework;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class CategoryTransferPrice : ObjectBase
    {
        int _CategoryTransferPriceId;
        BalanceSheetCategory _BalanceSheetCategory;
        int _Period;
        int _Year;
        decimal _Rate;
        CurrencyType _CurrencyType;
        bool _Active;


        public int CategoryTransferPriceId
        {
            get { return _CategoryTransferPriceId; }
            set
            {
                if (_CategoryTransferPriceId != value)
                {
                    _CategoryTransferPriceId = value;
                    OnPropertyChanged(() => CategoryTransferPriceId);
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


        public decimal Rate
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



        //class CategoryTransferPriceValidator : AbstractValidator<CategoryTransferPrice>
        //{
        //    public CategoryTransferPriceValidator()
        //    {
        //        RuleFor(obj => obj.BalanceSheetCategory).NotEmpty().WithMessage("BalanceSheetCategory is required.");
        //    }
        //}

        //protected override IValidator GetValidator()
        //{
        //    return new CategoryTransferPriceValidator();
        //}
    }
}
