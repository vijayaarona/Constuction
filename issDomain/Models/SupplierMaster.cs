using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace issDomain.Models
{
    public class SupplierMaster
    {
        public int ID { get; set; }
        public string Suppliername { get; set; }
        public bool isDeleted { get; set; } = false;
        public string address { get; set; }
        public string Contactperson { get; set; }
        public string Phonenumber { get; set; }
        public string EmailID { get; set; }
        public string GSTnumber { get; set; }
        public string MSMEnumber { get; set; }
        public int OpeningBalance { get; set; }
        public string Bankname { get; set; }
        public string Accountnumber { get; set; }
        public string Branch { get; set; }
        public string IFSCcode { get; set; }
        public int CategoryId { get; set; }
        public virtual CategoryMaster Category { get; set; }
        public string City { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int SupplierId { get; set; }
    }
}
