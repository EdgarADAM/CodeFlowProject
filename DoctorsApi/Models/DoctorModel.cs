namespace DoctorsApi.Models
{
    public class DoctorModel
    {
        public string userId { get; set; } = null!;
        public string name { get; set; } = null!;
        public string emailAddress { get; set; } = null!;
        public string birthDate { get; set; } = null!;
        public string country { get; set; } = null!;
        public string phoneNumber { get; set; } = null!;
        public string speciality { get; set; } = null!;
        public string hospital { get; set; } = null!;
        public string address { get; set; } = null!;
    }
}
