using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Курсач.Core.Interfaces;
using Курсач.Data.DTO;
using Курсач.Data.Entities;

namespace Курсач.Services
{
    public class GalleryService : IGalleryService
    {
        private readonly HttpClient HttpClient;

        public GalleryService(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public async Task<BelongToGallery> CreateGallery(GalleryData galleryData)
        {
            var response = await HttpClient.PostAsJsonAsync("user/Book/Character/gallery", galleryData);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception("Ошибка создания галереи") { Data = { { "Content", errorContent } } };
            }
            return await response.Content.ReadFromJsonAsync<BelongToGallery>();
        }

        public async Task<BelongToGallery> GetGallery(int id)
        {
            var response = await HttpClient.GetAsync($"user/Book/Character/gallery/{id}");
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception("Ошибка получения галереи") { Data = { { "Content", errorContent } } };
            }
            return await response.Content.ReadFromJsonAsync<BelongToGallery>();
        }

        public async Task DeletePictureFromGallery(int idPicture)
        {
            var response = await HttpClient.DeleteAsync($"user/Book/Character/gallery/{idPicture}");
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception("Ошибка удаления картинки из галереи") { Data = { { "Content", errorContent } } };
            }
        }
    }
}
