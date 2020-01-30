using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class IncomeSplitPoolsRatesAndBasis : ObjectBase
    {
        int _Id;
        decimal _PoolRateLCYAsset;
        decimal _PoolRateLCYLiability;
        decimal _PoolRateFCYAsset;
        decimal _PoolRateFCYLiability;
        int _Year;
        int _Period;

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

        public decimal PoolRateLCYAsset
        {
            get { return _PoolRateLCYAsset; }
            set
            {
                if (_PoolRateLCYAsset != value)
                {
                    _PoolRateLCYAsset = value;
                    OnPropertyChanged(() => PoolRateLCYAsset);
                }
            }
        }

        public decimal PoolRateLCYLiability
        {
            get { return _PoolRateLCYLiability; }
            set
            {
                if (_PoolRateLCYLiability != value)
                {
                    _PoolRateLCYLiability = value;
                    OnPropertyChanged(() => PoolRateLCYLiability);
                }
            }
        }

        public decimal PoolRateFCYAsset
        {
            get { return _PoolRateFCYAsset; }
            set
            {
                if (_PoolRateFCYAsset != value)
                {
                    _PoolRateFCYAsset = value;
                    OnPropertyChanged(() => PoolRateFCYAsset);
                }
            }
        }

        public decimal PoolRateFCYLiability
        {
            get { return _PoolRateFCYLiability; }
            set
            {
                if (_PoolRateFCYLiability != value)
                {
                    _PoolRateFCYLiability = value;
                    OnPropertyChanged(() => PoolRateFCYLiability);
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

        public int Period
        {
            get { return _Period; }
            set
            {
                if (_Period != value)
                {
                    _Period = value;
                    OnPropertyChanged(() => Period);
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



        //class IncomeSplitPoolsRatesAndBasisValidator : AbstractValidator<IncomeSplitPoolsRatesAndBasis>
        //{
        //    public IncomeSplitPoolsRatesAndBasisValidator()
        //    {
        //        RuleFor(obj => obj._PoolRateFCYAsset).NotEmpty().WithMessage("Pool rate FCY asset is required.");
        //    }
        //}

        //protected override IValidator GetValidator()
        //{
        //    return new IncomeSplitPoolsRatesAndBasisValidator();
        //}
    }
}
