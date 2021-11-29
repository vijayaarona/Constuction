using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace issDomain.Models
{
   public class AccountGroupMaster
    {
       public string GroupName { get; set; }
       public string ParentGroup { get; set; }
       public bool isDeleted { get; set; } = false;
       public int ID { get; set; }
       public DateTime? CreatedDate { get; set; }
       public string UpdateBy { get; set; }
       public DateTime? UpdatedDate { get; set; }
    }
}
