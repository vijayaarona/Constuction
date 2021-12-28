using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace issDomain.Models
{
   public class masterTbl
    {
        public int ID { get; set; }
        public DateTime? entryDate { get; set; }
        public string payType { get; set; }
        public string AccountID { get; set; }
        public string GroupID { get; set; }
        public string description { get; set; }
        public decimal expense { get; set; }
        public decimal income { get; set; }
        public string underGroup { get; set; }
        public string type { get; set; }
        public string financialYear { get; set; }
        public string projectName { get; set; }
        public string siteName { get; set; }
        public bool isDeleted { get; set; } = false;
        public DateTime? CreatedDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
