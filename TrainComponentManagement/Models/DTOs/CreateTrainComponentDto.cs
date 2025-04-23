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

    // Quantity might be set separately or initialized based on CanAssignQuantity
}