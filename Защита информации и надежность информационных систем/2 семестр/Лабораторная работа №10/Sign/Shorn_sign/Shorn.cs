using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Numerics;
using System.Security.Cryptography;

namespace Shorn_sign
{
    public static class ElGamal
    {
        public static BigInteger CalculateMd5Hash(string input)
        {
            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hash = md5.ComputeHash(inputBytes);
            return new BigInteger(hash.Concat(new byte[] { 0 }).ToArray());
        }
    }


    public static class Shnorr
    {
        public static void Do()
        {
            BigInteger p = 2267;
            BigInteger q = 103;

            string text = File.ReadAllText(".\\Test.txt");
            BigInteger g = 354;
            BigInteger obg = 967;
            int x = 30;

            BigInteger y = BigInteger.ModPow(obg, x, p);
            BigInteger a = BigInteger.Pow(g, 13) % p;
            BigInteger hash = ElGamal.CalculateMd5Hash(text + a.ToString());

            File.WriteAllText(".\\shnorr.txt", hash.ToString());
            BigInteger b = (13 + x * hash) % q;
            BigInteger dov = BigInteger.ModPow(g, b, p);
            BigInteger X = (dov * BigInteger.ModPow(y, hash, p)) % p;
            BigInteger hash2 = ElGamal.CalculateMd5Hash((text + X.ToString()));

            var f = hash == hash2;
            Console.WriteLine(f);
            string text2 = File.ReadAllText(".\\FakeTest.txt");
            BigInteger hash3 = ElGamal.CalculateMd5Hash((text2 + X.ToString()));
            var f2 = hash == hash3;
            Console.WriteLine(f2);
        }
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine($"\nЭЦП на основе Шнорра\n");

            Console.InputEncoding = Encoding.ASCII;
            var t = DateTime.Now;
            Shnorr.Do();
            Console.WriteLine("Shnorr:" + (DateTime.Now - t));
            Console.ReadLine();
        }
    }
}
