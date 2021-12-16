using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace issDomain.DTOs
{
    class AccountLedgerMasterDTOs
    {
        public int ID { get; set; }
        public string AccountLedger { get; set; }
        public int AccountGroupID { get; set; }
        public int OpeningBal { get; set; }
        public string Type { get; set; }
        public bool isDeleted { get; set; } = false;
        public DateTime? CreatedDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
