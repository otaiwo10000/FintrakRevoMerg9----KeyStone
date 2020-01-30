using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class MacroEconomicsNPL : ObjectBase
    {
        int _macroeconomicnplId;
        int _Seq;
        int _Year;
        double _NPL;
        bool _Active;

        public int macroeconomicnplId
        {
            get { return _macroeconomicnplId; }
            set
            {
                if (_macroeconomicnplId != value)
                {
                    _macroeconomicnplId = value;
                    OnPropertyChanged(() => macroeconomicnplId);
                }
            }
        }

        public int Seq
        {
            get { return _Seq; }
            set
            {
                if (_Seq != value)
                {
                    _Seq = value;
                    OnPropertyChanged(() => Seq);
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

        public double NPL
        {
            get { return _NPL; }
            set
            {
                if (_NPL != value)
                {
                    _NPL = value;
                    OnPropertyChanged(() => NPL);
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


        class MacroEconomicsNPLValidator : AbstractValidator<MacroEconomicsNPL>
        {
            public MacroEconomicsNPLValidator()
            {
                RuleFor(obj => obj.Year).NotEmpty().WithMessage("Year is required.");
              
            }
        }

        protected override IValidator GetValidator()
        {
            return new MacroEconomicsNPLValidator();
        }
    }
}
