namespace Курсач.Data.DTO
{
    /// <summary>
    /// Данные о записи в галерее.
    /// </summary>
    public class GalleryData
    {
        /// <summary>
        /// Идентификатор персонажа, к которому относится галерея.
        /// </summary>
        public int CharacterId { get; set; }

        /// <summary>
        /// Идентификатор изображения в галерее.
        /// </summary>
        public int PictureId { get; set; }

    }
}
