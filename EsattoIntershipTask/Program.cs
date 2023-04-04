using CurrencyManager.ConsoleApp.Services.Consoles;
using CurrencyManager.ConsoleApp.Services.Menu;
using EsattoIntershipTask.Repositories;
using EsattoIntershipTask.Services.ConsoleService;
using EsattoIntershipTask.Services.MenuService;
using Ninject;
using System;
using System.Dynamic;

namespace EsattoIntershipTask
{
    public class Program
    {
        private static readonly IKernel _kernel = new StandardKernel();
        private static IMenuService _menuService;
        private static IConsoleService _consoleService;
        private static ICustomerRepository _customerRepository;

        static void Main(string[] args)
        {
            PerformDependecyInjectionBindings();



            while (true)
            {
                _menuService.DisplayOptions();
                int userOption = _menuService.GetOption();

                if (userOption == 1)
                {
                    Console.Clear();

                    var customers = _customerRepository.GetCustomers();
                    _menuService.DisplayCustomers(customers);
                }

                else if (userOption == 2)
                {
                    try
                    {
                        Console.Clear();

                        var userToAdd = _customerRepository.GetCustomerDataToAdd("Set Name: ", "Set VatId: ", "Set Country: ", "Set Zip Code: ", "Set City: ", "Set Street: ");
                        _customerRepository.AddCustomer(userToAdd);

                        _menuService.DisplayMessage("User added successfully!\n ");
                    }

                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }

                else if (userOption == 3)
                {
                    try
                    {
                        Console.Clear();

                        int vatId = _consoleService.GetInteger("Input User VatId to delete: ");
                        _customerRepository.DeleteCustomer(vatId);

                        _menuService.DisplayMessage("User deleted successfully!\n ");
                    }
                    catch (Exception e)
                    {

                        Console.WriteLine(e.Message);
                    }

                }

                else if (userOption == 4)
                {
                    try
                    {
                        Console.Clear();

                        int vatId = _consoleService.GetInteger("Input User VatId:\n ");
                        var selectedCustomer = _customerRepository.GetSelectedCustomer(vatId);

                        Console.Clear();

                        _menuService.DisplaySelectedCustomer(selectedCustomer);
                        int propertyOption = _consoleService.GetIntegerWithinRange("\nChoose property to edit: ", 1, 6);

                        string newValue = _consoleService.GetString("Enter new value: ");
                        _customerRepository.EditSelectedCustomerProperty(selectedCustomer, propertyOption, newValue);

                        _menuService.DisplayMessage("User edited successfully!\n ");
                    }
                    catch (Exception e)
                    {
                        Console.Clear();
                        Console.WriteLine($"{e.Message}\n");
                    }

                }
            }
        }

        private static void PerformDependecyInjectionBindings()
        {
            _kernel.Bind<IConsoleService>().To<ConsoleService>();
            _kernel.Bind<IMenuService>().To<MenuService>();
            _kernel.Bind<ICustomerRepository>().To<CustomerRepository>();

            _consoleService = _kernel.Get<IConsoleService>();
            _menuService = _kernel.Get<IMenuService>();
            _customerRepository = _kernel.Get<ICustomerRepository>();
        }
    }
}