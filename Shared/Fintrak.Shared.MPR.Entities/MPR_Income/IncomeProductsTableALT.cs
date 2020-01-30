using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.MPR.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Fintrak.Shared.MPR.Entities
{
    public partial class IncomeProductsTableALT : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int ProductID { get; set; }


        [DataMember]
        [Required]
        public string ProductCode { get; set; }

        [DataMember]
        [Required]
        public string ProductName { get; set; }

        [DataMember]
        //[Required]
        public string Caption { get; set; }

        [DataMember]
       // [Required]
        public string Category { get; set; }

        [DataMember]
       // [Required]
        public string Category_Filter { get; set; }

        [DataMember]
        [Required]
        public string PPR_Status { get; set; }

        [DataMember]
        [Required]
        public string Currency { get; set; }

        [DataMember]
       // [Required]
        public string VolumeGL { get; set; }

        [DataMember]
        //[Required]
        public string RevGL { get; set; }

        [DataMember]
        // [Required] 
        public string ExpGL { get; set; }

        [DataMember]
        //[Required]
        public string GroupCode { get; set; }

        [DataMember]
        //[Required]
        public string SubGroupCode { get; set; }

        [DataMember]
        [Required]
        public bool APR_Status { get; set; }

        [DataMember]
        //[Required]
        public string ProductType { get; set; }

        [DataMember]
        //[Required]
        public int Position { get; set; }

        [DataMember]
        //[Required]
        public string ReplicationCaption { get; set; }

        [DataMember]
        //[Required]
        public string DeductionCaption { get; set; }

        [DataMember]
        //[Required]
        public string RetailCaption { get; set; }

        [DataMember]
        //[Required]
        public string Del_Flg { get; set; }

        [DataMember]
        //[Required]
        public string Product_SUPERCAPTION { get; set; }

        //[DataMember]
        //[Required]
        //public string PPR_Caption { get; set; }

        //[DataMember]
        ////[Required]
        //public string Cash_Reserve_Item { get; set; }

        [DataMember]
        // [Required] 
        public string Pool_Type { get; set; }

        //[DataMember]
        ////[Required]
        //public string PL_Caption { get; set; }

        //[DataMember]
        //// [Required] 
        //public string Cash_Vault_Item { get; set; }

        //[DataMember]
        ////[Required]
        //public string moduleownertype { get; set; }

        //[DataMember]
        ////[Required]
        //public string GroupCaption { get; set; }

        //[DataMember]
        ////[Required]
        //public string Liquidity_Reserve_Item { get; set; }

        //[DataMember]
        ////[Required]
        //public string GCapPosition { get; set; }

        //[DataMember]
        ////[Required]
        //public string SubTotal_item { get; set; }

        public int EntityId
        {
            get
            {
                return ProductID;
            }
        }
    }
}
