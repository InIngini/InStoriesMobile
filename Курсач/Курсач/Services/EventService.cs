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
    public class EventService : IEventService
    {
        private readonly HttpClient _httpClient;

        public EventService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Event> CreateEvent(EventData eventData)
        {
            var response = await _httpClient.PostAsJsonAsync("user/Book/Timeline/event", eventData);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Event>();
        }

        public async Task<Event> GetEvent(int id)
        {
            var response = await _httpClient.GetAsync($"user/Book/Timeline/event/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Event>();
        }

        public async Task UpdateEvent(int id, EventData eventData)
        {
            var response = await _httpClient.PutAsJsonAsync($"user/Book/Timeline/event/{id}", eventData);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteEvent(int id)
        {
            var response = await _httpClient.DeleteAsync($"user/Book/Timeline/event/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
