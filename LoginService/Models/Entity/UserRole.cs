using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoginService.Models.Entity
{
    public class UserRole
    {
        [Key]
        public int RoleId { get; set; }
        public string Role { get; set; }
        public ICollection<UserDetail> UserDetails { get; set; }
    }
}
