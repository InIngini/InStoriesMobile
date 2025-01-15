using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Курсач.Common;
using Курсач.Core.Common;
using Курсач.Core.Data.CommonModels;
using Курсач.Core.Data.DTO;
using Курсач.Core.Data.Entities;
using Курсач.Core.DB.Interfaces;
using Курсач.Core.Services.Interfaces;

namespace Курсач.Core.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient HttpClient;
        private readonly IUserRepository UserRepository;
        private readonly IDatabaseSyncService DataBaseSyncService;

        public UserService(HttpClient httpClient, IUserRepository userRepository, IDatabaseSyncService dataBaseSyncService)
        {
            HttpClient = httpClient;
            UserRepository = userRepository;
            DataBaseSyncService = dataBaseSyncService;
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
            if (!NetworkInterface.GetIsNetworkAvailable())
            {
                CommonData.Token = "token"; // Установка токена по умолчанию
                return CommonData.Token;
            }

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

                await UserRepository.AddUserAsync(user);
                CommonData.Token = loginResponse.UserToken.Token;

                await DataBaseSyncService.SyncDatabasesAsync(user.Id);
            }
            catch
            {
                CommonData.Token = "token";
            }

            return CommonData.Token;
            
        }

        public async Task<User> GetUser()
        {
            var user = await UserRepository.GetUserAsync();
            
            return user;
        }
    }
}
