using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите размер массива");
            object n = Convert.ToInt32(Console.ReadLine());
            //
            Func<object, int[]> func1 = new Func<object, int[]>(GetArray);
            Task<int[]> task1 = new Task<int[]>(func1, n);
            ///
            Func<Task<int[]>, int> func2 = new Func<Task<int[]>, int>(SummArray);
            Task task2 = task1.ContinueWith(func2);
            ////
            Func<Task<int[]>, int> func3 = new Func<Task<int[]>, int>(MaxArray);
            Task task3 = task1.ContinueWith(func3);
            /////
            task1.Start();
            Console.ReadKey();
        }
        static int[] GetArray(object m)
        {
            int n = (int)m;
            int[] array = new int[n];
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                array[i] = random.Next(0, 10);
                Console.Write(array[i]);
            }
            return array;
        }
        static int SummArray(Task<int[]> task)
        {
            int[] array = task.Result;
            int sumarray = 0;
            for (int i = 0; i < array.Count(); i++)
            {
                sumarray = sumarray + array[i];
            }
            Console.WriteLine("SUM");
            Console.WriteLine("{0}", sumarray);
            return sumarray;
        }
        static int MaxArray(Task<int[]> task)
        {
            int[] array = task.Result;
            int max = array[0];
            foreach (int a in array)
            {
                if (a > max)
                {
                    max = a;
                }
            }
            Console.WriteLine();
            Console.WriteLine("MAX");
            Console.WriteLine(max);
            return max;
        }
    }
}
