using System;
using System.Text;

namespace lab8
{
    class Program
    {
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            string input = "столб";
            string name = "Евгений";
            string surname = "Шедько";
            string variantInput = "гидроаэроионизация";
            string binVariantInputFirstThreeLetters = variantInput.Substring(0, 3);

            int k = input.Length;
            string[] matr = new string[k];

            #region matrgeneration
            matr[0] = input;
            for (int i = 1; i < matr.Length; i++)
            {
                string symbol = matr[i - 1].Substring(0, 1);
                matr[i] = Sdvig(matr[i - 1], symbol);
            }
            #endregion

            Console.WriteLine("Исходная матрица:");
            MatrOutPut(matr);
            matr = MatrSort(matr);
            Console.WriteLine();
            Console.WriteLine("Отсортированная матрица:");
            MatrOutPut(matr);

            int z = FindInputStrInMatr(matr, input) + 1;
            Console.WriteLine();
            Console.WriteLine("z = " + z + "-ая строка");
            string wk = FindWk(matr, input);
            Console.WriteLine("wk = " + wk);
            Console.WriteLine();

            string[] w = new string[matr.Length];
            w = ReverseTransformation(matr, wk);
            Console.WriteLine();
            Console.WriteLine("Итоговая преобразованная матрица:");
            MatrOutPut(w);

            labOutputForName(name);
            labOutputForSurname(surname);
            labOutputVariantInput(variantInput);

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Первые три буквы слова по варианту: " + binVariantInputFirstThreeLetters);
            string encodedBinVariantInputFirstThreeLetters = EncodingToBytes(binVariantInputFirstThreeLetters);
            Console.WriteLine("Эти буквы в битах: " + encodedBinVariantInputFirstThreeLetters);

            labOutputForBin(encodedBinVariantInputFirstThreeLetters);
        }

        //Первые 4-е функции можно заменить одной общей  более обобщенным названием
        public static void labOutputForName(string name)
        {
            Console.WriteLine();
            Console.WriteLine();
            int k = name.Length;
            string[] matr = new string[k];

            #region matrgeneration
            matr[0] = name;
            for (int i = 1; i < matr.Length; i++)
            {
                string symbol = matr[i - 1].Substring(0, 1);
                matr[i] = Sdvig(matr[i - 1], symbol);
            }
            #endregion

            Console.WriteLine("Исходная матрица:");
            MatrOutPut(matr);
            matr = MatrSort(matr);
            Console.WriteLine();
            Console.WriteLine("Отсортированная матрица:");
            MatrOutPut(matr);

            int z = FindInputStrInMatr(matr, name) + 1;
            Console.WriteLine();
            Console.WriteLine("z = " + z + "-ая строка");
            string wk = FindWk(matr, name);
            Console.WriteLine("wk = " + wk);
            Console.WriteLine();

            string[] w = new string[matr.Length];
            w = ReverseTransformation(matr, wk);
            Console.WriteLine();
            Console.WriteLine("Итоговая преобразованная матрица:");
            MatrOutPut(w);
        }

        public static void labOutputForSurname(string surname)
        {
            Console.WriteLine();
            Console.WriteLine();
            int k = surname.Length;
            string[] matr = new string[k];

            #region matrgeneration
            matr[0] = surname;
            for (int i = 1; i < matr.Length; i++)
            {
                string symbol = matr[i - 1].Substring(0, 1);
                matr[i] = Sdvig(matr[i - 1], symbol);
            }
            #endregion

            Console.WriteLine("Исходная матрица:");
            MatrOutPut(matr);
            matr = MatrSort(matr);
            Console.WriteLine();
            Console.WriteLine("Отсортированная матрица:");
            MatrOutPut(matr);

            int z = FindInputStrInMatr(matr, surname) + 1;
            Console.WriteLine();
            Console.WriteLine("z = " + z + "-ая строка");
            string wk = FindWk(matr, surname);
            Console.WriteLine("wk = " + wk);
            Console.WriteLine();

            string[] w = new string[matr.Length];
            w = ReverseTransformation(matr, wk);
            Console.WriteLine();
            Console.WriteLine("Итоговая преобразованная матрица:");
            MatrOutPut(w);
        }

