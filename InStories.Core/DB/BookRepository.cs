using InStories.Core.Data.Entities;
using InStories.Core.DB.Interfaces;
using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Path = System.IO.Path;

namespace InStories.Core.DB
{
    public class BookRepository : IBookRepository
    {
        private readonly SQLiteAsyncConnection Database;
        public BookRepository()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DB.sqlite");
            Database = new SQLiteAsyncConnection(dbPath);
            Database.CreateTableAsync<Book>().Wait();
        }
        public async Task<Book> GetBookAsync(int id)
        {
            return await Database.Table<Book>().FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task AddBookAsync(Book book)
        {
            await Database.InsertAsync(book);
        }
        public async Task UpdateBookAsync(Book book)
        {
            await Database.UpdateAsync(book);
        }
        public async Task DeleteBookAsync(int id)
        {
            var bookToDelete = await Database.Table<Book>().FirstOrDefaultAsync(b => b.Id == id);

            if (bookToDelete != null)
            {
                await Database.DeleteAsync(bookToDelete);
            }
        }

        public async Task DeleteAllBooksAsync()
        {
            await Database.DeleteAllAsync<Book>();
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await Database.Table<Book>().ToListAsync();
        }
    }

}
