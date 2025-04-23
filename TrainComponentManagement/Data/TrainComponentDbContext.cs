using Microsoft.EntityFrameworkCore;
using TrainComponentManagement.Entities;

namespace TrainComponentManagement.Data;

public class TrainComponentDbContext: DbContext
{
    public TrainComponentDbContext(DbContextOptions<TrainComponentDbContext> options) : base(options) { }
    
    public DbSet<TrainComponent> TrainComponents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TrainComponent>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e =>e.Name)
                .IsRequired()
                .HasMaxLength(100);
            
            // Configure UniqueNumber property (example)
            entity.Property(e => e.UniqueNumber)
                .IsRequired()
                .HasMaxLength(50);
            
            // Add a unique index for UniqueNumber
            entity.HasIndex(e => e.UniqueNumber)
                .IsUnique();
            // Optional: Explicitly configure remaining properties if desired
            // entity.Property(e => e.CanAssignQuantity).IsRequired();
            // entity.Property(e => e.Quantity); // Nullable by default for int?
        });
        
        // --- Call the Data Seeding Extension Method ---
        modelBuilder.Seed(); // This calls the method defined in ModelBuilderExtensions
    }
}