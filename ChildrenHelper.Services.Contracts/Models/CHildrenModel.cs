using System.Collections.Generic;


namespace ChildrenHelper.Services.Contracts.Models
{
    public class ChildrenModel
    {
        public int? Id { get; set; }
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

        public List<ChildrenModel> Childrens { get; set; }

        public Diagnoz()
        {
            Childrens = new List<ChildrenModel>();
        }
    }

}


