using System.Net.Http.Json;
using DatingApp.Client.Models;

namespace DatingApp.Client.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient("AuthAPI");
        }

        public async Task<AuthResult> Login(LoginModel loginModel)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginModel);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<AuthResult>();
                return result;
            }
            return new AuthResult { Success = false, Message = "Login failed" };
        }

        public async Task<AuthResult> Register(RegisterModel registerModel)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/register", registerModel);
            if (response.IsSuccessStatusCode)
            {
                return new AuthResult { Success = true, Message = "Registration successful" };
            }
            return new AuthResult { Success = false, Message = "Registration failed" };
        }

        public async Task Logout()
        {
            await _httpClient.PostAsync("api/auth/logout", null);
        }
    }
}
