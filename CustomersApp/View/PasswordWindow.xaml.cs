using System.Windows;
using System.Windows.Controls;
using CustomersApp.ViewModel;

namespace CustomersApp.View;

public partial class PasswordWindow : Window
{
    public PasswordWindow()
    {
        InitializeComponent();
        
        PasswordViewModel viewModel = (PasswordViewModel)MainGrid.DataContext;
        viewModel.Window = this;
    }
    
}