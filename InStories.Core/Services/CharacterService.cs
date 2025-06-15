using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using InStories.Core.Data.DTO;
using InStories.Core.Data.Entities;
using InStories.Core.Services.Interfaces;

namespace InStories.Core.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly HttpClient HttpClient;

        public CharacterService(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public async Task<Character> CreateCharacter(BookCharacterData bookCharacterData)
        {
            var response = await HttpClient.PostAsJsonAsync("user/Book/character", bookCharacterData);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception("Ошибка создания персонажа") { Data = { { "Content", errorContent } } };
            }
            return await response.Content.ReadFromJsonAsync<Character>();
        }

        public async Task<Character> GetCharacter(int id)
        {
            var response = await HttpClient.GetAsync($"user/Book/character/{id}");
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception("Ошибка получения персонажа") { Data = { { "Content", errorContent } } };
            }
            return await response.Content.ReadFromJsonAsync<Character>();
        }

        public async Task UpdateCharacter(int id, CharacterWithAnswers characterWithAnswers)
        {
            var response = await HttpClient.PutAsJsonAsync($"user/Book/character/{id}", characterWithAnswers);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception("Ошибка обновления персонажа") { Data = { { "Content", errorContent } } };
            }
        }

        public async Task DeleteCharacter(int id)
        {
            var response = await HttpClient.DeleteAsync($"user/Book/character/{id}");
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception("Ошибка удаления персонажа") { Data = { { "Content", errorContent } } };
            }
        }
    }
}
