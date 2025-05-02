namespace RenterScoreAPIv2.Logging;

public static class DIExtensions
{
    public static IServiceCollection AddCountAndElapsedTimeLogging<TService, TImplementation>(this IServiceCollection services) 
        where TService : class
        where TImplementation : notnull, TService
    {
        services.AddScoped(provider =>
        {
            var implementation = provider.GetRequiredService<TImplementation>();
            var serviceLogger = provider.GetRequiredService<ILogger<TService>>();
            var elapsedTimeDecorator = ElapsedTimeDecorator<TService>.Create(implementation, serviceLogger);
            var countDecorator = CountDecorator<TService>.Create(elapsedTimeDecorator, serviceLogger);
            return countDecorator;
        });
        return services;
    }
}