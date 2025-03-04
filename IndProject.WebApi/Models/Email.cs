using System.ComponentModel.DataAnnotations;

namespace IndProject.WebApi.Models
{
    public class Email
    {
        [Required]
        public string email;
    }
}
