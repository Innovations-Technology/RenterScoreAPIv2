namespace RenterScoreAPIv2.Logging;

using System.Collections.Concurrent;
using System.Reflection;

public class CountDecorator<T> : DispatchProxy
{
    private T? _inner;
    private ILogger<T>? _logger;
    private static ConcurrentDictionary<string, int> _countDict = [];

    public static T Create(T inner, ILogger<T> logger)
    {
        T decorator = Create<T, CountDecorator<T>>();
        var proxy = decorator as CountDecorator<T>
            ?? throw new InvalidOperationException("Failed to create proxy instance.");
        proxy._inner = inner;
        proxy._logger = logger;
        return decorator;
    }

    protected override object? Invoke(MethodInfo? targetMethod, object?[]? args)
    {
        var fullMethodName = $"{targetMethod?.DeclaringType?.Name}.{targetMethod?.Name}";
        var count = _countDict.AddOrUpdate(fullMethodName, 1, (key, oldValue) => oldValue + 1);
        _logger!.LogInformation("[Count] {fullMethodName} has been called {Count} times", fullMethodName, count);
        return targetMethod?.Invoke(_inner, args);
    }
}
