
using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Core.Framework;


namespace Fintrak.Client.MPR.Entities
{
    public class ScoreCard : ObjectBase
    {
        int _mpr_scorecard_stgId;      
        string _Caption;
        string _CaptionCode;
        string _Accountofficercode;
        string _TeamCode;
        string _Branchcode;
        decimal _Actual;
        decimal _AverageBal;
        decimal _Amount;
        decimal _Budget;
        string _Type;
        int _Period;
        int _Year;
        DateTime _Rundate;
      
        bool _Active;

        public int mpr_scorecard_stgId
        {
            get { return _mpr_scorecard_stgId; }
            set
            {
                if (_mpr_scorecard_stgId != value)
                {
                    _mpr_scorecard_stgId = value;
                     OnPropertyChanged(() => mpr_scorecard_stgId);
                }
            }
        }

        public string Caption
        {
            get { return _Caption; }
            set
            {
                if (_Caption != value)
                {
                    _Caption = value;
                    OnPropertyChanged(() => Caption);
                }
            }
        }

        public string CaptionCode
        {
            get { return _CaptionCode; }
            set
            {
                if (_CaptionCode != value)
                {
                    _CaptionCode = value;
                    OnPropertyChanged(() => CaptionCode);
                }
            }
        }

        public string Accountofficercode
        {
            get { return _Accountofficercode; }
            set
            {
                if (_Accountofficercode != value)
                {
                    _Accountofficercode = value;
                    OnPropertyChanged(() => Accountofficercode);
                }
            }
        }

        public string TeamCode
        {
            get { return _TeamCode; }
            set
            {
                if (_TeamCode != value)
                {
                    _TeamCode = value;
                    OnPropertyChanged(() => TeamCode);
                }
            }
        }

        public string Branchcode
        {
            get { return _Branchcode; }
            set
            {
                if (_Branchcode != value)
                {
                    _Branchcode = value;
                    OnPropertyChanged(() => Branchcode);
                }
            }
        }

        public decimal Actual
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

        public decimal AverageBal
        {
            get { return _AverageBal; }
            set
            {
                if (_AverageBal != value)
                {
                    _AverageBal = value;
                    OnPropertyChanged(() => AverageBal);
                }
            }
        }

        public decimal Amount
        {
            get { return _Amount; }
            set
            {
                if (_Amount != value)
                {
                    _Amount = value;
                    OnPropertyChanged(() => Amount);
                }
            }
        }

        public decimal Budget
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

        public string Type
        {
            get { return _Type; }
            set
            {
                if (_Type != value)
                {
                    _Type = value;
                    OnPropertyChanged(() => Type);
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

        public DateTime Rundate
        {
            get { return _Rundate; }
            set
            {
                if (_Rundate != value)
                {
                    _Rundate = value;
                    OnPropertyChanged(() => Rundate);
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



        //class ScoreCardWeightValidator : AbstractValidator<ScoreCardWeight>
        //{
        //    public ScoreCardWeightValidator()
        //    {
        //        RuleFor(obj => obj.Metric_Code).NotEmpty().WithMessage("Metric is required.");
        //        RuleFor(obj => obj.Weight).NotEmpty().WithMessage("Weight is required.");
        //        RuleFor(obj => obj.Period).NotEmpty().WithMessage("Period is required.");
        //        RuleFor(obj => obj.Year).NotEmpty().WithMessage("Year is required.");
               

        //    }
        //}

        //protected override IValidator GetValidator()
        //{
        //    return new ScoreCardWeightValidator();
        //}
    }
}
