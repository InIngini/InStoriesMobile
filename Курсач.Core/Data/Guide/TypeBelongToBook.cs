namespace Курсач.Core.Data.Guide
{
    /// <summary>
    /// Класс, представляющий тип принадлежности к книги.
    /// </summary>
    public class TypeBelongToBook
    {
        /// <summary>
        /// Уникальный идентификатор типа принадлежности.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название типа принадлежности (например: автор, соавтор).
        /// </summary>
        public string Name { get; set; }
    }
}
