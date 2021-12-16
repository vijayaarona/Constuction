using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace issDomain.DTOs
{
    class ProductMasterDTOs
    {
        public int ID { get; set; }
        public string ProductName { get; set; }
        public string UOM { get; set; }
        public int CategoryId { get; set; }
        public string  Category { get; set; }
        public decimal Tax { get; set; }
        public bool isDeleted { get; set; } = false;
        public DateTime? CreatedDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
