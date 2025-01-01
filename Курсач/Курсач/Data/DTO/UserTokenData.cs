namespace Курсач.Data.DTO
{
    /// <summary>
    /// Данные о пользователе.
    /// </summary>
    public class UserTokenData
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Логин пользователя.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Токен аутентификации.
        /// </summary>
        public string Token { get; set; }
    }
}
