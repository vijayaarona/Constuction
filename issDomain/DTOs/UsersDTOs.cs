using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace issDomain.DTOs
{
    class UsersDTOs
    {
        public string SelectUserType { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool isDeleted { get; set; } = false;
        public DateTime? CreatedDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int ID { get; set; }
    }
}
