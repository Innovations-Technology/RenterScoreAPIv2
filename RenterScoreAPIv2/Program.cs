using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using RenterScoreAPIv2.ActionFilters;
using RenterScoreAPIv2.AutoMapper;
using RenterScoreAPIv2.EntityFramework;
using RenterScoreAPIv2.PropertyDetails;
using RenterScoreAPIv2.PropertyDetailsWithImages;
using RenterScoreAPIv2.PropertyImage;
using RenterScoreAPIv2.Tab;
using RenterScoreAPIv2.UserProfile;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(optionsBuilder =>
    optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("CONNECTIONSTRING_DEFAULT")));
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
builder.Services.AddScoped<IPropertyDetailsWithImagesService, PropertyDetailsWithImagesService>();
builder.Services.AddScoped<PropertyDetailsWithImagesService>();
builder.Services.AddScoped<ITabFactory, TabFactory>();
builder.Services.AddScoped<UserProfileRepository>();
builder.Services.AddScoped<IUserProfileRepository, LoggingUserProfileRepositoryDecorator>();
builder.Services.AddScoped<IUserProfileService, UserProfileService>();

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