using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Курсач.Core.DB.Interfaces;

namespace Курсач.Core.Data
{
    public class Marker
    {
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public interface IWebMessageHandler
    {
        Task AddMarker(string name, double latitude, double longitude);
        Task DeleteAllMarker();
        Task<List<Marker>> GetAllMarker();
    }

    public class MarkerManager : IWebMessageHandler
    {
        private IMapRepository MapRepository { get; set; }
        public MarkerManager(IMapRepository mapRepository) 
        { 
            MapRepository = mapRepository;
        }

        public List<Marker> Markers { get; private set; } = new List<Marker>();

        public async Task AddMarker(string name, double latitude, double longitude)
        {
            var marker = new Marker { Name = name, Latitude = latitude, Longitude = longitude };
            Markers.Add(marker);
            await MapRepository.AddMarkerAsync(marker);
        }

        public async Task DeleteAllMarker()
        {
            await MapRepository.DeleteAllMarkersAsync();
        }

        public async Task<List<Marker>> GetAllMarker()
        {
            return await MapRepository.GetAllMarkersAsync();
        }
    }

}
