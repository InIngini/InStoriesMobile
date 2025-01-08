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
        private readonly HttpClient HttpClient;

        public ConnectionService(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public async Task<Connection> CreateConnection(ConnectionData connectionData)
        {
            var response = await HttpClient.PostAsJsonAsync("user/Book/Scheme/connection", connectionData);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Connection>();
        }

        public async Task<Connection> GetConnection(int id)
        {
            var response = await HttpClient.GetAsync($"user/Book/Scheme/connection/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Connection>();
        }

        public async Task DeleteConnection(int id)
        {
            var response = await HttpClient.DeleteAsync($"user/Book/Scheme/connection/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
