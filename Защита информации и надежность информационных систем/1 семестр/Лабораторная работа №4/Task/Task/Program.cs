using System;

namespace Task
{
    class Program
    {
        static void Main(string[] args)
        {
            string textFromFile = WorkWithFile.GetMessage("../../../../TwoSymbolName.txt");
            Console.WriteLine("Текст из файла: " + textFromFile);

            string originalXk = WorkWithString.ConvertToBinary(textFromFile);
            Console.WriteLine("Перевод в двоичную СС: " + originalXk);

            int k = 16;
            int r = (int)Math.Log(k, 2) + 1;
            
            char[,] matrix = CustomMath.GetCheckMatrix(r, k);
            Console.WriteLine("\nПроверочная матрица: ");
            CustomMath.PrintMatrix(matrix, r, k);

            string originalYk = originalXk;
            //----------------------------------------------------------------------------
            string redurantCharacters = CustomMath.FindRedundantCharacters(originalXk, matrix, r, k);
            Console.WriteLine("\nИзбыточные символы Yr: " + redurantCharacters);
            Console.WriteLine("Кодовое слово Yn: " + originalYk + " " + redurantCharacters);
            //----------------------------------------------------------------------------

            //Нет ошибок
            string errorYk = originalYk;
            string syndrom;

            Console.WriteLine("\nYk без ошибок:" + errorYk);

            string errorRedurantCharacters = CustomMath.FindRedundantCharacters(errorYk, matrix, r, k);
            Console.WriteLine("Избыточные символы Y'r: " + errorRedurantCharacters);
            Console.WriteLine("Синдром: " + WorkWithString.xorOperation(redurantCharacters, errorRedurantCharacters));

            //1 ошибка
            string errorVector;

            errorYk = WorkWithString.ApplyError(originalXk, 1);
            Console.WriteLine("\nYk одна ошибка:" + errorYk);

            errorRedurantCharacters = CustomMath.FindRedundantCharacters(errorYk, matrix, r, k);
            Console.WriteLine("Избыточные символы Y'r: " + errorRedurantCharacters);

            syndrom = WorkWithString.xorOperation(redurantCharacters, errorRedurantCharacters);
            Console.WriteLine("Синдром: " + syndrom);

            errorVector = CustomMath.GetErrorVector(CustomMath.GetErrorPosition(matrix, syndrom, r, k), r + k);
            Console.WriteLine("Вектор ошибки: " + errorVector);
            Console.WriteLine("После исправления ошибки: " + WorkWithString.xorOperation(errorYk + redurantCharacters, errorVector));

            //2 ошибки
            errorYk = WorkWithString.ApplyError(originalXk, 2);
            Console.WriteLine("\nYk две ошибки:" + errorYk);

            errorRedurantCharacters = CustomMath.FindRedundantCharacters(errorYk, matrix, r, k);
            Console.WriteLine("Избыточные символы Y'r: " + errorRedurantCharacters);

            syndrom = WorkWithString.xorOperation(redurantCharacters, errorRedurantCharacters);
            Console.WriteLine("Синдром: " + syndrom);

            //--------------------------------------------------------------------------------------------------------------------

            r = 6;
            Console.WriteLine();
            matrix = CustomMath.ModifyMatrix(matrix, r, k);
            Console.WriteLine("Модифицированая проверочная матрица: ");
            CustomMath.PrintMatrix(matrix, r, k);

            //----------------------------------------------------------------------------
            redurantCharacters = CustomMath.FindRedundantCharacters(originalXk, matrix, r, k);
            Console.WriteLine("\nИзбыточные символы Yr: " + redurantCharacters);
            Console.WriteLine("Кодовое слово Yn: " + originalYk + " " + redurantCharacters);
            //----------------------------------------------------------------------------

            //Нет ошибок
            errorYk = originalYk;
            Console.WriteLine("\nYk без ошибок:" + errorYk);

            errorRedurantCharacters = CustomMath.FindRedundantCharacters(errorYk, matrix, r, k);
            Console.WriteLine("Избыточные символы Y'r: " + errorRedurantCharacters);

            syndrom = WorkWithString.xorOperation(redurantCharacters, errorRedurantCharacters);
            Console.WriteLine("Синдром: " + syndrom);

            //1 ошибка
            errorYk = WorkWithString.ApplyError(originalXk, 1);
            Console.WriteLine("\nYk одна ошибка:" + errorYk);

            errorRedurantCharacters = CustomMath.FindRedundantCharacters(errorYk, matrix, r, k);
            Console.WriteLine("Избыточные символы Y'r: " + errorRedurantCharacters);

            syndrom = WorkWithString.xorOperation(redurantCharacters, errorRedurantCharacters);
            Console.WriteLine("Синдром: " + syndrom);

            errorVector = CustomMath.GetErrorVector(CustomMath.GetErrorPosition(matrix, syndrom, r, k), r + k);
            Console.WriteLine("Вектор ошибки: " + errorVector);
            Console.WriteLine("После исправления ошибки: " + WorkWithString.xorOperation(errorYk + redurantCharacters, errorVector));

            //2 ошибки
            errorYk = WorkWithString.ApplyError(originalXk, 2);
            Console.WriteLine("\nYk две ошибки:" + errorYk);

            errorRedurantCharacters = CustomMath.FindRedundantCharacters(errorYk, matrix, r, k);
            Console.WriteLine("Избыточные символы Y'r: " + errorRedurantCharacters);

            syndrom = WorkWithString.xorOperation(redurantCharacters, errorRedurantCharacters);
            Console.WriteLine("Синдром: " + syndrom);

            Console.ReadKey();
        }
    }
}
