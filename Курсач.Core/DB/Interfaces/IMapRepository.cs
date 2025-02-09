using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Курсач.Core.Data;

namespace Курсач.Core.DB.Interfaces
{
    public interface IMapRepository
    {
        Task AddMarkerAsync(Marker marker);
        Task DeleteAllMarkersAsync();
        Task<List<Marker>> GetAllMarkersAsync();

    }
}
