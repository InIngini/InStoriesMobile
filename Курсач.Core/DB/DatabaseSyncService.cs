using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Курсач.Core.Data.DTO;
using Курсач.Core.Data.Entities;
using Курсач.Core.DB.Interfaces;
using Курсач.Core.Interfaces;

namespace Курсач.Core.DB
{
    public class DatabaseSyncService : IDatabaseSyncService
    {
        private readonly IBookService BookService;
        private readonly IDatabaseManager DatabaseManager;

        public DatabaseSyncService(IBookService bookService, IDatabaseManager databaseManager)
        {
            BookService = bookService;
            DatabaseManager = databaseManager;
        }

        public async Task SyncDatabasesAsync(int userId)
        {
            await SyncBooksAsync(userId);
        }

        private async Task SyncBooksAsync(int userId)
        {
            // Получаем все книги из локальной базы данных
            var localBooks = await DatabaseManager.GetAllBooksAsync();
            var remoteBooks = await BookService.GetAllBooksForUser(userId);

            // Добавляем новые книги из локальной базы в удаленную
            if (localBooks.Any())
            {
                foreach (var localBook in localBooks)
                {
                    if (!remoteBooks.Any(r => r.Id == localBook.Id))
                    {
                        var userBook = new UserBookData()
                        {
                            NameBook = localBook.NameBook,
                        };
                        await DatabaseManager.DeleteBookAsync(localBook.Id);
                        var book = await BookService.CreateBook(userBook);
                        await DatabaseManager.AddBookAsync(book);
                    }
                    else
                    {
                        // Если книга уже существует на сервере, сравниваем, если что, обновляем
                        var remoteBook = remoteBooks.First(r => r.Id == localBook.Id);
                        if (localBook.NameBook != remoteBook.NameBook)
                        {
                            await BookService.UpdateBook(localBook.Id, localBook);
                        }
                    }
                }
            }
            else if (remoteBooks.Any())
            {
                foreach(var remoteBook in remoteBooks)
                {
                    await DatabaseManager.AddBookAsync(remoteBook);
                }
            }
        }
    }
}
