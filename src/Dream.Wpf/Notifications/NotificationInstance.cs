using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace Dream.Wpf.Notifications;

public interface INotificationHandle
{
    TimeSpan Time { get; }
    double Progress { get; }
    void Close();
}

public sealed partial class NotificationInstance : ObservableObject, INotificationHandle
{
    public NotificationBase Model { get; }
    public TimeSpan Time { get; private set; }
    [ObservableProperty]
    public partial double Progress { get; private set; }

    public NotificationInstance(NotificationBase model)
    {
        Model = model;
        model.Handle = this;
    }

    public void Close()
    {
        //NotificationService.Notifications.Remove(this);
    }

    //internal void Update(TimeSpan deltaTime)
    //{
    //    if (Model.Duration is null)
    //        return;
        
    //    Time += deltaTime;
    //    if (Time >= Model.Duration)
    //    {
    //        Time = Model.Duration.Value;
    //        Progress = 1;
    //        Close();
    //    }
    //    else Progress = Time.TotalSeconds / Model.Duration.Value.TotalSeconds;
    //}
}