        public static void labOutputVariantInput(string variantInput)
        {
            Console.WriteLine();
            Console.WriteLine();
            int k = variantInput.Length;
            string[] matr = new string[k];

            #region matrgeneration
            matr[0] = variantInput;
            for (int i = 1; i < matr.Length; i++)
            {
                string symbol = matr[i - 1].Substring(0, 1);
                matr[i] = Sdvig(matr[i - 1], symbol);
            }
            #endregion

            Console.WriteLine("Исходная матрица:");
            MatrOutPut(matr);
            matr = MatrSort(matr);
            Console.WriteLine();
            Console.WriteLine("Отсортированная матрица:");
            MatrOutPut(matr);

            int z = FindInputStrInMatr(matr, variantInput) + 1;
            Console.WriteLine();
            Console.WriteLine("z = " + z + "-ая строка");
            string wk = FindWk(matr, variantInput);
            Console.WriteLine("wk = " + wk);
            Console.WriteLine();

            string[] w = new string[matr.Length];
            w = ReverseTransformation(matr, wk);
            Console.WriteLine();
            Console.WriteLine("Итоговая преобразованная матрица:");
            MatrOutPut(w);
        }

        public static void labOutputForBin(string encodedBinVariantInputFirstThreeLetters)
        {
            Console.WriteLine();
            Console.WriteLine();
            int k = encodedBinVariantInputFirstThreeLetters.Length;
            string[] matr = new string[k];

            #region matrgeneration
            matr[0] = encodedBinVariantInputFirstThreeLetters;
            for (int i = 1; i < matr.Length; i++)
            {
                string symbol = matr[i - 1].Substring(0, 1);
                matr[i] = Sdvig(matr[i - 1], symbol);
            }
            #endregion

            Console.WriteLine("Исходная матрица:");
            MatrOutPut(matr);
            matr = MatrSort(matr);
            Console.WriteLine();
            Console.WriteLine("Отсортированная матрица:");
            MatrOutPut(matr);

            int z = FindInputStrInMatr(matr, encodedBinVariantInputFirstThreeLetters) + 1;
            Console.WriteLine();
            Console.WriteLine("z = " + z + "-ая строка");
            string wk = FindWk(matr, encodedBinVariantInputFirstThreeLetters);
            Console.WriteLine("wk = " + wk);
            Console.WriteLine();

            string[] w = new string[matr.Length];
            w = ReverseTransformation(matr, wk);
            Console.WriteLine();
            Console.WriteLine("Итоговая преобразованная матрица:");
            MatrOutPut(w);
        }

        public static string Sdvig(string matr, string symbol)
        {
            string temp = "";
            for (int i = 0; i < matr.Length - 1; i++)
            {
                temp = temp + matr[i + 1];
            }
            temp = temp + symbol;
            return temp;
        }

        public static string[] MatrSort(string[] matr)
        {
            Array.Sort(matr);
            return matr;
        }

        public static void MatrOutPut(string[] matr)
        {
            for (int i = 0; i < matr.Length; i++)
            {
                Console.WriteLine(matr[i]);
            }
        }

        public static int FindInputStrInMatr(string[] matr, string input)
        {
            int z = 0;
            for (int i = 0; i < matr.Length; i++)
            {
                if (matr[i] == input)
                {
                    z = i;
                }
            }
            return z;
        }

        public static string FindWk(string[] matr, string input)
        {
            string wk = "";
            int indexTo = input.Length - 1;
            for (int i = 0; i < matr.Length; i++)
            {
                wk = wk + matr[i].Substring(indexTo);
            }
            return wk;
        }

        public static string[] ReverseTransformation(string[] matr, string wk)
        {
            string[] w = new string[matr.Length];

            Console.WriteLine("Последовательные шаги преобразования матрицы(добавление wk + мгновенная сортировка):");
            for (int j = 0; j < w.Length; j++)
            {
                for (int i = 0; i < w.Length; i++)
                {
                    w[i] = wk[i] + w[i];
                }
                w = MatrSort(w);
                MatrOutPut(w);
                Console.WriteLine();
            }

            return w;
        }

        //Перевод в биты
        public static string EncodingToBytes(string message)
        {
            var enc1251 = Encoding.GetEncoding(1251);

            //if (string.IsNullOrEmpty(message))

            string bin = string.Empty;
            var txt1251 = enc1251.GetBytes(message.ToCharArray());
            foreach (var ch in txt1251)
            {
                //Console.Write($"{ch} ");
                bin += Convert.ToString(ch, 2);
            }
            //Console.WriteLine($"\n{bin}");
            return bin;
        }
    }
}
