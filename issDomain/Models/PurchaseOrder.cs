﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace issDomain.Models
{
     public class PurchaseOrder
    {
        public int ID { get; set; }
        public int RequestID { get; set; }
        public virtual PurchaseRequest PurchaseRequest { get; set; }
        public int OrderId { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? OrderDate { get; set; }
        public int CategoryId { get; set; }
        public virtual CategoryMaster Category { get; set; }
        public int SupplierId { get; set; }
        public int SupplierAddressId { get; set; }
        public virtual SupplierMaster Supplier { get; set; }
        public int SiteNameId { get; set; }
        public int SiteId { get; set; }
        public int SiteAddressId { get; set; }
        public virtual SiteDetails SiteName { get; set; }
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

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? CreatedDate { get; set; }
        public string UpdateBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? UpdatedDate { get; set; }
        public decimal Tax { get; set; }
        public decimal TotTax { get; set; }
        public decimal TotAmount { get; set; }
        public int Sta { get; set; } = 0;
    }
}
