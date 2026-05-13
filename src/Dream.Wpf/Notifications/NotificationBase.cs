using System;

namespace Dream.Wpf.Notifications;

public abstract record NotificationBase(string? Title, string? Message, TimeSpan? Duration, bool IsClosable = true)
{
    public INotificationHandle? Handle { get; internal set; }

    public bool IsAutoClose => Duration != null;
}
