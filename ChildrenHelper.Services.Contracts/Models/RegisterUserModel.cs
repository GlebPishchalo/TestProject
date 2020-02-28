using ChildrenHelper.DataBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ChildrenHelper.Services.Contracts.Models
{
    public class RegisterUserModel
    {
          public int? Id { get; set; }
        [EmailAddress(ErrorMessage = "Неверный Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }
        [Required]
        [Display(Name = "Surname")]
        public string Surname { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "SecName")]
        public string SecName { get; set; }
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public int? RoleId { get; set; }
        public Role Role { get; set; }
    }
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



