using System;

namespace HiNums
{
    class Program
    {
        static void Main(string[] args)
        {
            // Print the welcome message to the user.
            Console.WriteLine("Welcome to the number chooser. Please enter two numbers below, and they will be compared.");

            // Enter a loop wherein we will ask the user if they wish to compare two more numbers after
            // they complete the form once.
            do
            {
                HandleUserInput("Please enter number A.", "Please enter number B.", "Please enter a valid integer.");
                Console.WriteLine("Enter two more? Click X to exit; any other key to continue.");
            } while (Console.ReadKey().KeyChar != 'x');

            // Hold the console.
            Console.ReadKey();
        }

        /// <summary>
        /// Parse the user input to an integer.
        /// </summary>
        /// <param name="input">The user input to parse.</param>
        /// <param name="output">The variable to output the parsed integer to.</param>
        /// <param name="errorMessage">The error message to print when the user input is invalid.</param>
        /// <returns>A boolean indicating whether or not the parse operation was successful.</returns>
        protected static bool ParseUserInputToInt (string input, out int output, string errorMessage)
        {
            bool success = int.TryParse(input, out output);

            if (!success)
            {
                // If the input is invalid, print the error message.
                Console.WriteLine(errorMessage);
            }

            return success;
        }
        
        /// <summary>
        /// Ask the user two questions, await two numbers as a response, and then
        /// handle the numbers accordingly. The user is told if A is over B (and vice versa),
        /// if A is equal to B, and so forth.
        /// </summary>
        /// <param name="questionA">The string of the question to ask the user when collecting the first number.</param>
        /// <param name="questionB">The string of the question to ask the user when collecting the second number.</param>
        static void HandleUserInput (string questionA, string questionB, string errorMessage)
        {
            // Store two numbers.
            int response1 = 0;
            int response2 = 0;
            
            // Collect two numbers from the user. This prompt call will not resolve until the passed
            // predicate returns `true`, so it will continue forever until the user enters a valid number.
            Prompt(x => ParseUserInputToInt(x, out response1, errorMessage), questionA);
            Prompt(x => ParseUserInputToInt(x, out response2, errorMessage), questionB);

            // We need to handle the numbers accordingly, pretty-printing the comparison to the user.
            if (response1 > response2) Console.WriteLine("A is greater than B.");
            else if (response1 == response2) Console.WriteLine("A is equal to B.");
            else if (response1 < response2) Console.WriteLine("B is greater than A.");

            // This should never happen, but just in case it does...
            else throw new Exception("Invariant");
        }

        /// <summary>
        /// Prompt the user with a question and await a response.
        /// Also tests each response against a predicate, continuing to ask the question
        /// when the result does not meet the predicate's requirements.
        /// </summary>
        /// <param name="predicate">The function to test each value on.</param>
        /// <param name="question">The question to ask.</param>
        /// <returns>The selected value from the predicate, in raw form.</returns>
        static string Prompt (Func<string, bool> predicate, string question)
        {
            while (true)
            {
                Console.WriteLine(question);
                string response = Console.ReadLine();

                if (predicate(response))
                {
                    return response;
                }
            }
        }
    }
}
