namespace InStories.Core.Data.Entities
{
    /// <summary>
    /// Класс, представляющий связь между персонажем и событием.
    /// </summary>
    public class BelongToEvent
    {
        /// <summary>
        /// Уникальный идентификатор персонажа.
        /// </summary>
        public int CharacterId { get; set; }

        /// <summary>
        /// Связанный персонаж.
        /// </summary>
        public Character Character { get; set; }

        /// <summary>
        /// Уникальный идентификатор события.
        /// </summary>
        public int EventId { get; set; }

        /// <summary>
        /// Связанное событие.
        /// </summary>
        public Event Event { get; set; }
    }

}
