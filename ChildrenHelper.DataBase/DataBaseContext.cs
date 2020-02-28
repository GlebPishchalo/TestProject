using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChildrenHelper.Models;
using Microsoft.EntityFrameworkCore;

namespace ChildrenHelper.DataBase
{
    public class DataBaseContext :DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Children> Childrens { get; set; }
        public DbSet<Diagnoz> Diagnozes { get; set; }
        public DbSet<Role> Roles { get; set; }
        

        public DataBaseContext(DbContextOptions<DataBaseContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        
    }
}
