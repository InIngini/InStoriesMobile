﻿namespace InStories.Core.Data.DTO
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

        // Ответы по личности
        /// <summary> Ответ 1 по личным качествам. </summary>
        public string[] Answers { get; set; }

    }
}
