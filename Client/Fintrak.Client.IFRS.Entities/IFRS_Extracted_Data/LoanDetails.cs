using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class RawLoanDetails : ObjectBase
    {
        int _LoanDetailId;
        string _RefNo;
        string _AccountNo;
        double _PastDueAmount;
        double _ODLimit;
        double _CollateralHaircut;
        double _CollateralRecoverableAmt;
        string _Segment;
        string _CollateralType;
        double _PrincipalOutstandingBal;
        double _Amount;
        double _Interest_Receiv_Pay_UnEarn;
        string _ProductCode;
        string _ProductName;
        string _Currency;
        DateTime _ValueDate;
        DateTime _MaturityDate;
        DateTime _FirstRepaymentdate;
        DateTime _PrincipalFirstRepaymentDate;
        int _InterestRepayFreq;
        int _PrincipalRepayFreq;
        decimal _ExchangeRate;
        //string _Rating;
        decimal _Rate;
        int _Stage;
        double _CollateralValue;
        string _CompanyCode;
        string _Sector;
        string _Classification;
        string _SubClassification;
        bool _Active;


        public int LoanDetailId
        {
            get { return _LoanDetailId; }
            set
            {
                if (_LoanDetailId != value)
                {
                    _LoanDetailId = value;
                    OnPropertyChanged(() => LoanDetailId);
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

        public int Stage
        {
            get { return _Stage; }
            set
            {
                if (_Stage != value)
                {
                    _Stage = value;
                    OnPropertyChanged(() => Stage);
                }
            }
        }


        //public string Rating
        //{
        //    get { return _Rating; }
        //    set
        //    {
        //        if (_Rating != value)
        //        {
        //            _Rating = value;
        //            OnPropertyChanged(() => Rating);
        //        }
        //    }
        //}
        public string AccountNo
        {
            get { return _AccountNo; }
            set
            {
                if (_AccountNo != value)
                {
                    _AccountNo = value;
                    OnPropertyChanged(() => AccountNo);
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
        public double PastDueAmount
        {
            get { return _PastDueAmount; }
            set
            {
                if (_PastDueAmount != value)
                {
                    _PastDueAmount = value;
                    OnPropertyChanged(() => PastDueAmount);
                }
            }
        }
        public double ODLimit 
        {
            get { return _ODLimit ; }
            set
            {
                if (_ODLimit  != value)
                {
                    _ODLimit  = value;
                    OnPropertyChanged(() => ODLimit );
                }
            }
        }
        public double CollateralHaircut 
        {
            get { return _CollateralHaircut ; }
            set
            {
                if (_CollateralHaircut  != value)
                {
                    _CollateralHaircut  = value;
                    OnPropertyChanged(() => CollateralHaircut );
                }
            }
        }
        public double CollateralRecoverableAmt 
        {
            get { return _CollateralRecoverableAmt ; }
            set
            {
                if (_CollateralRecoverableAmt  != value)
                {
                    _CollateralRecoverableAmt  = value;
                    OnPropertyChanged(() => CollateralRecoverableAmt );
                }
            }
        }
        public string Segment
        {
            get { return _Segment; }
            set
            {
                if (_Segment != value)
                {
                    _Segment = value;
                    OnPropertyChanged(() => Segment);
                }
            }
        }
        public string CollateralType 
        {
            get { return _CollateralType ; }
            set
            {
                if (_CollateralType  != value)
                {
                    _CollateralType  = value;
                    OnPropertyChanged(() => CollateralType );
                }
            }
        }
        public double PrincipalOutstandingBal
        {
            get { return _PrincipalOutstandingBal; }
            set
            {
                if (_PrincipalOutstandingBal != value)
                {
                    _PrincipalOutstandingBal = value;
                    OnPropertyChanged(() => PrincipalOutstandingBal);
                }
            }
        }
        public double Interest_Receiv_Pay_UnEarn
        {
            get { return _Interest_Receiv_Pay_UnEarn; }
            set
            {
                if (_Interest_Receiv_Pay_UnEarn != value)
                {
                    _Interest_Receiv_Pay_UnEarn = value;
                    OnPropertyChanged(() => Interest_Receiv_Pay_UnEarn);
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
                    OnPropertyChanged(() => ProductCode);
                }
            }
        }
        public string ProductName
        {
            get { return _ProductName; }
            set
            {
                if (_ProductName != value)
                {
                    _ProductName = value;
                    OnPropertyChanged(() => ProductName);
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
        public DateTime FirstRepaymentdate
        {
            get { return _FirstRepaymentdate; }
            set
            {
                if (_FirstRepaymentdate != value)
                {
                    _FirstRepaymentdate = value;
                    OnPropertyChanged(() => FirstRepaymentdate);
                }
            }
        }

        public DateTime PrincipalFirstRepaymentDate
        {
            get { return _PrincipalFirstRepaymentDate; }
            set
            {
                if (_PrincipalFirstRepaymentDate != value)
                {
                    _PrincipalFirstRepaymentDate = value;
                    OnPropertyChanged(() => PrincipalFirstRepaymentDate);
                }
            }
        }

        public int InterestRepayFreq
        {
            get { return _InterestRepayFreq; }
            set
            {
                if (_InterestRepayFreq != value)
                {
                    _InterestRepayFreq = value;
                    OnPropertyChanged(() => InterestRepayFreq);
                }
            }
        }

        public int PrincipalRepayFreq
        {
            get { return _PrincipalRepayFreq; }
            set
            {
                if (_PrincipalRepayFreq != value)
                {
                    _PrincipalRepayFreq = value;
                    OnPropertyChanged(() => PrincipalRepayFreq);
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

        public decimal ExchangeRate
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

        public string Sector
        {
            get { return _Sector; }
            set
            {
                if (_Sector != value)
                {
                    _Sector = value;
                    OnPropertyChanged(() => Sector);
                }
            }
        }

        public string Classification
        {
            get { return _Classification.ToUpper(); }
            set
            {
                if (_Classification != value)
                {
                    _Classification = value;
                    OnPropertyChanged(() => Classification);
                }
            }
        }
        public string SubClassification
        {
            get { return _SubClassification.ToUpper(); }
            set
            {
                if (_SubClassification != value)
                {
                    _SubClassification = value;
                    OnPropertyChanged(() => SubClassification);
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
        class LoanDetailsValidator : AbstractValidator<RawLoanDetails>
        {
            public LoanDetailsValidator()
            {
                RuleFor(obj => obj.RefNo).NotEmpty().WithMessage("RefNo is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new LoanDetailsValidator();
        }
    }
}
