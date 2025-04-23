using System.ComponentModel.DataAnnotations;

namespace TrainComponentManagement.Entities;

public class TrainComponent
{
    public int Id { get; set; } // Primary Key
    [Required]
    [MaxLength(100)]
    public  string Name { get; set; }
    [Required]
    [MaxLength(50)]
    public  string UniqueNumber { get; set; }
    public bool CanAssignQuantity { get; set; }
    public int? Quantity { get; set; } // Nullable int
}