using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace issDomain.Models
{
   public class EmployeeMaster
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
        public virtual DesignationMaster Designation { get; set; }
        public int CategoryId { get; set; }
        public virtual CategoryMaster Category { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DateofJoin { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DateofRelive { get; set; }
        public bool isDeleted { get; set; } = false;
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? CreatedDate { get; set; }
        public string UpdateBy { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? UpdatedDate { get; set; }
      
          }
}
