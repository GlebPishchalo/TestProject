using ChildrenHelper.DataBase;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChildrenHelper.Models
{
    public class Children
    {
        [Key]
        public int Id { get; set; }
        public string Chname { get; set; }
        public string Chsurname { get; set; }
        public string Chsecname { get; set; }
        public int? DiagnozID { get; set; }
        public Diagnoz Diagnoz { get; set; }
        public string Chdesc { get; set; }
        public double Summa { get; set; }
        public byte[] Photo { get; set; }
    }

    public class Diagnoz
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Children> Childrens { get; set; }
        public Diagnoz()
        {
            Childrens = new List<Children>();
        }
    }

    
}
