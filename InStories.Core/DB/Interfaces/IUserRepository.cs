using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using InStories.Core.Data.Entities;

namespace InStories.Core.DB.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync();
        Task AddUserAsync(User user);
        Task DeleteUserAsync();
    }
}
