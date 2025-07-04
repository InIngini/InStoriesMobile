﻿namespace InStories.Core.Data.DTO
{
    /// <summary>
    /// Данные для входа пользователя.
    /// </summary>
    public class LoginData
    {
        /// <summary>
        /// Логин пользователя.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        public string Password { get; set; }

    }
}
