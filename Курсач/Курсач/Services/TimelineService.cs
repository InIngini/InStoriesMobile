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
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Timeline>();
        }

        public async Task<Timeline> GetTimeline(int id)
        {
            var response = await HttpClient.GetAsync($"user/Book/timeline/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Timeline>();
        }

        public async Task UpdateTimeline(int id, int eventId)
        {
            var response = await HttpClient.PutAsJsonAsync($"user/Book/timeline/{id}", eventId);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteTimeline(int id)
        {
            var response = await HttpClient.DeleteAsync($"user/Book/timeline/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
