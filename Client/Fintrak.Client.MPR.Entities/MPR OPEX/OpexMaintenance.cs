using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class OpexMaintenance : ObjectBase
    {
        int _ID;
        string _CAPTION;
        string _PRODUCT;
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

        public string CAPTION
        {
            get { return _CAPTION; }
            set
            {
                if (_CAPTION != value)
                {
                    _CAPTION = value;
                    OnPropertyChanged(() => CAPTION);
                }
            }
        }

        public string PRODUCT
        {
            get { return _PRODUCT; }
            set
            {
                if (_PRODUCT != value)
                {
                    _PRODUCT = value;
                    OnPropertyChanged(() => PRODUCT);
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


        
        class OpexMaintenanceValidator : AbstractValidator<OpexMaintenance>
        {
            public OpexMaintenanceValidator()
            {
                //RuleFor(obj => obj.ServiceCode).NotEmpty().WithMessage("ServiceCode is required.");
                //RuleFor(obj => obj.ServiceDescription).NotEmpty().WithMessage("ServiceDescription is required.");
             }
        }

        protected override IValidator GetValidator()
        {
            return new OpexMaintenanceValidator();
        }
    }
}
