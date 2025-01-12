namespace Курсач.Core.Data.Entities
{
    /// <summary>
    /// Класс, представляющий таймлайн.
    /// </summary>
    public class Timeline
    {
        /// <summary>
        /// Уникальный идентификатор временной шкалы.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название временной шкалы.
        /// </summary>
        public string NameTimeline { get; set; }

        /// <summary>
        /// Идентификатор книги, к которой относится временная шкала.
        /// </summary>
        public int BookId { get; set; }

        /// <summary>
        /// Связанная книга.
        /// </summary>
        public Book Book { get; set; }
    }
}
