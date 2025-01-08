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
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Character>();
        }

        public async Task<Character> GetCharacter(int id)
        {
            var response = await HttpClient.GetAsync($"user/Book/character/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Character>();
        }

        public async Task UpdateCharacter(int id, CharacterWithAnswers characterWithAnswers)
        {
            var response = await HttpClient.PutAsJsonAsync($"user/Book/character/{id}", characterWithAnswers);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteCharacter(int id)
        {
            var response = await HttpClient.DeleteAsync($"user/Book/character/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
