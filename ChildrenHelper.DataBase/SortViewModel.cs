using System;
using System.Collections.Generic;
using System.Text;

namespace ChildrenHelper.DataBase
{
   public class SortViewModel
    {
        public SortState NameSort { get; private set; } // значение для сортировки по имени

        public SortState Current { get; private set; }     // текущее значение сортировки

        public SortViewModel(SortState sortOrder)
        {
            NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
     
            Current = sortOrder;
        }
    }
}
