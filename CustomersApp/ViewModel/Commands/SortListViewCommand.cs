namespace CustomersApp.ViewModel.Commands;

public class SortListViewCommand : CommandBase
{
    public SortListViewCommand(CustomerListViewModel customerListViewModel) : base(customerListViewModel)
    {
    }

    public override void Execute(object? parameter)
    {
        if (parameter is string name)
        {
            _customerListViewModel.SortCustomers(name);
        }
    }
}