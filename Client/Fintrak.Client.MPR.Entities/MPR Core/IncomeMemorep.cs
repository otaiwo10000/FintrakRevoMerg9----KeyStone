using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Client.MPR.Entities
{
    public class IncomeMemorep : ObjectBase
    {
        int _ID;
        string _PL_CATEG;
        string _CATEGORYNAME;
        string _GLName;
        int _YEAR;
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


        public string PL_CATEG
        {
            get { return _PL_CATEG; }
            set
            {
                if (_PL_CATEG != value)
                {
                    _PL_CATEG = value;
                    OnPropertyChanged(() => PL_CATEG);
                }
            }
        }

        public string CATEGORYNAME
        {
            get { return _CATEGORYNAME; }
            set
            {
                if (_CATEGORYNAME != value)
                {
                    _CATEGORYNAME = value;
                    OnPropertyChanged(() => CATEGORYNAME);
                }
            }
        }

        public string GLName
        {
            get { return _GLName; }
            set
            {
                if (_GLName != value)
                {
                    _GLName = value;
                    OnPropertyChanged(() => GLName);
                }
            }
        }

        public int YEAR
        {
            get { return _YEAR; }
            set
            {
                if (_YEAR != value)
                {
                    _YEAR = value;
                    OnPropertyChanged(() => YEAR);
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


        class IncomeMemorepValidator : AbstractValidator<IncomeMemorep>
        {
            public IncomeMemorepValidator()
            {
               // RuleFor(obj => obj.OldMIS_Code).NotEmpty().WithMessage("OldMis_Code is required.");
              //  RuleFor(obj => obj.NewMIS_Code).NotEmpty().WithMessage("NewMis_Code is required.");
            


            }
        }

        protected override IValidator GetValidator()
        {
            return new IncomeMemorepValidator();
        }

    }
}
