using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Client.MPR.Entities
{
    public class IncomeSplitPoolsRate : ObjectBase
    {
        int _ID;
        decimal _PoolRateLCYAsset;
        decimal _PoolRateLCYLiability;
        decimal _PoolRateFCYAsset;
        decimal _PoolRateFCYLiability;
        int _Period;
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


        class IncomeSplitPoolsRateValidator : AbstractValidator<IncomeSplitPoolsRate>
        {
            public IncomeSplitPoolsRateValidator()
            {
               // RuleFor(obj => obj.OldMIS_Code).NotEmpty().WithMessage("OldMis_Code is required.");
              //  RuleFor(obj => obj.NewMIS_Code).NotEmpty().WithMessage("NewMis_Code is required.");
            


            }
        }

        protected override IValidator GetValidator()
        {
            return new IncomeSplitPoolsRateValidator();
        }

    }
}
