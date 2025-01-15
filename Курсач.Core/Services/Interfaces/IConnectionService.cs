using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Курсач.Core.Data.DTO;
using Курсач.Core.Data.Entities;

namespace Курсач.Core.Services.Interfaces
{
    public interface IConnectionService
    {
        Task<Connection> CreateConnection(ConnectionData connectionData);
        Task<Connection> GetConnection(int id);
        Task DeleteConnection(int id);
    }
}
