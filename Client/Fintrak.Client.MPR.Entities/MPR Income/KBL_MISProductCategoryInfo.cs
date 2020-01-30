
using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class KBL_MISProductCategoryInfo : ObjectBase
    {
       
        public int Id { get; set; }

       
        public string CATEGORY_CODE { get; set; }

        public string CATEGORY_DESCRIPTION { get; set; }

       
        public string SHORT_NAME { get; set; }


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
