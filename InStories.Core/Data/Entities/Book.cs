using SQLite;

namespace InStories.Core.Data.Entities
{
    /// <summary>
    /// Класс, представляющий книгу.
    /// </summary>
    public class Book
    {
        /// <summary>
        /// Уникальный идентификатор книги.
        /// </summary>
        [PrimaryKey]
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
        [Ignore] 
        public Picture Picture { get; set; }
    }
}
