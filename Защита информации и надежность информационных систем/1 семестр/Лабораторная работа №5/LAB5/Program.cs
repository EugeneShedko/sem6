using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB5
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                int n = 16;
                int[] arr = new int[n];

                arr = ArrayGeneration(arr);
                Console.WriteLine("Сгенерированная последовательность бит:");
                string arrInString = ConvertToString(arr);
                Console.WriteLine(arrInString);

                int k1 = 2;
                int k2 = 8;

                Console.WriteLine();
                Console.WriteLine("Первичная матрица " + k1 + " на " + k2 + ":");
                OutputStrAsMatrix(arrInString, k2);
                Console.WriteLine();
                Console.WriteLine();



                string choice = "2";
                //string temp = "234";
                //do
                //{
                //    Console.WriteLine("Введите по какому количеству паритетов(от 2 до 4) будут вычисляться проверочные биты:");
                //    choice = Console.ReadLine();
                //} while (!temp.Contains(choice));


                switch (choice)
                {
                    case "2":
                        {
                            string codeWord = CodeWordWithTwoParitets(arrInString, k1, k2);
                            Console.WriteLine();
                            Console.WriteLine($"Code word: {codeWord}");
                            int errorPosition = ErrorGenaration(n);
                            //Console.WriteLine($"errorPosition {errorPosition}");

                            string Yn;
                            string Xn;
                            if (errorPosition == -2)
                            {
                                break;
                            }
                            else if (errorPosition == -1)
                            {
                                Yn = SetError(codeWord, errorPosition);
                                Console.WriteLine($"Yn: {Yn}");
                                Console.WriteLine($"Xn: {Yn}");
                            }
                            else
                            {
                                Yn = SetError(codeWord, errorPosition);
                                Console.WriteLine($"Yn: {Yn}");
                                Xn = ErrorCorrection(Yn, n, k1, k2);
                                Console.WriteLine($"Xn: {Xn}");
                                Console.WriteLine("Ошибка исправлена!");
                                Console.WriteLine();
                                Console.WriteLine();
                            }
                            break;
                        }
                    case "3":
                        {
                            string codeWord = CodeWordWithThreeParitets(arrInString, k1, k2);
                            Console.WriteLine();
                            Console.WriteLine($"Code word: {codeWord}");
                            int errorPosition = ErrorGenaration(n);
                            //Console.WriteLine($"errorPosition {errorPosition}");

                            string Yn = codeWord;
                            string Xn;
                            if (errorPosition == -2)
                            {
                                break;
                            }
                            else if (errorPosition == -1)
                            {
                                Yn = SetError(codeWord, errorPosition);
                                Console.WriteLine($"Yn: {Yn}");
                                Console.WriteLine($"Xn: {Yn}");
                            }
                            else
                            {
                                Yn = SetError(codeWord, errorPosition);
                                Console.WriteLine($"Yn: {Yn}");
                                Xn = ErrorCorrection(Yn, n, k1, k2);
                                Console.WriteLine($"Xn: {Xn}");
                                Console.WriteLine("Ошибка исправлена!");
                                Console.WriteLine();
                                Console.WriteLine();
                            }
                            break;
                        }
                    case "4":
                        {
                            string codeWord = CodeWordWithFourParitets(arrInString, k1, k2);
                            Console.WriteLine();
                            Console.WriteLine($"Code word: {codeWord}");
                            int errorPosition = ErrorGenaration(n);
                            //Console.WriteLine($"errorPosition {errorPosition}");

                            string Yn;
                            string Xn;
                            if (errorPosition == -2)
                            {
                                break;
                            }
                            else if (errorPosition == -1)
                            {
                                Yn = SetError(codeWord, errorPosition);
                                Console.WriteLine($"Yn: {Yn}");
                                Console.WriteLine($"Xn: {Yn}");
                            }
                            else
                            {
                                Yn = SetError(codeWord, errorPosition);
                                Console.WriteLine($"Yn: {Yn}");
                                Xn = ErrorCorrection(Yn, n, k1, k2);
                                Console.WriteLine($"Xn: {Xn}");
                                Console.WriteLine("Ошибка исправлена!");
                                Console.WriteLine();
                                Console.WriteLine();
                            }
                            break;
                        }
                }

                Console.WriteLine();
                Console.WriteLine("Проверочная матрица для исправления ошибки с помощью синдрома:");
                byte[,] verifyMatr = new byte[k1 + k2, n];
                for (int i = 0; i < k1; i++)
                {
                    for (int j = 0; j < k2 * i; j++)
                    {
                        Console.Write("0 ");
                    }
                    for (int j = 0; j < k2; j++)
                    {
                        Console.Write("1 ");
                        verifyMatr[i, k2 * i + j] = 1;
                    }
                    for (int j = 0; j < k2 * (k1 - i - 1); j++)
                    {
                        Console.Write("0 ");
                    }
                    Console.WriteLine();
                }
                for (int i = 0; i < k2; i++)
                {
                    for (int j = 0; j < k1; j++)
                    {
                        for (int k = 0; k < i; k++)
                        {
                            Console.Write("0 ");
                        }
                        Console.Write("1 ");
                        verifyMatr[k1 + i, j * (k1 + 1) + i] = 1;
                        for (int k = 0; k < k2 - i - 1; k++)
                        {
                            Console.Write("0 ");
                        }
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();


                //Console.WriteLine("_________________");
                //for (int i = 0; i < k1 + k2; i++)
                //{
                //    for (int j = 0; j < n; j++)
                //    {
                //        Console.Write(verifyMatr[i, j]);
                //    }
                //    Console.WriteLine();
                //}

                byte[] Hr = CheckBits(arr, k1, k2);


                int error;
                try
                {
                    Console.WriteLine("Введите место первой ошибки");
                    error = Convert.ToInt32(Console.ReadLine()) - 1;
                    if (arr[error] == 1) arr[error] = 0;
                    else arr[error] = 1;
                }
                catch { }
                try
                {
                    Console.WriteLine("Введите место второй ошибки");
                    error = Convert.ToInt32(Console.ReadLine()) - 1;
                    if (arr[error] == 1) arr[error] = 0;
                    else arr[error] = 1;
                }
                catch { }

                Console.WriteLine("Ошибочное сообщение:");
                for (int i = 0; i < k1 * k2; i++)
                {
                    Console.Write(arr[i]);
                }
                Console.WriteLine();

                int[] Hk = FixErr(arr, Hr, verifyMatr, k1, k2);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();

            }
        }

        static int[] ArrayGeneration(int[] arr)
        {
            Random rnd = new Random();
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = rnd.Next(2);
            }

            return arr;
        }

        static string ConvertToString(int[] arr)
        {
            string result = "";
            for (int i = 0; i < arr.Length; i++)
            {
                if (Convert.ToString(arr[i]) == "1")
                {
                    result += "1";
                }
                else
                {
                    result += "0";
                }
            }
            return result;
        }

        static void OutputStrAsMatrix(string arrInString, int k2)
        {
            for (int i = 0; i < arrInString.Length; i++)
            {
                if (i % k2 == 0)
                {
                    Console.WriteLine();
                }
                Console.Write(arrInString[i]);
            }
        }

        static string CodeWordWithTwoParitets(string arrInString, int k1, int k2)
        {
            string result = arrInString;
            string Xh = "";
            string Xv = "";
            string Xhv = "";

            int countForRows = arrInString.Length / k2;
            //Console.WriteLine(countForRows);
            int tempForRows = 0;
            for (int i = 0; i < countForRows; i++)
            {
                string str = "";
                for (int j = tempForRows; j < tempForRows + k2; j++)
                {
                    str += arrInString[j];
                }
                Xh += XORforStr(str);
                tempForRows += k2;
            }
            //Console.WriteLine(Xh);

            int countForColumns = arrInString.Length / k1;
            int tempForColumns = 0;
            for (int i = 0; i < countForColumns; i++)
            {
                string str = "";
                for (int j = tempForColumns; j < arrInString.Length; j += k2)
                {
                    str += arrInString[j];
                }
                Xv += XORforStr(str);
                tempForColumns++;
            }
            //Console.WriteLine(Xv);

            string tempForXhv = XORforStr(Xh) + XORforStr(Xv);
            if (tempForXhv[0] == tempForXhv[1])
            {
                Xhv += "1";
            }
            else
            {
                Xhv += "0";
            }
            //Console.WriteLine(Xhv);

            Console.WriteLine($"Xh(горизонтальный паритет): {Xh}");
            Console.WriteLine($"Xv(вертикальный паритет): {Xv}");
            Console.WriteLine($"Xhv(паритет паритетов): {Xhv}");

            result += Xh + Xv + Xhv;

            return result;
        }

        static string CodeWordWithThreeParitets(string arrInString, int k1, int k2)
        {
            string result = arrInString;
            string Xh = "";
            string Xv = "";
            string Xdr = "";
            string Xhv = "";

            int countForRows = arrInString.Length / k2;
            //Console.WriteLine(countForRows);
            int tempForRows = 0;
            for (int i = 0; i < countForRows; i++)
            {
                string str = "";
                for (int j = tempForRows; j < tempForRows + k2; j++)
                {
                    str += arrInString[j];
                }
                Xh += XORforStr(str);
                tempForRows += k2;
            }
            //Console.WriteLine(Xh);

            int countForColumns = arrInString.Length / k1;
            int tempForColumns = 0;
            for (int i = 0; i < countForColumns; i++)
            {
                string str = "";
                for (int j = tempForColumns; j < arrInString.Length; j += k2)
                {
                    str += arrInString[j];
                }
                Xv += XORforStr(str);
                tempForColumns++;
            }
            //Console.WriteLine(Xv);

            int countForRightDiags = k2;
            int tempForRightDiags = 0;
            for (int i = 0; i < countForRightDiags; i++)
            {
                string str = "";
                for (int j = tempForRightDiags; j < arrInString.Length; j += k2 - 1)
                {
                    if (j == 0 || j == arrInString.Length - 1)
                    {
                        str += arrInString[j];
                        break;
                    }
                    else if (j % k2 == 0)
                    {
                        str += arrInString[j];
                        break;
                    }
                    else
                    {
                        str += arrInString[j];
                    }
                }
                Xdr += XORforStr(str);
                tempForRightDiags++;
            }

            int otherCountForRightDiags = k1 - 1;
            int otherTempForRightDiags = k2 * 2 - 1;
            for (int i = 0; i < otherCountForRightDiags; i++)
            {
                string str = "";
                for (int j = otherTempForRightDiags; j < arrInString.Length; j += k2 - 1)
                {
                    str += arrInString[j];
                }
                Xdr += XORforStr(str);
                otherTempForRightDiags += k2;
            }
            //Console.WriteLine(Xdr);


            string tempForXhv = XORforStr(Xh) + XORforStr(Xv) + XORforStr(Xdr);
            Xhv = XORforStr(tempForXhv);

            Console.WriteLine($"Xh(горизонтальный паритет): {Xh}");
            Console.WriteLine($"Xv(вертикальный паритет): {Xv}");
            Console.WriteLine($"Xdr(право-диагональный паритет): {Xdr}");
            Console.WriteLine($"Xhv(паритет паритетов): {Xhv}");

            result += Xh + Xv + Xdr + Xhv;

            return result;
        }

        static string CodeWordWithFourParitets(string arrInString, int k1, int k2)
        {
            string result = arrInString;
            string Xh = "";
            string Xv = "";
            string Xdr = "";
            string Xdl = "";
            string Xhv = "";

            int countForRows = arrInString.Length / k2;
            //Console.WriteLine(countForRows);
            int tempForRows = 0;
            for (int i = 0; i < countForRows; i++)
            {
                string str = "";
                for (int j = tempForRows; j < tempForRows + k2; j++)
                {
                    str += arrInString[j];
                }
                Xh += XORforStr(str);
                tempForRows += k2;
            }
            //Console.WriteLine(Xh);

            int countForColumns = arrInString.Length / k1;
            int tempForColumns = 0;
            for (int i = 0; i < countForColumns; i++)
            {
                string str = "";
                for (int j = tempForColumns; j < arrInString.Length; j += k2)
                {
                    str += arrInString[j];
                }
                Xv += XORforStr(str);
                tempForColumns++;
            }
            //Console.WriteLine(Xv);

            int countForRightDiags = k2;
            int tempForRightDiags = 0;
            for (int i = 0; i < countForRightDiags; i++)
            {
                string str = "";
                for (int j = tempForRightDiags; j < arrInString.Length; j += k2 - 1)
                {
                    if (j == 0 || j == arrInString.Length - 1)
                    {
                        str += arrInString[j];
                        break;
                    }
                    else if (j % k2 == 0)
                    {
                        str += arrInString[j];
                        break;
                    }
                    else
                    {
                        str += arrInString[j];
                    }
                }
                Xdr += XORforStr(str);
                tempForRightDiags++;
            }

            int otherCountForRightDiags = k1 - 1;
            int otherTempForRightDiags = k2 * 2 - 1;
            for (int i = 0; i < otherCountForRightDiags; i++)
            {
                string str = "";
                for (int j = otherTempForRightDiags; j < arrInString.Length; j += k2 - 1)
                {
                    str += arrInString[j];
                }
                Xdr += XORforStr(str);
                otherTempForRightDiags += k2;
            }
            //Console.WriteLine(Xdr);




            int countForLeftDiags = k2;
            int tempForLeftDiags = arrInString.Length - k2;
            for (int i = 0; i < countForLeftDiags; i++)
            {
                string str = "";
                for (int j = tempForLeftDiags; j >= 0; j = j - k2 - 1)
                {
                    if (j == arrInString.Length - k2 || j == (k2 - 1))
                    {
                        str += arrInString[j];
                        break;
                    }
                    else if (j % k2 == 0)
                    {
                        str += arrInString[j];
                        break;
                    }
                    else
                    {
                        str += arrInString[j];
                    }
                }
                Xdl += XORforStr(str);
                tempForLeftDiags++;
            }

            int otherCountForLeftDiags = k1 - 1;
            int otherTempForLeftDiags = arrInString.Length - k2 - 1;
            for (int i = 0; i < otherCountForLeftDiags; i++)
            {
                string str = "";
                for (int j = otherTempForLeftDiags; j >= 0; j = j - k2 - 1)
                {
                    str += arrInString[j];
                }
                Xdl += XORforStr(str);
                otherTempForLeftDiags -= k2;
            }
            //Console.WriteLine(Xdl);


            string tempForXhv = XORforStr(Xh) + XORforStr(Xv) + XORforStr(Xdr) + XORforStr(Xdl);
            Xhv = XORforStr(tempForXhv);

            Console.WriteLine($"Xh(горизонтальный паритет): {Xh}");
            Console.WriteLine($"Xv(вертикальный паритет): {Xv}");
            Console.WriteLine($"Xdr(право-диагональный паритет): {Xdr}");
            Console.WriteLine($"Xdl(лево-диагональный паритет): {Xdl}");
            Console.WriteLine($"Xhv(паритет паритетов): {Xhv}");

            result += Xh + Xv + Xdr + Xdl + Xhv;

            return result;
        }

        static string XORforStr(string k2Symbols)
        {
            string result = "";
            int count = 0;
            for (int i = 0; i < k2Symbols.Length; i++)
            {
                if (k2Symbols[i] == '1')
                {
                    count++;
                }
            }
            if (count % 2 == 0)
            {
                result += "0";
            }
            else
            {
                result += "1";
            }
            return result;
        }

        static int ErrorGenaration(int n)
        {
            string choice;
            string temp = "012";
            do
            {
                Console.Write("Выберите количество ошибок от 0 до 2: ");
                choice = Console.ReadLine();
            } while (!temp.Contains(choice));

            int numberOfErrors = Convert.ToInt32(choice);

            switch (numberOfErrors)
            {
                case 0:
                    {
                        return -1;
                    }
                case 1:
                    {
                        int errorPosition = GetRandom(0, n);
                        Console.WriteLine("Позиция ошибки: " + errorPosition);
                        return errorPosition;
                    }
                case 2:
                    {
                        Console.WriteLine("Для двух ошибок не исправляется");
                        return -2;
                    }
                default:
                    {
                        Console.WriteLine("Из-за некорректного ввода, количество ошибок взято за число 0");
                        Console.WriteLine();
                        return -1;
                    }
            }
        }

        public static int GetRandom(int begin, int end)
        {
            Random rnd = new Random();
            int value = rnd.Next(begin, end);
            return value;
        }

        static string SetError(string codeWord, int errorPosition)
        {
            string Yn = "";
            for (int i = 0; i < codeWord.Length; i++)
            {
                if (i == errorPosition && codeWord[i] == '1')
                {
                    Yn += "0";
                }
                else if (i == errorPosition && codeWord[i] == '0')
                {
                    Yn += "1";
                }
                else if (i != errorPosition && codeWord[i] == '1')
                {
                    Yn += "1";
                }
                else
                {
                    Yn += "0";
                }
            }
            return Yn;
        }

        static string ErrorCorrection(string Yn, int n, int k1, int k2)
        {
            string Xn = "";
            string temp = "";
            for (int i = 0; i < n; i++)
            {
                if (Yn[i] == '1')
                {
                    temp += "1";
                }
                else
                {
                    temp += "0";
                }
            }

            string newCodeWord = CodeWordWithTwoParitets(temp, k1, k2);
            //Console.WriteLine(newCodeWord);
            int rawError = 0;
            int columnError = 0;
            for (int i = n; i < n + k1; i++)
            {
                if (Yn[i] != newCodeWord[i])
                {
                    rawError = i - n;
                }
            }
            for (int i = n + k1; i < n + k1 + k2; i++)
            {
                if (Yn[i] != newCodeWord[i])
                {
                    columnError = i - n - k1;
                }
            }

            int Error = (rawError * k2) + columnError;

            for (int i = 0; i < Yn.Length; i++)
            {
                if (i == Error && Yn[i] == '1')
                {
                    Xn += "0";
                }
                else if (i == Error && Yn[i] == '0')
                {
                    Xn += "1";
                }
                else
                {
                    Xn += Yn[i];
                }
            }

            return Xn;
        }

        static byte[] CheckBits(int[] Hk, int k1, int k2)
        {
            byte[] Hr = new byte[k1 + k2 + 1];
            for (int i = 0; i < k1; i++)
            {
                for (int j = 0; j < k2; j++)
                {
                    Hr[i] = (byte)((Hr[i] + Hk[i * k2 + j]) % 2);
                }
                Hr[k1 + k2] = (byte)((Hr[k1 + k2] + Hr[i]) % 2);
            }
            for (int i = 0; i < k2; i++)
            {
                for (int j = 0; j < k1; j++)
                {
                    Hr[k1 + i] = (byte)((Hr[k1 + i] + Hk[j * k1 + i]) % 2);
                }
                Hr[k1 + k2] = (byte)((Hr[k1 + k2] + Hr[k1 + i]) % 2);
            }
            Console.WriteLine("Проверочные биты:");
            for (int i = 0; i < k1 + k2 + 1; i++)
            {
                Console.Write(Hr[i]);
            }
            Console.WriteLine();

            return Hr;
        }

        static int[] FixErr(int[] Hk, byte[] Hr, byte[,] verifMatr, int k1, int k2)
        {
            byte[] Yr = CheckBits(Hk, k1, k2);
            int k = Hk.Length;
            int r = Hr.Length - 1;

            byte[] syndrom = new byte[r];
            bool needFix = false;
            Console.WriteLine("Синдром:");
            for (int i = 0; i < r; i++)
            {
                syndrom[i] = (byte)((Yr[i] + Hr[i]) % 2);
                Console.Write(syndrom[i]);
                if (syndrom[i] != 0)
                {
                    needFix = true;
                }
            }
            //Console.WriteLine();

            if (!needFix)
            {
                Console.WriteLine("\nПередача без ошибок, исправление не требуется.");
                return Hk;
            }

            int colToFix = -1;
            for (int i = 0; i < k; i++)
            {
                bool equal = true;
                for (int j = 0; j < r; j++)
                {
                    if (syndrom[j] != verifMatr[j, i])
                    {
                        equal = false;
                        break;
                    }
                }
                if (equal)
                {
                    colToFix = i;
                    break;
                }
            }
            if (colToFix == -1)
            {
                Console.WriteLine("\nЧетное число ошибок. Невозможно исправить.");
                return Hk;
            }
            Hk[colToFix] = (byte)((Hk[colToFix] + 1) % 2);
            Console.WriteLine("\nОшибка исправлена. Полученная последовательность:");
            for (int i = 0; i < k1 * k2; i++)
            {
                Console.Write(Hk[i]);
            }
            for (int i = 0; i < k1 + k2 + 1; i++)
            {
                Console.Write(Hr[i]);
            }
            Console.WriteLine();

            return Hk;
        }
    }
}
