using Microsoft.EntityFrameworkCore;
using TrainComponentManagement.Data;
using TrainComponentManagement.Services;

namespace TrainComponentManagement;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: "_myAllowSpecificOrigins",
                policy =>
                {
                    policy.WithOrigins("http://localhost:4200") // Allow your Angular app's origin
                        .AllowAnyHeader() // Allow any standard header
                        .AllowAnyMethod() // Allow common HTTP methods (GET, POST, PUT, PATCH, DELETE etc.)
                        .WithExposedHeaders("X-Pagination"); // Expose the custom pagination header if you added it
                });
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();
        
        // --- Add DbContext ---
        builder.Services.AddDbContext<TrainComponentDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))); // Get connection string from appsettings.json

        builder.Services.AddScoped<ITrainComponentService, TrainComponentService>();
        
        var app = builder.Build();
        
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<TrainComponentDbContext>();
            db.Database.Migrate();
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        // 3. Use CORS middleware - **IMPORTANT: Place it BEFORE UseAuthorization and MapControllers**
        app.UseCors("_myAllowSpecificOrigins"); // Apply the policy you defined
        app.MapControllers();

        app.Run();

    }
}