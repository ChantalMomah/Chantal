// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using System;
using System.Text.RegularExpressions;

class DateFormat
{
    static void Main(string[] args)
    {
        // Loop to continuously prompt the user for input
        while (true)
        {
            Console.Write("Enter a date in mm/dd/yyyy format (or 'exit' to quit): ");
            string? input = Console.ReadLine(); // Use nullable string

            // Exit the loop if the user types "exit"
            if (input != null && input.ToLower() == "exit")
            {
                break;
            }

            // Call the ReverseDateFormat method and display the result
            string result = ReverseDateFormat(input);
            Console.WriteLine(result);
        }
    }

    public static string ReverseDateFormat(string? dateInput) // Use nullable string
    {
        // Regex pattern to match the date format mm/dd/yyyy
        string pattern = @"^(?<mon>\d{1,2})/(?<day>\d{1,2})/(?<year>\d{2,4})$";
        string replacement = "${year}-${mon}-${day}";

        try
        {
            // Set a timeout for the regex operation
            TimeSpan timeout = TimeSpan.FromMilliseconds(100);
            Regex regex = new Regex(pattern, RegexOptions.None, timeout);

            // Validate the date input
            if (dateInput != null && regex.IsMatch(dateInput))
            {
                // Perform the replacement to convert the date format
                return regex.Replace(dateInput, replacement);
            }
            else
            {
                return "Invalid date format. Please use mm/dd/yyyy.";
            }
        }
        catch (RegexMatchTimeoutException)
        {
            return "Regex operation timed out. Please try again.";
        }
        catch (Exception ex)
        {
            return $"An error occurred: {ex.Message}";
        }
    }
}