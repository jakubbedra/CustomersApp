namespace CustomersApp.ViewModel.Commands;

public class UpdateCustomerCommand : CommandBase
{
    public UpdateCustomerCommand(CustomerListViewModel customerListViewModel) : base(customerListViewModel)
    {
    }

    public override void Execute(object? parameter)
    {
        _customerListViewModel.UpdateCustomer();
    }
}