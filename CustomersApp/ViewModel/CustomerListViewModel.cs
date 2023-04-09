using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Bogus.DataSets;
using CustomersApp.Model;
using CustomersApp.ViewModel.Commands;
using Microsoft.Win32;

namespace CustomersApp.ViewModel;

public class CustomerListViewModel : INotifyPropertyChanged
{
    public ObservableCollection<Customer> Customers { get; set; }

    public string CustomerFilter { get; set; }

    private CollectionView _collectionView;

    public CollectionView CollectionView
    {
        get
        {
            _collectionView = (CollectionView)CollectionViewSource.GetDefaultView(Customers);
            return _collectionView;
        }
    }

    private Customer? _selectedCustomer;

    public Customer? SelectedCustomer
    {
        get { return _selectedCustomer; }
        set
        {
            _selectedCustomer = value;
            if (_selectedCustomer != null)
                SelectedCustomerState = $"{_selectedCustomer.Name} {_selectedCustomer.Surname}";
            OnPropertyChanged(nameof(SelectedCustomer));
        }
    }

    private string _selectedCustomerState;

    public string SelectedCustomerState
    {
        get { return _selectedCustomerState; }
        set
        {
            _selectedCustomerState = value;
            OnPropertyChanged(nameof(SelectedCustomerState));
        }
    }

    public ICommand SortListViewCommand { get; set; }
    public ICommand RefreshCustomersCommand { get; set; }
    public ICommand DeleteCustomerCommand { get; set; }
    public ICommand GeneratePdfCommand { get; set; }
    public ICommand UpdateCustomerCommand { get; set; }

    private SearchCriteria _selectedSearchCriteria;

    public SearchCriteria SelectedSearchCriteria
    {
        get { return _selectedSearchCriteria; }
        set
        {
            _selectedSearchCriteria = value;
            OnPropertyChanged(nameof(SelectedSearchCriteria));
        }
    }

    private CustomerService _customerService;
    private PdfService _pdfService;

    private const string StateNoData = "Nie wybrano danych";

    public CustomerListViewModel()
    {
        SelectedSearchCriteria = SearchCriteria.NameAndSurname;
        CustomerFilter = string.Empty;
        _customerService = ServiceProvider.CustomerServiceInstance();
        _pdfService = ServiceProvider.PdfServiceInstance();
        SortListViewCommand = new SortListViewCommand(this);
        RefreshCustomersCommand = new RefreshCustomersCommand(this);
        DeleteCustomerCommand = new DeleteCustomerCommand(this);
        GeneratePdfCommand = new GeneratePdfCommand(this);
        UpdateCustomerCommand = new UpdateCustomerCommand(this);
        Customers = new ObservableCollection<Customer>(_customerService.FindAll());
        CollectionView.Filter = FilterCustomers;
        SelectedCustomerState = StateNoData;
    }

    public void SortCustomers(string columnName)
    {
        SortDescription newSortDescription = new SortDescription(columnName, ListSortDirection.Ascending);

        if (CollectionView.SortDescriptions.Count > 0)
        {
            SortDescription oldSortDescription = CollectionView.SortDescriptions[0];
            if (oldSortDescription.PropertyName.Equals(columnName))
            {
                newSortDescription = new SortDescription(
                    columnName,
                    oldSortDescription.Direction == ListSortDirection.Ascending
                        ? ListSortDirection.Descending
                        : ListSortDirection.Ascending
                );
            }
        }

        CollectionView.SortDescriptions.Clear();
        CollectionView.SortDescriptions.Add(newSortDescription);
    }

    private bool FilterCustomers(object obj)
    {
        if (obj is Customer customer)
        {
            if (CustomerFilter != null && CustomerFilter != "")
            {
                SelectedCustomerState = StateNoData;
            }

            switch (SelectedSearchCriteria)
            {
                case SearchCriteria.NameAndSurname:
                    return FilterNormalString(customer.Name) || FilterNormalString(customer.Surname) ||
                           FilterNormalString(customer.Name + " " + customer.Surname) ||
                           FilterNormalString(customer.Surname + " " + customer.Name);
                case SearchCriteria.CertificateNumber:
                    return FilterNormalString(customer.CertificateNumber);
                case SearchCriteria.DeathCertificateNumber:
                    return FilterNormalString(customer.DeathCertificateNumber);
                case SearchCriteria.Sex:
                    return FilterNormalString(customer.Sex.ToString());
                case SearchCriteria.Address:
                    return FilterNormalString(customer.Address);
                case SearchCriteria.PlaceOfBirth:
                    return FilterNormalString(customer.PlaceOfBirth);
                case SearchCriteria.PlaceOfDeath:
                    return FilterNormalString(customer.PlaceOfDeath);
                case SearchCriteria.IssuedBy:
                    return FilterNormalString(customer.IssuedBy);
                case SearchCriteria.DateOfBirth:
                    return FilterDate(customer.DateOfBirth);
                case SearchCriteria.DateOfDeath:
                    return FilterDate(customer.DateOfDeath);
                case SearchCriteria.IssueDate:
                    return FilterDate(customer.IssueDate);
                default:
                    return false;
            }
        }

        return false;
    }

