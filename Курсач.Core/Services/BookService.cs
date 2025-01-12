using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Курсач.Core.Data.DTO;
using Курсач.Core.Data.Entities;
using Курсач.Core.Interfaces;

namespace Курсач.Core.Services
{
    public class BookService : IBookService
    {
        private readonly HttpClient HttpClient;

        public BookService(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public async Task<Book> CreateBook(UserBookData bookData)
        {
            var response = await HttpClient.PostAsJsonAsync("user/book", bookData);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception("Ошибка создания книги") { Data = { { "Content", errorContent } } };
            }
            return await response.Content.ReadFromJsonAsync<Book>();
        }

        public async Task<Book> GetBook(int id)
        {
            var response = await HttpClient.GetAsync($"user/book/{id}");
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception("Ошибка получения книги") { Data = { { "Content", errorContent } } };
            }
            return await response.Content.ReadFromJsonAsync<Book>();
        }

        public async Task UpdateBook(int id, Book book)
        {
            var response = await HttpClient.PutAsJsonAsync($"user/book/{id}", book);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception("Ошибка обновления книги") { Data = { { "Content", errorContent } } };
            }
        }

        public async Task DeleteBook(int id)
        {
            var response = await HttpClient.DeleteAsync($"user/book/{id}");
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception("Ошибка удаления книги") { Data = { { "Content", errorContent } } };
            }
        }

        public async Task<List<Book>> GetAllBooksForUser(int userId)
        {
            var response = await HttpClient.GetAsync($"user/book/all?userId={userId}");
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception("Ошибка получения всех книг для пользователя") { Data = { { "Content", errorContent } } };
            }
            return await response.Content.ReadFromJsonAsync<List<Book>>();
        }
    }
}
