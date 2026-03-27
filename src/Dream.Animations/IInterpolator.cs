using System;
using System.Collections.Generic;

namespace Dream.Animations;

public interface IInterpolator<TValue>
{
    TValue Interpolate(TValue start, TValue end, float t);
}

public static class InterpolatorRegistry
{
    private static readonly Dictionary<Type, object> _map = [];

    static InterpolatorRegistry()
    {

    }

    public static void Register<T>(IInterpolator<T> interpolator) => _map[typeof(T)] = interpolator;
    public static IInterpolator<T> Get<T>()
    {
        if (_map.TryGetValue(typeof(T), out object? value))
            return (IInterpolator<T>)value;

        throw new Exception($"No interpolator for {typeof(T)}");
    }
}
