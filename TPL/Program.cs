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
            int n = Convert.ToInt32(Console.ReadLine());
            //
            Func<object, int[]> func1 = new Func<object, int[]>(GetArray);
            Task<int[]> task1 = new Task<int[]>(func1, 6);
            ///
            Action<Task<int[]>> func2 = new Action<Task<int[]>>(SummArray);
            Task task2 = task1.ContinueWith(func2);
            ////
            Action<Task<int[]>> action = new Action<Task<int[]>>(MaxArray);
            Task task3 = task2.ContinueWith(func1);
            /////
            task1.Start();
            Console.ReadKey();
        }
        static int[] GetArray(object m)
        {
            int n = (int)m;
            int[] array = new int[6];
            Random random = new Random();
            for (int i = 0; i < 6; i++)
            {
                array[i] = random.Next(0, 10);
            }
            return array;
        }
        static void SummArray(Task<int[]> task)
        {
            int[] array = task.Result;
            int sumarray = 0;
            for (int i = 0; i < 6 - 1; i++)
            {
                sumarray += array[i];
                Console.Write($"{array[i]} ");
            }
        }
        static void MaxArray(Task<int[]> task)
        {
            int[] array = task.Result;
            int max = array[0];
            foreach (int a in array)
            {
                if (a > max)
                    max = a;
                Console.Write($"{array[max]} ");
            }
        }
    }
}
