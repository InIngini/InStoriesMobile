using InStories.Core.Data.DTO;
using InStories.Core.Data.Entities;
using InStories.Core.Data.Guide;
using InStories.Core.DB.Interfaces;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace InStories.Core.DB
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly SQLiteAsyncConnection _database;

        public CharacterRepository()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DB.sqlite");
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Character>().Wait();
            _database.CreateTableAsync<Answer>().Wait();
            _database.CreateTableAsync<Picture>().Wait();
            _database.CreateTableAsync<QuestionData>().Wait();
        }

        public async Task AddCharacterAsync(Character character)
        {
            var characterInBD = await _database.Table<Character>().FirstOrDefaultAsync(x => x.Id == character.Id);

            if (characterInBD is null)
            {
                await _database.InsertAsync(character);

                var questions = await _database.Table<QuestionData>().ToListAsync();
                foreach (var question in questions)
                {
                    Answer answer = new Answer()
                    {
                        CharacterId = character.Id,
                        QuestionId = question.Id,
                        AnswerText = ""
                    };
                    await _database.InsertAsync(answer);
                }
            }
        }

        public async Task UpdateCharacterAsync(CharacterWithAnswers characterWithAnswers, int id)
        {
            // Получение персонажа из базы данных
            var character = await _database.Table<Character>().FirstOrDefaultAsync(x => x.Id == id);
            if (character == null)
            {
                throw new KeyNotFoundException();
            }
            if (characterWithAnswers.PictureId != null)
            {
                character.PictureId = characterWithAnswers.PictureId;
            }
            if (characterWithAnswers.Name != null)
            {
                character.Name = characterWithAnswers.Name;
            }
            // Обновление персонажа
            await _database.UpdateAsync(character);

            // Обновление блоков
            for (int i = 1; i <= characterWithAnswers.Answers.Length; i++)
            {
                if (characterWithAnswers.Answers[i - 1] != "")
                {
                    var answer = await _database.Table<Answer>().Where(a => a.CharacterId == character.Id && a.QuestionId == i).FirstOrDefaultAsync();
                    if (answer == null)
                    {
                        throw new KeyNotFoundException();
                    }
                    answer.AnswerText = characterWithAnswers.Answers[i - 1];
                    await _database.UpdateAsync(answer);
                }
            }
        }

        public async Task DeleteCharacterAsync(int id)
        {
            await _database.DeleteAsync<Character>(id);
            await _database.Table<Answer>().DeleteAsync(x => x.CharacterId == id);
        }

        public async Task<CharacterWithAnswers> GetCharacterAsync(int id)
        {
            var character = await _database.Table<Character>().FirstOrDefaultAsync(x => x.Id == id);
            if (character == null)
            {
                throw new KeyNotFoundException();
            }
            var answers = await _database.Table<Answer>().Where(a => a.CharacterId == character.Id).ToListAsync();
            var characterWithAnswers = new CharacterWithAnswers()
            {
                Name = character.Name,
            };
            characterWithAnswers.Answers = answers.Select(x => x.AnswerText).ToArray();

            return characterWithAnswers;
        }

        public async Task<IEnumerable<CharacterAllData>> GetAllCharactersAsync(int bookId)
        {
            var characters = await _database.Table<Character>().Where(c => c.BookId == bookId).ToListAsync();
            var charactersAllData = new List<CharacterAllData>();

            foreach (var character in characters)
            {
                var characterAllData = new CharacterAllData
                {
                    Id = character.Id,
                    Name = character.Name
                };
                if (character.PictureId != null)
                    characterAllData.PictureContent = (await _database.Table<Picture>().FirstOrDefaultAsync(x => x.Id == (int)character.PictureId))?.PictureContent;

                charactersAllData.Add(characterAllData);
            }
            return charactersAllData;
        }

        public async Task SaveAllQuestionsAsync(IEnumerable<QuestionData> question)
        {
            var inLocal = await _database.Table<QuestionData>().ToListAsync();

            if (inLocal is null || inLocal.Count == 0)
            {
                await _database.InsertAllAsync(question);
                return;
            }

            if (inLocal.Count() != question.Count())
            {
                await _database.Table<QuestionData>().DeleteAsync();
                await _database.InsertAllAsync(question);
                return;
            }
        }
    }
}