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
    public class CharacterService
    {
        private readonly HttpClient _httpClient;

        public CharacterService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Character> CreateCharacter(BookCharacterData bookCharacterData)
        {
            var response = await _httpClient.PostAsJsonAsync("User /Book/character", bookCharacterData);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Character>();
        }

        public async Task<Character> GetCharacter(int id)
        {
            var response = await _httpClient.GetAsync($"User /Book/character/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Character>();
        }

        public async Task UpdateCharacter(int id, CharacterWithAnswers characterWithAnswers)
        {
            var response = await _httpClient.PutAsJsonAsync($"User /Book/character/{id}", characterWithAnswers);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteCharacter(int id)
        {
            var response = await _httpClient.DeleteAsync($"User /Book/character/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
