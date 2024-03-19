using HospitalProject.Entities.DbEntities;

namespace Hospital.WebUI.Models
{
    public class PatientProfileViewModel
    {
        //public string? Fullname { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PatientId { get; set; }
        //public string? Address { get; set; }
        public string? ImageUrl { get; set; }
        public ICollection<Recipe>? Recipes { get; set; }
    }
}