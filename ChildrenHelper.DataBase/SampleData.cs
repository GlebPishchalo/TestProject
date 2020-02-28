using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChildrenHelper.Models;

namespace ChildrenHelper.DataBase
{
    public static class SampleData
    {
        public static void Initialize(DataBaseContext context)
        {
            if (!context.Childrens.Any())
            {
                context.Childrens.AddRange(
                    new Children
                    {
                        Chname = "Alexander",
                        Chsurname = "Ivanov",
                        Summa = 600
                    },
                    new Children
                    {
                        Chname = "Petr",
                        Chsurname = "Petrov",
                        Summa = 600
                    },
                    new Children
                    {
                        Chname = "Sergey",
                        Chsurname = "Sergeev",
                        Summa = 600
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
