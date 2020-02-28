using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ChildrenHelper.Services.Contracts.Models;

namespace ChildrenHelper.Services.Contracts.Interfaces
{
    public interface IChildrenService
    {
        Task<bool> Create(ChildrenModel childrenModel);
        Task<bool> Edit(ChildrenModel childrenModel);
        Task<bool> Delete(ChildrenModel childrenModel);
        Task<bool> ConfirmDelete(int? Id);
        // Task<bool> Register(RegisterUserModel registerUserModel);
    }
}
