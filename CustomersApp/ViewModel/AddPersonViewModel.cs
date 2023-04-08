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
    public DateTime? DateOfBirth { get; set; }
    public DateTime? DateOfDeath { get; set; }
    public DateTime? IssueDate { get; set; }
    
    public string? Sex { get; set; }
    
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
               Sex != null && Customer.Address != null && Customer.IssuedBy != null &&
               Customer.PlaceOfBirth != null && Customer.PlaceOfDeath != null &&
               DateOfBirth != null && DateOfDeath != null & IssueDate != null
            ;
    }

    public void AddCustomer()
    {
        if (ValidateCustomer())
        {
            Customer.DateOfBirth = ConvertFromDateTime(DateOfBirth);
            Customer.DateOfDeath = ConvertFromDateTime(DateOfDeath);
            Customer.IssueDate = ConvertFromDateTime(IssueDate);
            Customer.Sex = Sex.ToCharArray()[0];
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