using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace issDomain.DTOs
{
    class PurchaseEntryDTOs
    {
        public int ID { get; set; }
        public int OrderId { get; set; }
        public string Invoice { get; set; }
        public int purchaseId { get; set; }
        public DateTime? purchaseDate { get; set; }
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }
        public int SupplierAddressId { get; set; }
        public int ProjectId { get; set; }
        public int SiteId { get; set; }
        public int SiteAddressId { get; set; }
        public string mobileno { get; set; }
        public string ReceivedBy { get; set; }
        public string Remarks { get; set; }
        public string ReffBillNo { get; set; }
        public string DeliveryNo { get; set; }
        public decimal totalDiscount { get; set; }
        public decimal totalTax { get; set; }
        public decimal freightCharges { get; set; }
        public decimal NetAmount { get; set; }
        public decimal grandTotal { get; set; }
        public decimal discountPercentage { get; set; }
        public int ProductNo { get; set; }
        public bool isDeleted { get; set; } = false;
        public DateTime? CreatedDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
