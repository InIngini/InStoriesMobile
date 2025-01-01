namespace Курсач.Data.Entities
{
    /// <summary>
    /// Класс, представляющий персонажа книги.
    /// </summary>
    public class Character
    {
        /// <summary>
        /// Уникальный идентификатор персонажа.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя персонажа.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Идентификатор книги, к которой принадлежит персонаж.
        /// </summary>
        public int BookId { get; set; }

        /// <summary>
        /// Связанная книга, к которой принадлежит персонаж.
        /// </summary>
        public Book Book { get; set; }

        /// <summary>
        /// Уникальный идентификатор изображения, связанного с персонажем (может быть null).
        /// </summary>
        public int? PictureId { get; set; }

        /// <summary>
        /// Связанное изображение персонажа.
        /// </summary>
        public Picture Picture { get; set; }
    }
}
