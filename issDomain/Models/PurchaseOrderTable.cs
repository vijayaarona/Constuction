using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace issDomain.Models
{
    public class PurchaseOrderTable
    {
        public int ID { get; set; }
        public int purchaseRequestId { get; set; }
        public int productId { get; set; }
        public virtual ProductMaster Product { get; set; }
        public string Description { get; set; }
        public decimal Tax { get; set; }
        public decimal Rate { get; set; }
        public decimal Quantity { get; set; }
        public decimal Amount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal discountPercent { get; set; }
        public decimal discountAmount { get; set; }
        public bool isDeleted { get; set; } = false;
        public DateTime? CreatedDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}