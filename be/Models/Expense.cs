using System.ComponentModel.DataAnnotations;

public class Expense
{
    public required int Id { get; set; }
    [Required]
    public required string UserId { get; set; }
    [Required]
    public required string Name { get; set; }
    [Required]
    public required string Type { get; set; }
    [Required]
    public decimal Amount { get; set; }
    public string? Attachment { get; set; }
}