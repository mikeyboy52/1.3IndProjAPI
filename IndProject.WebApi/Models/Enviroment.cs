using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace IndProject.WebApi.Models
{
    public class Enviroment
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(25, MinimumLength = 1, ErrorMessage = "Naam moet tussen de 1 en 25 karakters zijn.")]
        [JsonPropertyName("Name")]
        public string Name { get; set; }
        [Required]
        [JsonPropertyName("Email")]
        public string Email { get; set; }
        [Required]
        [JsonPropertyName("MaxHeight")]
        public float MaxHeight { get; set; }
        [Required]
        [JsonPropertyName("MaxLength")]
        public float MaxLength { get; set; }

    }
}
