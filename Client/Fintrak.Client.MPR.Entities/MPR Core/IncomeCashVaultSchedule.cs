using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Client.MPR.Entities
{
    public class IncomeCashVaultSchedule : ObjectBase
    {
        int _ID;
        string _AccountNumber;
        string _Branch;
        string _Originator;
        decimal _Ratio;
        int _Period;
        int _Year;
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


        public string AccountNumber
        {
            get { return _AccountNumber; }
            set
            {
                if (_AccountNumber != value)
                {
                    _AccountNumber = value;
                    OnPropertyChanged(() => AccountNumber);
                }
            }
        }


        public string Branch
        {
            get { return _Branch; }
            set
            {
                if (_Branch != value)
                {
                    _Branch = value;
                    OnPropertyChanged(() => Branch);
                }
            }
        }

        public string Originator
        {
            get { return _Originator; }
            set
            {
                if (_Originator != value)
                {
                    _Originator = value;
                    OnPropertyChanged(() => Originator);
                }
            }
        }

        public decimal Ratio
        {
            get { return _Ratio; }
            set
            {
                if (_Ratio != value)
                {
                    _Ratio = value;
                    OnPropertyChanged(() => Ratio);
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
                    OnPropertyChanged(() => Branch);
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


        class IncomeCashVaultScheduleValidator : AbstractValidator<IncomeCashVaultSchedule>
        {
            public IncomeCashVaultScheduleValidator()
            {
                RuleFor(obj => obj.AccountNumber).NotEmpty().WithMessage("Account Number is required.");
                //RuleFor(obj => obj.Branch).NotEmpty().WithMessage("Branch is required.");
        }
        }

        protected override IValidator GetValidator()
        {
            return new IncomeCashVaultScheduleValidator();
        }

    }
}
