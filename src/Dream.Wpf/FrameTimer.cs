using System;
using System.Windows.Media;

namespace Dream.Wpf;

public static class FrameTimer
{
    private static TimeSpan _last;
    public static event Action<float>? Rendering;
    
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

        float deltaTime = (float)(args.RenderingTime - _last).TotalSeconds;
        _last = args.RenderingTime;

        Rendering?.Invoke(deltaTime);
    }
}
