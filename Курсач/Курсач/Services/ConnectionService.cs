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
    public class ConnectionService : IConnectionService
    {
        private readonly HttpClient _httpClient;

        public ConnectionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Connection> CreateConnection(ConnectionData connectionData)
        {
            var response = await _httpClient.PostAsJsonAsync("user/Book/Scheme/connection", connectionData);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Connection>();
        }

        public async Task<Connection> GetConnection(int id)
        {
            var response = await _httpClient.GetAsync($"user/Book/Scheme/connection/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Connection>();
        }

        public async Task DeleteConnection(int id)
        {
            var response = await _httpClient.DeleteAsync($"user/Book/Scheme/connection/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
