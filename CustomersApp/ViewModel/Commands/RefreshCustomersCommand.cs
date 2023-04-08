namespace CustomersApp.ViewModel.Commands;

public class RefreshCustomersCommand : CommandBase
{
    public RefreshCustomersCommand(CustomerListViewModel customerListViewModel) : base(customerListViewModel)
    {
    }

    public override void Execute(object? parameter)
    {
        _customerListViewModel.RefreshCustomers();
    }
}