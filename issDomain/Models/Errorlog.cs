using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace issDomain.Models
{
    public class Errorlog
    {
        public int ID { get; set; }
        public DateTime Errordate { get; set; }
        public string controllername { get; set; }
        public string MethodNmae { get; set; }
        [Column(TypeName = "varchar(MAX)")]
        public string ErrorMessage { get; set; }
    }

   
}
