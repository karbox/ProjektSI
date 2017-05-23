using System;
using System.Collections.Generic;
using System.IO;
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

namespace ProjektSI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Title = "Sztuczna Inteligencja";
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.OpenFileDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = dialog.FileName;
                if (path.Contains(".txt"))
                {
                    using (StreamReader sr = new StreamReader(path))
                    {
                        String line = sr.ReadToEnd();
                        stringBox1.Text = line;
                    }
                }
                else
                    System.Windows.MessageBox.Show("Podaj plik .txt", "Ostrzeżenie");

            }
        }

        private void ciagDrugi_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.OpenFileDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = dialog.FileName;
                if (path.Contains(".txt"))
                {
                    using (StreamReader sr = new StreamReader(path))
                    {
                        String line = sr.ReadToEnd();
                        stringBox2.Text = line;
                    }
                }
                else
                    System.Windows.MessageBox.Show("Podaj plik .txt", "Ostrzeżenie");

            }
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void findButton1_Click(object sender, RoutedEventArgs e)
        {
            int d1, d2;//dlugosci
            if (stringBox1.Text.Length != 0 && stringBox2.Text.Length != 0)
            {
                d1 = stringBox1.Text.Length;
                d2 = stringBox2.Text.Length;
                string string1 = stringBox1.Text;
                string string2 = stringBox2.Text;

                int[,] M = new int[d1 + 1, d2 + 1];
                for (int i = 0; i <= d1; i++) M[i, 0] = 0;
                for (int j = 0; j <= d2; j++) M[0, j] = 0;

                for (int i = 0; i < d1; i++)
                {
                    for (int j = 0; j < d2; j++)
                    {
                        if (string1[i] == string2[j])
                            M[i + 1, j + 1] = 1 + M[i, j];
                        else
                            M[i + 1, j + 1] = Math.Max(M[i + 1, j], M[i, j + 1]);
                    }
                }

                int k = d1 - 1;
                int l = d2 - 1;
                string wynik = "";

                while ((k >= 0) && (l >= 0))
                {
                    if (string1[k] == string2[l])
                    {
                        wynik = string1[k] + wynik;
                        k--;
                        l--;
                    }
                    else
                    {
                        if (M[k + 1, l] > M[k, l + 1])
                            l--;
                        else
                            k--;
                    }
                }
                wynik = "Szukany podciąg: " + wynik;
                wynikBox.Text = wynikBox.Text + "\n" + wynik;
                tablicaBox.Text += "\n";
                tablicaBox.Text += "\t";
                tablicaBox.Text += "0";
                for (int i = 0; i < string2.Length; i++)
                {
                    tablicaBox.Text += " " + string2[i];
                }
                tablicaBox.Text += "\n";
                for (int i = 0; i <= d1 ; i++)
                {
                    if(i==0) tablicaBox.Text += "0\t";
                    else
                    tablicaBox.Text += string1[i-1] + "\t";
                    for (int j = 0; j <= d2 ; j++)
                    {
                        tablicaBox.Text += " " + M[i, j];
                    }
                    tablicaBox.Text += "\n";
                }
            }
            else
                MessageBox.Show("Podaj lub wczytaj ciągi", "Ostrzeżenie");
        }

        private void findButton2_Click(object sender, RoutedEventArgs e)
        {
            
            if (stringBox1.Text.Length != 0 && stringBox2.Text.Length != 0)
            {
            string sequence = string.Empty;
            string str1 = stringBox1.Text;
            string str2 = stringBox2.Text;

            int[,] num = new int[str1.Length, str2.Length];
            int maxlen = 0;
            int lastSubsBegin = 0;
            StringBuilder sequenceBuilder = new StringBuilder();

            for (int i = 0; i < str1.Length; i++)
            {
                for (int j = 0; j < str2.Length; j++)
                {
                    if (str1[i] != str2[j])
                        num[i, j] = 0;
                    else
                    {
                        if ((i == 0) || (j == 0))
                            num[i, j] = 1;
                        else
                            num[i, j] = 1 + num[i - 1, j - 1];

                        if (num[i, j] > maxlen)
                        {
                            maxlen = num[i, j];
                            int thisSubsBegin = i - num[i, j] + 1;
                            if (lastSubsBegin == thisSubsBegin)
                            {
                                sequenceBuilder.Append(str1[i]);
                            }
                            else 
                            {
                                lastSubsBegin = thisSubsBegin;
                                sequenceBuilder.Length = 0; 
                                sequenceBuilder.Append(str1.Substring(lastSubsBegin, (i + 1) - lastSubsBegin));
                            }
                        }
                    }
                }
            }
            sequence = "Szukany podłańcuch: " + sequenceBuilder.ToString();
            wynikBox.Text = wynikBox.Text + "\n" + sequence;
            tablicaBox.Text += "\t";
            for (int i = 0; i < str2.Length; i++)
            {
                tablicaBox.Text += " " + str2[i];
            }
            tablicaBox.Text += "\n";
            for (int i = 0; i < str1.Length; i++)
            {
                tablicaBox.Text += " " + str1[i] + "\t";
                for (int j = 0; j < str2.Length; j++)
                {
                    tablicaBox.Text += " " + num[i, j];
                }
                tablicaBox.Text += "\n";
            }
            }
            else
                MessageBox.Show("Podaj lub wczytaj ciągi", "Ostrzeżenie");
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            tablicaBox.Text = "";
            stringBox1.Text = "";
            stringBox2.Text = "";
            wynikBox.Text = "";
        }
    }
}
