namespace RenterScoreAPIv2.Logging;

using System.Diagnostics;
using System.Reflection;

public class ElapsedTimeDecorator<T> : DispatchProxy
{
    private T? _inner;
    private ILogger<T>? _logger;

    public static T Create(T inner, ILogger<T> logger)
    {
        T decorator = Create<T, ElapsedTimeDecorator<T>>();
        var proxy = decorator as ElapsedTimeDecorator<T>
            ?? throw new InvalidOperationException("Failed to create proxy instance.");
        proxy._inner = inner;
        proxy._logger = logger;
        return decorator;
    }

    protected override object? Invoke(MethodInfo? targetMethod, object?[]? args)
    {
        var fullMethodName = $"{targetMethod?.DeclaringType?.Name}.{targetMethod?.Name}";
        _logger!.LogInformation("[Start] {fullMethodName}()", fullMethodName);
        Stopwatch stopwatch = Stopwatch.StartNew();
        var result = targetMethod?.Invoke(_inner, args);
        stopwatch.Stop();
        _logger!.LogInformation(
            "[End] {fullMethodName}() took {ElapsedMilliseconds} ms",
            fullMethodName,
            stopwatch.ElapsedMilliseconds);
        return result;
    }
}
