using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class IFRSBonds : ObjectBase
    {
        int _BondId;
        string _RefNo;
        string _Currency;
        DateTime _ValueDate;
        DateTime _MaturityDate;
        double _CleanPrice;
        double _FaceValue;
        decimal _CouponRate;
        decimal _CurrentMarketYield;
        DateTime _FirstCouponDate;
        string _Classification;
        string _Classification_Category;
        string _CompanyCode;
        string _Narration;
        string _Symbol;
        string _SandP_Rating;
        double _Price;
        bool _Split;
        bool _Active;


        public int BondId

        {
            get { return _BondId; }
            set
            {
                if (_BondId != value)
                {
                    _BondId = value;
                    OnPropertyChanged(() => BondId);
                }
            }
        }
        public string SandP_Rating
        {
            get { return _SandP_Rating; }
            set
            {
                if (_SandP_Rating != value)
                {
                    _SandP_Rating = value;
                    OnPropertyChanged(() => SandP_Rating);
                }
            }
        }
        public string Classification_Category
        {
            get { return _Classification_Category; }
            set
            {
                if (_Classification_Category != value)
                {
                    _Classification_Category = value;
                    OnPropertyChanged(() => Classification_Category);
                }
            }
        }
        public double Price
        {
            get { return _Price; }
            set
            {
                if (_Price != value)
                {
                    _Price = value;
                    OnPropertyChanged(() => Price);
                }
            }
        }
        public string RefNo
        {
            get { return _RefNo; }
            set
            {
                if (_RefNo != value)
                {
                    _RefNo = value;
                    OnPropertyChanged(() => RefNo);
                }
            }
        }

        public string Symbol
        {
            get { return _Symbol; }
            set
            {
                if (_Symbol != value)
                {
                    _Symbol = value;
                    OnPropertyChanged(() => Symbol);
                }
            }
        }

        public string Currency
        {
            get { return _Currency; }
            set
            {
                if (_Currency != value)
                {
                    _Currency = value;
                    OnPropertyChanged(() => Currency);
                }
            }
        }

        public DateTime ValueDate
        {
            get { return _ValueDate; }
            set
            {
                if (_ValueDate != value)
                {
                    _ValueDate = value;
                    OnPropertyChanged(() => ValueDate);
                }
            }
        }

        public DateTime MaturityDate
        {
            get { return _MaturityDate; }
            set
            {
                if (_MaturityDate != value)
                {
                    _MaturityDate = value;
                    OnPropertyChanged(() => MaturityDate);
                }
            }
        }

        public double CleanPrice
        {
            get { return _CleanPrice; }
            set
            {
                if (_CleanPrice != value)
                {
                    _CleanPrice = value;
                    OnPropertyChanged(() => CleanPrice);
                }
            }
        }


        public double FaceValue
        {
            get { return _FaceValue; }
            set
            {
                if (_FaceValue != value)
                {
                    _FaceValue = value;
                    OnPropertyChanged(() => FaceValue);
                }
            }
        }

        public decimal CouponRate
        {
            get { return _CouponRate; }
            set
            {
                if (_CouponRate != value)
                {
                    _CouponRate = value;
                    OnPropertyChanged(() => CouponRate);
                }
            }
        }

        public decimal CurrentMarketYield
        {
            get { return _CurrentMarketYield; }
            set
            {
                if (_CurrentMarketYield != value)
                {
                    _CurrentMarketYield = value;
                    OnPropertyChanged(() => CurrentMarketYield);
                }
            }
        }

        public DateTime FirstCouponDate
        {
            get { return _FirstCouponDate; }
            set
            {
                if (_FirstCouponDate != value)
                {
                    _FirstCouponDate = value;
                    OnPropertyChanged(() => FirstCouponDate);
                }
            }
        }

        public string Classification
        {
            get { return _Classification; }
            set
            {
                if (_Classification != value)
                {
                    _Classification = value;
                    OnPropertyChanged(() => Classification);
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
        public string Narration
        {
            get { return _Narration; }
            set
            {
                if (_Narration != value)
                {
                    _Narration = value;
                    OnPropertyChanged(() => Narration);
                }
            }
        }

        public bool Split
        {
            get { return _Split; }
            set
            {
                if (_Split != value)
                {
                    _Split = value;
                    OnPropertyChanged(() => Split);
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
        class IFRSBondsValidator : AbstractValidator<IFRSBonds>
        {
            public IFRSBondsValidator()
            {
                RuleFor(obj => obj.RefNo).NotEmpty().WithMessage("RefNo is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new IFRSBondsValidator();
        }
    }
}
