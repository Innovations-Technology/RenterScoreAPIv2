using System.Diagnostics;

namespace RenterScoreAPIv2.PropertyDetails;

public class LoggingPropertyRepositoryDecorator(
    PropertyDetailsRepository inner,
    ILogger<LoggingPropertyRepositoryDecorator> logger) : IPropertyDetailsRepository
{
    private readonly PropertyDetailsRepository _inner = inner;
    private readonly ILogger<LoggingPropertyRepositoryDecorator> _logger = logger;

    public async Task<IEnumerable<PropertyDetails>> GetPropertiesWithUserProfilesAsync()
    {
        _logger.LogInformation("[Start] GetPropertiesWithUserProfilesAsync()");
        Stopwatch stopwatch = Stopwatch.StartNew();
        var result = await _inner.GetPropertiesWithUserProfilesAsync();
        stopwatch.Stop();
        _logger.LogInformation(
            "[End] GetPropertiesWithUserProfilesAsync() - {ElapsedMilliseconds} ms",
            stopwatch.ElapsedMilliseconds);
        return result;
    }

    public async Task<PropertyDetails?> GetPropertiesWithUserProfilesByIdAsync(long propertyId)
    {
        _logger.LogInformation("[Start] GetPropertiesWithUserProfilesByIdAsync()");
        Stopwatch stopwatch = Stopwatch.StartNew();
        var result = await _inner.GetPropertiesWithUserProfilesByIdAsync(propertyId);
        stopwatch.Stop();
        _logger.LogInformation(
            "[End] GetPropertiesWithUserProfilesByIdAsync() - {ElapsedMilliseconds} ms",
            stopwatch.ElapsedMilliseconds);
        return result;
    }
}
