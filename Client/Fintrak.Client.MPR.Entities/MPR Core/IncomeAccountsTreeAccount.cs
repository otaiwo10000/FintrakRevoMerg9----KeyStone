using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Client.MPR.Entities
{
    public class IncomeAccountsTreeAccount : ObjectBase
    {
        int _ID;
        string _AccountNumber;
        string _ShareReason;
        int _ExpirationPeriod;
        int _ExpirationYear;
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

        public string ShareReason
        {
            get { return _ShareReason; }
            set
            {
                if (_ShareReason != value)
                {
                    _ShareReason = value;
                    OnPropertyChanged(() => ShareReason);
                }
            }
        }

        public int ExpirationPeriod
        {
            get { return _ExpirationPeriod; }
            set
            {
                if (_ExpirationPeriod != value)
                {
                    _ExpirationPeriod = value;
                    OnPropertyChanged(() => ExpirationPeriod);
                }
            }
        }


        public int ExpirationYear
        {
            get { return _ExpirationYear; }
            set
            {
                if (_ExpirationYear != value)
                {
                    _ExpirationYear = value;
                    OnPropertyChanged(() => ExpirationYear);
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


        class IncomeAccountsTreeAccountValidator : AbstractValidator<IncomeAccountsTreeAccount>
        {
            public IncomeAccountsTreeAccountValidator()
            {
                RuleFor(obj => obj.AccountNumber).NotEmpty().WithMessage("AccountNumber is required.");
                RuleFor(obj => obj.ShareReason).NotEmpty().WithMessage("ShareReason is required.");
                RuleFor(obj => obj.ExpirationPeriod).NotEmpty().WithMessage("ExpirationPeriod is required.");
                RuleFor(obj => obj.ExpirationYear).NotEmpty().WithMessage("ExpirationYear is required.");
               
                


            }
        }

        protected override IValidator GetValidator()
        {
            return new IncomeAccountsTreeAccountValidator();
        }

    }
}
