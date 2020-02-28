using ChildrenHelper.Services.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChildrenHelper.Services.Contracts.Interfaces
{
    public interface IUserService
    {
        Task<bool> Login(LoginUserModel loginUserModel);
        Task<bool> Register(RegisterUserModel registerUserModel);
    }
}
