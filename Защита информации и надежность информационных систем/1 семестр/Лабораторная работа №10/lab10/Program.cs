using System;
using System.Text;

namespace lab10
{
    class Program
    {
        static void Main(string[] args)
        {
            string dictionary;
            string word;
            string message;
            string kodWord = "";
            int dictionaryLength;
            int wordLength;

            string inputMessage = "Шедько Евгений Александрович";
            string resultMessage = "";
            dictionaryLength = 8;
            wordLength = 8;
            while (true)
            {
                Console.WriteLine("Введите размер буфер словаря: ");
                int buf = 0;
                buf = Convert.ToInt32(Console.ReadLine());
                if (buf == 0)
                    continue;
                dictionaryLength = buf;
                if (buf != 0)
                    break;
            }
            while (true)
            {
                Console.WriteLine("Введите размер буфер данных: ");
                int buf = 0;
                buf = Convert.ToInt32(Console.ReadLine());
                if (buf == 0)
                    continue;
                wordLength = buf;
                if (buf != 0)
                    break;
            }

            word = inputMessage.Substring(0, wordLength);
            message = inputMessage.Substring(wordLength, inputMessage.Length - wordLength);

            int p = 0, q = 0;
            char c;
            dictionary = new string('0', dictionaryLength);
            Console.WriteLine("{0, 25}{1, 25}{2, 25}", "Словарь", "буфер данных", "Кодовое слово");
            
            while (word != "")
            {
                p = 0;
                q = 0;

                FindChars(dictionary, word, out p, out q, out c);

                SendChars(ref dictionary, ref word, q + 1);
                SendChars(ref word, ref message, q + 1);

                Minimize(ref dictionary, dictionaryLength);
                Minimize(ref word, wordLength);
                kodWord += p.ToString() + q.ToString() + c.ToString();


                Console.WriteLine("{0, 25}{1, 25}{2, 25}", dictionary, word, p.ToString().Trim() + q.ToString().Trim() + c.ToString().Trim());
                //Console.WriteLine("Словарь:             " + dictionary);
                //Console.WriteLine("Слово(буфер данных): " + word);
                //Console.WriteLine("Кодовое слово:       " + kodWord);
                //Console.WriteLine("-------------------------------");

            }
            Console.WriteLine("Кодовые триады: " + kodWord);
            string str = "";
            dictionary = new string('0', dictionaryLength);
            Console.WriteLine("{0, 25}{1, 25}{2, 25}", "Полученное слово", "Буфер", "Кодовая триада");

            for (int i = 0; i < kodWord.Length / 3; i++)
            {
                p = int.Parse(kodWord[i * 3].ToString());
                q = int.Parse(kodWord[i * 3 + 1].ToString());
                c = kodWord[i * 3 + 2];
                if (p == 0 && q == 0)
                {
                    resultMessage += c;
                    dictionary += c;
                }
                else
                {
                    str = dictionary.Substring((p - 1), q) + c;
                    resultMessage += str;
                    dictionary += str;
                }

                Minimize(ref dictionary, dictionaryLength);
                Console.WriteLine("{0, 25}{1, 25}{2, 25}", resultMessage, dictionary, p + " " + q + " " + c);
                
            }

            void FindChars(string dict, string bufWord, out int indexInArray, out int Length, out char lastElement)
            {

                indexInArray = 0;
                Length = 0;
                lastElement = bufWord[0];

                while (dict.Contains(bufWord.Substring(0, (Length + 1))))
                {
                    indexInArray = dict.IndexOf(bufWord.Substring(0, (Length + 1))) + 1;
                    Length++;
                    if (bufWord.Length == Length)
                    {
                        lastElement = '|';
                        break;
                    }
                    else
                        lastElement = bufWord[Length];
                }
            }

            void SendChars(ref string firstBuf, ref string secondByf, int charsCount)
            {
                charsCount = charsCount > secondByf.Length ? secondByf.Length : charsCount;
                if (charsCount > 0)
                {
                    firstBuf += secondByf.Substring(0, charsCount);
                    secondByf = secondByf.Substring(charsCount, secondByf.Length - charsCount);
                }
            }

            void Minimize(ref string byf, int size)
            {
                if (byf.Length > size)
                    byf = byf.Substring((byf.Length - size), byf.Length - (byf.Length - size));
            }

            Console.ReadKey();

        }
    }

    class Calculations
    {


    }
}
