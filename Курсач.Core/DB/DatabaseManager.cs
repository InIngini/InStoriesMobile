using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Курсач.Core.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using Курсач.Core.DB.Interfaces;
using Newtonsoft.Json.Linq;
using System.IO;
using Xamarin.Forms;
using SQLite;
using Xamarin.Forms.Shapes;
using System.Threading.Tasks;
using Path = System.IO.Path;

namespace Курсач.Core.DB
{
    public class DatabaseManager : IDatabaseManager
    {
        private readonly SQLiteAsyncConnection Database;
        public DatabaseManager()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DB.sqlite");
            Database = new SQLiteAsyncConnection(dbPath);
            Database.CreateTableAsync<User>().Wait();
        }
        public async Task<User> GetUserAsync()
        {
            return await Database.Table<User>().FirstOrDefaultAsync();
        }

        public async Task AddUserAsync(User user)
        {
            await DeleteUserAsync();
            await Database.InsertAsync(user);
        }

        public async Task DeleteUserAsync()
        {
            await Database.DeleteAllAsync<User>();
        }
    }

}
