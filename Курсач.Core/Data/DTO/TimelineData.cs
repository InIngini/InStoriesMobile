namespace Курсач.Core.Data.DTO
{
    /// <summary>
    /// Данные таймлайна.
    /// </summary>
    public class TimelineData
    {
        /// <summary>
        /// Идентификатор книги, к которой относится таймлайн.
        /// </summary>
        public int BookId { get; set; }

        /// <summary>
        /// Название таймлайна.
        /// </summary>
        public string NameTimeline { get; set; }

    }
}
