
using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Core.Framework;


namespace Fintrak.Client.MPR.Entities
{
    public class ScoreCardWeight : ObjectBase
    {
        int _WeightId;
       
        int _Metric_Code;
        string _Metric;
        decimal _Weight;
        int _Period;
        int _Year;
        bool _Active;


        //ModuleOwnerType _ModuleOwnerType;

        public int WeightId
        {
            get { return _WeightId; }
            set
            {
                if (_WeightId != value)
                {
                    _WeightId = value;
                     OnPropertyChanged(() => WeightId);
                }
            }
        }

        public int Metric_Code
        {
            get { return _Metric_Code; }
            set
            {
                if (_Metric_Code != value)
                {
                    _Metric_Code = value;
                    OnPropertyChanged(() => Metric_Code);
                }
            }
        }

        public string Metric
        {
            get { return _Metric; }
            set
            {
                if (_Metric != value)
                {
                    _Metric = value;
                    OnPropertyChanged(() => Metric);
                }
            }
        }

        public decimal Weight
        {
            get { return _Weight; }
            set
            {
                if (_Weight != value)
                {
                    _Weight = value;
                    OnPropertyChanged(() => Weight);
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



        class ScoreCardWeightValidator : AbstractValidator<ScoreCardWeight>
        {
            public ScoreCardWeightValidator()
            {
                RuleFor(obj => obj.Metric_Code).NotEmpty().WithMessage("Metric is required.");
                RuleFor(obj => obj.Weight).NotEmpty().WithMessage("Weight is required.");
                RuleFor(obj => obj.Period).NotEmpty().WithMessage("Period is required.");
                RuleFor(obj => obj.Year).NotEmpty().WithMessage("Year is required.");
               

            }
        }

        protected override IValidator GetValidator()
        {
            return new ScoreCardWeightValidator();
        }
    }
}
