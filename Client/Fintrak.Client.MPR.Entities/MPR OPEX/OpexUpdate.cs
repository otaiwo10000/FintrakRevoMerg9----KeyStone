using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class OpexUpdate : ObjectBase
    {
        int _ID;
        string _OLD;
        string _NEW;
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

        public string OLD
        {
            get { return _OLD; }
            set
            {
                if (_OLD != value)
                {
                    _OLD = value;
                    OnPropertyChanged(() => OLD);
                }
            }
        }

        public string NEW
        {
            get { return _NEW; }
            set
            {
                if (_NEW != value)
                {
                    _NEW = value;
                    OnPropertyChanged(() => NEW);
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


        
        class OpexUpdateValidator : AbstractValidator<OpexUpdate>
        {
            public OpexUpdateValidator()
            {
                //RuleFor(obj => obj.ServiceCode).NotEmpty().WithMessage("ServiceCode is required.");
                //RuleFor(obj => obj.ServiceDescription).NotEmpty().WithMessage("ServiceDescription is required.");
             }
        }

        protected override IValidator GetValidator()
        {
            return new OpexUpdateValidator();
        }
    }
}
