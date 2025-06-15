using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using InStories.Core.Data.Entities;

namespace InStories.Core.DB.Interfaces
{
    public interface IBookRepository
    {
        Task<Book> GetBookAsync(int id);
        Task AddBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(int id);
        Task DeleteAllBooksAsync();
        Task<List<Book>> GetAllBooksAsync();
    }
}
