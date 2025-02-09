using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Курсач.Core.Data;
using Курсач.Core.Data.Entities;
using Курсач.Core.DB.Interfaces;

namespace Курсач.Core.DB
{
    public class MapRepository : IMapRepository
    {
        private readonly SQLiteAsyncConnection Database;
        public MapRepository()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DB.sqlite");
            Database = new SQLiteAsyncConnection(dbPath);
            Database.CreateTableAsync<Marker>().Wait();
        }
        public async Task AddMarkerAsync(Marker marker)
        {
            await Database.InsertAsync(marker);
        }

        public async Task DeleteAllMarkersAsync()
        {
            await Database.DeleteAllAsync<Marker>();
        }

        public async Task<List<Marker>> GetAllMarkersAsync()
        {
            return await Database.Table<Marker>().ToListAsync();
        }
    }
}
