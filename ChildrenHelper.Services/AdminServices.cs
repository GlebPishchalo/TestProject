using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ChildrenHelper.DataBase;
using ChildrenHelper.Models;
using ChildrenHelper.Services.Contracts.Interfaces;
using ChildrenHelper.Services.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Diagnoz = ChildrenHelper.Models.Diagnoz;

namespace ChildrenHelper.Services
{
    public class AdminServices : IAdminServices

    {
        public DataBaseContext db;
       
        public AdminServices(DataBaseContext db)
        {
            this.db = db;
        }
        
        public IEnumerable<Children> Childrens()
        {
           return db.Childrens.Include(u => u.Diagnoz);
           

        }
        public IEnumerable<User> Users()
        {
            return db.Users.Include(u => u.Role);
        }
        public IEnumerable<Diagnoz> Diagnozs()
        {
            return db.Diagnozes;
        }

        public async Task<bool> Create(ChildrenModel childrenModel)
        {

            var children = await db.Childrens.FirstOrDefaultAsync(u => u.Chsurname == childrenModel.Chsurname);
            if (children == null)
            {
                db.Childrens.Add(new Children
                {
                    Chname = childrenModel.Chname,
                    Chsurname = childrenModel.Chsurname,
                    Chsecname = childrenModel.Chsecname,
                    DiagnozID = childrenModel.DiagnozID,
                    Chdesc = childrenModel.Chdesc,
                    Summa = childrenModel.Summa
                });
                await db.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }

        }

        public async Task<bool> Edit(Children childrenModel)
        {
            var children = await db.Childrens.FirstOrDefaultAsync(u => u.Id == childrenModel.Id);
           
            db.Entry<Children>(children).State = EntityState.Detached;
            if (children != null)
            {
                db.Childrens.Update(childrenModel);
                await db.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }

        }

        public async Task<bool> ConfirmDelete(int? Id)
        {
            if (Id != null)
            {


                var children = await db.Childrens.FirstOrDefaultAsync(u => u.Id == Id);

                return true;
            }
            else
            {
                return false;
            }
        }
        public Children GetById(int? id)
        {
            return Childrens().FirstOrDefault(children => children.Id == id);
        }
        public User GetById_user(int? id)
        {
            return Users().FirstOrDefault(children => children.Id == id);
        }
        public Diagnoz GetById_diagnoz(int? id)
        {
            return Diagnozs().FirstOrDefault(diagnoz => diagnoz.Id == id);
        }

        public async Task<bool> Delete(ChildrenModel childrenModel)
        {

            var children = await db.Childrens.FirstOrDefaultAsync(u => u.Id == childrenModel.Id);
            if (children != null)
            {
                db.Childrens.Remove(children);
                await db.SaveChangesAsync();
                return true;
            }

            else
            {
                return false;
            }
        }
        public async Task<bool> Create_user(RegisterUserModel registerUserModel)
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.Email == registerUserModel.Email);
            if (user == null)
            {
                db.Users.Add(new User
                {
                    Email = registerUserModel.Email,
                    Username = registerUserModel.Email,
                    Surname = registerUserModel.Email,
                    Name = registerUserModel.Email,
                    SecName = registerUserModel.Email,
                    RoleId = registerUserModel.RoleId,
                    Password=Hash(registerUserModel.Password)

                });
                await db.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }

        }
        public async Task<bool> Delete_user(User userModel)
        {

            var user = await db.Users.FirstOrDefaultAsync(u => u.Id == userModel.Id);
            if (user != null)
            {
                db.Users.Remove(user);
                await db.SaveChangesAsync();
                return true;
            }

            else
            {
                return false;
            }
        }
        public async Task<bool> Edit_user(User userModel)
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.Email == userModel.Email);

            db.Entry<User>(user).State = EntityState.Detached;
            if (user != null)
            {
                db.Users.Update(userModel);
                await db.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }

        }
        public async Task<bool> Create_diagnoz(Diagnoz diagnoz)
        {
            var diagnozes = await db.Diagnozes.FirstOrDefaultAsync(u => u.Name == diagnoz.Name);
            if (diagnozes == null)
            {
                db.Diagnozes.Add(new Diagnoz
                {
                    Name = diagnoz.Name,
                   

                });
                await db.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Edit_diagnoz(Diagnoz diagnoz)
        {
            var diagnozes = await db.Diagnozes.FirstOrDefaultAsync(u => u.Id == diagnoz.Id);

            db.Entry<Diagnoz>(diagnozes).State = EntityState.Detached;
            if (diagnozes != null)
            {
                db.Diagnozes.Update(diagnozes);
                await db.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Delete_diagnoz(Diagnoz diagnoz)
        {
            var diagnozes = await db.Diagnozes.FirstOrDefaultAsync(u => u.Id == diagnoz.Id);
            if (diagnozes != null)
            {
                db.Diagnozes.Remove(diagnozes);
                await db.SaveChangesAsync();
                return true;
            }

            else
            {
                return false;
            }
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
