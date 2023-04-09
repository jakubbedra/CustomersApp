using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using CustomersApp.Model;
using CustomersApp.View;
using CustomersApp.ViewModel.Commands;

namespace CustomersApp.ViewModel;

public class AddPersonViewModel : INotifyPropertyChanged
{
    public Customer Customer { get; set; }
    private CustomerService _customerService;

    public ICommand AddCustomerCommand { get; set; }
    public AddPersonWindow Window { get; set; }
    public CustomerListViewModel CustomerList { get; set; }

    public AddPersonViewModel()
    {
        Customer = new Customer();
        _customerService = ServiceProvider.CustomerServiceInstance();
        AddCustomerCommand = new AddCustomerCommand(this);
    }

    public bool ValidateCustomer()
    {
        return Customer.Name != null && Customer.Surname != null &&
               Customer.CertificateNumber != null && Customer.DeathCertificateNumber != null &&
               Customer.Sex != null && Customer.Address != null && Customer.IssuedBy != null &&
               Customer.PlaceOfBirth != null && Customer.PlaceOfDeath != null &&
               Customer.DateOfBirth != null && Customer.DateOfDeath != null & Customer.IssueDate != null
            ;
    }

    public void AddCustomer()
    {
        if (ValidateCustomer())
        {
            _customerService.AddCustomer(Customer);
            MessageBox.Show("Dane zmarłego pomyślnie dodane do bazy.", "Sukces");
            CustomerList.ReloadCustomers();
            Window.Close();
        }
    }

    private DateOnly ConvertFromDateTime(DateTime? dateTime)
    {
        return new DateOnly(dateTime.Value.Year, dateTime.Value.Month, dateTime.Value.Day);
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}