using System.ComponentModel.DataAnnotations;
using static Infinite.HealthCare.ProjectApi.Models.User;

namespace Infinite.HealthCare.ProjectApi.Models
{


    public class User : LoginModel
    {
        public int Id { get; set; }
        [Required]
        public string EmailId { get; set; }
        [Required]

        public string Role { get; set; }
    }
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }

}



    
