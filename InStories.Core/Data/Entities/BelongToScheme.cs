namespace InStories.Core.Data.Entities
{
    /// <summary>
    /// Класс, представляющий связь между схемой и связью.
    /// </summary>
    public class BelongToScheme
    {
        /// <summary>
        /// Уникальный идентификатор схемы.
        /// </summary>
        public int SchemeId { get; set; }

        /// <summary>
        /// Связанная схема.
        /// </summary>
        public Scheme Scheme { get; set; }

        /// <summary>
        /// Уникальный идентификатор связи.
        /// </summary>
        public int ConnectionId { get; set; }

        /// <summary>
        /// Связанная связь.
        /// </summary>
        public Connection Connection { get; set; }
    }

}
