using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using RenterScoreAPIv2.ActionFilters;
using RenterScoreAPIv2.AutoMapper;
using RenterScoreAPIv2.EntityFramework;
using RenterScoreAPIv2.PropertyDetails;
using RenterScoreAPIv2.PropertyDetailsWithImages;
using RenterScoreAPIv2.PropertyImage;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(optionsBuilder =>
    optionsBuilder.UseSqlServer(configuration.GetConnectionString("Default")));
builder.Services.AddControllers(options =>
{
    options.Filters.Add<LoggingActionFilter>();
}).AddJsonOptions(options =>
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower);
builder.Services.AddScoped<IPropertyDetailsRepository, LoggingPropertyRepositoryDecorator>();
builder.Services.AddScoped<PropertyDetailsRepository>();
builder.Services.AddScoped<PropertyImageRepository>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddScoped<LoggingActionFilter>();
builder.Services.AddScoped<PropertyDetailsWithImagesService>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapGet("/", context =>
    {
        context.Response.Redirect("/swagger/index.html");
        return Task.CompletedTask;
    });
}
app.MapControllers();
app.Run();