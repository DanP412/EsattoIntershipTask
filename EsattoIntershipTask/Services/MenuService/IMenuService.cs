using EsattoIntershipTask.Models;
using System.Collections.Generic;

namespace EsattoIntershipTask.Services.MenuService
{
    public interface IMenuService
    {
        void DisplayCustomers(List<Customer> customers);
        void DisplayError(string errorMessage);
        void DisplayMessage(string message);
        void DisplayOptions();
        void DisplaySelectedCustomer(Customer customer);
        int GetOption();
    }
}
