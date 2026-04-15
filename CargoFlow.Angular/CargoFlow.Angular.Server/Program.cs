using CargoFlow.Application;
using CargoFlow.Infrastructure;


WebApplicationBuilder builder = WebApplication.CreateBuilder();
{
    builder.Services.AddControllers();

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

if(app.Environment.IsProduction())
{
    app.UseDefaultFiles();
    app.MapStaticAssets();
}

app.UseHttpsRedirection();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();