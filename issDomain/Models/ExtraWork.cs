using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace issDomain.Models
{
    public class ExtraWork
    {
        public int Id { get; set; }
        public DateTime? date { get; set; }
        public virtual SiteDetails SiteDetails { get; set; }
        public int projectId { get; set; }
        public int siteNo { get; set; }
        public int siteName { get; set; }
        public int siteAddress { get; set; }
        public string headName { get; set; }
        public decimal totalAmt { get; set; }
        public string passBy { get; set; }
        public string remarks { get; set; }
        public bool isDeleted { get; set; } = false;
        public DateTime? CreatedDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
