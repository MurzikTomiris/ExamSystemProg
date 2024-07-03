namespace Task2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Parallel.For(1, 10001, i =>
            {
                string fileName = $"{i:D5}.txt";
                File.WriteAllText(fileName, fileName);
            });
        }
    }
}