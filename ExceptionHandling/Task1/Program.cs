using System;

namespace Task1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string input = Console.ReadLine();

            try
            {
                if (string.IsNullOrEmpty(input))
                    throw new ArgumentException("(input) cannot be null or empty");

                Console.WriteLine(input[0]);
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine("\nError: " + ex.Message + " \n");
            }
            finally
            {
                Console.WriteLine("\nExecution was finished!\n");
            }
        }
    }
}