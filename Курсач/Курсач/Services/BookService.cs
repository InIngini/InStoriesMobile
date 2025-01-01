using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Курсач.Data.DTO;
using Курсач.Data.Entities;

namespace Курсач.Services
{
    public class BookService
    {
        private readonly HttpClient _httpClient;

        public BookService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Book> CreateBook(UserBookData bookData)
        {
            var response = await _httpClient.PostAsJsonAsync("User /book", bookData);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Book>();
        }

        public async Task<Book> GetBook(int id)
        {
            var response = await _httpClient.GetAsync($"User /book/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Book>();
        }

        public async Task UpdateBook(int id, Book book)
        {
            var response = await _httpClient.PutAsJsonAsync($"User /book/{id}", book);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteBook(int id)
        {
            var response = await _httpClient.DeleteAsync($"User /book/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<Book>> GetAllBooksForUser(int userId)
        {
            var response = await _httpClient.GetAsync($"User /book/all?userId={userId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Book>>();
        }
    }
}
