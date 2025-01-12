using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Курсач.Common;
using Курсач.Core.Data.CommonModels;
using Курсач.Core.Data.DTO;
using Курсач.Core.Data.Entities;
using Курсач.Core.DB.Interfaces;
using Курсач.Core.Interfaces;

namespace Курсач.Core.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient HttpClient;
        private readonly IDatabaseManager DatabaseManager;

        public UserService(HttpClient httpClient, IDatabaseManager databaseManager)
        {
            HttpClient = httpClient;
            DatabaseManager = databaseManager;
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
            try
            {
                var response = await HttpClient.PostAsJsonAsync("user/login", loginData);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception("Ошибка входа") { Data = { { "Content", errorContent } } };
                }

                var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();

                var user = new User()
                {
                    Id = loginResponse.UserToken.UserId,
                    Login = loginResponse.UserToken.Login,
                    Password = loginResponse.UserToken.Password
                };

                await DatabaseManager.AddUserAsync(user);
                CommonData.Token = loginResponse.UserToken.Token;
            }
            catch
            {
                CommonData.Token = "token";
            }

            return CommonData.Token;
            
        }

        public async Task<User> GetUser(int id)
        {
            var response = await HttpClient.GetAsync($"user/{id}");
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception("Ошибка получения пользователя") { Data = { { "Content", errorContent } } };
            };
            return await response.Content.ReadFromJsonAsync<User>();
        }
    }
}
