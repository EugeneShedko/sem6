using System;

namespace lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            int numberOfTask;
            for (; ;)
            {
                Console.WriteLine("\n1). НОД");
                Console.WriteLine("2). Простые числа");
                numberOfTask = Convert.ToInt32(Console.ReadLine());
                switch (numberOfTask)
                {
                    case 1:
                        {
                            int a, b, c, number;
                            Console.WriteLine("Количество чисел: \n 1) 2 числа \n 2) 3 числа ");
                            number = Convert.ToInt32(Console.ReadLine());
                            switch (number)
                            {
                                case 1:
                                    {
                                        Console.WriteLine("Введите первое число: ");
                                        a = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("Введито второе число: ");
                                        b = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine($"НОД({a},{b}): {Tasks.NOD(a, b)}");
                                    }; break;
                                case 2:
                                    {
                                        Console.WriteLine("Введите первое число: ");
                                        a = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("Введито второе число: ");
                                        b = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("Введито третье число: ");
                                        c = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine($"НОД({a},{b},{c}): {Tasks.NOD(c, Tasks.NOD(a, b))}");
                                    };
                                    break;
                            }
                        }; break;
                    case 2: 
                        {
                            int aa, bb;
                            Console.WriteLine("Введите начало интервала: ");
                            aa = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Введите конец интервала: ");
                            bb = Convert.ToInt32(Console.ReadLine());
                            Tasks.Eratosfen(aa, bb);
                        }; break;
                }
            }
        }
    }
}
