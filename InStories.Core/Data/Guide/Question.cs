namespace InStories.Core.Data.Guide
{
    /// <summary>
    /// Класс, представляющий вопрос.
    /// </summary>
    public class Question
    {
        /// <summary>
        /// Уникальный идентификатор вопроса.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название или текст вопроса.
        /// </summary>
        public string QuestionText { get; set; }

        /// <summary>
        /// Идентификатор блока, к которому принадлежит вопрос.
        /// </summary>
        public int Block { get; set; }

        /// <summary>
        /// Связанный блок номеров.
        /// </summary>
        public NumberBlock NumberBlock { get; set; }
    }
}
