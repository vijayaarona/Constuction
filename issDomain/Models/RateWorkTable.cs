using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace issDomain.Models
{
   public class RateWorkTable
    {
        public int Id  { get; set; }
        public int rateId { get; set; }
        public string Description { get; set; }
        public decimal length { get; set; }
        public decimal Breath { get; set; }
        public decimal Deapth { get; set; }
        public int Nos { get; set; }
        public decimal quantity { get; set; }
        public decimal unitPrice { get; set; }
        public decimal amount { get; set; }
        public virtual ProductMaster ProductMaster { get; set; }
        public int umoId { get; set; }
        public bool isDeleted { get; set; } = false;
        public DateTime? CreatedDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
