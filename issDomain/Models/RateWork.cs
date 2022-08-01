using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace issDomain.Models
{
    public class RateWork
    {
        public int Id { get; set; }

        public int RateWorkId { get; set; }
        public DateTime? Date { get; set; }
        public int SiteNameId { get; set; }
        public int siteNoId { get; set; }
        public int siteId { get; set; }
        public int siteAddressId { get; set; }
        public virtual SiteDetails SiteName { get; set; }
        public string headName { get; set; }
        public decimal totalAmount { get; set; }
        public decimal deduction { get; set; }
        public decimal netAmount { get; set; }
        public string passedBy { get; set; }
        public string remark { get; set; }
        public bool isDeleted { get; set; } = false;
        public DateTime? CreatedDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
