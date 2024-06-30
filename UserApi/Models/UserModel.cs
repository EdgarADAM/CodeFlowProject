namespace UserApi.Models
{
    public class UserModel
    {
        public string type { get; set; } = null!;
        public string userId { get; set; } = null!;
        public string password { get; set; } = null!;
        public string name { get; set; } = null!;
        public string emailAddress { get; set; } = null!;
        public string birthDate { get; set; } = null!;
        public string country { get; set; } = null!;
        public string phoneNumber { get; set; } = null!;
    }
}
