﻿using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Basic.Entities
{
    public class IndividualSchedule : ObjectBase
    {
        int _Id;
        string _RefNo;
        decimal _IRR;
        double _AmountPrinEnd;
        double _Amount;
        DateTime _ValueDate;
        DateTime _MaturityDate;
        double _FeeAmount;
        DateTime _RunDate;
        bool _Processed;
        bool _Active;


        public int Id
        {
            get { return _Id; }
            set
            {
                if (_Id != value)
                {
                    _Id = value;
                    OnPropertyChanged(() => Id);
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

        public decimal IRR
        {
            get { return _IRR; }
            set
            {
                if (_IRR != value)
                {
                    _IRR = value;
                    OnPropertyChanged(() => IRR);
                }
            }
        }

        public double AmountPrinEnd
        {
            get { return _AmountPrinEnd; }
            set
            {
                if (_AmountPrinEnd != value)
                {
                    _AmountPrinEnd = value;
                    OnPropertyChanged(() => AmountPrinEnd);
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

        public double FeeAmount
        {
            get { return _FeeAmount; }
            set
            {
                if (_FeeAmount != value)
                {
                    _FeeAmount = value;
                    OnPropertyChanged(() => FeeAmount);
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

        public bool Processed
        {
            get { return _Processed; }
            set
            {
                if (_Processed != value)
                {
                    _Processed = value;
                    OnPropertyChanged(() => Processed);
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


        class IndividualScheduleValidator : AbstractValidator<IndividualSchedule>
        {
            public IndividualScheduleValidator()
            {
                RuleFor(obj => obj.RefNo).NotEmpty().WithMessage("RefNo is required.");
                RuleFor(obj => obj.IRR).GreaterThan(0).WithMessage("IRR is required."); 
                //RuleFor(obj => obj.AmountPrinEnd).GreaterThan(0).WithMessage("AmountPrinEnd is required.");
                RuleFor(obj => obj.Amount).GreaterThan(0).WithMessage("Amount is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new IndividualScheduleValidator();
        }

    }
}