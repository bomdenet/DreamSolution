using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Dream.Wpf.Notifications;

public sealed class NotificationHost : Control
{
    public ObservableCollection<NotificationInstance> Notifications => NotificationService.Notifications;

    static NotificationHost()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(NotificationHost),
            new FrameworkPropertyMetadata(typeof(NotificationHost)));
    }
}
