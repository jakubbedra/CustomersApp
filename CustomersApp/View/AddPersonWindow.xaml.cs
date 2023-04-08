using System.Windows;
using CustomersApp.ViewModel;

namespace CustomersApp.View;

public partial class AddPersonWindow : Window
{
    public AddPersonWindow(CustomerListViewModel model)
    {
        InitializeComponent();
        
        AddPersonViewModel viewModel = (AddPersonViewModel)MainGrid.DataContext;
        viewModel.CustomerList = model;
        viewModel.Window = this;
    }
}