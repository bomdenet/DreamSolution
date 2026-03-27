using System;

namespace Dream.Animations;

public enum SegmentMode
{
    To,
    By
}

public sealed class Segment<TValue> where TValue : struct
{
    public TValue? From { get; init; }
    public SegmentMode Mode { get; }
    public TValue Value { get; }
    public float Duration { get; }
    public IInterpolator<TValue> Interpolator { get; }

    private Segment(SegmentMode mode, TValue value, float duration, IInterpolator<TValue>? interpolator)
    {
        if (duration <= 0)
            throw new ArgumentOutOfRangeException(nameof(duration), "Duration must be greater than zero.");

        Mode = mode;
        Value = value;
        Duration = duration;
        Interpolator = interpolator ?? InterpolatorRegistry.Get<TValue>();
    }

    public static Segment<TValue> CreateTo(TValue to, float duration, IInterpolator<TValue>? interpolator = null) =>
        new(SegmentMode.To, to, duration, interpolator);
    public static Segment<TValue> CreateBy(TValue by, float duration, IInterpolator<TValue>? interpolator = null) =>
        new(SegmentMode.By, by, duration, interpolator);
    public static Segment<TValue> CreateFromTo(TValue from, TValue to, float duration, IInterpolator<TValue>? interpolator = null) =>
        new(SegmentMode.To, to, duration, interpolator) { From = from };
    public static Segment<TValue> CreateFromBy(TValue from, TValue by, float duration, IInterpolator<TValue>? interpolator = null) =>
        new(SegmentMode.By, by, duration, interpolator) { From = from };
}
