using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]

namespace Dream.Wpf.Notifications;

public sealed class NotificationHost : Control
{
    public static ObservableCollection<NotificationHost> Hosts { get; } = [];

    private static readonly ObservableCollection<NotificationInstance> _notifications = [];
    public static ReadOnlyObservableCollection<NotificationInstance> Notifications { get; } = new(_notifications);

    static NotificationHost()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(NotificationHost),
            new FrameworkPropertyMetadata(typeof(NotificationHost)));

        FrameTimer.RenderingTimeSpan += Update;
    }

    public NotificationHost()
    {
        Loaded += (s, e) => Hosts.Add(this);
        Unloaded += (s, e) => Hosts.Remove(this);
    }

    private static void Update(TimeSpan deltaTime)
    {
        //foreach (NotificationInstance notification in Notifications)
        //    notification.Update(deltaTime);
    }

    public static INotificationHandle Show(NotificationBase notification)
    {
        NotificationInstance notificationInstance = new(notification);
        _notifications.Add(notificationInstance);
        return notificationInstance;
    }
}
