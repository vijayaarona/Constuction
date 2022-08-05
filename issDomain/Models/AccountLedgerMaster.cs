﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace issDomain.Models
{
   public class AccountLedgerMaster
    {
        public int ID { get; set; }
        public string AccountLedger { get; set; }
        public int OpeningBal { get; set; }
        public int AccountGroupID { get; set; }
        public virtual AccountGroupMaster AccountGroup { get; set; }
        public string Type { get; set; }
        public bool isDeleted { get; set; } = false;

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? CreatedDate { get; set; }
        public string UpdateBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? UpdatedDate { get; set; }
    }
}
