using System.ComponentModel.DataAnnotations;

namespace IndProject.WebApi.Models
{
    public class Enviroment
    {
        [Required]
        public Guid Id;
        [Required]
        public string Name;
        [Required]
        public string UserId;
        [Required]
        public float MaxHeight;
        [Required]
        public float MaxLength;

    }
}
