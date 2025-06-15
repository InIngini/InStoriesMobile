using InStories.Core.Data.DTO;
using InStories.Core.Data.Entities;
using InStories.Core.Data.Guide;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InStories.Core.DB.Interfaces
{
    public interface ICharacterRepository
    {
        Task AddCharacterAsync(Character character);
        Task UpdateCharacterAsync(CharacterWithAnswers character, int id);
        Task DeleteCharacterAsync(int id);
        Task<CharacterWithAnswers> GetCharacterAsync(int id);
        Task<IEnumerable<CharacterAllData>> GetAllCharactersAsync(int bookId);
        Task SaveAllQuestionsAsync(IEnumerable<QuestionData> question);
    }
}

