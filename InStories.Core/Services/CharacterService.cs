using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using InStories.Core.Data.DTO;
using InStories.Core.Data.Entities;
using InStories.Core.DB;
using InStories.Core.DB.Interfaces;
using InStories.Core.Services.Interfaces;

namespace InStories.Core.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly HttpClient HttpClient;
        private readonly ICharacterRepository CharacterRepository;

        public CharacterService(HttpClient httpClient, ICharacterRepository characterRepository)
        {
            HttpClient = httpClient;
            CharacterRepository = characterRepository;
        }

        public async Task<Character> CreateCharacter(Character character)
        {
            try
            {
                var response = await HttpClient.PostAsJsonAsync("User/Book/Character", character);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception("Ошибка создания персонажа") { Data = { { "Content", errorContent } } };
                }

                var createdCharacter = await response.Content.ReadFromJsonAsync<Character>();
                await CharacterRepository.AddCharacterAsync(createdCharacter);
                return createdCharacter;
            }
            catch
            {
                throw new Exception("Вы не можете создать персонажа. Возможно, у вас отсутствует подключение к интернету");
            }
        }

        public async Task<Character> UpdateCharacter(CharacterWithAnswers characterWithAnswers, int id)
        {
            try
            {
                var response = await HttpClient.PutAsJsonAsync($"User/Book/Character/{id}", characterWithAnswers);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception("Ошибка обновления персонажа") { Data = { { "Content", errorContent } } };
                }

                var updatedCharacter = await response.Content.ReadFromJsonAsync<Character>();
                await CharacterRepository.UpdateCharacterAsync(characterWithAnswers, id);
                return updatedCharacter;
            }
            catch
            {
                throw new Exception("Вы не можете обновить персонажа. Возможно, у вас отсутствует подключение к интернету");
            }
        }

        public async Task DeleteCharacter(int id)
        {
            try
            {
                var response = await HttpClient.DeleteAsync($"User/Book/Character/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception("Ошибка удаления персонажа") { Data = { { "Content", errorContent } } };
                }

                await CharacterRepository.DeleteCharacterAsync(id);
            }
            catch
            {
                throw new Exception("Вы не можете удалить персонажа. Возможно, у вас отсутствует подключение к интернету");
            }
        }

        public async Task<CharacterWithAnswers> GetCharacter(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await HttpClient.GetAsync($"User/Book/Character/{id}", cancellationToken);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception("Ошибка получения персонажа") { Data = { { "Content", errorContent } } };
                }

                var characterWithAnswers = await response.Content.ReadFromJsonAsync<CharacterWithAnswers>();

                await CharacterRepository.UpdateCharacterAsync(characterWithAnswers, id);

                return characterWithAnswers;
            }
            catch
            {
                var character = await CharacterRepository.GetCharacterAsync(id);

                // Конвертируем Character в CharacterWithAnswers
                if (character == null) return null;

                return character;
            }
        }

        public async Task<IEnumerable<CharacterAllData>> GetAllCharacters(int idBook, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await HttpClient.GetAsync($"User/Book/Character/all?idBook={idBook}", cancellationToken);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception("Ошибка получения всех персонажей") { Data = { { "Content", errorContent } } };
                }

                var characters = await response.Content.ReadFromJsonAsync<IEnumerable<CharacterAllData>>();
                foreach (var characterAllData in characters)
                {
                    var character = new Character()
                    {
                        BookId = idBook,
                        Id = characterAllData.Id,
                        Name = characterAllData.Name
                    };
                    await CharacterRepository.AddCharacterAsync(character);
                }
                return characters;
            }
            catch
            {
                return await CharacterRepository.GetAllCharactersAsync(idBook);
            }
        }

        public async Task<IEnumerable<QuestionData>> GetQuestions()
        {
            try
            {
                var response = await HttpClient.GetAsync("User/Book/Character");
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception("Ошибка получения вопросов") { Data = { { "Content", errorContent } } };
                }

                var questions = await response.Content.ReadFromJsonAsync<IEnumerable<QuestionData>>();
                await CharacterRepository.SaveAllQuestionsAsync(questions);
                return questions;
            }
            catch
            {
                throw new Exception("Не удалось получить вопросы. Проверьте подключение к интернету");
            }
        }
    }
}
