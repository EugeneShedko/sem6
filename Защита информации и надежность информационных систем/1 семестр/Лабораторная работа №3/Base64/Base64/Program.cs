using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laba2;

namespace Base64
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputString;
            string baseString;
            double shenonA;
            double shenonB;
            double hartleyA;
            double hartleyB;
            string name = "Eugene";
            string familyName = "Shedko";
            Console.WriteLine("Входная строка(a): " + (inputString = Base64.Reader("../../../../text.txt")));
            Console.WriteLine("Base 64(b): " + (baseString = Base64.ConvertToBase(inputString)));
            Console.WriteLine("Энтропия по Шенону(а): " +  (shenonA = Laba2.Task.Entropiya(inputString)));
            Console.WriteLine("Энтропия по Шенону(b): " + (shenonB = Laba2.Task.Entropiya(baseString)));
            Console.WriteLine("Энтропия по Хартли(a): " + (hartleyA = Laba2.Task.HartleyEntropy(26)));
            Console.WriteLine("Энтропия по Хартли(b): " + (hartleyB = Laba2.Task.HartleyEntropy(64)));
            Console.WriteLine("Избытачность(а): " + Operation.Redundancy(shenonA, hartleyA));
            Console.WriteLine("Избытачность(b): " + Operation.Redundancy(shenonB, hartleyB));
            Console.WriteLine("XOR(ASCII)" + Operation.XOR(Laba2.Task.ConvertToAscii(name), Laba2.Task.ConvertToAscii(familyName)));
            Console.WriteLine("XOR(base64)" + Operation.XOR(Base64.DecoderBase(Base64.ConvertToBase(name)), Base64.DecoderBase(Base64.ConvertToBase(familyName))));

        }
    }
}
