using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Client.MPR.Entities
{
    public class DownloadBaseFintrakFinalManual : ObjectBase
    {    
        public int ID { get; set; }
        public string AccountNumber { get; set; }
        public string customername { get; set; }
        public string sbuCode { get; set; }
        public string MIS_Code { get; set; }
        public string accountofficercode { get; set; }
        public string accountofficer { get; set; }
        public decimal ActualBalance { get; set; }
        public decimal AverageBalance { get; set; }
        public decimal RevExp { get; set; }
        public decimal interestRate { get; set; }
        public string ProductCode { get; set; }
        public string Category { get; set; }
        public string Currency_Type { get; set; }
        public DateTime postedDate { get; set; }
        public int Period { get; set; }
        public int Year { get; set; }
        public string EntryStatus { get; set; }
        public string GL_Sub { get; set; }
        public string Refno { get; set; }
        public decimal PoolRate { get; set; }
        public decimal BankMaxRate { get; set; }
        public string CustomerRating { get; set; }
        public decimal EffYield { get; set; }
        public decimal PenalRate { get; set; }
        public decimal PenalCharge { get; set; }
        public decimal ExpRev { get; set; }
        public string Caption { get; set; }
        public string Category_Filter { get; set; }
        public string Branch { get; set; }
        public decimal Share_Ratio { get; set; }
        public string Indicator { get; set; }
        public DateTime Entry_Date { get; set; }
        public string Currency_Code { get; set; }


        class DownloadBaseFintrakFinalManualValidator : AbstractValidator<DownloadBaseFintrakFinalManual>
        {
            public DownloadBaseFintrakFinalManualValidator()
            {
                //RuleFor(obj => obj.Percentage).NotEmpty().WithMessage("Percentage is required.");
                //RuleFor(obj => obj.Branch).NotEmpty().WithMessage("Branch is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new DownloadBaseFintrakFinalManualValidator();
        }

    }
}
