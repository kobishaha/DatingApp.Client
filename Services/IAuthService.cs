using DatingApp.Client.Models;

namespace DatingApp.Client.Services
{
    public interface IAuthService
    {
        Task<AuthResult> Login(LoginModel loginModel);
        Task<AuthResult> Register(RegisterModel registerModel);
        Task Logout();

   
    }
}
