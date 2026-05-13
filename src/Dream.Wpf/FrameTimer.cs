using System;
using System.Windows.Media;

namespace Dream.Wpf;

public static class FrameTimer
{
    private static TimeSpan _last;

    public static TimeSpan DeltaTimeSpan { get; private set; }
    public static double DeltaTimeDouble { get; private set; }
    public static float DeltaTimeFloat { get; private set; }

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

        DeltaTimeSpan = args.RenderingTime - _last;
        DeltaTimeDouble = DeltaTimeSpan.TotalSeconds;
        DeltaTimeFloat = (float)DeltaTimeDouble;

        _last = args.RenderingTime;

        RenderingTimeSpan?.Invoke(DeltaTimeSpan);
        RenderingDouble?.Invoke(DeltaTimeDouble);
        RenderingFloat?.Invoke(DeltaTimeFloat);
    }
}
