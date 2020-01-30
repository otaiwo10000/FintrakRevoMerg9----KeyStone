using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Client.MPR.Entities
{
    public class IncomeAccountPoolRate : ObjectBase
    {
        int _Id;
        string _AcccountNumber;
        string _Customer_Code;
        decimal _PoolRate;
        bool _Active;

        public int Id
        {
            get { return _Id; }
            set
            {
                if (_Id != value)
                {
                    _Id = value;
                    OnPropertyChanged(() => Id);
                }
            }
        }


        public string AcccountNumber
        {
            get { return _AcccountNumber; }
            set
            {
                if (_AcccountNumber != value)
                {
                    _AcccountNumber = value;
                    OnPropertyChanged(() => AcccountNumber);
                }
            }
        }

        public string Customer_Code
        {
            get { return _Customer_Code; }
            set
            {
                if (_Customer_Code != value)
                {
                    _Customer_Code = value;
                    OnPropertyChanged(() => Customer_Code);
                }
            }
        }

        public decimal PoolRate
        {
            get { return _PoolRate; }
            set
            {
                if (_PoolRate != value)
                {
                    _PoolRate = value;
                    OnPropertyChanged(() => PoolRate);
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


        class IncomeAccountPoolRateValidator : AbstractValidator<IncomeAccountPoolRate>
        {
            public IncomeAccountPoolRateValidator()
            {
                //RuleFor(obj => obj.OldMIS_Code).NotEmpty().WithMessage("OldMis_Code is required.");
                //RuleFor(obj => obj.NewMIS_Code).NotEmpty().WithMessage("NewMis_Code is required.");            
            }
        }

        protected override IValidator GetValidator()
        {
            return new IncomeAccountPoolRateValidator();
        }

    }
}
