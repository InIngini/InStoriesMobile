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
        private readonly HttpClient HttpClient;

        public EventService(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public async Task<Event> CreateEvent(EventData eventData)
        {
            var response = await HttpClient.PostAsJsonAsync("user/Book/Timeline/event", eventData);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Event>();
        }

        public async Task<Event> GetEvent(int id)
        {
            var response = await HttpClient.GetAsync($"user/Book/Timeline/event/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Event>();
        }

        public async Task UpdateEvent(int id, EventData eventData)
        {
            var response = await HttpClient.PutAsJsonAsync($"user/Book/Timeline/event/{id}", eventData);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteEvent(int id)
        {
            var response = await HttpClient.DeleteAsync($"user/Book/Timeline/event/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
