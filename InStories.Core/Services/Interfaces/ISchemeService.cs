using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using InStories.Core.Data.DTO;
using InStories.Core.Data.Entities;

namespace InStories.Core.Services.Interfaces
{
    public interface ISchemeService
    {
        Task<Scheme> CreateScheme(SchemeData schemeData);
        Task<Scheme> GetScheme(int id);
        Task UpdateScheme(int id, int connectionId);
        Task DeleteScheme(int id);
    }
}
