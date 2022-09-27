using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Numerics;

namespace Ell_Gamal_sign
{
    class EllGamal
    {
        public static int obr(int a, int n)
        {
            int res = 0;
            for (int i = 0; i < 10000; i++)
            {
                if (((a * i) % n) == 1) return (i);
            }
            return (res);
        }


        static void Main(string[] args)
        {
            Console.WriteLine($"\nЭЦП на основе Эль-Гамаля\n");

            int p = 2137;      ///простое
            int g = 2127;      ///перв. корень < p
            int x = 1116;      /// < p
            int y = (int)BigInteger.ModPow(g, x, p);

            int k = 7;     ///вз-простое с p-1
            int a = (int)BigInteger.ModPow(g, k, p);
            Console.WriteLine($" p={p}\n g={g}\n x={x}\n y={y}\n k={k}\n a={a}\n\n");


            int H = 2119;
            int m = p - 1;
            int k_1 = obr(k, p-1);
            var b = new BigInteger((k_1 * (H - (x * a)%m)%m)%m);
            Console.WriteLine($" H={H}\n k_1={k_1}\n b={b}\n S = {a},{b} \n\n");


            Console.WriteLine("\n Верификация:");
            var ya = BigInteger.ModPow(y, a, p);
            var ab = BigInteger.ModPow(a, b, p);
            var pr1 = BigInteger.ModPow(ya*ab, 1, p);
            var pr2 = BigInteger.ModPow(g, H, p);

            if (pr1 == pr2)
            {
                Console.WriteLine($" {pr1}  =  {pr2}\n Верификация пройдена успешно");
            }
            Console.ReadKey();
        }
    }
}
