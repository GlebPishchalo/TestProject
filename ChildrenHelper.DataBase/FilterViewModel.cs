using System;
using System.Collections.Generic;
using System.Text;
using ChildrenHelper.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace ChildrenHelper.DataBase
{
    public class FilterViewModel
    {
        public FilterViewModel(List<Diagnoz> diagnozies, int? diagnoz, string name)
        {
            // устанавливаем начальный элемент, который позволит выбрать всех
            diagnozies.Insert(0, new Diagnoz() { Name = "Все", Id = 0 });
            Diagnozes = new SelectList(diagnozies, "Id", "Name", diagnoz);
            SelectedDiagnoz = diagnoz;
            SelectedName = name;
        }
        public SelectList Diagnozes { get; private set; } // список 
        public int? SelectedDiagnoz { get; private set; }   
        public string SelectedName { get; private set; }   
    }
}

