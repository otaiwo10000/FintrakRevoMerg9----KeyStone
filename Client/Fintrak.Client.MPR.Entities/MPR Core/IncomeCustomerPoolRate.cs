using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Client.MPR.Entities
{
    public class IncomeCustomerPoolRate : ObjectBase
    {
        int _Id;
        string _CustomerNo;
        string _AccountClass;
        string _AccountClassName;
        decimal _PoolRate;
        int _Year;
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


        public string CustomerNo
        {
            get { return _CustomerNo; }
            set
            {
                if (_CustomerNo != value)
                {
                    _CustomerNo = value;
                    OnPropertyChanged(() => CustomerNo);
                }
            }
        }

        public string AccountClass
        {
            get { return _AccountClass; }
            set
            {
                if (_AccountClass != value)
                {
                    _AccountClass = value;
                    OnPropertyChanged(() => AccountClass);
                }
            }
        }

        public string AccountClassName
        {
            get { return _AccountClassName; }
            set
            {
                if (_AccountClassName != value)
                {
                    _AccountClassName = value;
                    OnPropertyChanged(() => AccountClassName);
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


        class IncomeCustomerPoolRateValidator : AbstractValidator<IncomeCustomerPoolRate>
        {
            public IncomeCustomerPoolRateValidator()
            {
                //RuleFor(obj => obj.OldMIS_Code).NotEmpty().WithMessage("OldMis_Code is required.");
                //RuleFor(obj => obj.NewMIS_Code).NotEmpty().WithMessage("NewMis_Code is required.");            
            }
        }

        protected override IValidator GetValidator()
        {
            return new IncomeCustomerPoolRateValidator();
        }

    }
}
