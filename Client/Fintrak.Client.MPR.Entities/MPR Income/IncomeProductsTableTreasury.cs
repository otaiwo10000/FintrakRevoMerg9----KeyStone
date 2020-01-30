
using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class IncomeProductsTableTreasury : ObjectBase
    {
        public int ID { get; set; }

        public string GLCode { get; set; }

        public string Caption { get; set; }

        public string Type { get; set; }

        public string Status { get; set; }

        public string Currency { get; set; }

        public string SBUCode { get; set; }

        public string Category { get; set; }


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
