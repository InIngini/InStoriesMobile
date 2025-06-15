using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using InStories.Core.Data.DTO;
using InStories.Core.Data.Entities;

namespace InStories.Core.Services.Interfaces
{
    public interface ICharacterService
    {
        Task<Character> CreateCharacter(BookCharacterData bookCharacterData);
        Task<Character> GetCharacter(int id);
        Task UpdateCharacter(int id, CharacterWithAnswers characterWithAnswers);
        Task DeleteCharacter(int id);
    }
}
