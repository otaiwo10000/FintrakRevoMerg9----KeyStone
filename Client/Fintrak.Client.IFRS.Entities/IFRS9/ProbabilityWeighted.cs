using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class ProbabilityWeighted : ObjectBase
    {
        int _ProbabilityWeighted_Id;
        double _Optimistic;
        double _Best;
        double _Downturn;
        string _InstrumentType;
        bool _Active;

        public int ProbabilityWeighted_Id
        {
            get { return _ProbabilityWeighted_Id; }
            set
            {
                if (_ProbabilityWeighted_Id != value)
                {
                    _ProbabilityWeighted_Id = value;
                    OnPropertyChanged(() => ProbabilityWeighted_Id);
                }
            }
        }

        public double Optimistic
        {
            get { return _Optimistic; }
            set
            {
                if (_Optimistic != value)
                {
                    _Optimistic = value;
                    OnPropertyChanged(() => Optimistic);
                }
            }
        }


        public double Best
        {
            get { return _Best; }
            set
            {
                if (_Best != value)
                {
                    _Best = value;
                    OnPropertyChanged(() => Best);
                }
            }
        }


        public double Downturn
        {
            get { return _Downturn; }
            set
            {
                if (_Downturn != value)
                {
                    _Downturn = value;
                    OnPropertyChanged(() => Downturn);
                }
            }
        }


        public string InstrumentType
        {
            get { return _InstrumentType; }
            set
            {
                if (_InstrumentType != value)
                {
                    _InstrumentType = value;
                    OnPropertyChanged(() => InstrumentType);
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


        class ProbabilityWeightedValidator : AbstractValidator<ProbabilityWeighted>
        {
            public ProbabilityWeightedValidator()
            {
                RuleFor(obj => obj.InstrumentType).NotEmpty().WithMessage("Instrument Type is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new ProbabilityWeightedValidator();
        }
    }
}
