
using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class ScoreCardKPITypesKBL : ObjectBase
    {
        int _ID { get; set; }

       string _KPI_TYPE { get; set; }
        string _PERSPECTIVE { get; set; }

        string _KPI_METRIC { get; set; }
        decimal _KPI_WEIGHT { get; set; }
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

        public string KPI_TYPE
        {
            get { return _KPI_TYPE; }
            set
            {
                if (_KPI_TYPE != value)
                {
                    _KPI_TYPE = value;
                    OnPropertyChanged(() => KPI_TYPE);
                }
            }
        }

        public string PERSPECTIVE
        {
            get { return _PERSPECTIVE; }
            set
            {
                if (_PERSPECTIVE != value)
                {
                    _PERSPECTIVE = value;
                    OnPropertyChanged(() => PERSPECTIVE);
                }
            }
        }

        public string KPI_METRIC
        {
            get { return _KPI_METRIC; }
            set
            {
                if (_KPI_METRIC != value)
                {
                    _KPI_METRIC = value;
                    OnPropertyChanged(() => KPI_METRIC);
                }
            }
        }

        public decimal KPI_WEIGHT
        {
            get { return _KPI_WEIGHT; }
            set
            {
                if (_KPI_WEIGHT != value)
                {
                    _KPI_WEIGHT = value;
                    OnPropertyChanged(() => KPI_WEIGHT);
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
