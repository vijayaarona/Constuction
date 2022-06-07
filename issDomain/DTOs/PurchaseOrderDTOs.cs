using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace issDomain.DTOs
{
    class PurchaseOrderDTOs
    {
        public int ID { get; set; }
        public int RequestID { get; set; }
        public int OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }
        public int SupplierAddressId { get; set; }
        public int ProjectId { get; set; }
        public int SiteId { get; set; }
        public int SiteAddressId { get; set; }
        public string mobileno { get; set; }
        public decimal NetAmount { get; set; }
        public decimal grandTotal { get; set; }
        public decimal discountPercentage { get; set; }
        public decimal dicountAmount { get; set; }
        public string remarks { get; set; }
        public string requestBy { get; set; }
        public string orderby { get; set; }
        public int ProductNo { get; set; }
        public bool isDeleted { get; set; } = false;
        public DateTime? CreatedDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
