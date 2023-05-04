using System;
using System.Collections.Generic;
using Bogus;
using CustomersApp.Model;

namespace CustomersApp;

public class MockDataGenerator
{
    public static List<Customer> GenerateTestData()
    {
        List<Customer> customers = new List<Customer>();
        var customerFaker = new Faker<Customer>()
            //.RuleFor(c => c.Id, x => x.UniqueIndex)
            .RuleFor(c => c.Name, x => x.Person.FirstName)
            .RuleFor(c => c.Surname, x => x.Person.LastName)
            .RuleFor(c => c.CertificateNumber, x => x.Random.AlphaNumeric(10))
            .RuleFor(c => c.Sex, x => x.Random.Bool() ? 'K' : 'M')
            .RuleFor(c => c.DateOfBirth, x => x.Date.PastDateOnly())
            .RuleFor(c => c.PlaceOfBirth, x => x.Address.City())
            .RuleFor(c => c.DateOfDeath, x => x.Date.RecentDateOnly())
            .RuleFor(c => c.PlaceOfDeath, x => x.Address.City())
            .RuleFor(c => c.CremationDate, x => x.Date.RecentDateOnly())
            .RuleFor(c => c.DeathCertificateNumber, x => x.Random.AlphaNumeric(10))
            .RuleFor(c => c.IssuedBy, x => x.Company.CompanyName())
            .RuleFor(c => c.IssueDate, x => x.Date.RecentDateOnly())
            .RuleFor(c => c.Address, x => x.Company.Locale);
        for (int i = 0; i < 70000; i++)
        {
            var generate = customerFaker.Generate();
            customers.Add(generate);
        }

        return customers;
    }
}