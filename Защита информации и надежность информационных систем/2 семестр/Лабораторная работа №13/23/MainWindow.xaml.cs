using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
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
using System.Linq;
using Image = System.Windows.Controls.Image;
using Color = System.Drawing.Color;

namespace _23
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void PutTextIntoImage_Click(object sender, RoutedEventArgs e)
        {
            string text = InputText.Text;
            string textBits = "";
            foreach (byte item in Encoding.Unicode.GetBytes(text))
            {
                textBits += Convert.ToString(item, 2).PadLeft(8, '0');
            }
            EcnodedText.Text = textBits;
        }

        private void GetTextFromImage_Click(object sender, RoutedEventArgs e)
        {
            string textBits = EcnodedText.Text;
            List<byte> bytes = new List<byte>();
            for (int i = 0; i < textBits.Length; i += 8)
            {
                bytes.Add(Convert.ToByte(textBits.Substring(i, 8),2));
            }
            string text = Encoding.Unicode.GetString(bytes.ToArray());
            DecodedText.Text = text;
        }

        private void OpenFileToInsertText_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();

            openDialog.DefaultExt = ".png";
            openDialog.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            Nullable<bool> result = openDialog.ShowDialog();

            if (result == true)
            {
                string filename = openDialog.FileName;
            }

            string inserteredMessage = TextToInsert.Text + "00000000000000000000000000000000";

            Bitmap img = new Bitmap(openDialog.FileName);
            byte A, R, G, B;
            //for (int i = 0; i < img.Width; i++)
            //{
            //    for (int j = 0; j < img.Height; j++)
            //    {
            //        R = (byte)(img.GetPixel(i, j).R & 0b11111110);
            //        img.SetPixel(i, j, Color.FromArgb(img.GetPixel(i, j).A, R, img.GetPixel(i, j).G, img.GetPixel(i, j).B));
            //    }
            //}

            for (int i = 0; i < img.Width && i * img.Height < inserteredMessage.Length; i++)
            {
                for (int j = 0; j < img.Height && (i * img.Height + j) < inserteredMessage.Length; j++)
                {
                    if(inserteredMessage[i * img.Height + j] == '1')
                    {
                        R = (byte)(img.GetPixel(i, j).R | 0b00000001);
                    }
                    else
                    {
                        R = (byte)(img.GetPixel(i, j).R & 0b11111110);
                    }
                    img.SetPixel(i, j, Color.FromArgb(img.GetPixel(i, j).A, R, img.GetPixel(i, j).G, img.GetPixel(i, j).B));
                }
            }
            //SaveFileDialog saveDialog = new SaveFileDialog();
            string[] file_name = openDialog.FileName.Split('.');
            img.Save(file_name[0]
                + "_copy."
                + file_name[1]);

            Image1.Source = new BitmapImage(new Uri(openDialog.FileName));
        }

        private void OpenFileToGetText_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.DefaultExt = ".png";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
            }

            string inserteredMessage = "";
            int endOfFileCounter = 0;


            Bitmap img = new Bitmap(dlg.FileName);
            byte A, R, G, B;
            for (int i = 0; i < img.Width && endOfFileCounter < 16; i++)
            {
                for (int j = 0; j < img.Height && endOfFileCounter < 16; j++)
                {
                    R = (byte)(img.GetPixel(i, j).R);
                    if ((R % 2).ToString() == "1")
                        endOfFileCounter = 0;
                    else
                        endOfFileCounter++;
                    inserteredMessage += (R % 2).ToString();
                }
            }
            TextToGet.Text = inserteredMessage;
            Image1.Source = new BitmapImage(new Uri(dlg.FileName));
        }
    }
}
