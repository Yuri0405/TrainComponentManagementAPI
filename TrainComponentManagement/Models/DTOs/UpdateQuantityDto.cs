using System.ComponentModel.DataAnnotations;

namespace TrainComponentManagement.Models;

public class UpdateQuantityDto
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be a positive integer.")] // Ensures positive integer [cite: 11]
    public int Quantity { get; set; }
}