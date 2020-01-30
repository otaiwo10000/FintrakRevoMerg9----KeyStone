using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Client.MPR.Entities
{
    public class IncomeOtherBreakdown : ObjectBase
    {
        public int ID { get; set; }

        public string MIS_Code { get; set; }

        public string Caption { get; set; }

        public string Accountnumber { get; set; }

        public string Narrative { get; set; }

        public string CustomerName { get; set; }

        public int Period { get; set; }

        public int Year { get; set; }

        public decimal Amount { get; set; }

        public string AccountOfficer_Code { get; set; }

        public decimal Volume { get; set; }

        public string Indicator { get; set; }

        public string EntryStatus { get; set; }
        public DateTime DateEntered { get; set; }

        public string ProductCode { get; set; }

        public DateTime RunDate { get; set; }


        class IncomeOtherBreakdownValidator : AbstractValidator<IncomeOtherBreakdown>
        {
            public IncomeOtherBreakdownValidator()
            {
                //RuleFor(obj => obj.Percentage).NotEmpty().WithMessage("Percentage is required.");
                //RuleFor(obj => obj.Branch).NotEmpty().WithMessage("Branch is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new IncomeOtherBreakdownValidator();
        }

    }
}
