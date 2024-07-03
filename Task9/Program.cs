using Microsoft.Extensions.Configuration;
using System.Threading;
using System.Timers;

namespace Task9
{
    internal class Program
    {
        private static int countdownSeconds;

        static void Main(string[] args)
        {            
            var config = new ConfigurationBuilder()
            .AddJsonFile(@"C:\Tomiris\Courses\Step\ExamSystem\Task9\appsettings.json", optional: false, reloadOnChange: true)
            .Build();

            string res = config.GetSection("CountdownSeconds").Value;
            countdownSeconds = int.Parse(res);

            Console.WriteLine($"Считано из конфига значение {countdownSeconds}");

            MyTimer timer = new MyTimer();
            Console.ReadLine();
        }
        class MyTimer
        {
            System.Timers.Timer timer;
            public MyTimer()
            {
                timer = new System.Timers.Timer();
                timer.Interval = 1000; 
                timer.Elapsed += new ElapsedEventHandler(Countdown);
                timer.Start();
            }

            void Countdown(object sender, ElapsedEventArgs e)
            {
                countdownSeconds--;

                if (countdownSeconds >= 0)
                {
                    Console.WriteLine($"До перегрузки компьютера осталось {countdownSeconds} сек");
                }
                else
                {
                    Console.WriteLine("Выполняю перегрузку компьютера...");
                    timer.Stop();
                }
            }
        }
            
    }
}