using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Client.MPR.Entities
{
    public class IncomeAccountsFintrak : ObjectBase
    {
        int _ID;
        string _AccountNumber;
        string _CustomerName;
        string _MIS_Code;
        string _AccountOfficer_Code;
        string _OldMIS_Code;
        string _OldAccountOfficer_Code;
        int _ExpirationPeriod;
        string _Comments;
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

        public string MIS_Code
        {
            get { return _MIS_Code; }
            set
            {
                if (_MIS_Code != value)
                {
                    _MIS_Code = value;
                    OnPropertyChanged(() => MIS_Code);
                }
            }
        }

        public string AccountOfficer_Code
        {
            get { return _AccountOfficer_Code; }
            set
            {
                if (_AccountOfficer_Code != value)
                {
                    _AccountOfficer_Code = value;
                    OnPropertyChanged(() => AccountOfficer_Code);
                }
            }
        }


        public string OldMIS_Code
        {
            get { return _OldMIS_Code; }
            set
            {
                if (_OldMIS_Code != value)
                {
                    _OldMIS_Code = value;
                    OnPropertyChanged(() => OldMIS_Code);
                }
            }
        }

        public string OldAccountOfficer_Code
        {
            get { return _OldAccountOfficer_Code; }
            set
            {
                if (_OldAccountOfficer_Code != value)
                {
                    _OldAccountOfficer_Code = value;
                    OnPropertyChanged(() => OldAccountOfficer_Code);
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

        public string Comments
        {
            get { return _Comments; }
            set
            {
                if (_Comments != value)
                {
                    _Comments = value;
                    OnPropertyChanged(() => Comments);
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


        class IncomeAccountsFintrakValidator : AbstractValidator<IncomeAccountsFintrak>
        {
            public IncomeAccountsFintrakValidator()
            {
                RuleFor(obj => obj.AccountNumber).NotEmpty().WithMessage("AccountNumber is required.");
                RuleFor(obj => obj.AccountOfficer_Code).NotEmpty().WithMessage("AccountOfficer_Code is required.");
                RuleFor(obj => obj.CustomerName).NotEmpty().WithMessage("CustomerName is required.");
                RuleFor(obj => obj.MIS_Code).NotEmpty().WithMessage("MisCode is required.");
                RuleFor(obj => obj.OldMIS_Code).NotEmpty().WithMessage("OldMisCode is required.");
                RuleFor(obj => obj.OldAccountOfficer_Code).NotEmpty().WithMessage("OldAccountOfficer_Code is required.");
                RuleFor(obj => obj.ExpirationPeriod).NotEmpty().WithMessage("ExpirationPeriod is required.");
                RuleFor(obj => obj.ExpirationYear).NotEmpty().WithMessage("ExpirationYear is required.");
                RuleFor(obj => obj.Comments).NotEmpty().WithMessage("Comments is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new IncomeAccountsFintrakValidator();
        }

    }
}
