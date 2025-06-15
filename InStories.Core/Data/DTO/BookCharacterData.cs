namespace InStories.Core.Data.DTO
{
    /// <summary>
    /// Данные для создания персонажа книги.
    /// </summary>
    public class BookCharacterData
    {
        /// <summary>
        /// Идентификатор книги.
        /// </summary>
        public int BookId { get; set; }

        /// <summary>
        /// Идентификатор изображения персонажа (может быть null).
        /// </summary>
        public int? PictureId { get; set; }

    }
}
