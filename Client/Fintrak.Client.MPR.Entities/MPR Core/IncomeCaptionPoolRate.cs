using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Client.MPR.Entities
{
    public class IncomeCaptionPoolRate : ObjectBase
    {
        int _ID;
        string _Caption;
        decimal _Pool_rate;
        bool _ComputeInterest;
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


        public string Caption
        {
            get { return _Caption; }
            set
            {
                if (_Caption != value)
                {
                    _Caption = value;
                    OnPropertyChanged(() => Caption);
                }
            }
        }

        public decimal Pool_rate
        {
            get { return _Pool_rate; }
            set
            {
                if (_Pool_rate != value)
                {
                    _Pool_rate = value;
                    OnPropertyChanged(() => Pool_rate);
                }
            }
        }

        public bool ComputeInterest
        {
            get { return _ComputeInterest; }
            set
            {
                if (_ComputeInterest != value)
                {
                    _ComputeInterest = value;
                    OnPropertyChanged(() => ComputeInterest);
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


        class IncomeCaptionPoolRateValidator : AbstractValidator<IncomeCaptionPoolRate>
        {
            public IncomeCaptionPoolRateValidator()
            {
                //RuleFor(obj => obj.OldMIS_Code).NotEmpty().WithMessage("OldMis_Code is required.");
                //RuleFor(obj => obj.NewMIS_Code).NotEmpty().WithMessage("NewMis_Code is required.");
            


            }
        }

        protected override IValidator GetValidator()
        {
            return new IncomeCaptionPoolRateValidator();
        }

    }
}
