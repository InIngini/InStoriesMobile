namespace InStories.Core.Data.Entities
{
    /// <summary>
    /// Класс, представляющий изображения в галерее персонажей.
    /// </summary>
    public class BelongToGallery
    {
        /// <summary>
        /// Уникальный идентификатор изображения (может быть null).
        /// </summary>
        public int? PictureId { get; set; }

        /// <summary>
        /// Связанное изображение.
        /// </summary>
        public Picture Picture { get; set; }

        /// <summary>
        /// Идентификатор персонажа, к которому относится изображение.
        /// </summary>
        public int CharacterId { get; set; }

        /// <summary>
        /// Связанный персонаж.
        /// </summary>
        public Character Character { get; set; }
    }
}
