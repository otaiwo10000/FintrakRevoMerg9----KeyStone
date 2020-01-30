using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class OpexMemounits : ObjectBase
    {
        int _ID;
        string _MIS_CODE;
        string _MIS_DESCRIPTION;
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

        public string MIS_CODE
        {
            get { return _MIS_CODE; }
            set
            {
                if (_MIS_CODE != value)
                {
                    _MIS_CODE = value;
                    OnPropertyChanged(() => MIS_CODE);
                }
            }
        }

        public string MIS_DESCRIPTION
        {
            get { return _MIS_DESCRIPTION; }
            set
            {
                if (_MIS_DESCRIPTION != value)
                {
                    _MIS_DESCRIPTION = value;
                    OnPropertyChanged(() => MIS_DESCRIPTION);
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


        
        class OpexMemounitsValidator : AbstractValidator<OpexMemounits>
        {
            public OpexMemounitsValidator()
            {
                //RuleFor(obj => obj.ServiceCode).NotEmpty().WithMessage("ServiceCode is required.");
                //RuleFor(obj => obj.ServiceDescription).NotEmpty().WithMessage("ServiceDescription is required.");
             }
        }

        protected override IValidator GetValidator()
        {
            return new OpexMemounitsValidator();
        }
    }
}
