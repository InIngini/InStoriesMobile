using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Курсач.Data.DTO;
using Курсач.Data.Entities;

namespace Курсач.Services
{
    public class TimelineService
    {
        private readonly HttpClient _httpClient;

        public TimelineService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Timeline> CreateTimeline(TimelineData timelineData)
        {
            var response = await _httpClient.PostAsJsonAsync("User /Book/timeline", timelineData);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Timeline>();
        }

        public async Task<Timeline> GetTimeline(int id)
        {
            var response = await _httpClient.GetAsync($"User /Book/timeline/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Timeline>();
        }

        public async Task UpdateTimeline(int id, int eventId)
        {
            var response = await _httpClient.PutAsJsonAsync($"User /Book/timeline/{id}", eventId);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteTimeline(int id)
        {
            var response = await _httpClient.DeleteAsync($"User /Book/timeline/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
