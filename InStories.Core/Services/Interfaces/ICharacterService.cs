using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using InStories.Core.Data.DTO;
using InStories.Core.Data.Entities;

namespace InStories.Core.Services.Interfaces
{
    public interface ICharacterService
    {
        /// <summary>
        /// Создает нового персонажа
        /// </summary>
        Task<Character> CreateCharacter(Character character);

        /// <summary>
        /// Обновляет существующего персонажа
        /// </summary>
        Task<Character> UpdateCharacter(CharacterWithAnswers characterWithAnswers, int id);

        /// <summary>
        /// Удаляет персонажа по ID
        /// </summary>
        Task DeleteCharacter(int id);

        /// <summary>
        /// Получает персонажа с ответами по ID
        /// </summary>
        Task<CharacterWithAnswers> GetCharacter(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Получает всех персонажей книги
        /// </summary>
        Task<IEnumerable<CharacterAllData>> GetAllCharacters(int idBook, CancellationToken cancellationToken = default);

        /// <summary>
        /// Получает все вопросы
        /// </summary>
        Task<IEnumerable<QuestionData>> GetQuestions();
    }
}
