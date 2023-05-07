using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Bogus.DataSets;
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

    public Customer? FindLast() => _customerDbContext.Customers.OrderByDescending(e => e.Id).FirstOrDefault();
    
    public void AddCustomer(Customer customer)
    {
        YearStats? stats = _customerDbContext.YearStats.FirstOrDefault(s => s.Year == DateTime.Now.Year);
        if (stats == null)
        {
            stats = new YearStats()
            {
                Year = DateTime.Now.Year,
                Count = 1,
            };
            _customerDbContext.YearStats.Add(stats);
            _customerDbContext.SaveChanges();
        }

        stats.Count++;
        long maxId = _customerDbContext.Customers.Max(c => c.Id);
        maxId++;
        customer.Id = maxId;
        _customerDbContext.YearStats.Update(stats);
        _customerDbContext.SaveChanges();
        string certificateNumber = $"{maxId}G{stats.Count}/{stats.Year}";
        customer.CertificateNumber = certificateNumber;
        _customerDbContext.Add(customer);
        _customerDbContext.SaveChanges();
    }

    public string CreateNewCertificateNumber()
    {
        YearStats? stats = _customerDbContext.YearStats.FirstOrDefault(s => s.Year == DateTime.Now.Year);
        if (stats == null)
        {
            stats = new YearStats()
            {
                Year = DateTime.Now.Year,
                Count = 1,
            };
            _customerDbContext.YearStats.Add(stats);
        }

        int count = stats.Count + 1;
        
        long maxId = _customerDbContext.Customers.Max(c => c.Id);
        maxId++;
        
        return $"{maxId}G{count}/{stats.Year}";
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