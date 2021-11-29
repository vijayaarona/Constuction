using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace issDomain.DTOs
{
    class ErrorlogDTOs
    {
        public int ID { get; set; }
        public DateTime Errordate { get; set; }
        public string controllername { get; set; }
        public string MethodNmae { get; set; }
        [Column(TypeName = "varchar(MAX)")]
        public string ErrorMessage { get; set; }
    }
}
