using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CustomersApp.Model;

public class CustomerService
{
    private CustomerDbContext _customerDbContext;

    public CustomerService(CustomerDbContext customerDbContext)
    {
        _customerDbContext = customerDbContext;

        // test data
        //MockDataGenerator.GenerateTestData().ForEach(AddCustomer);
    }

    public List<Customer> FindAll() => _customerDbContext.Customers.ToList();

    public Customer? FindById(long id) => _customerDbContext.Customers.First(c => c.Id == id);

    public void AddCustomer(Customer customer)
    {
        _customerDbContext.Add(customer);
        _customerDbContext.SaveChanges();
    }

    public void DeleteCustomer(long id)
    {
        Customer customer = _customerDbContext.Customers.First(c => c.Id == id);
        _customerDbContext.Customers.Remove(customer);
        _customerDbContext.SaveChanges();
    }

    public void UpdateCustomer(Customer selectedCustomer)
    {
        Customer customer = _customerDbContext.Customers.First(c => c.Id == selectedCustomer.Id);
        _customerDbContext.Customers.Remove(customer);
        _customerDbContext.SaveChanges();
        selectedCustomer.Id = customer.Id;
        _customerDbContext.Customers.Add(selectedCustomer);
        _customerDbContext.SaveChanges();
    }

}