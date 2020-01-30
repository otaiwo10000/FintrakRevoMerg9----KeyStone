
using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class ScoreCardMISMappingKBL : ObjectBase
    {
        int _ID;

        string _MIS;
        string _KPI_TYPE;
        int _Period;
        int _Year;
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

        public string MIS
        {
            get { return _MIS; }
            set
            {
                if (_MIS != value)
                {
                    _MIS = value;
                    OnPropertyChanged(() => MIS);
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
