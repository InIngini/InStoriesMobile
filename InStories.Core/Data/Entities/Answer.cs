using InStories.Core.Data.Guide;
using SQLite;

namespace InStories.Core.Data.Entities
{
    /// <summary>
    /// Класс, представляющий ответ.
    /// </summary>
    public class Answer
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }  // Основной PK

        /// <summary>
        /// Уникальный идентификатор персонажа, к которому относится ответ.
        /// </summary>
        [Indexed]
        public int CharacterId { get; set; }

        /// <summary>
        /// Связанный персонаж.
        /// </summary>
        [Ignore]
        public Character Character { get; set; }

        /// <summary>
        /// Уникальный идентификатор вопроса, к которому относится ответ.
        /// </summary>
        [Indexed]
        public int QuestionId { get; set; }

        /// <summary>
        /// Связанный вопрос.
        /// </summary>
        [Ignore]
        public Question Question { get; set; }

        /// <summary>
        /// Ответ к вопросу.
        /// </summary>
        public string AnswerText { get; set; }

    }
}
