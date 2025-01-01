namespace Курсач.Data.Entities
{
    /// <summary>
    /// Класс, представляющий пользователя.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Уникальный идентификатор пользователя.
        /// </summary>
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
