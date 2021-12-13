using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace issDomain.Models
{
    public class PurchaseRequest
    {
        public int ID { get; set; }
        public string RequestID { get; set; }
        public DateTime? RequestDate { get; set; }
        public int CategoryId { get; set; }
        public virtual CategoryMaster Category { get; set; }
        public int SupplierId { get; set; }
        public int SupplierAddressId { get; set; }
        public virtual SupplierMaster Supplier { get; set; } 
        public int ProjectId { get; set; }
        public int SiteId { get; set; }
        public int SiteAddressId { get; set; }
        public virtual SiteDetails SiteDetails { get; set; }
        public string mobileno { get; set; }
        public decimal NetAmount { get; set; }
        public string RequestBy { get; set; }
        public string Remarks { get; set; }
        public bool isDeleted { get; set; } = false;
        public DateTime? CreatedDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
