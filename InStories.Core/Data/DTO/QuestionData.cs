namespace InStories.Core.Data.DTO
{
    /// <summary>
    /// Данные вопроса.
    /// </summary>
    public class QuestionData
    {
        /// <summary>
        /// Идентификатор вопроса.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название вопроса.
        /// </summary>
        public string QuestionText { get; set; }

        /// <summary>
        /// Блок, к которому принадлежит вопрос (может быть null).
        /// </summary>
        public string Block { get; set; }

    }
}
