using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Курсач.Data.DTO;
using Курсач.Data.Entities;

namespace Курсач.Core.Interfaces
{
    public interface IConnectionService
    {
        Task<Connection> CreateConnection(ConnectionData connectionData);
        Task<Connection> GetConnection(int id);
        Task DeleteConnection(int id);
    }
}
