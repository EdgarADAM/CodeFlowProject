namespace Common.Models
{
    public class SQLConfig
    {
        public string server { get; set; } = null!;
        public string dB { get; set; } = null!;
        public string user { get; set; } = null!;
        public string password { get; set; } = null!;
        public string timeOut { get; set; } = null!;
        public string iSecurity { get; set; } = null!;
        public string trustServerCertificate { get; set; } = null!;
    }
    
}
