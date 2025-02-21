using System.ComponentModel.DataAnnotations;

namespace IndProject.WebApi;

public class User
{
    [Required]
    public Guid Id;
    [Required]
    public string? Username;
    [Required]
    public string? Password;
}
