using Microsoft.EntityFrameworkCore;
using RenterScoreAPIv2.AutoMapper;
using RenterScoreAPIv2.EntityFramework;
using RenterScoreAPIv2.Property;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(optionsBuilder => optionsBuilder.UseSqlServer(configuration.GetConnectionString("Default")));
builder.Services.AddControllers();
builder.Services.AddScoped<PropertyRepository>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
app.MapGet("api/v2/health", () => Results.Ok("ok")).WithName("HealthCheck").WithOpenApi();
app.Run();