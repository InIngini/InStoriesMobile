using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Курсач.Data.DTO;
using Курсач.Data.Entities;

namespace Курсач.Core.Interfaces
{
    public interface ICharacterService
    {
        Task<Character> CreateCharacter(BookCharacterData bookCharacterData);
        Task<Character> GetCharacter(int id);
        Task UpdateCharacter(int id, CharacterWithAnswers characterWithAnswers);
        Task DeleteCharacter(int id);
    }
}
