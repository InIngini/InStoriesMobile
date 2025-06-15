namespace InStories.Core.Data.Guide
{
    /// <summary>
    /// Класс, представляющий тип связи.
    /// </summary>
    public class TypeConnection
    {
        /// <summary>
        /// Уникальный идентификатор типа связи.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название типа связи (например: партнер, ребенок-родитель, сиблинг).
        /// </summary>
        public string Name { get; set; }
    }
}
