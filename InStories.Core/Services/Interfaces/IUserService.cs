using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using InStories.Core.Data.DTO;
using InStories.Core.Data.Entities;

namespace InStories.Core.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> RegisterUser(LoginData loginData);
        Task<string> LoginUser(LoginData loginData);
        Task<User> GetUser();
    }

}
