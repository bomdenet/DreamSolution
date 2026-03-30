using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace Dream.Wpf.Notifications;

public interface INotificationHandle
{
    TimeSpan Time { get; }
    double Progress { get; }
    void Close();
}

public sealed partial class NotificationInstance(Notification model) : ObservableObject, INotificationHandle
{
    public Notification Model { get; init; } = model;
    public TimeSpan Time { get; private set; }
    [ObservableProperty]
    public partial double Progress { get; private set; }

    public void Close()
    {

    }

    internal void Update(TimeSpan deltaTime)
    {
        if (Model.Duration is not null)
        {
            Time += deltaTime;
            Progress = Time.TotalSeconds / Model.Duration.Value.TotalSeconds;
        }
    }
}
