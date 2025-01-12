namespace Курсач.Core.Data.DTO
{
    /// <summary>
    /// Данные полного списка событии.
    /// </summary>
    public class EventAllData
    {
        /// <summary>
        /// Идентификатор события.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название события.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Время события.
        /// </summary>
        public string Time { get; set; }

    }
}
