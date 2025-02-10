// See https://aka.ms/new-console-template for more information
using System;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Write("Enter a date (yyyy-MM-dd) or type 'exit' to quit: ");
            string input = Console.ReadLine();

            if (input.ToLower() == "exit")
            {
                break;
            }

            if (DateTime.TryParse(input, out DateTime enteredDate))
            {
                DateTime currentDate = DateTime.Today;
                TimeSpan difference = enteredDate - currentDate;

                if (difference.Days > 0)
                {
                    Console.WriteLine($"The entered date is {difference.Days} days in the future.");
                }
                else if (difference.Days < 0)
                {
                    Console.WriteLine($"The entered date was {-difference.Days} days ago.");
                }
                else
                {
                    Console.WriteLine("The entered date is today!");
                }
            }
            else
            {
                Console.WriteLine("Invalid date format. Please enter the date in yyyy-MM-dd format.");
            }
        }
    }
}
