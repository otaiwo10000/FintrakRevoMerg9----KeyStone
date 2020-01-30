using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class CollateralRealizationPeriod : ObjectBase
    {
        int _CollateralRealizationPeriodId;
        string _TypeCode;
        int _Duration;
        double _HairCut;
        double _AvgRecoveryCost;
        string _CompanyCode;
        bool _Active;

        public int CollateralRealizationPeriodId
        {
            get { return _CollateralRealizationPeriodId; }
            set
            {
                if (_CollateralRealizationPeriodId != value)
                {
                    _CollateralRealizationPeriodId = value;
                    OnPropertyChanged(() => CollateralRealizationPeriodId);
                }
            }
        }

        public string TypeCode
        {
            get { return _TypeCode; }
            set
            {
                if (_TypeCode != value)
                {
                    _TypeCode = value;
                    OnPropertyChanged(() => TypeCode);
                }
            }
        }


        public double AvgRecoveryCost
        {
            get { return _AvgRecoveryCost; }
            set
            {
                if (_AvgRecoveryCost != value)
                {
                    _AvgRecoveryCost = value;
                    OnPropertyChanged(() => AvgRecoveryCost);
                }
            }
        }



        public double HairCut
        {
            get { return _HairCut; }
            set
            {
                if (_HairCut != value)
                {
                    _HairCut = value;
                    OnPropertyChanged(() => HairCut);
                }
            }
        }

        public int Duration
        {
            get { return _Duration; }
            set
            {
                if (_Duration != value)
                {
                    _Duration = value;
                    OnPropertyChanged(() => Duration);
                }
            }
        }      

        public string CompanyCode
        {
            get { return _CompanyCode; }
            set
            {
                if (_CompanyCode != value)
                {
                    _CompanyCode = value;
                    OnPropertyChanged(() => CompanyCode);
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


        class CollateralRealizationPeriodValidator : AbstractValidator<CollateralRealizationPeriod>
        {
            public CollateralRealizationPeriodValidator()
            {
                RuleFor(obj => obj.TypeCode).NotEmpty().WithMessage("TypeCode is required.");
                RuleFor(obj => obj.Duration).GreaterThan(0).WithMessage("Duration is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new CollateralRealizationPeriodValidator();
        }
    }
}
