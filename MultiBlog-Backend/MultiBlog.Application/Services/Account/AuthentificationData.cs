namespace MultiBlog.Application.Services.Account
{
    public struct AuthentificationData
    {
        public bool IsSuccessCreated { get;set; }
        public string ErrorInfo { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
