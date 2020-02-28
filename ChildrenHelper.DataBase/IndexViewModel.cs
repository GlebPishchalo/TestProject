using ChildrenHelper.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChildrenHelper.DataBase
{
    public class IndexViewModel
    {
        public IEnumerable<Children> Childrens { get; set; }
        public IEnumerable<Diagnoz> Diagnozes { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
        public string SearchQuery { get; set; }
       

    }
}
