using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ExamSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Заддача 1");
            DateTime startTime = DateTime.Now;
            int start = 100;
            int end = 10000000;
            List<int> palindromes = new List<int>();

            Task<List<int>> task = Task.Run(() =>
            {
                return FindPalindromes(start, end);
            });

            palindromes = task.Result;

            string filePath = "palindromes.txt";
            File.WriteAllLines(filePath, palindromes.ConvertAll(p => p.ToString()));

            DateTime finish = DateTime.Now;

            var seconds = (finish - startTime).TotalSeconds;
            Console.WriteLine($"Вы потратили - {seconds} секунд");
            Console.WriteLine($"Найдено палиндромов: {palindromes.Count}");
            Console.WriteLine($"Палиндромы сохранены в: {filePath}");

        }


        static List<int> FindPalindromes(int start, int end)
        {
            List<int> palindromes = new List<int>();

            for (int i = start; i <= end; i++)
            {
                if (IsPalindrome(i))
                {
                    palindromes.Add(i);
                }
            }

            return palindromes;
        }

        static bool IsPalindrome(int number)
        {
            int original = number;
            int reversed = 0;

            while (number > 0)
            {
                int digit = number % 10;
                reversed = reversed * 10 + digit;
                number /= 10;
            }

            return original == reversed;
        }
    }
}