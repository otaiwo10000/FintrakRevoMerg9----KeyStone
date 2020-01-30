using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Client.MPR.Entities
{
    public class OneBankBranch : ObjectBase
    {
        int _Id;
        string _StaffName;
        string _BRANCH_CODE;
        string _GradeLevel;
        decimal _CASATarget;
        int _Period;
        int _Year;
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



        public string StaffName
        {
            get { return _StaffName; }
            set
            {
                if (_StaffName != value)
                {
                    _StaffName = value;
                    OnPropertyChanged(() => StaffName);
                }
            }
        }

        public string BRANCH_CODE
        {
            get { return _BRANCH_CODE; }
            set
            {
                if (_BRANCH_CODE != value)
                {
                    _BRANCH_CODE = value;
                    OnPropertyChanged(() => BRANCH_CODE);
                }
            }
        }

        public string GradeLevel
        {
            get { return _GradeLevel; }
            set
            {
                if (_GradeLevel != value)
                {
                    _GradeLevel = value;
                    OnPropertyChanged(() => GradeLevel);
                }
            }
        }


        public decimal CASATarget
        {
            get { return _CASATarget; }
            set
            {
                if (_CASATarget != value)
                {
                    _CASATarget = value;
                    OnPropertyChanged(() => CASATarget);
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
                    OnPropertyChanged(() => Year);
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


        //class OneBankAOValidator : AbstractValidator<OneBankAO>
        //{
        //    public OneBankAOValidator()
        //    {
        //        //RuleFor(obj => obj.Percentage).NotEmpty().WithMessage("Percentage is required.");
        //        //RuleFor(obj => obj.Branch).NotEmpty().WithMessage("Branch is required.");




        //    }
        //}

        //protected override IValidator GetValidator()
        //{
        //    return new OneBankAOValidator();
        //}

    }
}
