using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Курсач.Core.Interfaces;
using Курсач.Data.DTO;
using Курсач.Data.Entities;

namespace Курсач.Services
{
    public class BookService : IBookService
    {
        private readonly HttpClient _httpClient;

        public BookService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Book> CreateBook(UserBookData bookData)
        {
            var response = await _httpClient.PostAsJsonAsync("user/book", bookData);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Book>();
        }

        public async Task<Book> GetBook(int id)
        {
            var response = await _httpClient.GetAsync($"user/book/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Book>();
        }

        public async Task UpdateBook(int id, Book book)
        {
            var response = await _httpClient.PutAsJsonAsync($"user/book/{id}", book);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteBook(int id)
        {
            var response = await _httpClient.DeleteAsync($"user/book/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<Book>> GetAllBooksForUser(int userId)
        {
            var response = await _httpClient.GetAsync($"user/book/all?userId={userId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Book>>();
        }
    }
}
