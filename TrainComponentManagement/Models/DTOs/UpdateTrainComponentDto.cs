using System.ComponentModel.DataAnnotations;

namespace TrainComponentManagement.Models;

public class UpdateTrainComponentDto
{
    [Required(ErrorMessage = "Name is required.")] // Ensure Name is provided
    [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")] // Match entity configuration
    public  string Name { get; set; }
    
    [Required(ErrorMessage = "CanAssignQuantity field is required.")]
    public bool CanAssignQuantity { get; set; }
    
    [Range(1, int.MaxValue, ErrorMessage = "Quantity, if assigned, must be a positive integer.")] // Validate range if not null [cite: 11]
    public int? Quantity { get; set; } // Nullable int
}