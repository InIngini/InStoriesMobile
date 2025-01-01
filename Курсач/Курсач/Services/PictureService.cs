using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Курсач.Data.Entities;

namespace Курсач.Services
{
    public class PictureService
    {
        private readonly HttpClient _httpClient;

        public PictureService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Picture> CreatePicture(Picture picture)
        {
            var response = await _httpClient.PostAsJsonAsync("User /picture", picture);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Picture>();
        }

        public async Task<Picture> GetPicture(int id)
        {
            var response = await _httpClient.GetAsync($"User /picture/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Picture>();
        }

        public async Task DeletePicture(int id)
        {
            var response = await _httpClient.DeleteAsync($"User /picture/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
