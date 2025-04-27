using System.ComponentModel.DataAnnotations;

namespace TrainComponentManagement.Models;

public class CreateTrainComponentDto
{
    [Required]
    [MaxLength(100)] // Example validation
    public  string Name { get; set; }

    [Required]
    [MaxLength(50)] // Example validation
    public  string UniqueNumber { get; set; }

    public bool CanAssignQuantity { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be a positive integer.")] // Ensures positive integer [cite: 11]
    public int Quantity { get; set; }
}