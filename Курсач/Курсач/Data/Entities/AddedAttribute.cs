namespace Курсач.Data.Entities
{
    /// <summary>
    /// Класс, представляющий добавленный атрибут.
    /// </summary>
    public class AddedAttribute
    {
        /// <summary>
        /// Уникальный идентификатор атрибута.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Номер ответа, связанный с атрибутом.
        /// </summary>
        public int NumberAnswer { get; set; }

        /// <summary>
        /// Название атрибута.
        /// </summary>
        public string NameAttribute { get; set; }

        /// <summary>
        /// Содержимое атрибута.
        /// </summary>
        public string ContentAttribute { get; set; }

        /// <summary>
        /// Идентификатор персонажа, к которому принадлежит атрибут.
        /// </summary>
        public int CharacterId { get; set; }

        /// <summary>
        /// Связанный персонаж.
        /// </summary>
        public Character Character { get; set; }
    }

}
