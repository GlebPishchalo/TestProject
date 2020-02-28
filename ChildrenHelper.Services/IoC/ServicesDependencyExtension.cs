using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using ChildrenHelper.Services.Contracts;
using ChildrenHelper.Services.Contracts.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ChildrenHelper.Services.IoC
{
    public static class ServicesDependencyExtension
    {
        public static IServiceCollection InitializeServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UsersServices>();
            services.AddTransient<IChildrenService, ChildrenServices>();
            //services.AddTransient<>();
            services.AddTransient<IAdminServices,AdminServices>();
          
           
            return services;
        }
    }
}
