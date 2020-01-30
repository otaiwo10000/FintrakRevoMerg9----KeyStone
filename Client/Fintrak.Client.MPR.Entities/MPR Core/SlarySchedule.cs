using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Client.MPR.Entities
{
    public class SlarySchedule : ObjectBase
    {
        int _ID;
        string _EmpID;
        string _EMP_Name;
        string _Emp_Level;
        string _MIS_Code;
        decimal _Amount;
        string _Pay_Code;
        string _Location;
        string _SBU;
        string _Sol;
        decimal _AnnualPay;
        string _SType;
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


        public string EmpID
        {
            get { return _EmpID; }
            set
            {
                if (_EmpID != value)
                {
                    _EmpID = value;
                    OnPropertyChanged(() => EmpID);
                }
            }
        }


        public string EMP_Name
        {
            get { return _EMP_Name; }
            set
            {
                if (_EMP_Name != value)
                {
                    _EMP_Name = value;
                    OnPropertyChanged(() => EMP_Name);
                }
            }
        }

       public string Emp_Level
        {
            get { return _Emp_Level; }
            set
            {
                if (_Emp_Level != value)
                {
                    _Emp_Level = value;
                    OnPropertyChanged(() => Emp_Level);
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


        public decimal Amount
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

        public string Pay_Code
        {
            get { return _Pay_Code; }
            set
            {
                if (_Pay_Code != value)
                {
                    _Pay_Code = value;
                    OnPropertyChanged(() => Pay_Code);
                }
            }
        }

        public string Location
        {
            get { return _Location; }
            set
            {
                if (_Location != value)
                {
                    _Location = value;
                    OnPropertyChanged(() => Location);
                }
            }
        }

        public string SBU
        {
            get { return _SBU; }
            set
            {
                if (_SBU != value)
                {
                    _SBU = value;
                    OnPropertyChanged(() => SBU);
                }
            }
        }

        public string Sol
        {
            get { return _Sol; }
            set
            {
                if (_Sol != value)
                {
                    _Sol = value;
                    OnPropertyChanged(() => Sol);
                }
            }
        }

        public decimal AnnualPay
        {
            get { return _AnnualPay; }
            set
            {
                if (_AnnualPay != value)
                {
                    _AnnualPay = value;
                    OnPropertyChanged(() => AnnualPay);
                }
            }
        }

        public string SType
        {
            get { return _SType; }
            set
            {
                if (_SType != value)
                {
                    _SType = value;
                    OnPropertyChanged(() => SType);
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


        class SlaryScheduleValidator : AbstractValidator<SlarySchedule>
        {
            public SlaryScheduleValidator()
            {
                //RuleFor(obj => obj.Percentage).NotEmpty().WithMessage("Percentage is required.");
                //RuleFor(obj => obj.Branch).NotEmpty().WithMessage("Branch is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new SlaryScheduleValidator();
        }

    }
}
