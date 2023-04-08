namespace CustomersApp.ViewModel.Commands;

public class GeneratePdfCommand : CommandBase
{
    public GeneratePdfCommand(CustomerListViewModel customerListViewModel) : base(customerListViewModel)
    {
    }

    public override void Execute(object? parameter)
    {
        _customerListViewModel.GeneratePdf();
    }
}