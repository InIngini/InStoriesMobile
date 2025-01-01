namespace Курсач.Data.Entities
{
    /// <summary>
    /// Класс, представляющий связь между временной шкалой и событием.
    /// </summary>
    public class BelongToTimeline
    {
        /// <summary>
        /// Уникальный идентификатор временной шкалы.
        /// </summary>
        public int TimelineId { get; set; }

        /// <summary>
        /// Связанная временная шкала.
        /// </summary>
        public Timeline Timeline { get; set; }

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
