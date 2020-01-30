using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Client.MPR.Entities
{
    public class OneBankTeamTable : ObjectBase
    {
        int _ID;
        string _StaffName;
        string _Team_Code;
        string _GradeLevel;
        decimal _CASATarget;
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


        class OneBankTeamTableValidator : AbstractValidator<OneBankTeamTable>
        {
            public OneBankTeamTableValidator()
            {
          

            }
        }

        protected override IValidator GetValidator()
        {
            return new OneBankTeamTableValidator();
        }

    }
}
