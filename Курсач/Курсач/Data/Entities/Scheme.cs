namespace Курсач.Data.Entities
{
    /// <summary>
    /// Класс, представляющий схему.
    /// </summary>
    public class Scheme
    {
        /// <summary>
        /// Уникальный идентификатор схемы.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название схемы.
        /// </summary>
        public string NameScheme { get; set; }

        /// <summary>
        /// Идентификатор книги, к которой относится схема.
        /// </summary>
        public int BookId { get; set; }

        /// <summary>
        /// Связанная книга.
        /// </summary>
        public Book Book { get; set; }
    }
}
