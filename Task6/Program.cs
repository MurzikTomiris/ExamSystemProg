using System.Numerics;

namespace Task6
{
    internal class Program
    {
        static void Main(string[] args)
        {
             

            for (int i = 5; i <= 100; i += 5)
            {
                int factorial = CalculateFactorial(i);
                string fileName = $"{i}.txt";
                File.WriteAllText(fileName, factorial.ToString());

                Console.WriteLine($"Факториал числа {i} - {factorial}");
            }

        }

        static int CalculateFactorial(int n)
        {
            int result = 1;
            for (int i = 2; i <= n; i++)
            {
                result *= i;
            }
            return result;
        }
    }
}