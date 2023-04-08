using System;
using System.Windows.Input;

namespace CustomersApp.ViewModel.Commands;

public abstract class CommandBase : ICommand
{
    protected readonly CustomerListViewModel _customerListViewModel;

    public CommandBase(CustomerListViewModel customerListViewModel)
    {
        _customerListViewModel = customerListViewModel;
    }

    public virtual bool CanExecute(object? parameter)
    {
        return true;
    }
    
    public abstract void Execute(object? parameter);

    public event EventHandler? CanExecuteChanged;
}