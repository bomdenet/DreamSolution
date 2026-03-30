using System;
using System.Collections.ObjectModel;

namespace Dream.Wpf.Notifications;

public static class NotificationService
{
    public static ObservableCollection<NotificationInstance> Notifications { get; } = [];

    static NotificationService()
    {
        FrameTimer.RenderingTimeSpan += Update;
    }

    private static void Update(TimeSpan deltaTime)
    {
        foreach (NotificationInstance notification in Notifications)
            notification.Update(deltaTime);
    }

    public static INotificationHandle Show(Notification notification)
    {
        NotificationInstance notificationInstance = new(notification);
        Notifications.Add(notificationInstance);
        return notificationInstance;
    }
}
