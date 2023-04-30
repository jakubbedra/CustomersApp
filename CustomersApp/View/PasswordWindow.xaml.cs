using System.Windows;
using System.Windows.Controls;
using CustomersApp.ViewModel;

namespace CustomersApp.View;

public partial class PasswordWindow : Window
{
    public PasswordWindow()
    {
        InitializeComponent();
    }

    private void OnPasswordCorrect(object sender, RoutedEventArgs e)
    {
        MainWindow window = new MainWindow();
        window.Show();
    }

    private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        if (this.DataContext != null)
        {
            ((PasswordViewModel)this.DataContext).Password = ((PasswordBox)sender).Password;
        }
    }
}