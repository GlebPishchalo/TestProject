using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ChildrenHelper.DataBase;
using ChildrenHelper.Models;
using ChildrenHelper.Services.Contracts.Models;
using Diagnoz = ChildrenHelper.Models.Diagnoz;

namespace ChildrenHelper.Services.Contracts.Interfaces
{
    public interface IAdminServices
    {
        Task<bool> Create(ChildrenModel childrenModel);
        Task<bool> Edit(Children childrenModel);
        Task<bool> Delete(ChildrenModel childrenModel);

        IEnumerable<Children> Childrens();
        IEnumerable<User> Users();
        IEnumerable<Diagnoz> Diagnozs();
        
        Children GetById(int? id);
        User GetById_user(int? id);
        Diagnoz GetById_diagnoz(int? id);
        
        Task<bool> Create_user(RegisterUserModel userModel);
        Task<bool> Edit_user(User userModel);
        Task<bool> Delete_user(User userModel);

        Task<bool> Create_diagnoz(Diagnoz diagnoz);
        Task<bool> Edit_diagnoz(Diagnoz diagnoz);
        Task<bool> Delete_diagnoz(Diagnoz diagnoz);

    }
}
