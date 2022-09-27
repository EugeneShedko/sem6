using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Task 1 //
            string latPath = "../../../../MyFileLat.txt";
            string slovPath = "../../../../MyFileSlov.txt";
            string binaryPath = "../../../../MyBinary.txt";
            Console.WriteLine("Латышский: " + Task.Entropiya(Task.TextReader(latPath)));
            Console.WriteLine("Словацкий: " + Task.Entropiya(Task.TextReader(slovPath)));
            // Task 2 //
            Console.WriteLine("Бинарный: " + Task.Entropiya(Task.ConvertToAscii(Task.TextReader(binaryPath))));
            // Task 3 //
            Console.WriteLine("Количество информации в полном имени(Латышский): " + Task.AmountOfInformation(Task.TextReader(latPath)));
            Console.WriteLine("Количество информации в полном имени(ASCII): " + Task.AmountOfInformationAscii(Task.TextReader(binaryPath)));
            // Task 4 //
            Console.WriteLine("Количество информации в полном имени(Латышский p=0,1): " + Task.AmountOfInformationWithProbability(0.1, Task.TextReader(latPath)));
            Console.WriteLine("Количество информации в полном имени(ASCII p=0,1): " + Task.AmountOfInformationWithProbabilityAscii(0.1, Task.TextReader(binaryPath)));
            Console.WriteLine("Количество информации в полном имени(Латышский p=0,5): " + Task.AmountOfInformationWithProbability(0.5, Task.TextReader(latPath)));
            Console.WriteLine("Количество информации в полном имени(ASCII p=0,5): " + Task.AmountOfInformationWithProbabilityAscii(0.5, Task.TextReader(binaryPath)));
            Console.WriteLine("Количество информации в полном имени(Латышский p=1): " + Task.AmountOfInformationWithProbability(1, Task.TextReader(latPath)));
            Console.WriteLine("Количество информации в полном имени(ASCII p=1): " + Task.AmountOfInformationWithProbabilityAscii(1, Task.TextReader(binaryPath)));

        }
    }
}
