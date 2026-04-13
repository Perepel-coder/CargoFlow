using CargoFlow.Application;
using CargoFlow.Infrastructure;

namespace CargoFlow.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        {
            // Add services to the container.

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();

            builder.Services.AddInfrastructure();

            builder.Services.AddApplication();
        }

        WebApplication app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        if (app.Environment.IsProduction())
        {
            app.UseStaticFiles();
            builder.Services.AddControllersWithViews();
        }

        app.UseHttpsRedirection();

        app.MapControllers();

        app.MapGet("/", () => Results.Redirect("/customer/authorization/"));

        app.Run();
    }
}