    private bool FilterDate(DateOnly? date)
    {
        if (date == null)
        {
            return false;
        }

        DateTime dateTime;
        if (DateTime.TryParseExact(CustomerFilter, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out dateTime))
        {
            DateOnly filterDate = new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);
            return filterDate == date;
        }

        return false;
    }

    private bool FilterNormalString(string? value)
    {
        if (value == null)
        {
            return false;
        }

        // invariant culture did not seem to work, so here is a workaround
        //string normalizedFilter = new string(CustomerFilter).ToLower()
        //    .Replace('ą', 'a')
        //    .Replace('ć', 'c')
        //    .Replace('ę', 'e')
        //    .Replace('ł', 'l')
        //    .Replace('ó', 'o')
        //    .Replace('ś', 's')
        //    .Replace('ń', 'n')
        //    .Replace('ż', 'z')
        //    .Replace('ź', 'z');
        //string normalizedName = new string(value).ToLower()
        //    .Replace('ą', 'a')
        //    .Replace('ć', 'c')
        //    .Replace('ę', 'e')
        //    .Replace('ł', 'l')
        //    .Replace('ó', 'o')
        //    .Replace('ś', 's')
        //    .Replace('ń', 'n')
        //    .Replace('ż', 'z')
        //    .Replace('ź', 'z');
        //return normalizedName.Contains(normalizedFilter, StringComparison.InvariantCultureIgnoreCase);
        return value.Contains(CustomerFilter, StringComparison.InvariantCultureIgnoreCase);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void RefreshCustomers()
    {
        _collectionView.Refresh();
    }

    public void ReloadCustomers()
    {
        List<Customer> customerList = _customerService.FindAll();
        Customers.Clear();
        foreach (Customer customer in customerList)
        {
            Customers.Add(customer);
        }
    }

    public void DeleteSelectedCustomer()
    {
        if (_selectedCustomer != null)
        {
            MessageBoxResult result = MessageBox.Show(
                $"Czy na pewno chcesz usunąć zmarłego: {_selectedCustomer.Name} {_selectedCustomer.Surname} z bazy danych?",
                "Potwierdzenie usunięcia", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                _customerService.DeleteCustomer(_selectedCustomer.Id);
                RefreshCustomers();
                ReloadCustomers();
                SelectedCustomer = EmptyCustomer();
                SelectedCustomerState = StateNoData;
            }
        }
    }

    private Customer EmptyCustomer() => new Customer()
    {
        Id = -1,
        Name = "",
        Surname = "",
        CertificateNumber = "",
        Sex = ' ',
        DateOfBirth = null,
        PlaceOfBirth = "",
        DateOfDeath = null,
        PlaceOfDeath = "",
        IssueDate = null,
        DeathCertificateNumber = "",
        IssuedBy = "",
        Address = ""
    };

    public void GeneratePdf()
    {
        if (_selectedCustomer != null)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.Filter = "Pliki Dukumentów (*.pdf)|*.pdf|Wszystkie Pliki (*.*)|*.*";
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dialog.FileName = $"swiadectwo_{_selectedCustomer.Name}_{_selectedCustomer.Surname}.pdf";

            string path = "";
            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                path = dialog.FileName;
                _pdfService.GeneratePdf(_selectedCustomer, path);
            }
        }
        else
        {
            MessageBox.Show("Proszę wybrać dane zmarłego aby móc wygenerować plik PDF", "Nie wybrano danych",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    public void UpdateCustomer()
    {
        if (_selectedCustomer != null)
        {
            _customerService.UpdateCustomer(_selectedCustomer);
            SelectedCustomerState = $"{_selectedCustomer.Name} {_selectedCustomer.Surname}";
            MessageBox.Show("Zapisywanie zmian ukończone pomyślnie!", "Sukces", MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
        else
        {
            MessageBox.Show("Proszę wybrać dane zmarłego aby móc dokonać w nich zmian.", "Nie wybrano danych",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}