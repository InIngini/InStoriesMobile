using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using InStories.Core.Data.DTO;
using InStories.Core.Data.Entities;
using InStories.Core.Services.Interfaces;

namespace InStories.Core.Services
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
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception("Ошибка создания соединения") { Data = { { "Content", errorContent } } };
            }
            return await response.Content.ReadFromJsonAsync<Connection>();
        }

        public async Task<Connection> GetConnection(int id)
        {
            var response = await HttpClient.GetAsync($"user/Book/Scheme/connection/{id}");
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception("Ошибка получения соединения") { Data = { { "Content", errorContent } } };
            }
            return await response.Content.ReadFromJsonAsync<Connection>();
        }

        public async Task DeleteConnection(int id)
        {
            var response = await HttpClient.DeleteAsync($"user/Book/Scheme/connection/{id}");
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception("Ошибка удаления соединения") { Data = { { "Content", errorContent } } };
            }
        }
    }
}
