using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Курсач.Data.DTO;
using Курсач.Data.Entities;

namespace Курсач.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<User> RegisterUser(LoginData loginData)
        {
            var response = await _httpClient.PostAsJsonAsync("user/register", loginData);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<User>();
        }

        public async Task<string> LoginUser(LoginData loginData)
        {
            var response = await _httpClient.PostAsJsonAsync("user/login", loginData);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<dynamic>();
            return result.userToken;
        }

        public async Task<User> GetUser(int id)
        {
            var response = await _httpClient.GetAsync($"user/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<User>();
        }
    }
}
