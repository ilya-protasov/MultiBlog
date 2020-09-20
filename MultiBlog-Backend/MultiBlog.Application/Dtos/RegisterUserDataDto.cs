
namespace MultiBlog.Application.Dtos
{
    public struct RegisterUserDataDto
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public bool HasData()
        {
            return !string.IsNullOrEmpty( Login ) && !string.IsNullOrEmpty( Password );
        }
    }
}
