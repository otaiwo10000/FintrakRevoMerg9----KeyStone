using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Client.MPR.Entities
{
    public class IncomeAccountsNpl : ObjectBase
    {
        int _ID;
        string _AccountNumber;
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


        class IncomeAccountsNplValidator : AbstractValidator<IncomeAccountsNpl>
        {
            public IncomeAccountsNplValidator()
            {
                RuleFor(obj => obj.AccountNumber).NotEmpty().WithMessage("AccountNumber is required.");



            }
        }

        protected override IValidator GetValidator()
        {
            return new IncomeAccountsNplValidator();
        }

    }
}
