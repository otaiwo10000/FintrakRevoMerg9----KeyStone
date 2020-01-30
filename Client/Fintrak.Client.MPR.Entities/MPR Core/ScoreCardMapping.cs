
using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class ScoreCardMapping : ObjectBase
    {
        int _MappingId;     
        int _Metric_Code;      
        string _Actual_Caption;
        string _Budget_Caption;
        int _Period;
        int _Year;
        int _Mapping_code;
          

        //ModuleOwnerType _ModuleOwnerType;

        public int MappingId
        {
            get { return _MappingId; }
            set
            {
                if (_MappingId != value)
                {
                    _MappingId = value;
                     OnPropertyChanged(() => MappingId);
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

        public string Actual_Caption
        {
            get { return _Actual_Caption; }
            set
            {
                if (_Actual_Caption != value)
                {
                    _Actual_Caption = value;
                    OnPropertyChanged(() => Actual_Caption);
                }
            }
        }

        public string Budget_Caption
        {
            get { return _Budget_Caption; }
            set
            {
                if (_Budget_Caption != value)
                {
                    _Budget_Caption = value;
                    OnPropertyChanged(() => Budget_Caption);
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

        public int Mapping_code
        {
            get { return _Mapping_code; }
            set
            {
                if (_Mapping_code != value)
                {
                    _Mapping_code = value;
                    OnPropertyChanged(() => Mapping_code);
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
