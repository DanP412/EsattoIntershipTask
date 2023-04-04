using EsattoIntershipTask.Models;

namespace EsattoIntershipTask.Services.ConsoleService
{
    public interface IConsoleService
    {
        int GetInteger(string message, string errorMessage = null);
        int GetIntegerWithinRange(string message, int rangeFrom, int rangeTo);
        string GetString(string message);
    }
}
