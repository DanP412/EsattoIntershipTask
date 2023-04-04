using EsattoIntershipTask.Models;
using System;
using System.Collections.Generic;

namespace EsattoIntershipTask.Repositories
{
    public interface ICustomerRepository
    {
        void AddCustomer(Customer customer);
        void DeleteCustomer(int vatID);
        List<Customer> GetCustomers();
        Customer GetSelectedCustomer(int VatId);
        void EditSelectedCustomerProperty(Customer customer, int selectedProperty, object newValue);
        Customer GetCustomerDataToAdd(string nameMessage, string vatIdMessage, string countryMessage, string zipCodeMessage, string cityMessage, string streetMessage);
    }
}
