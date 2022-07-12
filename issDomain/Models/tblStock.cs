using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace issDomain.Models
{
   public class tblStock
    {
        public int ID { get; set; }
        public int categoryId { get; set; }
        public int productId { get; set; }
        public decimal quantity { get; set; }
        public decimal rate { get; set; }
        public bool isDeleted { get; set; } = false;
        public DateTime? CreatedDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
