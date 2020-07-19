using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TayDuKy.Models
{
    public class User
    {
        public String UserId { get; set; }
        public String FullName { get; set; }
        public String Password { get; set; }
        public String Email { get; set; }
        public String Phone { get; set; }
        public String ImageUrl { get; set; }
        public String Gender { get; set; }

        public String UpdatedBy { get; set; }
        public DateTime UpadtedDate { get; set; }

        public String Description { get; set; }
        public bool IsDelete { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public ICollection<Character> Characters { get; set; }
    }
}
