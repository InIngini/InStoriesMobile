using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using InStories.Core.Common;
using InStories.Core.Data.DTO;
using InStories.Core.Data.Entities;
using InStories.Core.DB;
using InStories.Core.DB.Interfaces;
using InStories.Core.Services.Interfaces;

namespace InStories.Core.Services
{
    public class BookService : IBookService
    {
        private readonly HttpClient HttpClient;
        private IBookRepository BookRepository { get; set; }

        public BookService(HttpClient httpClient, IBookRepository databaseManager)
        {
            HttpClient = httpClient;
            BookRepository = databaseManager;
        }

        public async Task<Book> CreateBook(UserBookData bookData)
        {
            var book = new Book();

            try
            {
                var response = await HttpClient.PostAsJsonAsync("user/book", bookData);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception("Ошибка создания книги") { Data = { { "Content", errorContent } } };
                }
                book = await response.Content.ReadFromJsonAsync<Book>();

                await BookRepository.AddBookAsync(book);
            }
            catch
            {
                throw new Exception("Вы не можете добавить книгу. Возможно, у вас отсутствует подключение к интернету");
            }

            return book;
        }

        public async Task<Book> GetBook(int id)
        {
            var book = new Book();
            try
            {
                var response = await HttpClient.GetAsync($"user/book/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception("Ошибка получения книги") { Data = { { "Content", errorContent } } };
                }
                book = await response.Content.ReadFromJsonAsync<Book>();
            }
            catch
            {
                book = await BookRepository.GetBookAsync(id);
            }
            return book;
        }

        public async Task UpdateBook(int id, Book book)
        {
            try
            {
                var response = await HttpClient.PutAsJsonAsync($"user/book/{id}", book);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception("Ошибка обновления книги") { Data = { { "Content", errorContent } } };
                }
                await BookRepository.UpdateBookAsync(book);
            }
            catch
            {
                if (book.NameBook != (await BookRepository.GetBookAsync(book.Id)).NameBook)
                    throw new Exception("Вы не можете обновить книгу. Возможно, у вас отсутствует подключение к интернету");
            }
        }

        public async Task DeleteBook(int id)
        {
            try
            {
                var response = await HttpClient.DeleteAsync($"user/book/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception("Ошибка удаления книги") { Data = { { "Content", errorContent } } };
                }
                await BookRepository.DeleteBookAsync(id);
            }
            catch
            {
                throw new Exception("Вы не можете удалить книгу. Возможно, у вас отсутствует подключение к интернету");
            }
        }

        public async Task<List<Book>> GetAllBooksForUser(int userId)
        {
            var books = new List<Book>();
            try
            {
                var response = await HttpClient.GetAsync($"user/book/all?userId={userId}");
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception("Ошибка получения всех книг для пользователя") { Data = { { "Content", errorContent } } };
                }
                books = await response.Content.ReadFromJsonAsync<List<Book>>();
            }
            catch
            {
                books = await BookRepository.GetAllBooksAsync();
            }
            return books;
        }
    }
}
