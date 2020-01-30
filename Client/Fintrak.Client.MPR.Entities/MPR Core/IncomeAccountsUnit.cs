using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Client.MPR.Entities
{
    public class IncomeAccountsUnit : ObjectBase
    {
        int _ID;
        string _AccountNumber;
        string _Sector_Code;
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

        public string Sector_Code
        {
            get { return _Sector_Code; }
            set
            {
                if (_Sector_Code != value)
                {
                    _Sector_Code = value;
                    OnPropertyChanged(() => Sector_Code);
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


        class IncomeAccountsUnitValidator : AbstractValidator<IncomeAccountsUnit>
        {
            public IncomeAccountsUnitValidator()
            {
               // RuleFor(obj => obj.OldMIS_Code).NotEmpty().WithMessage("OldMis_Code is required.");
              //  RuleFor(obj => obj.NewMIS_Code).NotEmpty().WithMessage("NewMis_Code is required.");
            


            }
        }

        protected override IValidator GetValidator()
        {
            return new IncomeAccountsUnitValidator();
        }

    }
}
