using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    public static class Tasks
    {
        public static int NOD(int a, int b)
        {
            int ostatoc;
            while(true)
            {
                if ((ostatoc = a % b) == 0)
                    return b;
                a = b;
                b = ostatoc;
            }
        }
        public static void Eratosfen(int aa, int bb)
        {
            List<int> numbers = GenerateList(bb);
            for(int i = 0; i < numbers.Count; i++)
            {
                for(int j = i + 1; j < numbers.Count; j++)
                {
                    if (numbers[j] % numbers[i] == 0)
                        numbers.RemoveAt(j);
                }
            }
            Print(numbers.Where(t => t >= aa).ToList());
        }
        private static void Print(List<int> numbers)
        {
            foreach (int i in numbers)
                Console.Write(i + " ");
            Console.WriteLine($"\nКоличество простых чисел: {numbers.Count}");
        }
        private static List<int> GenerateList(int length)
        {
            List<int> numbers = new List<int>();
            for(int i = 2; i<= length; i++)
            {
                numbers.Add(i);
            }
            return numbers;
        }
    }
}
