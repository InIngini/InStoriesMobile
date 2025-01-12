namespace Курсач.Core.Data.Guide
{
    /// <summary>
    /// Класс, представляющий пол.
    /// </summary>
    public class Sex
    {
        /// <summary>
        /// Уникальный идентификатор пола.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название пола (например, "М", "Ж", "Не указан").
        /// </summary>
        public string Name { get; set; }
    }
}
