using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace issDomain.Models
{
    public class paymentEntry
    {
        public int ID { get; set; }
        public int paymentID { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? paymenttDate { get; set; }
        public int LedgerId { get; set; }
        public virtual AccountLedgerMaster Ledger { get; set; }
        public int accountLedgerId { get; set; }
        public virtual AccountLedgerMaster accountLedger { get; set; }
        public int siteNameId { get; set; }
        public int siteId { get; set; }
        public virtual SiteDetails siteName { get; set; }
        public string givenBy { get; set; }
        public string collectBy { get; set; }
        public string approvedBy { get; set; }
        public string preparedBy { get; set; }
        public decimal amount { get; set; }
        public string remarks { get; set; }
        public bool isDeleted { get; set; } = false;

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? CreatedDate { get; set; }
        public string UpdateBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? UpdatedDate { get; set; }
    }
}
