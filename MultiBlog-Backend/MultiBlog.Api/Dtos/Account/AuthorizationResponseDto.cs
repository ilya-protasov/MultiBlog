namespace MultiBlog.Api.Dtos.Account
{
    public struct AuthorizationResponseDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
