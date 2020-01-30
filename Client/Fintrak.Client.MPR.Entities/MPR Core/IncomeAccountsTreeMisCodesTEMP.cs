using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Client.MPR.Entities
{
    public class IncomeAccountsTreeMisCodesTEMP : ObjectBase
    {
        int _ID;
        string _AccountNumber;
        string _AccountOfficer_Code;
        string _AccountOfficerName;
        decimal _ShareRatio;
        //decimal _Ratio;
        string _Team_Code;
        string _ApprovalStatus { get; set; }
        bool _Migrated { get; set; }
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


        public string AccountOfficerName
        {
            get { return _AccountOfficerName; }
            set
            {
                if (_AccountOfficerName != value)
                {
                    _AccountOfficerName = value;
                    OnPropertyChanged(() => AccountOfficerName);
                }
            }
        }

        public decimal ShareRatio
        {
            get { return _ShareRatio; }
            set
            {
                if (_ShareRatio != value)
                {
                    _ShareRatio = value;
                    OnPropertyChanged(() => ShareRatio);
                }
            }
        }

        //public decimal Ratio
        //{
        //    get { return _Ratio; }
        //    set
        //    {
        //        if (_Ratio != value)
        //        {
        //            _Ratio = value;
        //            OnPropertyChanged(() => Ratio);
        //        }
        //    }
        //}

        public string Team_Code
        {
            get { return _Team_Code; }
            set
            {
                if (_Team_Code != value)
                {
                    _Team_Code = value;
                    OnPropertyChanged(() => Team_Code);
                }
            }
        }

        public string ApprovalStatus
        {
            get { return _ApprovalStatus; }
            set
            {
                if (_ApprovalStatus != value)
                {
                    _ApprovalStatus = value;
                    OnPropertyChanged(() => ApprovalStatus);
                }
            }
        }

        public bool Migrated
        {
            get { return _Migrated; }
            set
            {
                if (_Migrated != value)
                {
                    _Migrated = value;
                    OnPropertyChanged(() => Migrated);
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


        class IncomeAccountsTreeMisCodesTEMPValidator : AbstractValidator<IncomeAccountsTreeMisCodesTEMP>
        {
            public IncomeAccountsTreeMisCodesTEMPValidator()
            {
                RuleFor(obj => obj.AccountNumber).NotEmpty().WithMessage("AccountNumber is required.");
                RuleFor(obj => obj.AccountOfficer_Code).NotEmpty().WithMessage("AccountOfficer_Code is required.");
                RuleFor(obj => obj.AccountOfficerName).NotEmpty().WithMessage("AccountOfficerName is required.");
                RuleFor(obj => obj.ShareRatio).NotEmpty().WithMessage("ShareRatio is required.");
                //RuleFor(obj => obj.Ratio).NotEmpty().WithMessage("Ratio is required.");
                RuleFor(obj => obj.Team_Code).NotEmpty().WithMessage("Team_Code is required.");
                


            }
        }

        protected override IValidator GetValidator()
        {
            return new IncomeAccountsTreeMisCodesTEMPValidator();
        }

    }
}
