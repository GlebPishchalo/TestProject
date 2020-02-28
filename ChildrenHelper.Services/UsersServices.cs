using System.Collections.Generic;
using System.Security.Claims;
using ChildrenHelper.DataBase;
using ChildrenHelper.Services.Contracts.Interfaces;
using ChildrenHelper.Services.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;


namespace ChildrenHelper.Services
{
    public class UsersServices : IUserService
    {
        public DataBaseContext db;

        public UsersServices(DataBaseContext db)
        {
            this.db = db;
        }

        //public async Task<string> Login(LoginUserModel loginUserModel)
        //{
        //    if (loginUserModel!=null) {

        //        var result = userContext.Users.FirstOrDefault(login => login.Username == loginUserModel.Username && login.Password == loginUserModel.Password);
        //        return "Successful";
        //            }
        //    else
        //    return "Fail";
        //}

        public async Task<bool> Register(RegisterUserModel registerUserModel)
        {
            // var salt = Helper.GenerateSalt();

            var user = await db.Users.FirstOrDefaultAsync(u => u.Email == registerUserModel.Email);
            //RegisterUserModel user = new RegisterUserModel { Email = registerUserModel.Email, Username = registerUserModel.Username, Password = registerUserModel.Password, Name = registerUserModel.Name, Surname = registerUserModel.Surname, SecName = registerUserModel.SecName };
            if (user == null)
            {
                user = new User { Email = registerUserModel.Email, Username = registerUserModel.Username, Password = Hash(registerUserModel.Password), Name = registerUserModel.Name, Surname = registerUserModel.Surname, SecName = registerUserModel.SecName };
                ChildrenHelper.DataBase.Role userRole = await db.Roles.FirstOrDefaultAsync(r => r.Name == "user");
                if (userRole != null)
                {
                    user.Role = userRole;
                    db.Users.Add(user);
                    await db.SaveChangesAsync();
                    return true;
                }
                else
                    return false;
            }
            else
                return false;

        }

        public async Task<bool> Login(LoginUserModel loginUserModel)
        {
            User user = await db.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(
                    u => u.Email == loginUserModel.Email && u.Password == Hash(loginUserModel.Password));

            

            if (user != null)
            {


                return true;
            }
            else return false;
        }

        static string Hash(string input)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));

                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    // can be "x2" if you want lowercase
                    sb.Append(b.ToString("X2"));
                }
                
                return sb.ToString();
            }
        }
       





    }


}

