
using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class ScoreCardMetrics : ObjectBase
    {
        int _MetricId;
       
        string _Metric;       
        int _Metric_Code;      
        string _Metric_Description;
        string _MisCode;
        int _Metric_Score_determinant;
        int _Period;
        int _Year;
        int _PerspectiveId;
        int _Metric_Position;
        int _Mapping_Code;       

        //ModuleOwnerType _ModuleOwnerType;

        public int MetricId
        {
            get { return _MetricId; }
            set
            {
                if (_MetricId != value)
                {
                    _MetricId = value;
                     OnPropertyChanged(() => MetricId);
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

        public string Metric_Description
        {
            get { return _Metric_Description; }
            set
            {
                if (_Metric_Description != value)
                {
                    _Metric_Description = value;
                    OnPropertyChanged(() => Metric_Description);
                }
            }
        }

        public string MisCode
        {
            get { return _MisCode; }
            set
            {
                if (_MisCode != value)
                {
                    _MisCode = value;
                    OnPropertyChanged(() => MisCode);
                }
            }
        }

        public int Metric_Score_determinant
        {
            get { return _Metric_Score_determinant; }
            set
            {
                if (_Metric_Score_determinant != value)
                {
                    _Metric_Score_determinant = value;
                    OnPropertyChanged(() => Metric_Score_determinant);
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

        public int PerspectiveId
        {
            get { return _PerspectiveId; }
            set
            {
                if (_PerspectiveId != value)
                {
                    _PerspectiveId = value;
                    OnPropertyChanged(() => PerspectiveId);
                }
            }
        }

        public int Metric_Position
        {
            get { return _Metric_Position; }
            set
            {
                if (_Metric_Position != value)
                {
                    _Metric_Position = value;
                    OnPropertyChanged(() => Metric_Position);
                }
            }
        }

        public int Mapping_Code
        {
            get { return _Mapping_Code; }
            set
            {
                if (_Mapping_Code != value)
                {
                    _Mapping_Code = value;
                    OnPropertyChanged(() => Mapping_Code);
                }
            }
        }



        class ScoreCardMetricsValidator : AbstractValidator<ScoreCardMetrics>
        {
            public ScoreCardMetricsValidator()
            {
                RuleFor(obj => obj.Metric).NotEmpty().WithMessage("Metric Name is required.");
                RuleFor(obj => obj.Metric_Code).NotEmpty().WithMessage("Metric Code is required.");             
                RuleFor(obj => obj.Period).NotEmpty().WithMessage("Period is required.");
                RuleFor(obj => obj.Year).NotEmpty().WithMessage("Year is required.");

            }
        }

        protected override IValidator GetValidator()
        {
            return new ScoreCardMetricsValidator();
        }
    }
}
