using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace issDomain.DTOs
{
    class paymentEntryDTOs
    {
        public int ID { get; set; }
        public int paymentID { get; set; }
        public DateTime? paymenttDate { get; set; }
        public int paymentTypeId { get; set; }
        public int accountLedgerNameId { get; set; }
        public int projectNameId { get; set; }
        public int siteNameId { get; set; }
        public string givenBy { get; set; }
        public string collectBy { get; set; }
        public decimal amount { get; set; }
        public string remarks { get; set; }
        public bool isDeleted { get; set; } = false;
        public DateTime? CreatedDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
