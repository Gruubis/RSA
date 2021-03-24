using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Windows.Forms;

namespace RSA
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            int[] message = new int[textBox1.Text.Length];
            int counter = 0;
            int exponent = 0;
            foreach (char c in textBox1.Text)
            {
                message[counter] = c % 128;
                counter++;
            }
            int n = int.Parse(textBox2.Text) * int.Parse(textBox3.Text);
            int fn = (int.Parse(textBox2.Text) - 1) * (int.Parse(textBox3.Text) - 1);
           
            for (int i = 2; i < fn; i++)
            {

                if (GreatestCommonDivisor(i, fn) == 1)
                {
                    exponent = i;
                    if (exponent > 1)
                    {
                        break;
                    }
                }


            }
            File.WriteAllText(@"C:\Users\rolik\OneDrive\Desktop\New folder\public_Key.txt", exponent.ToString());
  
            StreamWriter sw = new StreamWriter(@"C:\Users\rolik\OneDrive\Desktop\New folder\public_Key.txt", true);
            sw.WriteLine("\n" + n.ToString());
            sw.Close();

            String text = "";
            int[] encrypted = Encryption(exponent, n, message);
            for (int i = 0; i < encrypted.Length; i++)
            {

                text += (char)(encrypted[i]);
            }
            textBox4.Text = text;
            
        }

        public int GreatestCommonDivisor(int a, int b)
        {
            int Remainder;

            while (b != 0)
            {
                Remainder = a % b;
                a = b;
                b = Remainder;
            }

            return a;
        }
        public int[] Encryption(int exponent, int n, int[] message)
        {
            int counter = 0;
            int[] encrypted = new int[message.Length];

            foreach (int m in message)
            {
                BigInteger bigInt = BigInteger.Pow(m, exponent);
                encrypted[counter] = (int)(bigInt % n );
                Console.WriteLine(encrypted[counter]);
                counter++;
            }
            return encrypted;
        }
        public int GetPrivateKey(int fn, int exponent)
        {
            int d = 2;
            while(d * exponent % fn != 1)
            {
                d++;
            }
            
            return d;
        }

        public int[] Decryption(int[] message, int n, int exponent)
        {
            int[] decrypted = new int[message.Length];
            int counter = 0;
            int p = findPrimePair(n);
            int q = n / p;
            int fn = (p - 1) * (q - 1); 
            int d = GetPrivateKey(fn, exponent);

            foreach (int m in message)
            {
                BigInteger bigInt = BigInteger.Pow(m, d);
                decrypted[counter] = (int)(bigInt % n);
                counter++;
            }
            return decrypted;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int[] encryptedmsg = new int[textBox4.Text.Length];
            int counter = 0;
            foreach (char c in textBox4.Text)
            {
                encryptedmsg[counter] = c % 128;
                counter++;
            }
            StreamReader sr = new StreamReader(@"C:\Users\rolik\OneDrive\Desktop\New folder\public_Key.txt");
            int exponent = int.Parse(sr.ReadLine());
            int n = int.Parse(sr.ReadLine());
            sr.Close();
            int[] decrypted = Decryption(encryptedmsg, n, exponent);
            String txt = "";
            for (int i = 0; i < decrypted.Length; i++)
            {

                txt += (char)(decrypted[i]);
            }
            textBox5.Text = txt;
        }
        static void GeneratePrimeNumbers(int n,
bool[] isPrime)
{
        isPrime[0] = isPrime[1] = false;
    for (int i = 2; i <= n; i++)
        isPrime[i] = true;
 
    for (int p = 2; p* p <= n; p++)
    {
        if (isPrime[p] == true)
        {
            for (int i = p* 2; i <= n; i += p)
                isPrime[i] = false;
        }
}
}
 
static int findPrimePair(int n)
{
    int flag = 0;

    bool[] isPrime = new bool[n + 1];
    GeneratePrimeNumbers(n, isPrime);
    for (int i = 2; i < n; i++)
    {
        int x = n / i;

        if (isPrime[i] && isPrime[x] &&
                x != i && x * i == n)
        {
            Console.Write(i + " " + x);
            flag = 1;
            return x;
        }
    }

    if (flag == 0)
        Console.Write("No such pair found");
            return 0;
}   
    }
}

