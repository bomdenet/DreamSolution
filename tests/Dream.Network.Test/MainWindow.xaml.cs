using System.Windows;

namespace Dream.Network.Test;

public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(textBox.Text))
            return;


        textBox.Text = string.Empty;
    }
}
