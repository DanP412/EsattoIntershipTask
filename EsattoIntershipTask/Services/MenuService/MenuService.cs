using EsattoIntershipTask.Models;
using EsattoIntershipTask.Services.ConsoleService;
using EsattoIntershipTask.Services.MenuService;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace CurrencyManager.ConsoleApp.Services.Menu
{
    public class MenuService : IMenuService
    {
        private readonly IConsoleService _consoleService;

        private List<string> _options = new List<string>
        {
            "Display customers",
            "Add new customer",
            "Delete customer",
            "Edit customer"

        };

        public MenuService(IConsoleService consoleService)
        {
            _consoleService = consoleService;
        }

        public void DisplayMessage(string message)
        {
            Console.Clear();
            Console.WriteLine(message);
        }


        public void DisplayOptions()
        {
            int number = 1;

            foreach (var option in _options)
            {
                Console.WriteLine($"{number}. {option}");
                number++;
            }
        }

        public int GetOption()
        {
            int optionCount = _options.Count;
            int option = _consoleService.GetIntegerWithinRange("\nPodaj opcję: ", 1, optionCount);
            return option;
        }

        public void DisplayCustomers(List<Customer> customers)
        {
            int customerNumber = 1;

            Console.WriteLine();

            foreach (var customer in customers)
            {
                Console.WriteLine($"{customerNumber}.{customer.Name} - ID: {customer.VatId}");
                customerNumber++;
            }
            
            Console.WriteLine();
        }

        public void DisplayError(string errorMessage)
        {
            Console.WriteLine("Wystąpił błąd");
            Console.WriteLine($"* {errorMessage} *");
        }

        public void DisplaySelectedCustomer(Customer customer)
        {
            int listIndex = 1;
            Type type = customer.GetType();
            Type adress = customer.Adress.GetType();

            Console.WriteLine($"Customer:\n");

            foreach (var property in type.GetProperties())
            {
                var value = property.GetValue(customer, null);

                if (property.Name == "Adress")
                {
                    foreach (var adressProperty in adress.GetProperties())
                    {
                        var adressValue = adressProperty.GetValue(customer.Adress, null);
                        Console.WriteLine($"{listIndex}. {adressProperty.Name}: {adressValue}");
                        listIndex++;
                    }
                }

                else if (property.Name == "CreationDate")
                {
                    Console.WriteLine($"{property.Name}: {value}\n");
                }

                else
                {
                    Console.WriteLine($"{listIndex}. {property.Name}: {value}");
                    listIndex++;
                }
            }
        }

    }
}
