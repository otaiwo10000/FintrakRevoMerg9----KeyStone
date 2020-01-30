﻿using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class BondEclComputationResult : ObjectBase
    {
        int _ID;
        string _Refno;
        string _CustomerName;
        DateTime _date_pmt;
        int _YTM;
        string _Code;
        string _FacilityType;
        int _Stage;
        int _Seq;
        double _AmortizedCost;
        string _Currency;
        double _Exchange;
        double _LifetimePD;
        DateTime _Rundate;
        double _DiscountFactor;
        bool _Active;
        double _LGD;
        string _Sector;
        string _CollateralStatus;
        double _ECLOutput;
        double _MacroEco_ECLOutput;
        double _FinalECLOutput;



        public double FinalECLOutput
        {
            get { return _FinalECLOutput; }
            set
            {
                if (_FinalECLOutput != value)
                {
                    _FinalECLOutput = value;
                    OnPropertyChanged(() => FinalECLOutput);
                }
            }
        }

        public double MacroEco_ECLOutput
        {
            get { return _MacroEco_ECLOutput; }
            set
            {
                if (_MacroEco_ECLOutput != value)
                {
                    _MacroEco_ECLOutput = value;
                    OnPropertyChanged(() => MacroEco_ECLOutput);
                }
            }
        }

        public double ECLOutput
        {
            get { return _ECLOutput; }
            set
            {
                if (_ECLOutput != value)
                {
                    _ECLOutput = value;
                    OnPropertyChanged(() => ECLOutput);
                }
            }
        }

        public string CollateralStatus
        {
            get { return _CollateralStatus; }
            set
            {
                if (_CollateralStatus != value)
                {
                    _CollateralStatus = value;
                    OnPropertyChanged(() => CollateralStatus);
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

        public double LGD
        {
            get { return _LGD; }
            set
            {
                if (_LGD != value)
                {
                    _LGD = value;
                    OnPropertyChanged(() => LGD);
                }
            }
        }

        public DateTime Rundate
        {
            get { return _Rundate; }
            set
            {
                if (_Rundate != value)
                {
                    _Rundate = value;
                    OnPropertyChanged(() => Rundate);
                }
            }
        }

        public double Exchange
        {
            get { return _Exchange; }
            set
            {
                if (_Exchange != value)
                {
                    _Exchange = value;
                    OnPropertyChanged(() => Exchange);
                }
            }
        }

        public double AmortizedCost
        {
            get { return _AmortizedCost; }
            set
            {
                if (_AmortizedCost != value)
                {
                    _AmortizedCost = value;
                    OnPropertyChanged(() => AmortizedCost);
                }
            }
        }

        public double LifetimePD
        {
            get { return _LifetimePD; }
            set
            {
                if (_LifetimePD != value)
                {
                    _LifetimePD = value;
                    OnPropertyChanged(() => LifetimePD);
                }
            }
        }

        public double DiscountFactor
        {
            get { return _DiscountFactor; }
            set
            {
                if (_DiscountFactor != value)
                {
                    _DiscountFactor = value;
                    OnPropertyChanged(() => DiscountFactor);
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

        public int Seq
        {
            get { return _Seq; }
            set
            {
                if (_Seq != value)
                {
                    _Seq = value;
                    OnPropertyChanged(() => Seq);
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

        public DateTime date_pmt
        {
            get { return _date_pmt; }
            set
            {
                if (_date_pmt != value)
                {
                    _date_pmt = value;
                    OnPropertyChanged(() => date_pmt);
                }
            }
        }

        public string FacilityType
        {
            get { return _FacilityType; }
            set
            {
                if (_FacilityType != value)
                {
                    _FacilityType = value;
                    OnPropertyChanged(() => FacilityType);
                }
            }
        }

        public string Code
        {
            get { return _Code; }
            set
            {
                if (_Code != value)
                {
                    _Code = value;
                    OnPropertyChanged(() => Code);
                }
            }
        }

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


        public int YTM
        {
            get { return _YTM; }
            set
            {
                if (_YTM != value)
                {
                    _YTM = value;
                    OnPropertyChanged(() => YTM);
                }
            }
        }

        public string Refno
        {
            get { return _Refno; }
            set
            {
                if (_Refno != value)
                {
                    _Refno = value;
                    OnPropertyChanged(() => Refno);
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


        class BondEclComputationResultValidator : AbstractValidator<BondEclComputationResult>
        {
            public BondEclComputationResultValidator()
            {
                //RuleFor(obj => obj._CustomerName).NotEmpty().WithMessage("CustomerName is required.");

            }
        }

        protected override IValidator GetValidator()
        {
            return new BondEclComputationResultValidator();
        }
    }
}
