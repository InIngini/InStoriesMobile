using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Курсач.Core.Data.DTO;
using Курсач.Core.Data.Entities;

namespace Курсач.Core.Services.Interfaces
{
    public interface ISchemeService
    {
        Task<Scheme> CreateScheme(SchemeData schemeData);
        Task<Scheme> GetScheme(int id);
        Task UpdateScheme(int id, int connectionId);
        Task DeleteScheme(int id);
    }
}
