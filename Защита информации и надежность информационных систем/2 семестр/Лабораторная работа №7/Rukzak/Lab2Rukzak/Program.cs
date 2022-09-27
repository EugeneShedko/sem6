using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2Rukzak
{
    class Program
    {

        public static List<int> superPosled = new List<int>();

        public static List<int> publicKey = new List<int>();

        public static List<string> ascList = new List<string>();

        public static List<string> decAscList = new List<string>();

        public static List<int> message = new List<int>();

        public static List<int> encryptedMessage = new List<int>();

        public static List<int> decryptMessage = new List<int>();

        public static int rukzakLength = 8;

        static void Main(string[] args)
        {
            string messageText = "Eugene Shedko";
            int sum = 0;
            for (int i = 0; i < rukzakLength; i++)
            {
                sum = superPosled.Sum();
                superPosled.Add((1 + sum));
            }
            int n = superPosled.Sum() + 1;
            int a = 2;

            while (NOD(n, a) != 1)
            {
                a++;
            }

            for (int i = 0; i < superPosled.Count; i++)
            {
                publicKey.Add((a * superPosled[i]) % n);
            }
            Console.Write("Private key: ");
            foreach (var item in superPosled)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Public key: ");

            foreach (var item in publicKey)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Message: " + messageText);
            Console.WriteLine();
            for (int i =0; i < messageText.Length; i++)
            {
                if(Convert.ToString((int)messageText[i], 2).Length == 7)
                {
                    string asc = "0" + Convert.ToString((int)messageText[i], 2);
                    Console.WriteLine(messageText[i] + " - " + asc);
                    ascList.Add(asc);
                }
                else
                {
                    string asc = "00" + Convert.ToString((int)(int)messageText[i], 2);
                    Console.WriteLine(messageText[i] + " - " + asc);
                    ascList.Add(asc);
                }
            }
            foreach (string item in ascList)
            {
                int z = 0;
                for(int i = 0; i<item.Length; i++)
                {
                    int num = 0;
                    if (item[i] == '0')
                        num = 0;
                    else
                        num = 1;
                    z += num * publicKey[i];
                }
                encryptedMessage.Add(z);
            }
            Console.WriteLine();
            Console.Write("Encrypted text: ");
            foreach (var item in encryptedMessage)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();

            Console.Write("New numbers: ");
            foreach (int item in encryptedMessage)
            {
                decryptMessage.Add((modInverse(a, n) * item) % n);
            }
            foreach (var item in decryptMessage)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
            Console.WriteLine();
            superPosled.Reverse();
            foreach (var item in decryptMessage)
            {
                int tmp = 0;
                string decAsc = "";
                for (int i = 0; i < superPosled.Count; i++)
                {
                    if (superPosled[i] + tmp <= item)
                    {
                        decAsc += "1";
                        tmp += superPosled[i];
                    }
                    else
                    {
                        decAsc += "0";
                    }
                }
                decAscList.Add(ReverseString(decAsc));
            }

            Console.Write("ASCII codes: ");
            foreach (var item in decAscList)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Decrypt message: ");

            foreach (var item in decAscList)
            {
                Console.Write((char)Convert.ToInt32(item,2));
            }

            Console.ReadKey();

        }

        static int NOD(int a, int b)
        {
            while (a != b)
            {
                if (a > b)
                {
                    a = a - b;
                }
                else
                {
                    b = b - a;
                }
            }
            return a;
        }

        static int modInverse(int a, int m)
        {
            a = a % m;
            for (int x = 1; x < m; x++)
                if ((a * x) % m == 1)
                    return x;
            return 1;
        }

        public static string ReverseString(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }


    }
}
