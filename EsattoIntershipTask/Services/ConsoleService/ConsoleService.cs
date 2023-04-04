using EsattoIntershipTask.Models;
using EsattoIntershipTask.Services.ConsoleService;
using System;
using System.Runtime.CompilerServices;

namespace CurrencyManager.ConsoleApp.Services.Consoles
{
    public class ConsoleService : IConsoleService
    {
        public string GetString(string message)
        {
            Console.Write(message);
            string stringFromUser = Console.ReadLine();

            return stringFromUser;
        }

        public int GetInteger(string message, string errorMessage = null)
        {
            while (true)
            {
                Console.Write(message);
                string stringFromUser = Console.ReadLine();

                bool parsingResult = int.TryParse(stringFromUser, out int integerFromUser);

                bool errorMessageWasPassed = errorMessage != null;

                if (parsingResult)
                {
                    return integerFromUser;
                }
                else if (errorMessageWasPassed)
                {
                    Console.WriteLine(errorMessage);
                }
            }
        }
        public int GetIntegerWithinRange(string message, int rangeFrom, int rangeTo)
        {
            while (true)
            {
                Console.Write($"{message} ({rangeFrom}-{rangeTo}) : ");

                string potentialInteger = Console.ReadLine();
                bool parsingSuccess = int.TryParse(potentialInteger, out int parsedInteger);
                bool isIntegerWithinRange = parsedInteger >= rangeFrom && parsedInteger <= rangeTo;

                if (parsingSuccess && isIntegerWithinRange)
                {
                    return parsedInteger;
                }
            }
        }

       
    }
}