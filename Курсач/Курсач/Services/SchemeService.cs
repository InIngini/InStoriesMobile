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
    public class SchemeService : ISchemeService
    {
        private readonly HttpClient _httpClient;

        public SchemeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Scheme> CreateScheme(SchemeData schemeData)
        {
            var response = await _httpClient.PostAsJsonAsync("user/Book/scheme", schemeData);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Scheme>();
        }

        public async Task<Scheme> GetScheme(int id)
        {
            var response = await _httpClient.GetAsync($"user/Book/scheme/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Scheme>();
        }

        public async Task UpdateScheme(int id, int connectionId)
        {
            var response = await _httpClient.PutAsJsonAsync($"user/Book/scheme/{id}", connectionId);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteScheme(int id)
        {
            var response = await _httpClient.DeleteAsync($"user/Book/scheme/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
