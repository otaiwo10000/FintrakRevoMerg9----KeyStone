using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class IncomeAccountsListingTEMP : ObjectBase
    {
        int _Id;
        string _accountnumber;
        string _CustomerName;
        string _MIS_Code;
        string _BranchCode;
        string _AccountOfficer_Code;
        string _Team_branch;
        DateTime _Date_Open;
        string _ApprovalStatus;
        bool _Migrated;

        
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

        public string accountnumber
        {
            get { return _accountnumber; }
            set
            {
                if (_accountnumber != value)
                {
                    _accountnumber = value;
                    OnPropertyChanged(() => accountnumber);
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

        public string BranchCode
        {
            get { return _BranchCode; }
            set
            {
                if (_BranchCode != value)
                {
                    _BranchCode = value;
                    OnPropertyChanged(() => BranchCode);
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

        public string Team_branch
        {
            get { return _Team_branch; }
            set
            {
                if (_Team_branch != value)
                {
                    _Team_branch = value;
                    OnPropertyChanged(() => Team_branch);
                }
            }
        }

        public DateTime Date_Open
        {
            get { return _Date_Open; }
            set
            {
                if (_Date_Open != value)
                {
                    _Date_Open = value;
                    OnPropertyChanged(() => Date_Open);
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



        //class IncomeSplitPoolsRatesAndBasisValidator : AbstractValidator<IncomeSplitPoolsRatesAndBasis>
        //{
        //    public IncomeSplitPoolsRatesAndBasisValidator()
        //    {
        //        RuleFor(obj => obj._PoolRateFCYAsset).NotEmpty().WithMessage("Pool rate FCY asset is required.");
        //    }
        //}

        //protected override IValidator GetValidator()
        //{
        //    return new IncomeSplitPoolsRatesAndBasisValidator();
        //}
    }
}
