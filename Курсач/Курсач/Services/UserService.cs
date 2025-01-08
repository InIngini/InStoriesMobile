using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Курсач.Core.Interfaces;
using Курсач.Data.CommonModels;
using Курсач.Data.DTO;
using Курсач.Data.Entities;

namespace Курсач.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient HttpClient;
        private string Token;

        public UserService(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public async Task<User> RegisterUser(LoginData loginData)
        {
            var response = await HttpClient.PostAsJsonAsync("user/register", loginData);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception("Ошибка регистрации") { Data = { { "Content", errorContent } } };
            }

            return await response.Content.ReadFromJsonAsync<User>();
        }

        public async Task<string> LoginUser(LoginData loginData)
        {
            var response = await HttpClient.PostAsJsonAsync("user/login", loginData);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception("Ошибка входа") { Data = { { "Content", errorContent } } };
            }

            var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
            Token = loginResponse.UserToken.Token; // Сохранение токена
            return Token;
        }

        public async Task<User> GetUser(int id)
        {
            var response = await HttpClient.GetAsync($"user/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<User>();
        }

        public string GetToken()
        {
            return Token;
        }
    }
}
