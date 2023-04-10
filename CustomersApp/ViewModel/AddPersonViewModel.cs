using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using CustomersApp.Model;
using CustomersApp.View;
using CustomersApp.ViewModel.Commands;

namespace CustomersApp.ViewModel;

public class AddPersonViewModel : INotifyPropertyChanged
{
    public Customer Customer { get; set; }
    private CustomerService _customerService;

    private string _emptyFields;

    public string EmptyFields
    {
        get { return _emptyFields; }
        set
        {
            _emptyFields = value;
            OnPropertyChanged(nameof(EmptyFields));
        }
    }

    public ICommand AddCustomerCommand { get; set; }
    public AddPersonWindow Window { get; set; }
    public CustomerListViewModel CustomerList { get; set; }

    public AddPersonViewModel()
    {
        Customer = new Customer();
        EmptyFields = "";
        _customerService = ServiceProvider.CustomerServiceInstance();
        AddCustomerCommand = new AddCustomerCommand(this);
    }

    public List<string> ValidateCustomer()
    {
        List<string> validationErrors = new List<string>();
        if (Customer.Name == null)
        {
            validationErrors.Add("Imię");
        }

        if (Customer.Surname == null)
        {
            validationErrors.Add("Nazwisko");
        }

        if (Customer.CertificateNumber == null)
        {
            validationErrors.Add("Numer świadectwa");
        }

        if (Customer.DeathCertificateNumber == null)
        {
            validationErrors.Add("Numer aktu zgonu");
        }

        if (Customer.Sex != 'K' || Customer.Sex != 'M')
        {
            validationErrors.Add("Płeć");
        }

        if (Customer.Address == null)
        {
            validationErrors.Add("Adres");
        }

        if (Customer.IssuedBy == null)
        {
            validationErrors.Add("Wydanego przez");
        }

        if (Customer.PlaceOfBirth == null)
        {
            validationErrors.Add("Miejsce urodzenia");
        }

        if (Customer.PlaceOfDeath == null)
        {
            validationErrors.Add("Miejsce śmierci");
        }

        if (Customer.DateOfBirth == null)
        {
            validationErrors.Add("Data urodzenia");
        }

        if (Customer.DateOfDeath == null)
        {
            validationErrors.Add("Data śmierci");
        }

        if (Customer.IssueDate == null)
        {
            validationErrors.Add("Data wydania");
        }

        return validationErrors;
    }

    public void AddCustomer()
    {
        EmptyFields = "";
        List<string> validationErrors = ValidateCustomer();
        if (validationErrors.Count == 0)
        {
            _customerService.AddCustomer(Customer);
            MessageBox.Show("Dane zmarłego pomyślnie dodane do bazy.", "Sukces");
            CustomerList.ReloadCustomers();
            Window.Close();
        }
        else
        {
            EmptyFields = "Następujące pola nie zostały wypełnione: \n";
            for (var i = 0; i < validationErrors.Count; i++)
            {
                EmptyFields +=
                    $"{validationErrors[i]}{(i != validationErrors.Count - 1 ? ", " : "")}{(i == 8 ? "\n" : "")}";
            }
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