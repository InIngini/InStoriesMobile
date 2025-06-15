using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using InStories.Common;
using InStories.Core.Data.DTO;
using InStories.Core.Data.Entities;
using InStories.Core.DB.Interfaces;
using InStories.Core.Services.Interfaces;

namespace InStories.Core.DB
{
    public class DatabaseSyncService : IDatabaseSyncService
    {
        private readonly IBookService BookService;
        private readonly IBookRepository BookRepository;

        public DatabaseSyncService(IBookService bookService, IBookRepository bookRepository)
        {
            BookService = bookService;
            BookRepository = bookRepository;
        }

        public async Task SyncDatabasesAsync(int userId)
        {
            await SyncBooksAsync(userId);
        }

        private async Task SyncBooksAsync(int userId)
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                // Получаем все книги из локальной базы данных
                var localBooks = await BookRepository.GetAllBooksAsync();
                var remoteBooks = await BookService.GetAllBooksForUser(userId);

                // Добавляем новые книги из локальной базы в удаленную
                if (localBooks.Any())
                {
                    foreach (var remoteBook in remoteBooks)
                    {
                        if (!localBooks.Any(r => r.Id == remoteBook.Id)) // Если книги еще нет, добавляем
                        {
                            await BookRepository.AddBookAsync(remoteBook);
                        }
                        else
                        {
                            // Если книга уже существует локально, сравниваем, если что, обновляем
                            var localBook = localBooks.First(r => r.Id == remoteBook.Id);
                            if (localBook.NameBook != remoteBook.NameBook)
                            {
                                await BookService.UpdateBook(localBook.Id, localBook);
                            }
                        }
                    }
                }
                else if (remoteBooks.Any())
                {
                    foreach (var remoteBook in remoteBooks)
                    {
                        await BookRepository.AddBookAsync(remoteBook);
                    }
                }
            }
        }
    }
}
