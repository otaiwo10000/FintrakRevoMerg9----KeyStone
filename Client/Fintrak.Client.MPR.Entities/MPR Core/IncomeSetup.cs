using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Client.MPR.Entities
{
    public class IncomeSetup : ObjectBase
    {
        int _ID;
       decimal _NDIC;
        decimal _CRR;
        int _CurrentPeriod;
        int _Year;
       string _PDLProductCode;
        int _FinYearCount;
        decimal _GLLP;
        DateTime _StartDate;
        DateTime _EndDate;
        double _TaxRate;
        string _Runmode;
        string _ReportMode;
        string _ExcoMIS;
        string _HRMIS;
        decimal _ManagingSharePerc;
        decimal _BrokerSharePerc;
        string _SFU;
        //string _ExcoAccountOfficer;
        //string _ProPrietryMIS;
        //string _Othermis;
        //int _accountnumberdigit;
        //string _localcurrcode;
        //decimal _LMP;
        //decimal _CRRInt;
        //decimal _LMPInt;
        Double _Vault_Cash;
        //string _Title;
        bool _Active;

        public int ID
        {
            get { return _ID; }
            set
            {
                if (_ID != value)
                {
                    _ID = value;
                    OnPropertyChanged(() => ID);
                }
            }
        }

        public decimal NDIC
        {
            get { return _NDIC; }
            set
            {
                if (_NDIC != value)
                {
                    _NDIC = value;
                    OnPropertyChanged(() => NDIC);
                }
            }
        }

        public decimal CRR
        {
            get { return _CRR; }
            set
            {
                if (_CRR != value)
                {
                    _CRR = value;
                    OnPropertyChanged(() => CRR);
                }
            }
        }

        public int CurrentPeriod
        {
            get { return _CurrentPeriod; }
            set
            {
                if (_CurrentPeriod != value)
                {
                    _CurrentPeriod = value;
                    OnPropertyChanged(() => CurrentPeriod);
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

     

        public string PDLProductCode
        {
            get { return _PDLProductCode; }
            set
            {
                if (_PDLProductCode != value)
                {
                    _PDLProductCode = value;
                    OnPropertyChanged(() => PDLProductCode);
                }
            }
        }

        public int FinYearCount
        {
            get { return _FinYearCount; }
            set
            {
                if (_FinYearCount != value)
                {
                    _FinYearCount = value;
                    OnPropertyChanged(() => FinYearCount);
                }
            }
        }

        public decimal GLLP
        {
            get { return _GLLP; }
            set
            {
                if (_GLLP != value)
                {
                    _GLLP = value;
                    OnPropertyChanged(() => GLLP);
                }
            }
        }

        public DateTime StartDate
        {
            get { return _StartDate; }
            set
            {
                if (_StartDate != value)
                {
                    _StartDate = value;
                    OnPropertyChanged(() => StartDate);
                }
            }
        }

        public DateTime EndDate
        {
            get { return _EndDate; }
            set
            {
                if (_EndDate != value)
                {
                    _EndDate = value;
                    OnPropertyChanged(() => EndDate);
                }
            }
        }

        public double TaxRate
        {
            get { return _TaxRate; }
            set
            {
                if (_TaxRate != value)
                {
                    _TaxRate = value;
                    OnPropertyChanged(() => TaxRate);
                }
            }
        }

        public string Runmode
        {
            get { return _Runmode; }
            set
            {
                if (_Runmode != value)
                {
                    _Runmode = value;
                    OnPropertyChanged(() => Runmode);
                }
            }
        }

        public string ReportMode
        {
            get { return _ReportMode; }
            set
            {
                if (_ReportMode != value)
                {
                    _ReportMode = value;
                    OnPropertyChanged(() => ReportMode);
                }
            }
        }

        public string ExcoMIS
        {
            get { return _ExcoMIS; }
            set
            {
                if (_ExcoMIS != value)
                {
                    _ExcoMIS = value;
                    OnPropertyChanged(() => ExcoMIS);
                }
            }
        }

        public string HRMIS
        {
            get { return _HRMIS; }
            set
            {
                if (_HRMIS != value)
                {
                    _HRMIS = value;
                    OnPropertyChanged(() => HRMIS);
                }
            }
        }

        public decimal ManagingSharePerc
        {
            get { return _ManagingSharePerc; }
            set
            {
                if (_ManagingSharePerc != value)
                {
                    _ManagingSharePerc = value;
                    OnPropertyChanged(() => ManagingSharePerc);
                }
            }
        }

        public decimal BrokerSharePerc
        {
            get { return _BrokerSharePerc; }
            set
            {
                if (_BrokerSharePerc != value)
                {
                    _BrokerSharePerc = value;
                    OnPropertyChanged(() => BrokerSharePerc);
                }
            }
        }

        public string SFU
        {
            get { return _SFU; }
            set
            {
                if (_SFU != value)
                {
                    _SFU = value;
                    OnPropertyChanged(() => SFU);
                }
            }
        }

        public Double Vault_Cash
        {
            get { return _Vault_Cash; }
            set
            {
                if (_Vault_Cash != value)
                {
                    _Vault_Cash = value;
                    OnPropertyChanged(() => Vault_Cash);
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


        class IncomeSetupValidator : AbstractValidator<IncomeSetup>
        {
            public IncomeSetupValidator()
            {
              //  RuleFor(obj => obj.Percentage).NotEmpty().WithMessage("Percentage is required.");




            }
        }

        protected override IValidator GetValidator()
        {
            return new IncomeSetupValidator();
        }

    }
}
