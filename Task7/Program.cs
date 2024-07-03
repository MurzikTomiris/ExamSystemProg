namespace Task7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string searchPath = @"C:\Tomiris\Courses\Step\system prog";
            string dllsAllFilePath = @"dlls_all.txt";
            string dlls5NewFilePath = @"dlls_5_new.txt";

            DateTime startTime = DateTime.Now;

            Task<List<string>> getAllDllsTask = Task.Run(() =>
            {
                return GetAllDllFiles(searchPath);
            });

            try
            {
                List<string> allDlls = getAllDllsTask.Result;

                File.WriteAllLines(dllsAllFilePath, allDlls);

                var newestDlls = allDlls
                    .OrderByDescending(file => new FileInfo(file).LastWriteTime)
                    .Take(5)
                    .ToList();

                File.WriteAllLines(dlls5NewFilePath, newestDlls);

                DateTime finishTime = DateTime.Now;
                TimeSpan executionTime = finishTime - startTime;

                Console.WriteLine($"Время исполнения: {executionTime.TotalMilliseconds} мс");
                Console.WriteLine($"Файлы .dll сохранены в {dllsAllFilePath}");
                Console.WriteLine($"5 самых новых .dll файлов сохранены в {dlls5NewFilePath}");
            }
            catch (AggregateException ex)
            {
                foreach (var innerEx in ex.InnerExceptions)
                {
                    if (innerEx is UnauthorizedAccessException)
                    {
                        Console.WriteLine($"Ошибка доступа: {innerEx.Message}");
                    }
                    else
                    {
                        Console.WriteLine($"Ошибка: {innerEx.Message}");
                    }
                }
            }
            Console.ReadLine();
        }

        static List<string> GetAllDllFiles(string searchPath)
        {
            List<string> dllFiles = new List<string>();

            try
            {
                dllFiles = Directory.GetFiles(searchPath, "*.dll", SearchOption.AllDirectories).ToList();
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine($"Ошибка доступа: {e.Message}");
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine($"Директория не найдена: {e.Message}");
            }

            return dllFiles;
        }
    }
}
