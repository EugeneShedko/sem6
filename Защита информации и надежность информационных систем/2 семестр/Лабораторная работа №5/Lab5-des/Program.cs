
using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;


class MainClass
{
    private static string inputPath = "in.txt";
    private static string outputPath = "out.txt";
    private static string keyPath1 = "key1.txt";
    private static string keyPath2 = "key2.txt";

    public static void Main(string[] args)
    {
        var input = File.ReadAllText(inputPath);
        var key1 = File.ReadAllText(keyPath1);
        var key2 = File.ReadAllText(keyPath2);

        long OldTicks = DateTime.Now.Ticks;
        var encoded = Encode(input, key1);
            encoded = Encode(encoded, key2);
            encoded = Encode(encoded, key1);
        Console.WriteLine($"Время зашифрования: {(DateTime.Now.Ticks - OldTicks) / 1000} мс");


        OldTicks = DateTime.Now.Ticks;
        var decoded = Decode(encoded, key1);
            decoded = Decode(decoded, key2);
            decoded = Decode(decoded, key1);
        Console.WriteLine($"Время расашифрования: {(DateTime.Now.Ticks - OldTicks) / 1000} мс");

        
        Console.WriteLine("\nencoded: {0}", encoded);
        Console.WriteLine("\ndecoded: {0}", decoded);

        using (StreamWriter sw = new StreamWriter(outputPath, false, System.Text.Encoding.Unicode))
        { sw.WriteLine(encoded); }

        Console.ReadLine();
    }

    private static string Encode(string input, string key)
    {
        var toEncryptArray = UTF8Encoding.UTF8.GetBytes(input);
        var hashmd5 = new MD5CryptoServiceProvider();
        var keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
        hashmd5.Clear();

        var tdes = new TripleDESCryptoServiceProvider();
        tdes.Key = keyArray;
        tdes.Mode = CipherMode.ECB;///режим шифр
        tdes.Padding = PaddingMode.Zeros;///дополнение (PKCS7)

        var cTransform = tdes.CreateEncryptor();
        byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
        tdes.Clear();
        return Convert.ToBase64String(resultArray, 0, resultArray.Length);
    }

    private static string Decode(string input, string key)
    {
        var toEncryptArray = Convert.FromBase64String(input);
        var hashmd5 = new MD5CryptoServiceProvider();
        var keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
        hashmd5.Clear();

        var tdes = new TripleDESCryptoServiceProvider();
        tdes.Key = keyArray;
        tdes.Mode = CipherMode.ECB;
        tdes.Padding = PaddingMode.Zeros;

        var cTransform = tdes.CreateDecryptor(); // !
        byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
        tdes.Clear();
        return UTF8Encoding.UTF8.GetString(resultArray);
    }
}
