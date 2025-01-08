using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Курсач.Core.Interfaces;
using Курсач.Data.Entities;

namespace Курсач.Services
{
    public class PictureService : IPictureService
    {
        private readonly HttpClient HttpClient;

        public PictureService(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public async Task<Picture> CreatePicture(Picture picture)
        {
            var response = await HttpClient.PostAsJsonAsync("user/picture", picture);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception("Ошибка создания картинки") { Data = { { "Content", errorContent } } };
            }
            return await response.Content.ReadFromJsonAsync<Picture>();
        }

        public async Task<Picture> GetPicture(int id)
        {
            var response = await HttpClient.GetAsync($"user/picture/{id}");
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception("Ошибка получения картинки") { Data = { { "Content", errorContent } } };
            }
            return await response.Content.ReadFromJsonAsync<Picture>();
        }

        public async Task DeletePicture(int id)
        {
            var response = await HttpClient.DeleteAsync($"user/picture/{id}");
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception("Ошибка удаления картинки") { Data = { { "Content", errorContent } } };
            }
        }
    }
}
