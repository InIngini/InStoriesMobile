using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Курсач.Core.Data.DTO;
using Курсач.Core.Data.Entities;
using Курсач.Core.Services.Interfaces;

namespace Курсач.Core.Services
{
    public class EventService : IEventService
    {
        private readonly HttpClient HttpClient;

        public EventService(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public async Task<Event> CreateEvent(EventData eventData)
        {
            var response = await HttpClient.PostAsJsonAsync("user/Book/Timeline/event", eventData);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception("Ошибка создания события") { Data = { { "Content", errorContent } } };
            }
            return await response.Content.ReadFromJsonAsync<Event>();
        }

        public async Task<Event> GetEvent(int id)
        {
            var response = await HttpClient.GetAsync($"user/Book/Timeline/event/{id}");
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception("Ошибка получения события") { Data = { { "Content", errorContent } } };
            }
            return await response.Content.ReadFromJsonAsync<Event>();
        }

        public async Task UpdateEvent(int id, EventData eventData)
        {
            var response = await HttpClient.PutAsJsonAsync($"user/Book/Timeline/event/{id}", eventData);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception("Ошибка обновления события") { Data = { { "Content", errorContent } } };
            }
        }

        public async Task DeleteEvent(int id)
        {
            var response = await HttpClient.DeleteAsync($"user/Book/Timeline/event/{id}");
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception("Ошибка удаления события") { Data = { { "Content", errorContent } } };
            }
        }
    }
}
