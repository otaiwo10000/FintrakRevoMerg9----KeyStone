
using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class ScoreCardSetMetricTargetKBL : ObjectBase
    {
       int _ID { get; set; }
       string _SetMetric { get; set; }
       decimal _SetTarget { get; set; }
        decimal _FullYear { get; set; }
       bool _ProrateYTD { get; set; }
       int _Period { get; set; }
       int _Year { get; set; }



        //ModuleOwnerType _ModuleOwnerType;

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

        public string SetMetric
        {
            get { return _SetMetric; }
            set
            {
                if (_SetMetric != value)
                {
                    _SetMetric = value;
                    OnPropertyChanged(() => SetMetric);
                }
            }
        }

        public decimal SetTarget
        {
            get { return _SetTarget; }
            set
            {
                if (_SetTarget != value)
                {
                    _SetTarget = value;
                    OnPropertyChanged(() => SetTarget);
                }
            }
        }

        public decimal FullYear
        {
            get { return _FullYear; }
            set
            {
                if (_FullYear != value)
                {
                    _FullYear = value;
                    OnPropertyChanged(() => FullYear);
                }
            }
        }

        public bool ProrateYTD
        {
            get { return _ProrateYTD; }
            set
            {
                if (_ProrateYTD != value)
                {
                    _ProrateYTD = value;
                    OnPropertyChanged(() => ProrateYTD);
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



        //class ScoreCardMetricsValidator : AbstractValidator<ScoreCardMetrics>
        //{
        //    public ScoreCardMetricsValidator()
        //    {
        //        RuleFor(obj => obj.Metric).NotEmpty().WithMessage("Metric Name is required.");
        //        RuleFor(obj => obj.Metric_Code).NotEmpty().WithMessage("Metric Code is required.");             
        //        RuleFor(obj => obj.Period).NotEmpty().WithMessage("Period is required.");
        //        RuleFor(obj => obj.Year).NotEmpty().WithMessage("Year is required.");

        //    }
        //}

        //protected override IValidator GetValidator()
        //{
        //    return new ScoreCardMetricsValidator();
        //}
    }
}
