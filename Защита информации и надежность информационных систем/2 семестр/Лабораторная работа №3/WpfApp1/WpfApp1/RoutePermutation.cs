using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class RoutePermutation
    {
        private char[] _apha = { 'a', 'ä', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'ö', 'p', 'q', 'r', 's', 'ß', 't', 'u', 'ü', 'v', 'w', 'x', 'y', 'z' };
        public string DecryptText(string text)
        {
            string decryptText = "";
            char[,] decryptTable = GetDecryptTable(text);
            int decryptTableRowNumber = decryptTable.GetUpperBound(0) + 1;
            int decryptTableColumnNumber = decryptTable.GetUpperBound(1) + 1;
            for (int i = 0; i < decryptTableRowNumber; i++)
            {
                for(int j = 0; j < decryptTableColumnNumber; j++)
                {
                    decryptText += decryptTable[i, j];
                }
            }
            return decryptText.TrimEnd();
        }
        public string EncryptText(string text)
        {
            string encryptText = "";
            char[,] encryptTable = GetEncryptTable(text);
            int encryptTableRowNumber = encryptTable.GetUpperBound(0) + 1;
            int enсryptTableColumnNumber = encryptTable.GetUpperBound(1) + 1;
            
            //где - то еще нужно поставить проверку на встречу с последним символом
            for(int i = 0, j = 0; ;)
            {
                encryptText += encryptTable[i, j];
                if (j + 1 < enсryptTableColumnNumber)
                {
                    encryptText += encryptTable[i, ++j];
                }
                else
                {
                    encryptText += encryptTable[++i, j];
                    if ((i + 1) * (j + 1) == encryptTable.Length)
                    {
                        return encryptText;
                    }
                }
                //в этом цикле спускаемся по таблице
                for(; ;)
                {
                    if((i + 1 < encryptTableRowNumber) && (j - 1 >= 0))
                    {
                        encryptText += encryptTable[++i, --j];
                        continue;
                    }
                    //нужно отследить, что именно выходит за пределы таблицы: переменная по строкам или столбцам
                    //чтобы понять в какую сторону дальше двигаться вверх или вниз
                    if (i + 1 < encryptTableRowNumber)
                    {
                        encryptText += encryptTable[++i, j];
                        break;
                    }
                    if(i + 1 == encryptTableRowNumber)
                    {
                        encryptText += encryptTable[i, ++j];
                        //здесь может закончится таблица
                        if ((i + 1) * (j + 1) == encryptTable.Length)
                        {
                            return encryptText;
                        }
                        break;
                    }
                }
                //в этом цикле поднимаемся по таблице
                for(; ;)
                {
                    if ((i - 1 >= 1) && (j + 1 < enсryptTableColumnNumber - 1))
                    {
                        encryptText += encryptTable[--i, ++j];
                        continue;
                    }else
                    {
                        i -= 1;
                        j += 1;
                    }
                    break;
                }

            }
        }

        public char[,] GetDecryptTable(string text)
        {

            int size = ((int)Math.Sqrt(text.Length) % 2 == 0) ? (int)Math.Sqrt(text.Length) : (int)Math.Sqrt(text.Length) + 1;
            char[,] decryptTable = new char[size, size];
            int decryptTableRowNumber = decryptTable.GetUpperBound(0) + 1;
            int decryptTableColumnNumber = decryptTable.GetUpperBound(1) + 1;


            for (int i = 0, j = 0, z = 0; ;)
            {
                decryptTable[i, j] = text[z++];
                //encryptText += encryptTable[i, j];
                if (j + 1 < decryptTableColumnNumber)
                {
                    decryptTable[i, ++j] = text[z++];
                    //encryptText += encryptTable[i, ++j];
                }
                else
                {
                    decryptTable[++i, j] = text[z++];
                    //encryptText += encryptTable[++i, j];
                    if ((i + 1) * (j + 1) == decryptTable.Length)
                    {
                        return decryptTable;
                    }
                }
                //в этом цикле спускаемся по таблице
                for (; ; )
                {
                    if ((i + 1 < decryptTableRowNumber) && (j - 1 >= 0))
                    {
                        decryptTable[++i, --j] = text[z++];
                        //encryptText += encryptTable[++i, --j];
                        continue;
                    }
                    //нужно отследить, что именно выходит за пределы таблицы: переменная по строкам или столбцам
                    //чтобы понять в какую сторону дальше двигаться вверх или вниз
                    if (i + 1 < decryptTableRowNumber)
                    {
                        decryptTable[++i, j] = text[z++];
                        //encryptText += encryptTable[++i, j];
                        break;
                    }
                    if (i + 1 == decryptTableRowNumber)
                    {
                        decryptTable[i, ++j] = text[z++];
                        //encryptText += encryptTable[i, ++j];
                        //здесь может закончится таблица
                        if ((i + 1) * (j + 1) == decryptTable.Length)
                        {
                            return decryptTable;
                        }
                        break;
                    }
                }
                //в этом цикле поднимаемся по таблице
                for (; ; )
                {
                    if ((i - 1 >= 1) && (j + 1 < decryptTableColumnNumber - 1))
                    {
                        decryptTable[--i, ++j] = text[z++];
                        //encryptText += encryptTable[--i, ++j];
                        continue;
                    }
                    else
                    {
                        i -= 1;
                        j += 1;
                    }
                    break;
                }

            }
        }
        private char[,] GetEncryptTable(string text)
        {
            int size = ((int)Math.Sqrt(text.Length) % 2 == 0) ? (int)Math.Sqrt(text.Length) : (int)Math.Sqrt(text.Length) + 1;
            char[,] encryptTable = new char[size, size];
            int encryptTableRowNumber = encryptTable.GetUpperBound(0) + 1;
            int encryptTableColumnNumber = encryptTable.Length / encryptTableRowNumber;
            for(int i = 0, k = 0; i < encryptTableColumnNumber; i++)
            {
                for(int j = 0; j < encryptTableRowNumber; j++)
                {
                    if (k < text.Length)
                    {
                        encryptTable[i, j] = text[k++];
                    }
                    else
                    {
                        encryptTable[i, j] = ' ';
                    }
                }
            }
            return encryptTable;
        }

        public double[] GetSymbolProbability(string text)
        {
            double[] probability = new double[_apha.Length];
            int counter;
            for(int i = 0; i < _apha.Length ; i++)
            {
                counter = 0;
                for(int j = 0; j < text.Length; j++)
                {
                    if (_apha[i] == text[j])
                    {
                        counter++;
                    }
                }
                probability[i] = (double)counter / text.Length;
            }
         return probability;
        }
    }
}
