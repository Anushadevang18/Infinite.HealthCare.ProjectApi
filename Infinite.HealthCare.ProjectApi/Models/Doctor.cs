namespace Infinite.HealthCare.ProjectApi.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string DoctorName { get; set; }
        public string EmailId { get; set; }

        public string Qualification { get; set; }
       

        public int Experience { get; set; }
        //navigation property
        public Specialization Specialization { get; set; }

        public int SpecializationId { get; set; }

    
    }
}
