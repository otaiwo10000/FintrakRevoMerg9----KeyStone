using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Client.MPR.Entities
{
    public class IncomePoolRateSbuYear : ObjectBase
    {
        int _ID;
        string _Levels;
        string _SBU;
        double _Rate;
        string _Category;
        string _Currency_Type;
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


        public string Levels
        {
            get { return _Levels; }
            set
            {
                if (_Levels != value)
                {
                    _Levels = value;
                    OnPropertyChanged(() => Levels);
                }
            }
        }

        public string SBU
        {
            get { return _SBU; }
            set
            {
                if (_SBU != value)
                {
                    _SBU = value;
                    OnPropertyChanged(() => SBU);
                }
            }
        }


        public double Rate
        {
            get { return _Rate; }
            set
            {
                if (_Rate != value)
                {
                    _Rate = value;
                    OnPropertyChanged(() => Rate);
                }
            }
        }

        public string Category
        {
            get { return _Category; }
            set
            {
                if (_Category != value)
                {
                    _Category = value;
                    OnPropertyChanged(() => Category);
                }
            }
        }

        public string Currency_Type
        {
            get { return _Currency_Type; }
            set
            {
                if (_Currency_Type != value)
                {
                    _Currency_Type = value;
                    OnPropertyChanged(() => Currency_Type);
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


        class IncomePoolRateSbuYearValidator : AbstractValidator<IncomePoolRateSbuYear>
        {
            public IncomePoolRateSbuYearValidator()
            {
                RuleFor(obj => obj.Levels).NotEmpty().WithMessage("Levels is required.");
                RuleFor(obj => obj.SBU).NotEmpty().WithMessage("SBU is required.");
                RuleFor(obj => obj.Rate).NotEmpty().WithMessage("Rate is required.");
                RuleFor(obj => obj.Category).NotEmpty().WithMessage("Category is required.");
                RuleFor(obj => obj.Currency_Type).NotEmpty().WithMessage("Currency_Type is required.");
                RuleFor(obj => obj.Period).NotEmpty().WithMessage("Period is required.");
                RuleFor(obj => obj.Year).NotEmpty().WithMessage("Year is required.");


            }
        }

        protected override IValidator GetValidator()
        {
            return new IncomePoolRateSbuYearValidator();
        }

    }
}
