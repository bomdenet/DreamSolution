using System.Collections.Generic;

namespace Dream.Animations;

public sealed class Animation<TObject> where TObject : class
{
    public TObject Object { get; }
    public bool IsLooping { get; }
    public IReadOnlyList<ILayer<TObject>> Layers { get; }

    public float Duration { get; }

    public Animation(TObject obj, bool isLooping, params ILayer<TObject>[] layers)
    {
        Object = obj;
        IsLooping = isLooping;
        Layers = layers.AsReadOnly();

        foreach (ILayer<TObject> layer in Layers)
            Duration = float.Max(Duration, layer.Duration);
    }
}
