using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Numerics;



namespace RSA_sign
{
    class RSA
    {
        public static readonly char[] characters = new char[] { '#', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '-' };

        //проверка: простое ли число?
        public bool IsTheNumberSimple(long n)
        {
            if (n < 2) return false;

            if (n == 2) return true;

            for (long i = 2; i < n; i++)
                if (n % i == 0) return false;

            return true;
        }


        //вычисление параметра e
        public int Calculate_e(int d, int m)
        {
            int e = 10;

            while (true)
            {
                if ((e * d) % m == 1) break;
                else e++;
            }
            return (int)e;
        }


        //вычисление параметра d. d должно быть взаимно простым с m
        public int Calculate_d(int m)
        {
            int d = m - 1;

            for (int i = 2; i <= m; i++)
                if ((m % i == 0) && (d % i == 0)) ///если имеют общие делители
                {
                    d--;
                    i = 1;
                }
            return d;
        }


        //зашифрование
        public List<string> RSA_Encode(string hash, int e, int n)
        {
            
            List<string> result = new List<string>();

            BigInteger bi;

            for (int i = 0; i < hash.Length; i++)
            {
                int index = Array.IndexOf(characters, hash[i]);

                bi = new BigInteger(index);
                bi = BigInteger.Pow(bi, (int)e);

                BigInteger n_ = new BigInteger((int)n);

                bi = bi % n_;
                result.Add(bi.ToString());
            }
            return result;
        }


        //расшифровать
        public string RSA_Decode(List<string> input, int d, int n)
        {
            try
            {
                string result = "";
                BigInteger bi;

                foreach (string item in input)
                {
                    bi = new BigInteger(Convert.ToDouble(item));
                    bi = BigInteger.Pow(bi, (int)d);

                    BigInteger n_ = new BigInteger((int)n);

                    bi = bi % n_;
                    int index = Convert.ToInt32(bi.ToString());
                    result += characters[index].ToString();
                }

                return result;
            }
            catch (Exception ex) { return ""; }
        }
    }

    class Program
    {
        public static readonly char[] characters = new char[] { '#', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '-' };




        static void Main(string[] args)
        {
            //----- СОЗДАНИЕ ПОДПИСИ -----
            Console.WriteLine($"\nЭЦП на основе RSA\n");

            var rsa = new RSA();
            string M = File.ReadAllText("in.txt");
            Process.Start("in.txt");
            //string M = "Chistyakova Julia";
            int p = 101;
            int q = 103;

            string hash = M.GetHashCode().ToString();
            int n = p * q;
            int m = (p - 1) * (q - 1);
            int d = rsa.Calculate_d(m);
            int e_ = rsa.Calculate_e(d, m);
            Console.WriteLine($" p = {p}\n q = {q}\n n = {n}\n ф(n) = {m}\n d = {d}\n e = {e_}\n M = {M}\n Хеш = {hash}\n");

            List<string> sign = rsa.RSA_Encode(hash, e_, n);


            // ----- РАСШИФРОВАНИЕ -----

            while (true)
            {
                Console.ReadKey();
                {
                    List<string> input = new List<string>();
                    string hash2 = File.ReadAllText("in.txt").GetHashCode().ToString();


                    string result = rsa.RSA_Decode(sign, d, n);
                    Console.WriteLine($"Хэш эл подписи = {result}");
                    Console.WriteLine($"Хэш файла = {hash2}");

                    if (result.Equals(hash2)) Console.WriteLine("Файл подлинный. Подпись верна. \n");
                    else Console.WriteLine("Внимание! Файл НЕ подлинный!!!\n");

                }
            }
        }
    }
}
