using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Client.MPR.Entities
{
    public class OneBankRegionTD : ObjectBase
    {
        int _ID;
        string _StaffName;
        string _Grade_Level;
        string _Region_Code;
        decimal _CASA_Target;
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

        public string Grade_Level
        {
            get { return _Grade_Level; }
            set
            {
                if (_Grade_Level != value)
                {
                    _Grade_Level = value;
                    OnPropertyChanged(() => Grade_Level);
                }
            }
        }

        public string Region_Code
        {
            get { return _Region_Code; }
            set
            {
                if (_Region_Code != value)
                {
                    _Region_Code = value;
                    OnPropertyChanged(() => Region_Code);
                }
            }
        }

        public decimal CASA_Target
        {
            get { return _CASA_Target; }
            set
            {
                if (_CASA_Target != value)
                {
                    _CASA_Target = value;
                    OnPropertyChanged(() => CASA_Target);
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


        class OneBankRegionTDValidator : AbstractValidator<OneBankRegionTD>
        {
            public OneBankRegionTDValidator()
            {
          

            }
        }

        protected override IValidator GetValidator()
        {
            return new OneBankRegionTDValidator();
        }

    }
}
