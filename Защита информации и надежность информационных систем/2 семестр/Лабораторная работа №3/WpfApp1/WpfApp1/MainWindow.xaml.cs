using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _initialFilePath = "../../../../InitialText.txt";
        private string _encryptFilePath = "../../../../EncryptText.txt";
        private string _decryptFilePath = "../../../../DecryptText.txt";
        private string _initialProbability = "../../../../InitialProbability.xls";
        private string _encryptProbability = "../../../../EncryptProbability.xls";
        private string _algorithm;
        public MainWindow()
        {
            InitializeComponent();
        }

        private string ReadFileText(string filePath)
        {
            string fileText = "";
            using (StreamReader reader = new StreamReader(filePath))
            {
                fileText = reader.ReadToEnd();
            }
            return fileText;

        }

        private void WriteFileText(string text, string filePath)
        {
            using(StreamWriter writer = new StreamWriter(filePath, false))
            {
                writer.Write(text);
            }
        }

        private void WriteFileText(double[] text, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath, false))
            {
                for(int i = 0; i < text.Length; i++)
                {
                    writer.WriteLine(text[i]);
                }
            }
        }

        private string RemoveSymbol(string text)
        {
            return Regex.Replace(text, @"[^a-zA-Z]+", "").ToLower();
        }

        private void Encrypt(object sender, RoutedEventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            switch (_algorithm)
            {
                case "Маршрутная перестановка":
                    {
                        RoutePermutation routePermutation = new RoutePermutation();
                        stopwatch.Start();
                        WriteFileText(routePermutation.EncryptText(RemoveSymbol(ReadFileText(_initialFilePath))), _encryptFilePath);
                        WriteFileText(routePermutation.GetSymbolProbability(ReadFileText(_initialFilePath)), _initialProbability);
                        WriteFileText(routePermutation.GetSymbolProbability(ReadFileText(_encryptFilePath)), _encryptProbability);
                        stopwatch.Stop();
                    }
                    break;
                case "Множественная перестановка":
                    {
                        MultiplePermutation multiplePermutation = new MultiplePermutation();
                        stopwatch.Start();
                        WriteFileText(multiplePermutation.EncryptText(RemoveSymbol(ReadFileText(_initialFilePath))), _encryptFilePath);
                        WriteFileText(multiplePermutation.GetSymbolProbability(ReadFileText(_initialFilePath)), _initialProbability);
                        WriteFileText(multiplePermutation.GetSymbolProbability(ReadFileText(_encryptFilePath)), _encryptProbability);
                        stopwatch.Stop();
                    }
                    break;
                default: MessageBox.Show("Выберите алгоритм!");break;
            }
            MessageBox.Show("Сообщение зашифровано, время шифрования: " + stopwatch.Elapsed);
        }

        private void Decrypt(object sender, RoutedEventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            switch (_algorithm)
            {
                case "Маршрутная перестановка":
                    {
                        RoutePermutation routePermutation = new RoutePermutation();
                        stopwatch.Start();
                        WriteFileText(routePermutation.DecryptText(ReadFileText(_encryptFilePath)) , _decryptFilePath);
                        stopwatch.Stop();
                    }
                    break;
                case "Множественная перестановка":
                    {
                        MultiplePermutation multiplePermutation = new MultiplePermutation();
                        stopwatch.Start();
                        WriteFileText(multiplePermutation.DecryptText(ReadFileText(_encryptFilePath)), _decryptFilePath);
                        stopwatch.Stop();
                    }
                    break;
                default: MessageBox.Show("Выберите алгоритм!"); break;
            }
            MessageBox.Show("Сообщение дешифровано, время дешифрования: " + stopwatch.Elapsed);
        }

        private void SelectFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if(fileDialog.ShowDialog() == true)
            {
                _initialFilePath = fileDialog.FileName;
            }
        }

        private void SelectAlgorithm(object sender, RoutedEventArgs e)
        {
            _algorithm = ((RadioButton)sender).Content.ToString();
        }
    }
}
