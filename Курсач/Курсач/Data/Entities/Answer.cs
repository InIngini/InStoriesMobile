using Курсач.Data.Guide;

namespace Курсач.Data.Entities
{
    /// <summary>
    /// Класс, представляющий ответ.
    /// </summary>
    public class Answer
    {
        /// <summary>
        /// Уникальный идентификатор персонажа, к которому относится ответ.
        /// </summary>
        public int CharacterId { get; set; }

        /// <summary>
        /// Связанный персонаж.
        /// </summary>
        public Character Character { get; set; }

        /// <summary>
        /// Уникальный идентификатор вопроса, к которому относится ответ.
        /// </summary>
        public int QuestionId { get; set; }

        /// <summary>
        /// Связанный вопрос.
        /// </summary>
        public Question Question { get; set; }

        /// <summary>
        /// Ответ к вопросу.
        /// </summary>
        public string AnswerText { get; set; }

    }
}
