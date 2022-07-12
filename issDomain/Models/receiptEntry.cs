using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace issDomain.Models
{

    public class receiptEntry
    {
        public int ID { get; set; }
        public int receiptID { get; set; }
        public DateTime? receiptDate { get; set; }
        //public int accountGroupId { get; set; } 
        //public virtual AccountGroupMaster accountGroup { get; set; }
        public int accountLedgerId { get; set; }
        public virtual AccountLedgerMaster accountLedger { get; set; }
        public int siteDetailsId { get; set; }
        public virtual SiteDetails siteDetails { get; set; }
        public string givenBy { get; set; }
        public string collectBy { get; set; }
        public string approvedBy { get; set; }
        public string preparedBy { get; set; }
        public decimal amount { get; set; }
        public string remarks { get; set; }
        public bool isDeleted { get; set; } = false;
        public DateTime? CreatedDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
