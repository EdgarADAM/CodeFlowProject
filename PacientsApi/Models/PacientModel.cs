namespace PacientsApi.Models
{
    public class PacientModel
    {
        public string userId { get; set; } = null!;
        public string name { get; set; } = null!;
        public string emailAddress { get; set; } = null!;
        public string birthDate { get; set; } = null!;
        public string country { get; set; } = null!;
        public string phoneNumber { get; set; } = null!;
        public string height { get; set; } = null!;
        public string width { get; set; } = null!;
        public string allergies { get; set; } = null!;
    }
}
