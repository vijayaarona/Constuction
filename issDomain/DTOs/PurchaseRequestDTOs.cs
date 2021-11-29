﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace issDomain.DTOs
{
    public class PurchaseRequestDTOs
    {
        public int ID { get; set; }
        public string RequestID { get; set; }
        public DateTime? RequestDate { get; set; }
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
