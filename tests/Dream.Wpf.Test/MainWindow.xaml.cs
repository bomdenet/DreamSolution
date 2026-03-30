using Dream.Wpf.Notifications;
using System.Windows;

namespace Dream.Wpf.Test;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        NotificationService.Show(Notification.CreateMessage("Привет!"));
    }
}
