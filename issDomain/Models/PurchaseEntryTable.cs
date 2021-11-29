using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace issDomain.Models
{
    public class PurchaseEntryTable
    {
        public int ID { get; set; }
        public int purchaseRequestId { get; set; }
        public int productId { get; set; }
        public virtual ProductMaster Product { get; set; }
        public string Description { get; set; }
        public string Rate { get; set; }
        public string Quantity { get; set; }
        public string Tax { get; set; }
        public string Amount { get; set; }
        public string TotalAmount { get; set; }
        public string ReceivedBy { get; set; }
        public string ReffBillNo { get; set; }
        public string DeliveryNo { get; set; }
        public string Remarks { get; set; }
        public string RequestBy { get; set; }
        public string Discount { get; set; }
        public string TotalTax { get; set; }
        public string freightCharges { get; set; }
        public string NetAmount { get; set; }
        public bool isDeleted { get; set; } = false;
        public DateTime? CreatedDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
