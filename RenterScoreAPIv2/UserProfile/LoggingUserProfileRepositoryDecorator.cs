using System.Diagnostics;

namespace RenterScoreAPIv2.UserProfile;

public class LoggingUserProfileRepositoryDecorator(
    IUserProfileRepository inner,
    ILogger<LoggingUserProfileRepositoryDecorator> logger) : IUserProfileRepository
{
    private readonly IUserProfileRepository _inner = inner;
    private readonly ILogger<LoggingUserProfileRepositoryDecorator> _logger = logger;

    public async Task<UserProfile?> GetUserProfileByUserIdAsync(long userId)
    {
        _logger.LogInformation("[Start] GetUserProfileByUserIdAsync({UserId})", userId);
        Stopwatch stopwatch = Stopwatch.StartNew();
        var result = await _inner.GetUserProfileByUserIdAsync(userId);
        stopwatch.Stop();
        _logger.LogInformation(
            "[End] GetUserProfileByUserIdAsync({UserId}) - {ElapsedMilliseconds} ms",
            userId,
            stopwatch.ElapsedMilliseconds);
        return result;
    }
} 