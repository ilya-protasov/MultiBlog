using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiBlog.Api.Dtos;
using MultiBlog.Api.Dtos.Account;
using MultiBlog.Application.Dtos;
using MultiBlog.Application.Services;

namespace MultiBlog.Api.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController( IAccountService accountService )
        {
            _accountService = accountService;
        }

        [HttpPost( "authorize" )]
        [AllowAnonymous]
        public async Task<ResponseDto<AuthorizationResponseDto>> Authorize( [FromBody] AuthorizationRequestDto requestDto )
        {
            var login = requestDto.Login;
            var password = requestDto.Password;

            if ( string.IsNullOrEmpty( login ) && string.IsNullOrEmpty( password ) )
            {
                return new ResponseDto<AuthorizationResponseDto>
                {
                    HttpStatus = 401,
                    ErrorInfo = "Login or password fields are empty"
                };
            }

            var authentificationData = await _accountService.Authentificate( login, password );

            if ( !authentificationData.IsSuccessCreated )
            {
                return new ResponseDto<AuthorizationResponseDto>
                {
                    HttpStatus = 401,
                    ErrorInfo = authentificationData.ErrorInfo
                };
            }

            return new ResponseDto<AuthorizationResponseDto>
            {
                HttpStatus = 200,
                Result = new AuthorizationResponseDto
                {
                    RefreshToken = authentificationData.RefreshToken,
                    AccessToken = authentificationData.AccessToken
                }
            };
        }

        [HttpPost("update-access-data")]
        [AllowAnonymous]
        public async Task<ResponseDto<AuthorizationResponseDto>> UpdateAccessToken( [FromBody] UpdateAccessDataDto updateAccessDataDto)
        {
            if ( string.IsNullOrEmpty( updateAccessDataDto.RefreshToken ) )
            {
                return new ResponseDto<AuthorizationResponseDto>
                {
                    HttpStatus = 401,
                    ErrorInfo = "Refresh token data not found or empty"
                };
            }

            var authentificationData = await _accountService.UpdateAccessToken( updateAccessDataDto.RefreshToken );

            if ( !authentificationData.IsSuccessCreated )
            {
                return new ResponseDto<AuthorizationResponseDto>
                {
                    HttpStatus = 401,
                    ErrorInfo = authentificationData.ErrorInfo
                };
            }

            return new ResponseDto<AuthorizationResponseDto>
            {
                HttpStatus = 200,
                Result = new AuthorizationResponseDto
                {
                    RefreshToken = authentificationData.RefreshToken,
                    AccessToken = authentificationData.AccessToken
                }
            };
        }

        [HttpPost("sign-up")]
        [AllowAnonymous]
        public async Task<ResponseDto<AuthorizationResponseDto>> SignUp( [FromBody] SignUpRequestDto signUpRequestDto )
        {
            var registerUserDataDto = new RegisterUserDataDto
            {
                Login = signUpRequestDto.Login,
                Password = signUpRequestDto.Password,
                FirstName = signUpRequestDto.FirstName,
                LastName = signUpRequestDto.LastName
            };

            if ( !registerUserDataDto.HasData() )
            {
                return new ResponseDto<AuthorizationResponseDto>
                {
                    HttpStatus = 401,
                    ErrorInfo = 
                    $"Login or password not found or empty: login-> { signUpRequestDto.Login }, password-> { signUpRequestDto.Password }"
                };
            }

            var authentifactionData = await _accountService.RegisterUser( registerUserDataDto );

            if ( !authentifactionData.IsSuccessCreated )
            {
                return new ResponseDto<AuthorizationResponseDto>
                {
                    HttpStatus = 401,
                    ErrorInfo = authentifactionData.ErrorInfo
                };
            }

            return new ResponseDto<AuthorizationResponseDto>
            {
                HttpStatus = 200,
                Result = new AuthorizationResponseDto
                {
                    RefreshToken = authentifactionData.RefreshToken,
                    AccessToken = authentifactionData.AccessToken
                }
            };
        }
    }
}