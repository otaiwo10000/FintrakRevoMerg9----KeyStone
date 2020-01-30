
using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class ScoreCardMetricsKBL : ObjectBase
    {
        
        int _MetricID { get; set; }

        string _Metric_Description { get; set; }

        string _Metric { get; set; }
        string _Actual { get; set; }

        string _Budget { get; set; }

        bool _TargetIsPreviousYear { get; set; }

        bool _TargetOverActual { get; set; }
        decimal _Divisior { get; set; }

       int _Position { get; set; }
       int _Year { get; set; }

       bool _SetToZeroIfNoBudget { get; set; }
        string _YTDAction { get; set; }
        bool _SetToZeroIfNegativeActual { get; set; }




        //ModuleOwnerType _ModuleOwnerType;

        public int MetricID
        {
            get { return _MetricID; }
            set
            {
                if (_MetricID != value)
                {
                    _MetricID = value;
                     OnPropertyChanged(() => MetricID);
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

        public string Actual
        {
            get { return _Actual; }
            set
            {
                if (_Actual != value)
                {
                    _Actual = value;
                    OnPropertyChanged(() => Actual);
                }
            }
        }

        public string Budget
        {
            get { return _Budget; }
            set
            {
                if (_Budget != value)
                {
                    _Budget = value;
                    OnPropertyChanged(() => Budget);
                }
            }
        }

        public bool TargetIsPreviousYear
        {
            get { return _TargetIsPreviousYear; }
            set
            {
                if (_TargetIsPreviousYear != value)
                {
                    _TargetIsPreviousYear = value;
                    OnPropertyChanged(() => TargetIsPreviousYear);
                }
            }
        }

        public bool TargetOverActual
        {
            get { return _TargetOverActual; }
            set
            {
                if (_TargetOverActual != value)
                {
                    _TargetOverActual = value;
                    OnPropertyChanged(() => TargetOverActual);
                }
            }
        }

        public decimal Divisior
        {
            get { return _Divisior; }
            set
            {
                if (_Divisior != value)
                {
                    _Divisior = value;
                    OnPropertyChanged(() => Divisior);
                }
            }
        }

        public int Position
        {
            get { return _Position; }
            set
            {
                if (_Position != value)
                {
                    _Position = value;
                    OnPropertyChanged(() => Position);
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

        public bool SetToZeroIfNoBudget
        {
            get { return _SetToZeroIfNoBudget; }
            set
            {
                if (_SetToZeroIfNoBudget != value)
                {
                    _SetToZeroIfNoBudget = value;
                    OnPropertyChanged(() => SetToZeroIfNoBudget);
                }
            }
        }

        public string YTDAction
        {
            get { return _YTDAction; }
            set
            {
                if (_YTDAction != value)
                {
                    _YTDAction = value;
                    OnPropertyChanged(() => YTDAction);
                }
            }
        }

    
        public bool SetToZeroIfNegativeActual
        {
            get { return _SetToZeroIfNegativeActual; }
            set
            {
                if (_SetToZeroIfNegativeActual != value)
                {
                    _SetToZeroIfNegativeActual = value;
                    OnPropertyChanged(() => SetToZeroIfNegativeActual);
                }
            }
        }

        //class TeamStructureValidator : AbstractValidator<TeamStructure>
        //{
        //    public TeamStructureValidator()
        //    {
        //        RuleFor(obj => obj.Accountofficer_Code).NotEmpty().WithMessage("Accountofficer code is required.");
        //        RuleFor(obj => obj.Team_Code).NotEmpty().WithMessage("Team Code is required.");
        //        RuleFor(obj => obj.Branch_Code).NotEmpty().WithMessage("Branch code is required.");
        //        RuleFor(obj => obj.Group_Code).NotEmpty().WithMessage("Group Code is required.");
        //        RuleFor(obj => obj.Region_Code).NotEmpty().WithMessage("Accountofficer code is required.");
        //        RuleFor(obj => obj.Division_Code).NotEmpty().WithMessage("Team Code is required.");
        //        RuleFor(obj => obj.DIRECTORATECODE).NotEmpty().WithMessage("Accountofficer code is required.");
        //        RuleFor(obj => obj.unit_code).NotEmpty().WithMessage("Team Code is required.");
        //        RuleFor(obj => obj.PPRCategory).NotEmpty().WithMessage("Accountofficer code is required.");
        //        RuleFor(obj => obj.staff_id).NotEmpty().WithMessage("Team Code is required.");
        //        RuleFor(obj => obj.Period).NotEmpty().WithMessage("Accountofficer code is required.");
        //        RuleFor(obj => obj.Year).NotEmpty().WithMessage("Team Code is required.");

        //    }
        //}

        //protected override IValidator GetValidator()
        //{
        //    return new TeamStructureValidator();
        //}
    }
}
