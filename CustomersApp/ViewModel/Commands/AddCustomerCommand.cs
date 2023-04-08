using System;
using System.Windows.Input;

namespace CustomersApp.ViewModel.Commands;

public class AddCustomerCommand : ICommand
{
    private AddPersonViewModel _viewModel;

    public AddCustomerCommand(AddPersonViewModel viewModel)
    {
        _viewModel = viewModel;
    }
    
    public void Execute(object? parameter)
    {
        _viewModel.AddCustomer();
    }

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public event EventHandler? CanExecuteChanged;
}