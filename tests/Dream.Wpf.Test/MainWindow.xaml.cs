using Dream.Wpf.Notifications;
using System;
using System.Windows;

namespace Dream.Wpf.Test;

public partial class MainWindow : Window
{
    private static readonly Random _random = new();
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        int type = _random.Next(0, 4);

        MessageBox.Show($"Тип уведомления: {type}");

        if (type == 0)
            NotificationService.Show(Notification.CreateMessage("Привет!"));
        else if (type == 1)
            NotificationService.Show(Notification.CreateWarning("Привет!"));
        else if (type == 2)
            NotificationService.Show(Notification.CreateError("Привет!"));
        else if (type == 3)
            NotificationService.Show(NotificationSystemError.Create(new Exception("Привет!"), "Привет!"));

        MessageBox.Show($"Тип уведомления: {type}");
    }
}
