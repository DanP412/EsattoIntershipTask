using EsattoIntershipTask.Models;
using EsattoIntershipTask.Repositories;
using EsattoIntershipTask.Services.ConsoleService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace EsattoIntershipTask.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private static IConsoleService _consoleService;
        public CustomerRepository(IConsoleService consoleService)
        {
            _consoleService = consoleService;
        }

        private readonly List<Customer> _customers = new List<Customer>
        {
             new Customer
             {
                Name = "Janusz",
                VatId = 23,

                Adress = new Adress
                {
                    City = "Dąbrowa Tarnowska",
                    Country = "Poland",
                    Street = "Gruszowska",
                    ZipCode = "33-200"
                }
             },

            new Customer
            {
                Name = "Michał",
                VatId = 13,

                Adress = new Adress
                {
                    City = "Dąbrowa Górnicza",
                    Country = "Poland",
                    Street = "Mirosława Dzielskiego",
                    ZipCode = "32-145"
                }
            }
    };

        public List<Customer> GetCustomers()
        {
            return _customers.ToList();
        }
        public Customer GetSelectedCustomer(int VatId)
        {
            var customer = _customers.Single(c => c.VatId == VatId);

            return customer;
        }
        public void AddCustomer(Customer customer)
        {
            bool idAlreadyExist = _customers.Any(c => c.VatId == customer.VatId);

            if (idAlreadyExist)
            {
                throw new Exception("ID already Exist! ");
            }

            _customers.Add(customer);
        }
        public Customer GetCustomerDataToAdd(string nameMessage, string vatIdMessage, string countryMessage, string zipCodeMessage, string cityMessage, string streetMessage)
        {
            Console.WriteLine(nameMessage);
            string name = Console.ReadLine();

            int vatId = _consoleService.GetInteger(vatIdMessage);

            Console.WriteLine(countryMessage);
            string country = Console.ReadLine();

            Console.WriteLine(zipCodeMessage);
            string zipcode = Console.ReadLine();

            Console.WriteLine(cityMessage);
            string city = Console.ReadLine();

            Console.WriteLine(streetMessage);
            string street = Console.ReadLine();

            Customer newCustomer = new Customer
            {
                Name = name,
                VatId = vatId,

                Adress = new Adress
                {
                    Country = country,
                    ZipCode = zipcode,
                    City = city,
                    Street = street
                }
            };

            return newCustomer;
        }

        public void DeleteCustomer(int vatId)
        {
            bool isIExist = _customers.Any(c => c.VatId == vatId);

            if (!isIExist)
            {
                throw new Exception("Enterd ID does not Exist! ");
            }

            var customerToDelete = _customers.Single(c => c.VatId == vatId);
            _customers.Remove(customerToDelete);
        }

        public void EditSelectedCustomerProperty(Customer customer, int propertyIndex, object newValue)
        {
            List<object> custmerProperties = new List<object>();

            Type type = customer.GetType();
            Type adress = customer.Adress.GetType();

            foreach (var property in type.GetProperties())
            {

                if (property.Name != "CreationDate" && property.Name != "Adress")
                {
                    custmerProperties.Add(property);
                }

                if (property.Name == "Adress")
                {
                    foreach (var adressProperty in adress.GetProperties())
                    {
                        custmerProperties.Add(adressProperty);
                    }
                }
            }


            var selectedProperty = (PropertyInfo)custmerProperties[propertyIndex - 1];

            if ( selectedProperty.Name == "Name")
            {
                selectedProperty.SetValue(customer, newValue);
            }

            else if (selectedProperty.Name == "VatId")
            {
                bool parsingSuccesfull = Int32.TryParse(newValue.ToString(), out int intValue);

                if (parsingSuccesfull)
                {
                    selectedProperty.SetValue(customer, intValue);
                }
                else
                {
                    throw new ArgumentException("Invalid value for VatId!");
                }
            }

            else
            {
                var customerAdress = customer.Adress;
                var adressProperty = customerAdress.GetType().GetProperty(selectedProperty.Name);
                adressProperty.SetValue(customerAdress, newValue);
            }

        }
    }
}
