namespace CustomersApp.ViewModel.Commands;

public class DeleteCustomerCommand : CommandBase
{
    public DeleteCustomerCommand(CustomerListViewModel customerListViewModel) : base(customerListViewModel)
    {
    }

    public override void Execute(object? parameter)
    {
        _customerListViewModel.DeleteSelectedCustomer();
    }
}