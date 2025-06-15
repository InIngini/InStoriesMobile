using SQLite;

namespace InStories.Core.Data.Entities
{
    /// <summary>
    /// Класс, представляющий пользователя.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Уникальный идентификатор пользователя.
        /// </summary>
        [PrimaryKey]
        public int Id { get; set; }

        /// <summary>
        /// Логин пользователя для входа.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Пароль пользователя для входа.
        /// </summary>
        public string Password { get; set; }
    }
}
