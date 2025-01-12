namespace Курсач.Core.Data.DTO
{
    /// <summary>
    /// Данные полного списка персонажей книги.
    /// </summary>
    public class CharacterAllData
    {
        /// <summary>
        /// Идентификатор персонажа.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя персонажа.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Изображение персонажа в виде массива байтов (может быть null).
        /// </summary>
        public byte[] PictureContent { get; set; }

    }
}
