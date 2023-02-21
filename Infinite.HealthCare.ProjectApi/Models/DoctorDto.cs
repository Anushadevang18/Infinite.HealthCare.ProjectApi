namespace Infinite.HealthCare.ProjectApi.Models
{
    public class DoctorDto
    {
        public int Id { get; set; }
        public string DoctorName { get; set; }
        public string EmailId { get; set; }

        public string Qualification { get; set; }

        public int Experience { get; set; }
        public int SpecializationId { get; set; }
        public string SpecializationName { get; set; }
    }
}
