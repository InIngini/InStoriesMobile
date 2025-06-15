namespace InStories.Core.Data.Entities
{
    /// <summary>
    /// Класс, представляющий изображение.
    /// </summary>
    public class Picture
    {
        /// <summary>
        /// Уникальный идентификатор изображения.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Двоичные данные изображения.
        /// </summary>
        public byte[] PictureContent { get; set; }
    }
}
