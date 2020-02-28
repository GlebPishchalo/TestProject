using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ChildrenHelper.Services.Contracts.Models
{
    public class LoginUserModel
    {
        [Required(ErrorMessage = "Neverniy email")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public int? RoleId { get; set; }
        public Role Role { get; set; }
    }
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<LoginUserModel> Users { get; set; }
        public Role()
        {
            Users = new List<LoginUserModel>();
        }
    }
}
