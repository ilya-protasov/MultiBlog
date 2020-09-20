using System.Threading.Tasks;
using MultiBlog.Application.Dtos;
using MultiBlog.Application.Services.Account;

namespace MultiBlog.Application.Services
{
    public interface IAccountService
    {
        Task<AuthentificationData> Authentificate( string login, string password );
        Task<AuthentificationData> UpdateAccessToken( string refreshToken );
        Task<AuthentificationData> RegisterUser( RegisterUserDataDto registerUserDataDto );
    }
}
