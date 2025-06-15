using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using InStories.Core.Data.DTO;
using InStories.Core.Data.Entities;

namespace InStories.Core.Services.Interfaces
{
    public interface IConnectionService
    {
        Task<Connection> CreateConnection(ConnectionData connectionData);
        Task<Connection> GetConnection(int id);
        Task DeleteConnection(int id);
    }
}
