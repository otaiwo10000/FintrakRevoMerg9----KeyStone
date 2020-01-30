using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class OffBalanceSheetExposure : ObjectBase
    {
        int _ObeId;
        string _RefNo;
        string _CUR;
        DateTime _TRNX_DATE;
        string _Maturity_profile;
        DateTime _MATURITY_DATE;
        double _Amount_FCY;
        double _Amount_NGN;
        decimal _Ex_Rate;
        int _TenorMonths;
        string _Portfolio;
        string _RATING;
        string _SUB_PORTFOLIO;
        bool _CanCrystallize;
        bool _Active;
        int _Staging;
        double _EIR;
        string _Type;

        public int ObeId

        {
            get { return _ObeId; }
            set
            {
                if (_ObeId != value)
                {
                    _ObeId = value;
                    OnPropertyChanged(() => ObeId);
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
        public string SUB_PORTFOLIO
        {
            get { return _SUB_PORTFOLIO; }
            set
            {
                if (_SUB_PORTFOLIO != value)
                {
                    _SUB_PORTFOLIO = value;
                    OnPropertyChanged(() => SUB_PORTFOLIO);
                }
            }
        }

        public string Maturity_profile
        {
            get { return _Maturity_profile; }
            set
            {
                if (_Maturity_profile != value)
                {
                    _Maturity_profile = value;
                    OnPropertyChanged(() => Maturity_profile);
                }
            }
        }
        public string RATING
        {
            get { return _RATING; }
            set
            {
                if (_RATING != value)
                {
                    _RATING = value;
                    OnPropertyChanged(() => RATING);
                }
            }
        }
        public int Staging
        {
            get { return _Staging; }
            set
            {
                if (_Staging != value)
                {
                    _Staging = value;
                    OnPropertyChanged(() => Staging);
                }
            }
        }
        public string Type
        {
            get { return _Type; }
            set
            {
                if (Type != value)
                {
                    _Type = value;
                    OnPropertyChanged(() => Type);
                }
            }
        }

        public string CUR
        {
            get { return _CUR; }
            set
            {
                if (_CUR != value)
                {
                    _CUR = value;
                    OnPropertyChanged(() => CUR);
                }
            }
        }
        public DateTime TRNX_DATE
        {
            get { return _TRNX_DATE; }
            set
            {
                if (_TRNX_DATE != value)
                {
                    _TRNX_DATE = value;
                    OnPropertyChanged(() => TRNX_DATE);
                }
            }
        }

        public DateTime MATURITY_DATE
        {
            get { return _MATURITY_DATE; }
            set
            {
                if (_MATURITY_DATE != value)
                {
                    _MATURITY_DATE = value;
                    OnPropertyChanged(() => MATURITY_DATE);
                }
            }
        }

        public double Amount_FCY
        {
            get { return _Amount_FCY; }
            set
            {
                if (_Amount_FCY != value)
                {
                    _Amount_FCY = value;
                    OnPropertyChanged(() => Amount_FCY);
                }
            }
        }


        public double Amount_NGN
        {
            get { return _Amount_NGN; }
            set
            {
                if (_Amount_NGN != value)
                {
                    _Amount_NGN = value;
                    OnPropertyChanged(() => Amount_NGN);
                }
            }
        }

        public decimal Ex_Rate
        {
            get { return _Ex_Rate; }
            set
            {
                if (_Ex_Rate != value)
                {
                    _Ex_Rate = value;
                    OnPropertyChanged(() => Ex_Rate);
                }
            }
        }

        public double EIR
        {
            get { return _EIR; }
            set
            {
                if (_EIR != value)
                {
                    _EIR = value;
                    OnPropertyChanged(() => EIR);
                }
            }
        }

        public int TenorMonths
        {
            get { return _TenorMonths; }
            set
            {
                if (_TenorMonths != value)
                {
                    _TenorMonths = value;
                    OnPropertyChanged(() => TenorMonths);
                }
            }
        }

        public string Portfolio
        {
            get { return _Portfolio; }
            set
            {
                if (_Portfolio != value)
                {
                    _Portfolio = value;
                    OnPropertyChanged(() => Portfolio);
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
        public bool CanCrystallize
        {
            get { return _CanCrystallize; }
            set
            {
                if (_CanCrystallize != value)
                {
                    _CanCrystallize = value;
                    OnPropertyChanged(() => CanCrystallize);
                }
            }
        }
        
        class OffBalanceSheetExposureValidator : AbstractValidator<OffBalanceSheetExposure>
        {
            public OffBalanceSheetExposureValidator()
            {
                RuleFor(obj => obj.RefNo).NotEmpty().WithMessage("RefNo is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new OffBalanceSheetExposureValidator();
        }
    }
}
