using Calculate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Task4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Раннее связывание");
            ClassCalculate reflexia = new ClassCalculate();
            double res = reflexia.CalculateCircleArea(3);
            Console.WriteLine(res.ToString());

            Console.WriteLine("Позднее связывание");
            Assembly assembly = Assembly.LoadFrom(@"C:\Tomiris\Courses\Step\ExamSystem\Task4\Calculate.dll");
            foreach (var item in assembly.GetTypes())
            {
                object link = Activator.CreateInstance(item);
                var method = item.GetMethod("CalculateCircleArea");
                object result = method.Invoke(link, new object[] { 3 });
                Console.WriteLine(result.ToString());

                var method2 = item.GetMethod("CalculateFactorial");
                object result2 = method.Invoke(link, new object[] { 5 });
                Console.WriteLine(result2.ToString());
                Console.ReadLine();

            }
            Console.ReadLine();
        } 
    }
}
