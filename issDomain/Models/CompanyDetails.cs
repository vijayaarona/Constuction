using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace issDomain.Models
{
    public class CompanyDetails
    {
        public int ID { get; set; }
        public string NameoftheCompany { get; set; }
        public string PrintName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string EmailID { get; set; }
        public string PhoneNumber { get; set; }
        public string GSTNumber { get; set; }
        public bool isDeleted { get; set; } = false;
        public DateTime? CreatedDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
