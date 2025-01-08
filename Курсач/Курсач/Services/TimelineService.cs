using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Курсач.Core.Interfaces;
using Курсач.Data.DTO;
using Курсач.Data.Entities;

namespace Курсач.Services
{
    public class TimelineService : ITimelineService
    {
        private readonly HttpClient HttpClient;

        public TimelineService(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public async Task<Timeline> CreateTimeline(TimelineData timelineData)
        {
            var response = await HttpClient.PostAsJsonAsync("user/Book/timeline", timelineData);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception("Ошибка создания таймлайна") { Data = { { "Content", errorContent } } };
            }
            return await response.Content.ReadFromJsonAsync<Timeline>();
        }

        public async Task<Timeline> GetTimeline(int id)
        {
            var response = await HttpClient.GetAsync($"user/Book/timeline/{id}");
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception("Ошибка получения таймлайна") { Data = { { "Content", errorContent } } };
            }
            return await response.Content.ReadFromJsonAsync<Timeline>();
        }

        public async Task UpdateTimeline(int id, int eventId)
        {
            var response = await HttpClient.PutAsJsonAsync($"user/Book/timeline/{id}", eventId);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception("Ошибка обновления таймлайна") { Data = { { "Content", errorContent } } };
            }
        }

        public async Task DeleteTimeline(int id)
        {
            var response = await HttpClient.DeleteAsync($"user/Book/timeline/{id}");
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception("Ошибка удаления таймлайна") { Data = { { "Content", errorContent } } };
            }
        }
    }
}
