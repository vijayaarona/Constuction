using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace issDomain.DTOs
{
    class EmployeeMasterDTOs
    {
        public int ID { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string Emailid { get; set; }
        public string Mobilenumber { get; set; }
        public string Status { get; set; }
        public string SalaryType { get; set; }
        public string SalaryAmount { get; set; }
        public string City { get; set; }
        public int DesignationId { get; set; }
        public string DesignationMaster { get; set; }
        public int CategoryId { get; set; }
        public string CategoryMaster { get; set; }
        public DateTime? DateofJoin { get; set; }
        public DateTime? DateofRelive { get; set; }
        public bool isDeleted { get; set; } = false;
        public DateTime? CreatedDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
