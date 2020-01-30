using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class Placement : ObjectBase
    {
        int _Placement_Id;
        string _RefNo;
        string _CustomerName;
        string _Rating;
        DateTime _BookingDate;
        DateTime _ValueDate;
        DateTime _MaturityDate;
        double _Amount;
        double _Rate;
        string _Currency;
        double _ExchangeRate;
        double _LCY_Amount;
        string _CollateralType;
        double _CollateralValue;
        double _CollateralHaircut;
        DateTime _RunDate;
        bool _Active;

        public int Placement_Id
        {
            get { return _Placement_Id; }
            set
            {
                if (_Placement_Id != value)
                {
                    _Placement_Id = value;
                    OnPropertyChanged(() => Placement_Id);
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


        public string CustomerName
        {
            get { return _CustomerName; }
            set
            {
                if (_CustomerName != value)
                {
                    _CustomerName = value;
                    OnPropertyChanged(() => CustomerName);
                }
            }
        }

        public string Rating
        {
            get { return _Rating; }
            set
            {
                if (_Rating != value)
                {
                    _Rating = value;
                    OnPropertyChanged(() => Rating);
                }
            }
        }

        public DateTime BookingDate
        {
            get { return _BookingDate; }
            set
            {
                if (_BookingDate != value)
                {
                    _BookingDate = value;
                    OnPropertyChanged(() => BookingDate);
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

        public double Amount
        {
            get { return _Amount; }
            set
            {
                if (_Amount != value)
                {
                    _Amount = value;
                    OnPropertyChanged(() => Amount);
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

        public double ExchangeRate
        {
            get { return _ExchangeRate; }
            set
            {
                if (_ExchangeRate != value)
                {
                    _ExchangeRate = value;
                    OnPropertyChanged(() => ExchangeRate);
                }
            }
        }

        public double LCY_Amount
        {
            get { return _LCY_Amount; }
            set
            {
                if (_LCY_Amount != value)
                {
                    _LCY_Amount = value;
                    OnPropertyChanged(() => LCY_Amount);
                }
            }
        }

        public string CollateralType
        {
            get { return _CollateralType; }
            set
            {
                if (_CollateralType != value)
                {
                    _CollateralType = value;
                    OnPropertyChanged(() => CollateralType);
                }
            }
        }

        public double CollateralValue
        {
            get { return _CollateralValue; }
            set
            {
                if (_CollateralValue != value)
                {
                    _CollateralValue = value;
                    OnPropertyChanged(() => CollateralValue);
                }
            }
        }

        public double CollateralHaircut
        {
            get { return _CollateralHaircut; }
            set
            {
                if (_CollateralHaircut != value)
                {
                    _CollateralHaircut = value;
                    OnPropertyChanged(() => CollateralHaircut);
                }
            }
        }

        public DateTime RunDate
        {
            get { return _RunDate; }
            set
            {
                if (_RunDate != value)
                {
                    _RunDate = value;
                    OnPropertyChanged(() => RunDate);
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


        class PlacementValidator : AbstractValidator<Placement>
        {
            public PlacementValidator()
            {
                RuleFor(obj => obj.RefNo).NotEmpty().WithMessage("RefNo is required.");
              
            }
        }

        protected override IValidator GetValidator()
        {
            return new PlacementValidator();
        }
    }
}
