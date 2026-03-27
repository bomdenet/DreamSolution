using System;
using System.Collections.Generic;

namespace Dream.Animations;

public interface ILayer<TObject> where TObject : class
{
    float Duration { get; }
}

internal interface IRuntimeLayer<TObject> : ILayer<TObject> where TObject : class
{
    void Apply(TObject obj, float time);
}

public sealed class Layer<TObject, TValue> : IRuntimeLayer<TObject>
    where TObject : class
    where TValue : struct
{
    public Func<TObject, TValue> Getter { get; }
    public Action<TObject, TValue> Setter { get; }
    public IReadOnlyList<Segment<TValue>> Segments { get; }

    public float Duration { get; }

    public Layer(Func<TObject, TValue> getter, Action<TObject, TValue> setter, params Segment<TValue>[] segments)
    {
        Getter = getter;
        Setter = setter;
        Segments = segments.AsReadOnly();

        foreach (Segment<TValue> segment in Segments)
            Duration += segment.Duration;
    }

    internal void Apply(TObject obj, float time)
    {
        //Setter(obj, );
        float remainingTime = time;
        foreach (Segment<TValue> segment in Segments)
        {
            if (remainingTime <= segment.Duration)
            {
                //TValue from = segment.From ?? Getter(obj);
                //TValue to = segment.Mode == SegmentMode.To ? segment.Value : InterpolatorRegistry.Get<TValue>().Interpolate(from, segment.Value, 1f);
                //TValue value = segment.Interpolator.Interpolate(from, to, remainingTime / segment.Duration);
                //Setter(obj, value);
                break;
            }
            else remainingTime -= segment.Duration;
        }
    }

    void IRuntimeLayer<TObject>.Apply(TObject obj, float time)
    {
        throw new NotImplementedException();
    }
}
