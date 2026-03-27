//using System.Collections.Generic;

//namespace Dream.Animations;

//internal sealed class AnimationInstance<TObject, TValue>(params Animation<TObject, TValue>[] animations)
//    where TObject : class
//    where TValue : struct
//{
//    public float Time { get; private set; } = 0f;
//    public IReadOnlyList<Animation<TObject, TValue>> Animations { get; } = animations.AsReadOnly();

//    //public bool IsPlaying { get; private set; }
//    //public bool IsCompleted { get; private set; }

//    public void Update(float deltaTime)
//    {
//        Time += deltaTime;
//        foreach (Layer<TObject, TValue> layer in Animations[0].Layers)
//        {
//            float time = float.Clamp(Time, 0f, layer.Duration);
//            foreach (Segment<TValue> segment in layer.Segments)
//            {
//                if (time <= segment.Duration)
//                {
//                    //TValue from = segment.From ?? layer.Getter();
//                    //TValue to = segment.Mode == SegmentMode.To ? segment.Value : InterpolatorRegistry.Get<TValue>().Interpolate(from, segment.Value, 1f);
//                    //TValue value = segment.Interpolator.Interpolate(from, to, time / segment.Duration);
//                    //layer.Setter(value);
//                    break;
//                }
//                else time -= segment.Duration;
//            }
//        }
//    }
//}
