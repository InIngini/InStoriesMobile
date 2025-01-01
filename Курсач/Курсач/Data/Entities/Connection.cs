namespace Курсач.Data.Entities
{
    /// <summary>
    /// Класс, представляющий связь между двумя персонажами.
    /// </summary>
    public class Connection
    {
        /// <summary>
        /// Уникальный идентификатор связи.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Тип связи между персонажами.
        /// </summary>
        public int TypeConnection { get; set; }

        /// <summary>
        /// Идентификатор первого персонажа в связи.
        /// </summary>
        public int Character1Id { get; set; }

        /// <summary>
        /// Связанный первый персонаж.
        /// </summary>
        public Character Character1 { get; set; }

        /// <summary>
        /// Идентификатор второго персонажа в связи.
        /// </summary>
        public int Character2Id { get; set; }

        /// <summary>
        /// Связанный второй персонаж.
        /// </summary>
        public Character Character2 { get; set; }
    }
}
