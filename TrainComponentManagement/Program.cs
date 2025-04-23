using Microsoft.EntityFrameworkCore;
using TrainComponentManagement.Data;
using TrainComponentManagement.Services;

namespace TrainComponentManagement;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();
        
        // --- Add DbContext ---
        builder.Services.AddDbContext<TrainComponentDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))); // Get connection string from appsettings.json

        builder.Services.AddScoped<ITrainComponentService, TrainComponentService>();
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapControllers();

        app.Run();

    }
}