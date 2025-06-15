namespace InStories.Core.Data.DTO
{
    /// <summary>
    /// Персонаж с ответами на вопросы по его личностным качествам, внешности, темпераменту и истории.
    /// </summary>
    public class CharacterWithAnswers
    {
        /// <summary>
        /// Идентификатор изображения персонажа (может быть null).
        /// </summary>
        public int? PictureId { get; set; }

        /// <summary>
        /// Имя персонажа.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Ответы
        /// </summary>
        public string[] Answers { get; set; }

    }
}
