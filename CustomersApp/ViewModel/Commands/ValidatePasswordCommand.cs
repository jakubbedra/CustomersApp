using System;
using System.Windows.Input;

namespace CustomersApp.ViewModel.Commands;

public class ValidatePasswordCommand : ICommand
{
    private PasswordViewModel _viewModel;

    public ValidatePasswordCommand(PasswordViewModel viewModel)
    {
        _viewModel = viewModel;
    }
    
    public void Execute(object? parameter)
    {
        _viewModel.ValidatePassword();
    }

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public event EventHandler? CanExecuteChanged;
}