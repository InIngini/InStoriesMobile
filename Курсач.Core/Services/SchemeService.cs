using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Курсач.Core.Data.DTO;
using Курсач.Core.Data.Entities;
using Курсач.Core.Services.Interfaces;

namespace Курсач.Core.Services
{
    public class SchemeService : ISchemeService
    {
        private readonly HttpClient HttpClient;

        public SchemeService(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public async Task<Scheme> CreateScheme(SchemeData schemeData)
        {
            var response = await HttpClient.PostAsJsonAsync("user/Book/scheme", schemeData);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception("Ошибка создания схемы") { Data = { { "Content", errorContent } } };
            }
            return await response.Content.ReadFromJsonAsync<Scheme>();
        }

        public async Task<Scheme> GetScheme(int id)
        {
            var response = await HttpClient.GetAsync($"user/Book/scheme/{id}");
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception("Ошибка получения схемы") { Data = { { "Content", errorContent } } };
            }
            return await response.Content.ReadFromJsonAsync<Scheme>();
        }

        public async Task UpdateScheme(int id, int connectionId)
        {
            var response = await HttpClient.PutAsJsonAsync($"user/Book/scheme/{id}", connectionId);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception("Ошибка обновления схемы") { Data = { { "Content", errorContent } } };
            }
        }

        public async Task DeleteScheme(int id)
        {
            var response = await HttpClient.DeleteAsync($"user/Book/scheme/{id}");
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception("Ошибка удаления схемы") { Data = { { "Content", errorContent } } };
            }
        }
    }
}
