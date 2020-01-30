
using System;
using System.Linq;
using System.Xml.Serialization;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class IncomeProductsTable : ObjectBase
    {

        public int ProductID { get; set; }

        public string ProductCode { get; set; }

        public string ProductName { get; set; }

        public string Caption { get; set; }

        public string Category { get; set; }

        public string Category_Filter { get; set; }

        public string PPR_Status { get; set; }

        public string Currency { get; set; }

        public string VolumeGL { get; set; }

        public string RevGL { get; set; }

        public string ExpGL { get; set; }

        public dynamic APR_Status { get; set; }

        public string PPR_Caption { get; set; }

        public int Position { get; set; }

        public string Cash_Reserve_Item { get; set; }

        public string Pool_Type { get; set; }

        public string PL_Caption { get; set; }

        public string Cash_Vault_Item { get; set; }

        public string moduleownertype { get; set; }

        public string GroupCaption { get; set; }

        public string Liquidity_Reserve_Item { get; set; }

        public string GCapPosition { get; set; }

        public string SubTotal_item { get; set; }

        public string GroupCode { get; set; }

        public string SubGroupCode { get; set; }

        public string ProductType { get; set; }

        public string ReplicationCaption { get; set; }

        public string DeductionCaption { get; set; }

        public string RetailCaption { get; set; }

        public string Del_Flg { get; set; }

        public string Product_SUPERCAPTION { get; set; }




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
