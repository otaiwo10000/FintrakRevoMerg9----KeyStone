
using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class ProductTransferPrice : ObjectBase
    {
        int _ID;

        string _ProductCode;
        string _Rating;
        string _Description;
        public Fintrak.Shared.MPR.Framework.BalanceSheetCategory _Category { get; set; }

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

        public string ProductCode
        {
            get { return _ProductCode; }
            set
            {
                if (_ProductCode != value)
                {
                    _ProductCode = value;
                    OnPropertyChanged(() => ProductCode);
                }
            }
        }

        public string Rating
        {
            get { return _Rating; }
            set
            {
                if (_Rating != value)
                {
                    _Rating = value;
                    OnPropertyChanged(() => Rating);
                }
            }
        }

        public string Description
        {
            get { return _Description; }
            set
            {
                if (_Description != value)
                {
                    _Description = value;
                    OnPropertyChanged(() => Description);
                }
            }
        }

        public Fintrak.Shared.MPR.Framework.BalanceSheetCategory Category
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
