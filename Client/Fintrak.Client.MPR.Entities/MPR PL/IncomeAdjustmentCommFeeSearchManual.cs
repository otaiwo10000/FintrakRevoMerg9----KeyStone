using System;
using System.Linq;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.Core.Framework;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class IncomeAdjustmentCommFeeSearchManual : ObjectBase
    {
        public int ID { get; set; }

        public string MIS_Code { get; set; }
        public string BranchCode { get; set; }
        public string Inc_Exp { get; set; }

        public decimal Amount { get; set; }

        public string CurrencyType { get; set; }

        public string GL_Code { get; set; }

        public string RelatedAccount { get; set; }

        public string Narrative { get; set; }

        public int Period { get; set; }
        public int Year { get; set; }

        public string CustomerCode { get; set; }

        public string AccountOfficer_Code { get; set; }

        public string CustomerName { get; set; }

        public DateTime P_Date { get; set; }

        public string Caption { get; set; }

        public string Tran_ID { get; set; }

        public DateTime Tran_Date { get; set; }

        public string GLName { get; set; }

        public string EntryStatus { get; set; }
        public string GroupCaption { get; set; }

        public DateTime Entry_Date { get; set; }
        public string UserName_EntryMade { get; set; }

        public string sub_head_gl_code { get; set; }
        public string ProductCode { get; set; }


        public bool Active { get; set; }
       


        //class RevenueValidator : AbstractValidator<Revenue>
        //{
        //    public RevenueValidator()
        //    {
        //        RuleFor(obj => obj.TeamCode).NotEmpty().WithMessage("Team is required.");
        //    }
        //}

        //protected override IValidator GetValidator()
        //{
        //    return new RevenueValidator();
        //}
    }
}