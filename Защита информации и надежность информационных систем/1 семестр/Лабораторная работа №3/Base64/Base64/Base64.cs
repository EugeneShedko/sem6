using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base64
{
    static class Base64
    {
        static readonly char[] base64Table = {'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O',
                                                       'P','Q','R','S','T','U','V','W','X','Y','Z','a','b','c','d',
                                                       'e','f','g','h','i','j','k','l','m','n','o','p','q','r','s',
                                                       't','u','v','w','x','y','z','0','1','2','3','4','5','6','7',
                                                       '8','9','+','/','=' };
        static Dictionary<char, int> base64DIctionary = new Dictionary<char, int>()
        {
            {'A', 0 },{'B', 1 },{'C', 2 },{'D', 3 },{'E', 4 },{'F', 5 },{'G', 6 },{'H', 7 },{'I', 8 },{'J', 9 },{'K', 10 },{'L', 11 },{'M', 12 },{'N', 13 },{'O', 14 },{'P', 15 },{'Q', 16 },{'R', 17 },{'S', 18 },{'T', 19 },{'U', 20 },{'V', 21 },{'W', 22 },{'X', 23 },{'Y', 24 },{'Z', 25 },{'a', 26 },{'b', 27 },{'c', 28 },{'d', 29 },{'e', 30 },{'f', 31 },{'g', 32 },{'h', 33 },{'i', 34 },{'j', 35 },{'k', 36 },{'l', 37 },{'m', 38 },{'n', 39 },{'o', 40 },{'p', 41 },{'q', 42 },{'r', 43 },{'s', 44 },{'t', 45 },{'u', 46 },{'v', 47 },{'w', 48 },{'x', 49 },{'y', 50 },{'z', 51 },{'0', 52 },{'1', 53 },{'2', 54 },{'3', 55 },{'4', 56 },{'5', 57 },{'6', 58 },{'7', 59 },{'8', 60 },{'9', 61 },{'+', 62 },{'/', 63 },{'=', -1 }
        };
        public static string ConvertToBase(string data)
        {
            var arrayOfBinaryStrings = data.Select(x => Convert.ToString(x, 2).PadLeft(8, '0'));
            var count = arrayOfBinaryStrings.Count();
            var append = count % 3 == 1 ? "==" : count % 3 == 2 ? "=" : "";

            var allBytes = string.Join("", arrayOfBinaryStrings);
            var countOfBytes = allBytes.Count();
            var remOfDivision = countOfBytes % 6;
            var newList = Enumerable.Range(0, countOfBytes / 6).Select(x => allBytes.Substring(x * 6, 6)).ToList();

            if (remOfDivision != 0)
            {
                newList.Add(allBytes.Substring(countOfBytes / 6 * 6, remOfDivision).PadRight(6, '0'));
            }

            return (string.Join("", newList.Select(x => base64Table[Convert.ToByte(x, 2)])) + append);
        }
        public static string DecoderBase(string str)
        {
            return string.Join("", str.Where(x => x != '=').Select(x => Convert.ToString(base64DIctionary[x], 2).PadLeft(6, '0')));
        }
        public static string Reader(string path)
        {
            string str;
            using (StreamReader reader = new StreamReader(path))
            {
                str = reader.ReadToEnd();
            }
            return str;
        }
        public static void Writer(string str)
        {
            using (StreamWriter writer = new StreamWriter("../../../../textBase64.txt"))
            {
                writer.WriteLine(str);
            }
        }
    }
}
