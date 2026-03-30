using System;
using System.Windows.Media;

namespace Dream.Wpf;

public static class FrameTimer
{
    private static TimeSpan _last;
    public static event Action<TimeSpan>? RenderingTimeSpan;
    public static event Action<double>? RenderingDouble;
    public static event Action<float>? RenderingFloat;

    static FrameTimer() => Load();

    private static void Load() => CompositionTarget.Rendering += Update;
    private static void Update(object? sender, EventArgs e)
    {
        RenderingEventArgs args = (RenderingEventArgs)e;

        if (_last == TimeSpan.Zero)
        {
            _last = args.RenderingTime;
            return;
        }

        TimeSpan deltaTimeTimeSpan = args.RenderingTime - _last;
        double deltaTimeDouble = deltaTimeTimeSpan.TotalSeconds;
        float deltaTimeFloat = (float)deltaTimeDouble;

        _last = args.RenderingTime;

        RenderingTimeSpan?.Invoke(deltaTimeTimeSpan);
        RenderingDouble?.Invoke(deltaTimeDouble);
        RenderingFloat?.Invoke(deltaTimeFloat);
    }
}
