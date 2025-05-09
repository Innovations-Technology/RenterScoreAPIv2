using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using RenterScoreAPIv2.ActionFilters;
using RenterScoreAPIv2.AutoMapper;
using RenterScoreAPIv2.EntityFramework;
using RenterScoreAPIv2.Logging;
using RenterScoreAPIv2.PropertyDetails;
using RenterScoreAPIv2.PropertyDetailsWithImages;
using RenterScoreAPIv2.PropertyImage;
using RenterScoreAPIv2.PropertyRating;
using RenterScoreAPIv2.Tab;
using RenterScoreAPIv2.UserProfile;
using RenterScoreAPIv2.Bookmark;

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
builder.Services.AddCountAndElapsedTimeLogging<IPropertyDetailsRepository, PropertyDetailsRepository>();
builder.Services.AddCountAndElapsedTimeLogging<IPropertyDetailsWithImagesService, PropertyDetailsWithImagesService>();
builder.Services.AddScoped<PropertyDetailsRepository>();
builder.Services.AddScoped<PropertyDetailsWithImagesService>();
builder.Services.AddScoped<PropertyImageRepository>();
builder.Services.AddScoped<RatingRepository>();
builder.Services.AddScoped<IRatingRepository, RatingRepository>();
builder.Services.AddScoped<IPropertyRatingService, PropertyRatingService>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddScoped<LoggingActionFilter>();
builder.Services.AddScoped<UserProfileRepository>();
builder.Services.AddScoped<IUserProfileRepository>(provider => 
{
    var repository = provider.GetRequiredService<UserProfileRepository>();
    var logger = provider.GetRequiredService<ILogger<LoggingUserProfileRepositoryDecorator>>();
    return new LoggingUserProfileRepositoryDecorator(repository, logger);
});
builder.Services.AddScoped<IUserProfileService, UserProfileService>();
builder.Services.AddScoped<ITabFactory, TabFactory>();
builder.Services.AddScoped<BookmarkRepository>();
builder.Services.AddScoped<IBookmarkRepository, BookmarkRepository>();

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