using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private string filePath;
        private string encryptFilePath = "../../../../encryptFile.txt";
        private string decryptFilePath = "../../../../decryptFile.txt";
        private string ver = "../../../../ver.xls";
        private string ver1 = "../../../../ver1.xls";
        private string radioButtonName;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if(fileDialog.ShowDialog() == true)
            {
                filePath = fileDialog.FileName;
            }
        }

        private string ReadFileText(string filePath)
        {
            string fileText;
            using(StreamReader reader = new StreamReader(filePath))
            {
                fileText = reader.ReadToEnd();
            }
            return fileText;
        }

        private string RemoveSymbol(string text)
        {
            return Regex.Replace(text, @"[^a-zA-Z]+", "").ToLower();
        }


        private void WriteToFile(string filePath, string text)
        {
            using(StreamWriter writer = new StreamWriter(filePath, false))
            {
                writer.Write(text);
            }
        }
        private void WriteTofile(string filePath, double[] text)
        {
            using(StreamWriter writer = new StreamWriter(filePath, false))
            {
                for (int i = 0; i < text.Length; i++)
                {
                    writer.WriteLine(text[i]);
                }
            }
        }

        private void Encrypt(object sender, RoutedEventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            string encryptText = "";
            string fileText = RemoveSymbol(ReadFileText(filePath));
            switch (radioButtonName)
            {
                case "Шифр Виженера": {
                        Visener visener = new Visener();
                        WriteTofile(ver, visener.GetSymbolFrequency(fileText));
                        stopwatch.Start();
                        encryptText = visener.Encrypt(fileText);
                        stopwatch.Stop();
                        MessageBox.Show("Время зашифрования: " + stopwatch.Elapsed);
                        WriteTofile(ver1, visener.GetSymbolFrequency(encryptText));
                    } break;
                case "Шифр Порты": {
                        stopwatch.Start();
                        Port port = new Port();
                        encryptText = port.Encrypt(fileText);
                        stopwatch.Stop();
                        MessageBox.Show("Время зашифрования: " + stopwatch.Elapsed);
                        WriteTofile(ver1, port.GetSymbolFrequency(encryptText));
                    } break;
            }
            WriteToFile(encryptFilePath, encryptText);
            MessageBox.Show("Текст зашифрован!");
        }

        private void Decrypt(object sender, RoutedEventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            string fileText = ReadFileText(encryptFilePath);
            string decryptText = "";
            switch (radioButtonName)
            {
                case "Шифр Виженера":
                    {
                        Visener visener = new Visener();
                        stopwatch.Start();
                        decryptText = visener.Decrypt(fileText);
                        stopwatch.Stop();
                        MessageBox.Show("Время расшифрования: " + stopwatch.Elapsed);
                    }
                    break;
                case "Шифр Порты":
                    {
                        Port port = new Port();
                        stopwatch.Start();
                        decryptText = port.Decrypt(fileText);
                        stopwatch.Stop();
                        MessageBox.Show("Время расшифрования: " + stopwatch.Elapsed);
                    }
                    break;
            }
            WriteToFile(decryptFilePath, decryptText);
            MessageBox.Show(decryptText);
            MessageBox.Show("Текст расшифрован!");
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            radioButtonName = ((RadioButton)sender).Content.ToString();
        }
    }
}
