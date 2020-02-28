using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChildrenHelper.DataBase
{
    public class User
    {
        [Key]
        public int Id { get; set; }
       
        public string Username { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string SecName { get; set; }

        public string Password { get; set; }
        public string Email { get; set; }
        public int? RoleId { get; set; }
        public Role Role { get; set; }
    }
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }
        public Role()
        {
            Users = new List<User>();
        }
    }


}

