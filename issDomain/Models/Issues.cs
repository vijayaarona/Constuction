using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace issDomain.Models
{
    public class Issues
    {
        public int ID { get; set; }
        public int IssueID { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? IssuesDate { get; set; }
        public int SNameId { get; set; }
       // public virtual SiteDetails SName { get; set; }
        public int SiteId { get; set; }
        public int SiteNameId { get; set; }
        public int SiteAddressId { get; set; }
        public virtual SiteDetails SiteName { get; set; }
        public decimal netAmount { get; set; }
        public bool isDeleted { get; set; } = false;

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? CreatedDate { get; set; }
        public string UpdateBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? UpdatedDate { get; set; }
        public int GNameId { get; set; }

      //  public virtual Godown GName { get; set; }
        public int TypeId { get; set; }
        public  virtual TblType Type { get; set; }
        public int ProductNo { get; set; }

    }
}
