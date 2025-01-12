namespace Курсач.Core.Data.Entities
{
    /// <summary>
    /// Класс, представляющий книгу.
    /// </summary>
    public class Book
    {
        /// <summary>
        /// Уникальный идентификатор книги.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название книги.
        /// </summary>
        public string NameBook { get; set; }

        /// <summary>
        /// Уникальный идентификатор изображения, связанного с книгой (может быть null).
        /// </summary>
        public int? PictureId { get; set; }

        /// <summary>
        /// Связанное изображение книги.
        /// </summary>
        public Picture Picture { get; set; }
    }
}
