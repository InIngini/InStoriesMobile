using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Курсач.Core.Data.Entities;
using Курсач.Core.DB.Interfaces;

namespace Курсач.Core.DB
{
    public class UserRepository : IUserRepository
    {
        private SQLiteAsyncConnection Database {  get; set; }
        private IBookRepository BookRepository {  get; set; } 
        public UserRepository(IBookRepository bookRepository)
        {
            BookRepository = bookRepository;
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
            var dbu = await Database.Table<User>().ToListAsync();
            if (dbu.Count == 0 || Database.Table<User>().FirstOrDefaultAsync(b => b.Id == user.Id) == null) // Если в базе нет пользователя или нет именно этого пользователя
            {
                await DeleteUserAsync();
                await BookRepository.DeleteAllBooksAsync();
                await Database.InsertAsync(user);
            }
        }

        public async Task DeleteUserAsync()
        {
            await Database.DeleteAllAsync<User>();
        }
    }
}
