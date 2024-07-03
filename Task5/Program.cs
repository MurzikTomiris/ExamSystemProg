using System.Diagnostics;

namespace Task5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Process> lst = new List<Process>();

            for (int i = 0; i < 5; i++)
            {
                lst.Add(StartProcess("calc"));
                lst.Add(StartProcess("Notepad.exe"));
            }
            Thread.Sleep(10000);

            //Console.ReadLine();
            foreach (Process p in lst)
            {
                p.Kill();
            }
            Thread.Sleep(10000);

        }

        static Process StartProcess(string Name)
        {
            var p = Process.Start(Name);
            return p as Process;
        }
    }
}