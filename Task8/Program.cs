using System.Collections.Concurrent;

namespace Task8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string searchPath = @"C:\Tomiris\Courses\Step\system prog";
            string outputFile = @"content.txt";

            DateTime startTime = DateTime.Now;

            BlockingCollection<string> fileContents = new BlockingCollection<string>();

            Task.Run(() =>
            {
                GetAllTxtFileContents(searchPath, fileContents);
            }).ContinueWith((task) =>
            {
                File.WriteAllLines(outputFile, fileContents);

                DateTime finishTime = DateTime.Now;
                TimeSpan executionTime = finishTime - startTime;

                Console.WriteLine($"Время исполнения: {executionTime.TotalMilliseconds} мс");
                Console.WriteLine($"Содержимое всех .txt файлов сохранено в {outputFile}");
            }).Wait(); 

            Console.ReadLine();
        }

        static void GetAllTxtFileContents(string searchPath, BlockingCollection<string> fileContents)
        {
            try
            {
                // Используем Parallel.ForEach для параллельного обхода файлов
                Parallel.ForEach(Directory.GetFiles(searchPath, "*.txt", SearchOption.AllDirectories), (file) =>
                {
                    try
                    {
                        string content = File.ReadAllText(file);
                        fileContents.Add(content);
                    }
                    catch (UnauthorizedAccessException e)
                    {
                        Console.WriteLine($"Ошибка доступа к файлу '{file}': {e.Message}");
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine($"Ошибка при чтении файла '{file}': {e.Message}");
                    }
                });
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine($"Ошибка доступа к директории '{searchPath}': {e.Message}");
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine($"Директория '{searchPath}' не найдена: {e.Message}");
            }
        }
    }
}