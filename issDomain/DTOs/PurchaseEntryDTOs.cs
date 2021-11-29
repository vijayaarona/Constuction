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
        public string purchaseId { get; set; }
        public DateTime? purchaseDate { get; set; }
        public string Invoice { get; set; }
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }
        public int SupplierAddressId { get; set; }
        public int ProjectId { get; set; }
        public int SiteId { get; set; }
        public int SiteAddressId { get; set; }
        public string mobileno { get; set; }
        public bool isDeleted { get; set; } = false;
        public DateTime? CreatedDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
