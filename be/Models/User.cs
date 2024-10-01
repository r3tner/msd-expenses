using System.ComponentModel.DataAnnotations;

public class User
{
    [Required]
    public required string Id { get; set; }
    [Required]
    public required string Name { get; set; }
    public string? Email { get; set; }
}