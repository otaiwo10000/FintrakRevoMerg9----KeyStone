using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class IncomeProductShare : ObjectBase
    {
        int _ID;
        string _Product;
        string _Originator;
        string _Branch;
        decimal _Ratio;
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

        public string Product
        {
            get { return _Product; }
            set
            {
                if (_Product != value)
                {
                    _Product = value;
                    OnPropertyChanged(() => Product);
                }
            }
        }

        public string Originator
        {
            get { return _Originator; }
            set
            {
                if (_Originator != value)
                {
                    _Originator = value;
                    OnPropertyChanged(() => Originator);
                }
            }
        }

        public string Branch
        {
            get { return _Branch; }
            set
            {
                if (_Branch != value)
                {
                    _Branch = value;
                    OnPropertyChanged(() => Branch);
                }
            }
        }

        public decimal Ratio
        {
            get { return _Ratio; }
            set
            {
                if (_Ratio != value)
                {
                    _Ratio = value;
                    OnPropertyChanged(() => Ratio);
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


        
        class IncomeProductShareValidator : AbstractValidator<IncomeProductShare>
        {
            public IncomeProductShareValidator()
            {
                //RuleFor(obj => obj.ServiceCode).NotEmpty().WithMessage("ServiceCode is required.");
                //RuleFor(obj => obj.ServiceDescription).NotEmpty().WithMessage("ServiceDescription is required.");
             }
        }

        protected override IValidator GetValidator()
        {
            return new IncomeProductShareValidator();
        }
    }
}
