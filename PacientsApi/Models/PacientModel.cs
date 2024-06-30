namespace PacientsApi.Models
{
    public class PacientModel
    {
        public string userId { get; set; } = null!;
        public string height { get; set; } = null!;
        public string width { get; set; } = null!;
        public string allergies { get; set; } = null!;
        public string email { get; set; } = null!;
    }
}
